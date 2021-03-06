﻿namespace KarenShop.Api.Domains.Dtos
{
    public class RegisterDto 
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public bool IsSeller { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
    }
}
