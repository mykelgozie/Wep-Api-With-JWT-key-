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
    public class UserRepository : IuserRepository
    {
        private UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public List<UserModel> GetallUsers()
        {

            var allUsers = new List<UserModel>();
            var result = _userManager.Users;
            foreach (var user in result)
            {
                var person = new UserModel();
                person.FirstName = user.FirstName;
                person.lastName = user.LastName;
                person.Email = user.Email;
                person.DateCreated = user.DateCreated;

                allUsers.Add(person);


            }

            return allUsers;

        }


        public async Task<IdentityResult> CreatUser(ApplicationUser user, RegisterUser regUser)
        {
            var result = await _userManager.CreateAsync(user, regUser.Password);
            return result;
        }


        public async Task<bool> DeleteUser(TokenEmail model)
        {
           
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }

            return false;

        }



        public async Task<bool> UpdateUser(string email, UpdateUser model)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }



    }
}
