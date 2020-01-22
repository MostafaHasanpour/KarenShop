using KarenShop.Api.Domains.Dtos;
using KarenShop.Api.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Domains.Contracts
{
    public interface IShopUserRepository
    {
        Task<ShopUser> GetUser(int id);
        Task<ShopUser> LoginUser(LoginDto login);
        Task<ShopUser> RegisterUser(RegisterDto register);
        Task<bool> ResetPassword(ResetPasswordDto resetPassword);
        Task<ShopUser> UpdateUser(UpdateProfileDtoDto updateProfile);
    }
}
