using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TangCaController : Controller
    {
        private readonly ITangCaService _TangCaService;
        public TangCaController(ITangCaService TangCaService)
        {
            _TangCaService = TangCaService;
        }
        [HttpGet]
        public IActionResult GetAllTangCa()
        {
            var result = _TangCaService.GetAllTangCa();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTangCa(Guid id)
        {
            var result = _TangCaService.GetTangCaId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateTangCa(TangCaDto request)
        {
            var result = _TangCaService.CreateTangCa(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult EditTangCa(TangCaDto request)
        {
            var result = _TangCaService.EditTangCa(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteTangCa(Guid id)
        {

            var result = _TangCaService.DeleteTangCa(id);

            return Ok(result);

        }
    }


}
