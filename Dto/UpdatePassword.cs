using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.Dto
{
    public class UpdatePassword
    {
        // updete password DtO model
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
