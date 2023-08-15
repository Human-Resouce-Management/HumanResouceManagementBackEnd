using AutoMapper;
using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
    public class UserManagementService : IUserManagementService
    {

        private readonly IUserManagementRespository _userManagementRespository ;
        private readonly IMapper _mapper;

        public UserManagementService(IUserManagementRespository userManagementRespository, IMapper mapper)
        {
            _userManagementRespository = userManagementRespository;
            _mapper = mapper;
        }
        
        public AppResponse<UserManagementDto> CreatNhanVien(UserManagementDto request)
        {
            var result = new AppResponse<UserManagementDto>();
            try
            {
                var nhanvien = new NhanVien();
                nhanvien = _mapper.Map<NhanVien>(nhanvien);
                nhanvien.Id = Guid.NewGuid();
                _userManagementRespository.Add(nhanvien);

                request.Id = nhanvien.Id;
                result.IsSuccess = true;
                result.Data = request;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }
        public AppResponse<string> DeleteNhanVien(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var nhanvien = new NhanVien();
                nhanvien = _userManagementRespository.Get(Id);
                nhanvien.IsDeleted = true;

                _userManagementRespository.Edit(nhanvien);

                result.IsSuccess = true;
                result.Data = "Delete Sucessfuly";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ":" + ex.StackTrace;
                return result;

            }
        }

        public AppResponse<UserManagementDto> EditNhanVien(UserManagementDto? request)
        {
            var result = new AppResponse<UserManagementDto>();
            try
            {
                var nhanvien = new NhanVien();
                if (request.Id == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Id cannot be null";
                    return result;
                }
                nhanvien = _userManagementRespository.Get(request.Id.Value);
                nhanvien.Ten = request.Ten;
                nhanvien.CapBat = request.CapBat;
                nhanvien.HeSo = request.HeSo;
                nhanvien.MucLuong = request.MucLuong;              
                //budgetcat.Id = Guid.NewGuid();
                _userManagementRespository.Edit(nhanvien);

                result.IsSuccess = true;
                result.Data = request;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ":" + ex.StackTrace;
                return result;

            }
        }

        public AppResponse<List<UserManagementDto>> GetAllNhanVien()
        {
            var result = new AppResponse<List<UserManagementDto>>();
            //string userId = "";
            try
            {
                var query = _userManagementRespository.GetAll();
                var list = query.Select(m => new UserManagementDto
                {
                    Ten = m.Ten,
                    CapBat = m.CapBat,
                    HeSo = m.HeSo,
                    MucLuong = m.MucLuong,
                   
                }).ToList();
                result.IsSuccess = true;
                result.Data = list;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }

    }
}
