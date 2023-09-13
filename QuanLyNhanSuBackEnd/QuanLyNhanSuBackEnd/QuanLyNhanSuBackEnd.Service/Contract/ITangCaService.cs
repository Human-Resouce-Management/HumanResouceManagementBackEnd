﻿using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Contract
{
    public interface ITangCaService
    {
        AppResponse<List<TangCaDto>> GetAllTangCa();
        AppResponse<TangCaDto> GetTangCaId(Guid Id);
        AppResponse<TangCaDto> CreateTangCa(TangCaDto request);
        AppResponse<TangCaDto> EditTangCa(TangCaDto request);
        AppResponse<string> DeleteTangCa(Guid Id);
    }
}