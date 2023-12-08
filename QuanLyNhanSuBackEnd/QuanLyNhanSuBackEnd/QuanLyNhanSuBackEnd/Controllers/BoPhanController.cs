using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using QuanLyNhanSuBackEnd.DAL.Models.Context;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;
using QuanLyNhanSuBackEnd.Service.Implementation;
using System.Linq;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BoPhanController : Controller
    {
        private readonly IBoPhanService _BoPhanService;
     
        public BoPhanController(IBoPhanService BoPhanService )
        {
            _BoPhanService = BoPhanService;
           
        }
        [HttpGet]
        public IActionResult GetAllBoPhan()
        {
            var result = _BoPhanService.GetAllBoPhan();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBoPhan(Guid id)
        {
            var result = _BoPhanService.GetBoPhanId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateBoPhan(BoPhanDto request)
        {
            var result = _BoPhanService.CreateBoPhan(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult EditBoPhan(BoPhanDto request)
        {
            var result = _BoPhanService.EditBoPhan(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteBoPhan(Guid id)
        {

            var result = _BoPhanService.DeleteBoPhan(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public IActionResult SearchUser([FromBody] SearchRequest request)
        {
            var result =  _BoPhanService.SearchBoPhan(request);

            return Ok(result);
        }
        [HttpPost("Download")]
        public IActionResult DownloadSelectedRows(SearchRequest request)
        {
            var data = _BoPhanService.SearchBoPhan(request);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SelectedRows");
                worksheet.Cells[1, 1].Value = "TenBoPhan";
                worksheet.Cells[1, 2].Value = "QuanLy";
                // Populate the worksheet with your data
                for (var i = 0; i < data.Data.Data.Count; i++)
                {
                    // Modify this section based on your entity properties
                    worksheet.Cells[i + 1 + 1, 1].Value = data.Data.Data[i].TenBoPhan;
                    worksheet.Cells[i + 1 + 1, 2].Value = data.Data.Data[i].QuanLy;
                    // Add more properties as needed
                }

                // Stream the Excel package to the client
                MemoryStream stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SelectedRows.xlsx");
            }
        }

    }
}
