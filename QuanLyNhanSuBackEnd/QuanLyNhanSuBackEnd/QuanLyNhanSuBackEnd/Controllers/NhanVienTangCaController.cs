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
    public class NhanVienTangCaController : Controller
    {
        private readonly INhanVienTangCaService _NhanVienTangCaService;
        public NhanVienTangCaController(INhanVienTangCaService NhanVienTangCaService)
        {
            _NhanVienTangCaService = NhanVienTangCaService;
        }
        [HttpGet]
        public IActionResult GetAllNhanVienTangCa()
        {
            var result = _NhanVienTangCaService.GetAllNhanVienTangCa();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetNhanVienTangCa(Guid id)
        {
            var result = _NhanVienTangCaService.GetNhanVienTangCaId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateTangCa(NhanVienTangCaDto request)
        {
            var result = _NhanVienTangCaService.CreateNhanVienTangCa(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult EditTangCa(NhanVienTangCaDto request)
        {
            var result = _NhanVienTangCaService.EditNhanVienTangCa(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteNhanVienTangCa(Guid id)
        {

            var result = _NhanVienTangCaService.DeleteNhanVienTangCa(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _NhanVienTangCaService.SearchNhanVienTangCa(request);

            return Ok(result);
        }
    }


}
