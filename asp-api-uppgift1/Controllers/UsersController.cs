using asp_api_uppgift1.Data;
using asp_api_uppgift1.Models;
using asp_api_uppgift1.Services;
using asp_api_uppgift1.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityService _identity;

        public UsersController(IIdentityService identity)
        {
            _identity = identity;
        }



        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signupModel)
        {
            if (await _identity.CreateUserAsync(signupModel))
                return new OkResult();

            return new BadRequestResult();
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var response = await _identity.SignInAsync(signInModel.Email, signInModel.Password);
            

            if (response.Succeeded)
            {
                return new OkObjectResult(response);                
            }
                

            return new BadRequestResult();
        }



        [AllowAnonymous]
        [HttpPost("Customers")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerModel customerModel)
        {
            if (await _identity.CreateCustomerAsync(customerModel))
                return new OkResult();

            return new BadRequestResult();
        }


        [AllowAnonymous]
        [HttpPost("Issues")]
        public async Task<IActionResult> CreateIssueAsync([FromBody] CreateIssueModel issueModel)
        {
            if (await _identity.CreateIssueAsync(issueModel))
                return new OkResult();

            return new BadRequestResult();
        }




        [HttpGet]
        public async Task<IActionResult> GetUsers() 
        {
            //return new OkObjectResult( await _identity.GetUsersAsync());

            if (_identity.ValidateAccessRights(IdentityRequest()))
                return new OkObjectResult(await _identity.GetUsersAsync());

            return new UnauthorizedResult();
        }



        private RequestUser IdentityRequest()
        {
            return new RequestUser
            {
                //UserId = int.Parse(HttpContext.User.FindFirst("UserId").Value),
                AccessToken = Request.Headers["Authorization"].ToString().Split(" ")[1]
            };

        }



        //eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxMDA5IiwibmJmIjoxNjE1NTgxNDgyLCJleHAiOjE2MTU1OTIyODIsImlhdCI6MTYxNTU4MTQ4Mn0.FSYFXgnRlhDABSfHTpwxGi4_SELRazyKi9ZPo8E3gUvpPG05tSC1_AYl4j9ikw53kNGczbj56m3IRcyNrz9Unw

    }
}
