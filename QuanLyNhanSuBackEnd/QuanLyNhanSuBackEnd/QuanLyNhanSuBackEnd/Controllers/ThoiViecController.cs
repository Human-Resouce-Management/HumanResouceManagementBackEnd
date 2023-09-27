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
    public class ThoiViecController : Controller
    {
        private readonly IThoiViecService _ThoiViecService;
        public ThoiViecController(IThoiViecService ThoiViecService)
        {
            _ThoiViecService = ThoiViecService;
        }
        [HttpGet]
        public IActionResult GetAllChucVu()
        {
            var result = _ThoiViecService.GetAllThoiViec();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetChucVu(Guid id)
        {
            var result = _ThoiViecService.GetThoiViecId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateChucVu(ThoiViecDto request)
        {
            var result = _ThoiViecService.CreateThoiViec(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult EditChucVu(ThoiViecDto request)
        {
            var result = _ThoiViecService.EditThoiViec(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteTangCa(Guid id)
        {

            var result = _ThoiViecService.DeleteThoiViec(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _ThoiViecService.SearchThoiViec(request);

            return Ok(result);
        }
    }
}
