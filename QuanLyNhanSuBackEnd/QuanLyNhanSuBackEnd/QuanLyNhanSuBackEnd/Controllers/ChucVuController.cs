using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVuController : Controller
    {
        private readonly IChucVuService _ChucVuService;
        public ChucVuController(IChucVuService ChucVuService)
        {
            _ChucVuService = ChucVuService;
        }
        [HttpGet]
        public IActionResult GetAllChucVu()
        {
            var result = _ChucVuService.GetAllChucVu();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetChucVu(Guid id)
        {
            var result = _ChucVuService.GetChucVuId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateChucVu(ChucVuDto request)
        {
            var result = _ChucVuService.CreateChucVu(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult EditChucVu(ChucVuDto request)
        {
            var result = _ChucVuService.EditChucVu(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteTangCa(Guid id)
        {

            var result = _ChucVuService.DeleteChucVu(id);

            return Ok(result);

        }
    }
}
