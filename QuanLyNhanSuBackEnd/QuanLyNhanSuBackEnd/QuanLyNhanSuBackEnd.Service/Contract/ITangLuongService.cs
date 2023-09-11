using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Contract
{
    public interface ITangLuongService
    {
        AppResponse<List<TangLuongDto>> GetAllTangLuong();
        AppResponse<TangLuongDto> GetTangLuongId(Guid Id);
        AppResponse<TangLuongDto> CreateTangLuong(TangLuongDto request);
        AppResponse<TangLuongDto> EditTangLuong(TangLuongDto request);
        AppResponse<string> DeleteTangLuong(Guid Id);
    }
}
