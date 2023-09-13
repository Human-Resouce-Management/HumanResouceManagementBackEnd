﻿using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;

namespace QuanLyNhanSuBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NghiPhepController : Controller
    {
        private readonly INghiPhepService _NghiPhepService;
        public NghiPhepController(INghiPhepService NghiPhepService)
        {
            _NghiPhepService = NghiPhepService;
        }
        [HttpGet]
        public IActionResult GetAllNghiPhep()
        {
            var result = _NghiPhepService.GetAllNghiPhep();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetNghiPhep(Guid id)
        {
            var result = _NghiPhepService.GetNghiPhepId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateNghiPhep(NghiPhepDto request)
        {
            var result = _NghiPhepService.CreateNghiPhep(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult EditNghiPhep(NghiPhepDto request)
        {
            var result = _NghiPhepService.EditNghiPhep(request);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult DeleteNghiPhep(Guid id)
        {

            var result = _NghiPhepService.DeleteNghiPhep(id);

            return Ok(result);

        }
    }
}