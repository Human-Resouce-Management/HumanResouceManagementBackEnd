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
    public class TuyenDungBackEndController : Controller
    {
            private readonly ITuyenDungService _warehouseService;
            public TuyenDungBackEndController(ITuyenDungService warehouseService)
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
            [Route("{id}")]
            public IActionResult GetTuyenDung(Guid id)
            {
                var result = _warehouseService.GetTuyenDungId(id);
                return Ok(result);
            }
            [HttpPost]
            public IActionResult CreateTuyenDung(TuyenDungDto request)
            {
                var result = _warehouseService.CreateTuyenDung(request);
                return Ok(result);
            }
            [HttpPut]
          
            public IActionResult EditTuyenDung(TuyenDungDto request)
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
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _warehouseService.SearchTuyenDung(request);

            return Ok(result);
        }


    }
    
}
