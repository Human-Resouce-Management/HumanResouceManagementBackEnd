using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class PhuCapController : Controller
    {
        private readonly IPhuCapService _BoPhanService;
        public PhuCapController(IPhuCapService BoPhanService)
        {
            _BoPhanService = BoPhanService;
        }
        [HttpGet]
        public IActionResult GetAllPhuCap()
        {
            var result = _BoPhanService.GetAllPhuCap();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPhuCap(Guid id)
        {
            var result = _BoPhanService.GetPhuCapId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreatePhuCap(PhuCapDto request)
        {
            var result = _BoPhanService.CreatePhuCap(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult EditPhuCap(PhuCapDto request)
        {
            var result = _BoPhanService.EditPhuCap(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeletePhuCap(Guid id)
        {

            var result = _BoPhanService.DeletePhuCap(id);

            return Ok(result);

        }
    }
}
