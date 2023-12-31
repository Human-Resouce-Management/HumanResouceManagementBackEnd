﻿using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Model.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Contract
{
    public interface ITinhLuongService
    {
        AppResponse<List<TinhLuongDto>> GetAllTinhLuong();
        AppResponse<TinhLuongDto> GetTinhLuongId(Guid Id);
        AppResponse<TinhLuongDto> CreateTinhLuong(TinhLuongDto request);
        AppResponse<TinhLuongDto> EditTinhLuong(TinhLuongDto request);
        AppResponse<string> DeleteTinhLuong(Guid Id);
        Task<AppResponse<SearchTinhLuongRespository>> SearchTinhLuong(SearchRequest request);
        AppResponse<double> TinhLuongs(Guid id);
        Task<byte[]> ExportToExcel(SearchRequest request);
    }
}
