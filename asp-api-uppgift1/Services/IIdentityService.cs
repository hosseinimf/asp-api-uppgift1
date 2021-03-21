using asp_api_uppgift1.Models;
using asp_api_uppgift1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.Services
{
    public interface IIdentityService
    {
        Task<bool> CreateUserAsync(SignUpModel signupModel);
        Task<SignInResponse> SignInAsync(string email, string password);
        Task<IEnumerable<UserResponse>> GetUsersAsync();

        bool ValidateAccessRights(RequestUser requestUser);
        Task<bool> CreateCustomerAsync(CreateCustomerModel customerModel);
        Task<bool> CreateIssueAsync(CreateIssueModel issueModel);
        Task<bool> CreateCommentAsync(CreateCommentModel comment);
    }
}
