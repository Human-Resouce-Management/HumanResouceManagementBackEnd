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
    public interface INghiPhepService
    {
        AppResponse<List<NghiPhepDto>> GetAllNghiPhep();
        AppResponse<NghiPhepDto> GetNghiPhepId(Guid Id);
        AppResponse<NghiPhepDto> CreateNghiPhep(NghiPhepDto request);
        AppResponse<NghiPhepDto> EditNghiPhep(NghiPhepDto request);
        AppResponse<string> DeleteNghiPhep(Guid Id);
        Task<AppResponse<SearchNghiPhepRespository>> SearchNghiPhep(SearchRequest request);
    }
}
