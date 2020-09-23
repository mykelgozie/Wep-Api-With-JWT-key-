using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.Dto
{
    // return user model 
    public class ReturnUser
    {

        public string LastName { get; set; }
       
        public string FirstName { get; set; }
        public string Photo { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
    }
}
