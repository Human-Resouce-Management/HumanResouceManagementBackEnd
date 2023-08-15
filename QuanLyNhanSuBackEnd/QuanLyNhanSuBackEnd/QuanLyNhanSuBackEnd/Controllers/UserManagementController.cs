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
            var result = _userManagementService.GetAllNhanVien();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateTuyenDung(UserManagementDto request)
        {
            var result = _userManagementService.CreatNhanVien(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditTuyenDung(UserManagementDto request)
        {
            var result = _userManagementService.EditNhanVien(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteNhanVien(Guid id)
        {

            var result = _userManagementService.DeleteNhanVien(id);

            return Ok(result);

        }
    }
}
