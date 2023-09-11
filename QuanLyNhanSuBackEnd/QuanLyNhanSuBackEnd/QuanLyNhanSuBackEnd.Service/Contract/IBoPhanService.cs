using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
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
    }
}
