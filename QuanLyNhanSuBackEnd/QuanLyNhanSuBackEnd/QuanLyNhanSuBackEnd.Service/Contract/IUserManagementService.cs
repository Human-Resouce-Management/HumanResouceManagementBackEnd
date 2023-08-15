using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Contract
{
    public interface IUserManagementService
    {
        AppResponse<List<UserManagementDto>> GetAllNhanVien();
        AppResponse<UserManagementDto> CreatNhanVien(UserManagementDto request);
        AppResponse<UserManagementDto> EditNhanVien(UserManagementDto request);
        AppResponse<string> DeleteNhanVien(Guid Id);
    }
}
