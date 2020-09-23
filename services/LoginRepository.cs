using AspWebApi.Dto;
using AspWebApi.Interface;
using AspWebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.services
{
    // login repository 
    public class LoginRepository :ILoginRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public LoginRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;

        }



        // find user by mail
        public async Task<ApplicationUser> FindUserByEmail(LoginViewModel model)
        {
            var user =  await _userManager.FindByEmailAsync(model.Email);
            return user;

        }

        // login user 
        public async Task <SignInResult> LoginUser(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, false);
            return result;
        }
    }
}
