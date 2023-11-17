using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;
using QuanLyNhanSuBackEnd.Service.Implementation;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class TangLuongController : Controller
    {
        private readonly ITangLuongService _TangLuongService;
        public TangLuongController(ITangLuongService TangLuongService)
        {
            _TangLuongService = TangLuongService;
        }
        [HttpGet]
        public IActionResult GetAllTangLuong()
        {
            var result = _TangLuongService.GetAllTangLuong();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTangLuong(Guid id)
        {
            var result = _TangLuongService.GetTangLuongId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateTangLuong(TangLuongDto request)
        {
            var result = _TangLuongService.CreateTangLuong(request);
            return Ok(result);
        }
        [HttpPut]
     
        public IActionResult EditTangLuong(TangLuongDto request)
        {
            var result = _TangLuongService.EditTangLuong(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteTangLuong(Guid id)
        {

            var result = _TangLuongService.GetTangLuongId(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _TangLuongService.SearchTangLuong(request);

            return Ok(result);
        }
    }
}
