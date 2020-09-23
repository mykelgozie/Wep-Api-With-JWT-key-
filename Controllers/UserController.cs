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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AspWebApi.Controllers
{
    // user controller 
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IuserRepository _userepo;
        private int page_num;
        private int page_size = 2;
        private int total;
        private int max_page;

        // constructor containing dependency injection variable 
        public UserController(UserManager<ApplicationUser> userManager, IuserRepository userepo)
        {
            _userManager = userManager;
            _userepo = userepo;
        }

     
        // get all users 
        [HttpGet("{page}")]
        public IActionResult Get(int page)
        {
            // all users 
            var allUsers = _userepo.GetallUsers();    
            // user count 
            total = allUsers.Count();

            // pagination
            if (page <= 0)
            {
                page = 1;
            }
            max_page = (total % page_size > 0) ? total / page_size + 1 : total / page_size;
            // assign page number 
            page_num = page;
            // pagination values 
            var pagUser = allUsers.Skip((page_num - 1) * page_size).Take(page_size);
            // return users
            return Ok(pagUser);

        }

        // allowanonymous
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> OnPostUser(RegisterUser regUser)
        {
            // check if valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // new user 
            var user = new ApplicationUser() { FirstName = regUser.FirstName, UserName = regUser.Email, LastName = regUser.LastName, Email = regUser.Email };
            // create user 
            var result = await _userepo.CreatUser(user, regUser);
            // check if succeeded
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }


        // delete route
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(TokenEmail model)
        {
            // get and  delete user by email
            var result = await _userepo.DeleteUser(model);
          
            // check if deleted 
            if (result == true)
            {
                return Ok();
            }
            // return bad request
            return BadRequest();



        }

        // update user route
        [HttpPatch("update/{email}")]
        public async Task<IActionResult> UpdateUser(string email, UpdateUser model)
        {
            // check if valid 
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // update user
         var result =  await _userepo.UpdateUser(email, model);
            if (result == true)
            {
                return Ok();
            }
            // return bad request
            return BadRequest();

        }






    }
}
