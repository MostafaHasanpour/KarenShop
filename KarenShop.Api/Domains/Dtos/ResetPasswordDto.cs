﻿namespace KarenShop.Api.Domains.Dtos
{
    public class ResetPasswordDto
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
