using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Domains.Models
{
    public class ShopUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSeller { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
    }
}
