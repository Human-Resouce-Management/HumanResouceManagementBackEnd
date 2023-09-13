﻿using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoPhanController : Controller
    {
        private readonly IBoPhanService _BoPhanService;
        public BoPhanController(IBoPhanService BoPhanService)
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
        [Route("{Id}")]
        public IActionResult EditBoPhan(BoPhanDto request)
        {
            var result = _BoPhanService.EditBoPhan(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteBoPhan(Guid id)
        {

            var result = _BoPhanService.DeleteBoPhan(id);

            return Ok(result);

        }
    }
}