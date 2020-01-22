using KarenShop.Api.Domains;
using KarenShop.Api.Domains.Contracts;
using KarenShop.Api.Domains.Dtos;
using KarenShop.Api.Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Infrastructures.Repository
{
    public class ShopUserRepository : BaseRepository, IShopUserRepository
    {
        protected DbSet<ShopUser> _shopUsers;
        public ShopUserRepository(KarenShopDbContext dbContext) : base(dbContext)
        {
            _shopUsers = dbContext.ShopUsers;
        }

        public async Task<ShopUser> GetUser(int id) => await _shopUsers.FindAsync(id);

        public async Task<ShopUser> LoginUser(LoginDto login)
        {
            return await _shopUsers.FirstOrDefaultAsync(x => (x.Email == login.EmailOrPhoneNumber || x.PhoneNumber == login.EmailOrPhoneNumber) &&
            x.Password == login.Password);
        }

        public async Task<ShopUser> RegisterUser(RegisterDto register)
        {
            if (!(await _shopUsers.AnyAsync(x => x.Email == register.Email || x.PhoneNumber == register.Phone)))
            {
                ShopUser shopUser = new ShopUser()
                {
                    FullName = register.FullName,
                    Email = register.Email,
                    PhoneNumber = register.Phone,
                    CompanyName = "",
                    Password = register.Password
                };
                await _shopUsers.AddAsync(shopUser);
                await _context.SaveChangesAsync();
                return _shopUsers.FirstOrDefault(x => x.Email == register.Email);
            }
            else return null;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto resetPassword)
        {
            try
            {
                var user = await GetUser(resetPassword.Id);
                if (user.Password == resetPassword.CurrentPassword)
                {
                    user.Password = resetPassword.NewPassword;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ShopUser> UpdateUser(UpdateProfileDtoDto updateProfile)
        {
            try
            {
                var user = await GetUser(updateProfile.Id);
                user.Address = updateProfile.Address;
                user.CompanyName = updateProfile.CompanyName;
                user.Email = updateProfile.Email;
                user.FullName = updateProfile.FullName;
                user.PhoneNumber = updateProfile.Phone;
                user.ProfilePicture = updateProfile.ProfilePicture;

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}