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
    public interface INhanVienTangCaService
    {
        AppResponse<List<NhanVienTangCaDto>> GetAllNhanVienTangCa();
        AppResponse<NhanVienTangCaDto> GetNhanVienTangCaId(Guid Id);
        AppResponse<NhanVienTangCaDto> CreateNhanVienTangCa(NhanVienTangCaDto request);
        AppResponse<NhanVienTangCaDto> EditNhanVienTangCa(NhanVienTangCaDto request);
        AppResponse<string> DeleteNhanVienTangCa(Guid Id);
        Task<AppResponse<SearchNhanVienTangCaRespository>> SearchNhanVienTangCa(SearchRequest request);
    }
}
