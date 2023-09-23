using AutoMapper;
using MayNghien.Common.Helpers;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
    public class PhuCapService : IPhuCapService
    {
        private readonly IPhuCapRespository _PhuCapRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public PhuCapService(IPhuCapRespository PhuCapRepository, IMapper mapper,  IHttpContextAccessor httpContextAccessor)
        {
            _PhuCapRepository = PhuCapRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<PhuCapDto> CreatePhuCap(PhuCapDto request)
        {
            var result = new AppResponse<PhuCapDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var phuCap = new PhuCap();
                phuCap = _mapper.Map<PhuCap>(request);
                phuCap.Id = Guid.NewGuid();
                phuCap.CreatedBy = UserName;
                _PhuCapRepository.Add(phuCap);

                request.Id = phuCap.Id;
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

        public AppResponse<string> DeletePhuCap(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var phuCap = new PhuCap();
                phuCap = _PhuCapRepository.Get(Id);
                phuCap.IsDeleted = true;

                _PhuCapRepository.Edit(phuCap);

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



        public AppResponse<PhuCapDto> EditPhuCap(PhuCapDto request)
        {
            var result = new AppResponse<PhuCapDto>();
            try
            {
                var phuCap = new PhuCap();
                phuCap = _mapper.Map<PhuCap>(request);
                _PhuCapRepository.Edit(phuCap);

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

        public AppResponse<List<PhuCapDto>> GetAllPhuCap()
        {
            var result = new AppResponse<List<PhuCapDto>>();
            //string userId = "";
            try
            {
                var query = _PhuCapRepository.GetAll()
                    .Include(n => n.NhanVien)
                    ;
                var list = query.Select(m => new PhuCapDto
                {
                    Id = m.Id,
                  ThuongCoDinh = m.ThuongCoDinh,
                  SoTien = m.SoTien,
                  NhanVienId = m.NhanVienId,
                  ten = m.NhanVien.Ten,
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



        public AppResponse<PhuCapDto> GetPhuCapId(Guid Id)
        {
            var result = new AppResponse<PhuCapDto>();
            try
            {
                var query = _PhuCapRepository.FindBy(x => x.Id == Id).Include(x => x.NhanVien);
                var data = query.Select(x=> new PhuCapDto
                {
                    Id=x.Id,
                    NhanVienId=x.NhanVienId,
                    SoTien =x.SoTien,
                    ten = x.NhanVien.Ten,
                    ThuongCoDinh = x.ThuongCoDinh
                }).First();
                result.IsSuccess = true;
                result.Data = data;
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
