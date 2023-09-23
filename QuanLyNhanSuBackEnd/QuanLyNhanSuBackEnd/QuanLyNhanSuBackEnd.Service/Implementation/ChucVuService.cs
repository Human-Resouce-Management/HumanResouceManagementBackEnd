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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
    public class ChucVuService : IChucVuService
    {
        private readonly IChucVuRespository _ChucVuRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public ChucVuService(IChucVuRespository ChucVuRespository, IMapper mapper,  IHttpContextAccessor httpContextAccessor)
        {
            _ChucVuRespository = ChucVuRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<ChucVuDto> CreateChucVu(ChucVuDto request)
        {
            var result = new AppResponse<ChucVuDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var chucVu = new ChucVu();
                chucVu = _mapper.Map<ChucVu>(request);
                chucVu.Id = Guid.NewGuid();
                chucVu.CreatedBy = UserName;
                _ChucVuRespository.Add(chucVu);

                request.Id = chucVu.Id;
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

        public AppResponse<string> DeleteChucVu(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var chucVu = new ChucVu();
                chucVu = _ChucVuRespository.Get(Id);
                chucVu.IsDeleted = true;

                _ChucVuRespository.Edit(chucVu);

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



        public AppResponse<ChucVuDto> EditChucVu(ChucVuDto request)
        {
            var result = new AppResponse<ChucVuDto>();
            try
            {
                var chucVu = new ChucVu();
                chucVu = _mapper.Map<ChucVu>(request);
                _ChucVuRespository.Edit(chucVu);

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

        public AppResponse<List<ChucVuDto>> GetAllChucVu()
        {
            var result = new AppResponse<List<ChucVuDto>>();
            //string userId = "";
            try
            {
                var query = _ChucVuRespository.GetAll();
                var list = query.Select(m => new ChucVuDto
                {
                    Id = m.Id,
                   TenChucVu = m.TenChucVu,
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



        public AppResponse<ChucVuDto> GetChucVuId(Guid Id)
        {
            var result = new AppResponse<ChucVuDto>();
            try
            {
                var chucVu = _ChucVuRespository.Get(Id);
                var data = _mapper.Map<ChucVuDto>(chucVu);
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
