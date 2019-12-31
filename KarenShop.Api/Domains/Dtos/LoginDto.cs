using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Domains.Dtos
{
    public class LoginDto
    {
        public string EmailOrPhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
