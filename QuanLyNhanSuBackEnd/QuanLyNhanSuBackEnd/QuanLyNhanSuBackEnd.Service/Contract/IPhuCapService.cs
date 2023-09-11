using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Contract
{
    public interface IPhuCapService
    {
        AppResponse<List<PhuCapDto>> GetAllPhuCap();
        AppResponse<PhuCapDto> GetPhuCapId(Guid Id);
        AppResponse<PhuCapDto> CreatePhuCap(PhuCapDto request);
        AppResponse<PhuCapDto> EditPhuCap(PhuCapDto request);
        AppResponse<string> DeletePhuCap(Guid Id);
    }
}
