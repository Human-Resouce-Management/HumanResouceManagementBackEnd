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
            var result =  _TangCaService.CreateTangCa(request);
            return Ok(result);
        }
        [HttpPut]
   
        public IActionResult EditTangCa(TangCaDto request)
        {
            var result = _TangCaService.EditTangCa(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteTangCa(Guid id)
        {

            var result = _TangCaService.DeleteTangCa(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _TangCaService.SearchTangCa(request);

            return Ok(result);
        }
        [HttpPost("Download")]
        public async Task<IActionResult> Dowloadexcel(SearchRequest request)
        {
            var ex = await _TangCaService.ExportToExcel(request);
            MemoryStream stream = new MemoryStream(ex);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SelectedRows.xlsx");
        }
    }


}
