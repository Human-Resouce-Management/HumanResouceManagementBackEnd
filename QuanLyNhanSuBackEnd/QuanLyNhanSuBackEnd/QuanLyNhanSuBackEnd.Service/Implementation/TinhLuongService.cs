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
    public class TinhLuongService : ITinhLuongService
    {
        private readonly ITinhLuongRespository _ThoiViecRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public TinhLuongService(ITinhLuongRespository ThoiViecRespository, IMapper mapper , IHttpContextAccessor httpContextAccessor)
        {
            _ThoiViecRepository = ThoiViecRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<TinhLuongDto> CreateTinhLuong(TinhLuongDto request)
        {
            var result = new AppResponse<TinhLuongDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = _mapper.Map<TinhLuong>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;
                // tuyendung.BoPhanId = Guid.NewGuid();
                //tuyendung.ChucVuId = Guid.NewGuid();
                _ThoiViecRepository.Add(tuyendung);
                //request.ChucVuId = tuyendung.ChucVuId;
                //request.BoPhanId = tuyendung.BoPhanId;
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

        public AppResponse<string> DeleteTinhLuong(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new TinhLuong();
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



        public AppResponse<TinhLuongDto> EditTinhLuong(TinhLuongDto tuyendung)
        {
            var result = new AppResponse<TinhLuongDto>();
            try
            {
                var request = new TinhLuong();
                request = _mapper.Map<TinhLuong>(tuyendung);
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

        public AppResponse<List<TinhLuongDto>> GetAllTinhLuong()
        {
            var result = new AppResponse<List<TinhLuongDto>>();
            //string userId = "";
            try
            {
                var query = _ThoiViecRepository.GetAll()
                   .Include(n=>n.NhanVien);

                var list = query.Select(m => new TinhLuongDto
                {
                    Id = m.Id,
                    ten = m.NhanVien.Ten,
                   SoLuong = m.SoLuong,
                   MucLuong = m.MucLuong,
                   CacKhoangThem = m.CacKhoangThem,
                   CacKhoangTru = m.CacKhoangTru,
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



        public AppResponse<TinhLuongDto> GetTinhLuongId(Guid Id)
        {
            var result = new AppResponse<TinhLuongDto>();
            try
            {
                var tuyendung = _ThoiViecRepository.FindBy(x=>x.Id == Id).Include(x=>x.NhanVien);
                var data = tuyendung.Select(x => new TinhLuongDto
                {
                    CacKhoangThem = x.CacKhoangThem,
                    CacKhoangTru =x.CacKhoangTru,
                    Id = Id,
                    MucLuong = x.MucLuong,
                    NhanVienId=x.NhanVienId,
                    SoLuong = x.SoLuong,
                    ten = x.NhanVien.Ten
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
