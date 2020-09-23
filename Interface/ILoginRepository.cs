using AspWebApi.Dto;
using AspWebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.Interface
{
    // login interface
   public  interface ILoginRepository
    {

        // find user by mail
        public Task<ApplicationUser> FindUserByEmail(LoginViewModel model);

        // login user
        public Task<SignInResult> LoginUser(LoginViewModel model);
    }
}
