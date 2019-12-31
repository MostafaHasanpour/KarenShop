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
                    CompanyName=user.CompanyName,
                    Email=user.Email,
                    FullName=user.FullName,
                    PhoneNumber=user.PhoneNumber,
                    Id = user.Id
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
                    Id=user.Id
                };
            }
        }
    }
}