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
    public interface IBoPhanService
    {
        AppResponse<List<BoPhanDto>> GetAllBoPhan();
        AppResponse<BoPhanDto> GetBoPhanId(Guid Id);
        AppResponse<BoPhanDto> CreateBoPhan(BoPhanDto request);
        AppResponse<BoPhanDto> EditBoPhan(BoPhanDto request);
        AppResponse<string> DeleteBoPhan(Guid Id);
        Task<AppResponse<SearchBoPhanRespository>> SearchBoPhan(SearchRequest request);
    }
}
