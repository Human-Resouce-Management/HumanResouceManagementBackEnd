using MayNghien.Models.Request.Base;
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
    public interface IThoiViecService
    {
        AppResponse<List<ThoiViecDto>> GetAllThoiViec();
        AppResponse<ThoiViecDto> GetThoiViecId(Guid Id);
        AppResponse<ThoiViecDto> CreateThoiViec(ThoiViecDto request);
        AppResponse<ThoiViecDto> EditThoiViec(ThoiViecDto request);
        AppResponse<string> DeleteThoiViec(Guid Id);
        Task<AppResponse<SearchThoiViecRespository>> SearchThoiViec(SearchRequest request);
    }
}
