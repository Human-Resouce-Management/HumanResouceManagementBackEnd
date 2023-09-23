using AutoMapper;
using MayNghien.Common.Helpers;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Implementation;
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
    public class ThoiViecService : IThoiViecService
    {
        private readonly IThoiViecRespository _ThoiViecRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public ThoiViecService(IThoiViecRespository ThoiViecRespository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ThoiViecRepository = ThoiViecRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<ThoiViecDto> CreateThoiViec(ThoiViecDto request)
        {
            var result = new AppResponse<ThoiViecDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }

                var tuyendung = _mapper.Map<ThoiViec>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;
                _ThoiViecRepository.Add(tuyendung);
                request.Id = tuyendung.Id;
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

        public AppResponse<string> DeleteThoiViec(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new ThoiViec();
                tuyendung = _ThoiViecRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _ThoiViecRepository.Edit(tuyendung);

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



        public AppResponse<ThoiViecDto> EditThoiViec(ThoiViecDto tuyendung)
        {
            var result = new AppResponse<ThoiViecDto>();
            try
            {
                var request = new ThoiViec();
                request = _mapper.Map<ThoiViec>(tuyendung);
                _ThoiViecRepository.Edit(request);

                result.IsSuccess = true;
                result.Data = tuyendung;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }

        public AppResponse<List<ThoiViecDto>> GetAllThoiViec()
        {
            var result = new AppResponse<List<ThoiViecDto>>();
            //string userId = "";
            try
            {
                var query = _ThoiViecRepository.GetAll()
                   .Include(n=>n.NhanVien);

                var list = query.Select(m => new ThoiViecDto
                {
                    Id = m.Id,
                    Ten = m.NhanVien.Ten,
                    NgayNghi = m.NgayNghi,
                    DaThoiViec = m.DaThoiViec,
                    NhanVienId = m.NhanVienId,
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



        public AppResponse<ThoiViecDto> GetThoiViecId(Guid Id)
        {
            var result = new AppResponse<ThoiViecDto>();
            try
            {
                var tuyendung = _ThoiViecRepository.FindBy(x=>x.Id == Id)
                    .Include(x=>x.NhanVien);
                var data = tuyendung.Select(x=> new ThoiViecDto
                {
                    DaThoiViec = x.DaThoiViec,
                    Id = Id,
                    NgayNghi=x.NgayNghi,
                    NhanVienId = x.NhanVienId,
                    Ten = x.NhanVien.Ten
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
