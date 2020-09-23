using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.Dto
{

    // email model
    public class TokenEmail
    {
        [Required]
        public string Email { get; set; }
    }
}
