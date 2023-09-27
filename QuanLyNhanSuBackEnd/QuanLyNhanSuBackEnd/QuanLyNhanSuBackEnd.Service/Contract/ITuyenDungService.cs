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
   public interface ITuyenDungService
    {
        AppResponse<List<TuyenDungDto>> GetAllTuyenDung();
        AppResponse<TuyenDungDto> GetTuyenDungId(Guid Id);
        AppResponse<TuyenDungDto> CreateTuyenDung(TuyenDungDto request);
        AppResponse<TuyenDungDto> EditTuyenDung(TuyenDungDto request);
        AppResponse<string> DeleteTuyenDung(Guid Id);
        Task<AppResponse<SearchTuyenDungRespository>> SearchTuyenDung(SearchRequest request);
    }
}
