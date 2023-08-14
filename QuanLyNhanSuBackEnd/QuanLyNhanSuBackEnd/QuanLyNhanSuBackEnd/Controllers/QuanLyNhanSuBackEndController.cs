using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class QuanLyNhanSuBackEndController : Controller
        {
            private readonly IQuanLyNhanSuService _warehouseService;
            public QuanLyNhanSuBackEndController(IQuanLyNhanSuService warehouseService)
            {
                _warehouseService = warehouseService;
            }
            [HttpGet]
            public IActionResult GetAll()
            {
                var result = _warehouseService.GetAllTuyenDung();
                return Ok(result);
            }
            [HttpGet]
            [Route("{Id}")]
            public IActionResult GetTuyenDung(Guid id)
            {
                var result = _warehouseService.GetTuyenDungId(id);
                return Ok(result);
            }
            [HttpPost]
            public IActionResult CreateTuyenDung(QuanLyNhanSuBackEndDto request)
            {
                var result = _warehouseService.CreateTuyenDung(request);
                return Ok(result);
            }
            [HttpPut]
            [Route("{id}")]
            public IActionResult EditTuyenDung(QuanLyNhanSuBackEndDto request)
            {
                var result = _warehouseService.EditTuyenDung(request);
                return Ok(result);
            }
            [HttpDelete]
            public IActionResult DeleteTuyenDung(Guid id)
            {

                var result = _warehouseService.DeleteTuyenDung(id);

                return Ok(result);

            }
        }
    
}
