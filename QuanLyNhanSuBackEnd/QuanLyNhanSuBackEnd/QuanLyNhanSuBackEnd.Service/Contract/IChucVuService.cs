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
    public interface IChucVuService
    {
        AppResponse<List<ChucVuDto>> GetAllChucVu();
        AppResponse<ChucVuDto> GetChucVuId(Guid Id);
        AppResponse<ChucVuDto> CreateChucVu(ChucVuDto request);
        AppResponse<ChucVuDto> EditChucVu(ChucVuDto request);
        AppResponse<string> DeleteChucVu(Guid Id);
        Task<AppResponse<SearchChucVuRespository>> SearchChucVu(SearchRequest request);
    }
}
