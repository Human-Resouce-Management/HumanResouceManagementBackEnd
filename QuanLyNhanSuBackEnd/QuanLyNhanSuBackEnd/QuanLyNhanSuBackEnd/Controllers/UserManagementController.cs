using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController:Controller
    {
        private readonly IUserManagementService _userManagementService;
        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userManagementService.GetAllUser();
            return Ok(result);
        }
        [HttpPut]      
        public IActionResult ChangePassWordUser(Guid Id)
        {
            var result = _userManagementService.ResetPassWordUser(Id);

            return Ok(result);
        }
    }
}
