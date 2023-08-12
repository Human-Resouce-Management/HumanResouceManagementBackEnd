using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;
using static QuanLyNhanSuBackEnd.Service.Implementation.LoginService;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : Controller
    {
       ILoginService _iloginService;

        public AccountController(ILoginService iloginService)
        {
            _iloginService = iloginService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserModel login)
        {
            var result = await _iloginService.AuthenticateUser(login);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Regisger(UserModel login)
        {
            var result = await _iloginService.CreateUser(login);

            return Ok(result);
        }

    }
}
