using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarenShop.Api.Domains;
using KarenShop.Api.Domains.Contracts;
using KarenShop.Api.Domains.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarenShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IShopUserRepository _shopUserRepository;

        public AccountController(IShopUserRepository shopUserRepository)
        {
            this._shopUserRepository = shopUserRepository;
        }

        [HttpPost("/api/v1/login")]
        public async Task<ResponseUserDto> Login(LoginDto model)
        {
            var user = await _shopUserRepository.LoginUser(model);
            if (user == null)
            {
                return new ResponseUserDto()
                {
                    IsSuccess = false,
                    Error = "نام کاربری یا رمز عبور اشتباه است"
                };
            }
            else
            {
                return new ResponseUserDto()
                {
                    IsSuccess = true,
                    Error = "",
                    CompanyName = user.CompanyName,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id,
                    IsSeller = user.IsSeller,
                    Address = user.Address,
                    ProfilePicture = user.ProfilePicture
                };
            }
        }

        [HttpPost("/api/v1/register")]
        public async Task<ResponseUserDto> Register(RegisterDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new ResponseUserDto()
                {
                    IsSuccess = false,
                    Error = "کلمه عبور و تایید آن یکسان نیستند..."
                };
            }
            var user = await _shopUserRepository.RegisterUser(model);
            if (user == null)
            {
                return new ResponseUserDto()
                {
                    IsSuccess = false,
                    Error = " این ایمیل یا شماره تلفن قبلا وارد شده است..."
                };
            }
            else
            {
                return new ResponseUserDto()
                {
                    IsSuccess = true,
                    Error = "",
                    CompanyName = user.CompanyName,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id,
                    IsSeller = user.IsSeller,
                    Address = user.Address,
                    ProfilePicture = user.ProfilePicture
                };
            }
        }

        [HttpPost("/api/v1/change-password")]
        public async Task<BaseResponseDto> ResetPassword(ResetPasswordDto model)
        {
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                return new BaseResponseDto()
                {
                    Error = "رمز عبور جدید و تایید آن یکسان نیست",
                    IsSuccess = false
                };
            }
            else
            {
                var result = await _shopUserRepository.ResetPassword(model);
                if (result == true)
                {
                    return new BaseResponseDto()
                    {
                        IsSuccess = true,
                        Error = "",
                    };
                }
                else
                {
                    return new BaseResponseDto()
                    {
                        IsSuccess = false,
                        Error = "رمز عبور را اشتباه وارد کرده اید...",
                    };
                }
            }
        }

        [HttpPost("/api/v1/update-user")]
        public async Task<ResponseUserDto> UpdateUser(UpdateProfileDtoDto model)
        {
            var result = await _shopUserRepository.UpdateUser(model);
            if (result != null)
            {
                return new ResponseUserDto()
                {
                    IsSuccess = true,
                    Error = "",
                    Address = result.Address,
                    CompanyName = result.CompanyName,
                    Email = result.Email,
                    FullName = result.FullName,
                    Id = result.Id,
                    IsSeller = result.IsSeller,
                    PhoneNumber = result.PhoneNumber,
                    ProfilePicture = result.ProfilePicture
                };
            }
            else
            {
                return new ResponseUserDto()
                {
                    IsSuccess = false,
                    Error = "خطایی رخ داده است...",
                    Id = 0,
                    IsSeller = false
                };
            }
        }

        [HttpGet("/api/api-data")]
        public async Task<object> GetApiDataAsync()
        {
            return new UpdateProfileDtoDto()
            {
            };
        }
    }
}