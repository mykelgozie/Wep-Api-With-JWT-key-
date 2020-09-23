using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspWebApi.Dto;
using AspWebApi.Interface;
using AspWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspWebApi.Controllers
{
    // login route
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

       
        private readonly ILoginRepository _userLogin;

        public LoginController(ILoginRepository login)
        {
         
            _userLogin = login;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // check if valid
            if (ModelState.IsValid)
            {
                // login user 
                var result = await _userLogin.LoginUser(model);
                if (result.Succeeded)
                {
                    // find user by mail
                    var user = await _userLogin.FindUserByEmail(model);
                    var realUser = new UserModel();
                    realUser.FirstName = user.FirstName;
                    realUser.lastName = user.LastName;
                    realUser.Email = user.Email;
                    realUser.DateCreated = user.DateCreated;

                   // return user 
                    return Ok(realUser);
                }

            }
            // return badrequest
            return BadRequest();

        }
    }
}
