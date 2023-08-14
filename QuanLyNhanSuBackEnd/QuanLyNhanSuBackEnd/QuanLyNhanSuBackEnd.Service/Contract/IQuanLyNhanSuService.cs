using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Contract
{
   public interface IQuanLyNhanSuService
    {
        AppResponse<List<QuanLyNhanSuBackEndDto>> GetAllTuyenDung();
        AppResponse<QuanLyNhanSuBackEndDto> GetTuyenDungId(Guid Id);
        AppResponse<QuanLyNhanSuBackEndDto> CreateTuyenDung(QuanLyNhanSuBackEndDto request);
        AppResponse<QuanLyNhanSuBackEndDto> EditTuyenDung(QuanLyNhanSuBackEndDto request);
        AppResponse<string> DeleteTuyenDung(Guid Id);
      
    }
}
