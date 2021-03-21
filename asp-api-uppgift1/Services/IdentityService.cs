using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using asp_api_uppgift1.Services;
using asp_api_uppgift1.Data;
using asp_api_uppgift1.ViewModel;
using asp_api_uppgift1.Models;

namespace WebApiWithAuth.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SqlDbContext _context;
        private IConfiguration _configuration { get; }

        public IdentityService(SqlDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> CreateUserAsync(SignUpModel model)
        {
            if (!_context.Users.Any(user => user.Email == model.Email))
            {
                try
                {
                    var user = new User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email
                    };
                    user.CreatePasswordWithHash(model.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch { }
            }

            return false;
        }


        public async Task<SignInResponse> SignInAsync(string email, string password)
        {

            System.Diagnostics.Debug.WriteLine($"LEmail: {email} LPass: {password}");
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);


                if (user != null)
                {
                    System.Diagnostics.Debug.WriteLine($"email: {user.Email} passhash: {user.PasswordHash} pass: {password}");


                    try
                    {


                        if (user.ValidatePasswordHash(password))
                        {
                            System.Diagnostics.Debug.WriteLine("Validated password");

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var _secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value);

                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[] { new Claim("UserId", user.Id.ToString()) }),
                                Expires = DateTime.Now.AddHours(3),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha512Signature)
                            };

                            var _accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

                            var existingToken = _context.SessionTokens.Find(user.Id);
                            if (existingToken != null)
                            {
                                existingToken.AccessToken = _accessToken;
                                _context.SessionTokens.Update(existingToken);
                            } else
                            {
                                _context.SessionTokens.Add(new SessionToken { UserId = user.Id, AccessToken = _accessToken });
                            }

                            await _context.SaveChangesAsync();

                            return new SignInResponse
                            {
                                Succeeded = true,
                                Result = new SignInResponseResult
                                {
                                    Id = user.Id,
                                    Email = user.Email,
                                    AccessToken = _accessToken
                                }
                            };
                        } else
                        {
                            System.Diagnostics.Debug.WriteLine("Did not validate password");
                        }

                    }
                    catch { }
                }
            }
            catch { }

            return new SignInResponse { Succeeded = false };

        }



        public async Task<IEnumerable<UserResponse>> GetUsersAsync()
        {
            var users = new List<UserResponse>();

            foreach (var user in await _context.Users.ToListAsync())
            {
                users.Add(new UserResponse { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email });
            }

            return users;
        }


        public bool ValidateAccessRights(RequestUser requestUser)
        {
            //if (_context.SessionTokens.Any(x => x.UserId == requestUser.UserId && x.AccessToken == requestUser.AccessToken))

            if (_context.SessionTokens.Any(x => x.AccessToken == requestUser.AccessToken))
                return true;

            return false;
        }



        public async Task<bool> CreateCustomerAsync(CreateCustomerModel customerModel)
        {
            if (!_context.Customers.Any(customer => customer.Email == customerModel.Email))
            {
                try
                {
                    var customer = new Customer()
                    {
                        FirstName = customerModel.FirstName,
                        LastName = customerModel.LastName,
                        Email = customerModel.Email,
                        Created = DateTime.Now
                    };
                    
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch { }
            }

            return false;
        }


        public async Task<bool> CreateIssueAsync(CreateIssueModel issueModel)
        {
            if (!_context.Issues.Any(issue => issue.Title == issueModel.Title))
            {
                try
                {
                    var issue = new Issue()
                    {
                        Title = issueModel.Title,
                        Description = issueModel.Description,
                        Status = issueModel.Status,
                        UserId = 3,
                        CustomerId = issueModel.CustomerId,
                        Created = DateTime.Now
                    };
                    _context.Issues.Add(issue);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch { }
            }
            return false;
        }



        public async Task<bool> CreateCommentAsync(CreateCommentModel commentModel)
        {
                try
                {
                    var comment = new Comment()
                    {
                        IssueId = commentModel.IssueId,
                        Description = commentModel.Description,
                        Created = DateTime.Now
                    };
                    _context.Comments.Add(comment);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch { return false; }    
        }












        //--------------------------------------------------------------------------
    }
}
