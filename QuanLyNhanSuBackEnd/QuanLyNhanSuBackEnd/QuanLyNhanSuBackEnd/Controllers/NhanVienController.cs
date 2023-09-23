using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class NhanVienController : Controller
    {
        private readonly INhanVienService _NhanVienService;
        public NhanVienController(INhanVienService NhanVienService)
        {
            _NhanVienService = NhanVienService;
        }
        [HttpGet]
        public IActionResult GetAllNhanVien()
        {
            var result = _NhanVienService.GetAllNhanVien();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBoPhan(Guid id)
        {
            var result = _NhanVienService.GetNhanVienId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateBoPhan(NhanVienDto request)
        {
            var result = _NhanVienService.CreateNhanVien(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult EditBoPhan(NhanVienDto request)
        {
            var result = _NhanVienService.EditNhanVien(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteBoPhan(Guid id)
        {

            var result = _NhanVienService.DeleteNhanVien(id);

            return Ok(result);

        }
    }
}
