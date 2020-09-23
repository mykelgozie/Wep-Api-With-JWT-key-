using AspWebApi.Dto;
using AspWebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.Interface
{
    public interface IuserRepository
    {

        // getalluser 
        public List<UserModel> GetallUsers();

        //  create user 
        public Task<IdentityResult> CreatUser(ApplicationUser user,  RegisterUser regUser);

        public Task<bool> DeleteUser(TokenEmail model);

        // update user 
        public Task<bool> UpdateUser(string email, UpdateUser model);
    }
}
