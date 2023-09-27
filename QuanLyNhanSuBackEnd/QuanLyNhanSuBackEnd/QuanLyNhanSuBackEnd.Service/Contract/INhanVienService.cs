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
    public interface INhanVienService
    {
        AppResponse<List<NhanVienDto>> GetAllNhanVien();
        AppResponse<NhanVienDto> GetNhanVienId(Guid Id);
        AppResponse<NhanVienDto> CreateNhanVien(NhanVienDto request);
        AppResponse<NhanVienDto> EditNhanVien(NhanVienDto request);
        AppResponse<string> DeleteNhanVien(Guid Id);
        Task<AppResponse<SearchNhanVienRespository>> SearchNhanVien(SearchRequest request);
    }
}
