using Azure.Core;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserManagementController:Controller
    {
        IUserManagementService _userManagementService;
        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _userManagementService.GetAllUser();
            return Ok(result);
        }
        [HttpPut]
        [Route("{Id}")]
        public async Task< IActionResult> RestPassWordUser(string Id)
        {
            var result = await _userManagementService.ResetPassWordUser(Id);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel request)
        {
            var result = await _userManagementService.CreateUser(request);

            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] string id)
        {
            var result = await _userManagementService.DeleteUser(id);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await _userManagementService.GetUser(id);

            return Ok(result);
        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _userManagementService.Search(request);

            return Ok(result);
        }
        //[HttpPut]
        //[Route("{id}")]
        //public async Task<IActionResult> EditUser([FromBody] UserModel request)
        //{
        //    var result = await _userManagementService.EditUser(request);

        //    return Ok(result);
        //}

    }
}
