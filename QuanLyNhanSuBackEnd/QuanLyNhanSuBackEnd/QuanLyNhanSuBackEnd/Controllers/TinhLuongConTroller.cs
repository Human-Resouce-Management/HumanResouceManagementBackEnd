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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TinhLuongController : Controller
    {
        private readonly ITinhLuongService _BoPhanService;
        public TinhLuongController(ITinhLuongService BoPhanService)
        {
            _BoPhanService = BoPhanService;
        }
        [HttpGet]
        public IActionResult GetAllBoPhan()
        {
            var result = _BoPhanService.GetAllTinhLuong();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBoPhan(Guid id)
        {
            var result = _BoPhanService.GetTinhLuongId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateBoPhan(TinhLuongDto request)
        {
            var result = _BoPhanService.CreateTinhLuong(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult EditBoPhan(TinhLuongDto request)
        {
            var result = _BoPhanService.EditTinhLuong(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteBoPhan(Guid id)
        {

            var result = _BoPhanService.DeleteTinhLuong(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _BoPhanService.SearchTinhLuong(request);

            return Ok(result);
        }
        [HttpPut]
        [Route("Sum")]
        public IActionResult TinhLuong(Guid request)
        {
            var result = _BoPhanService.TinhLuongs(request);
            return Ok(result);
        }
    }
}
