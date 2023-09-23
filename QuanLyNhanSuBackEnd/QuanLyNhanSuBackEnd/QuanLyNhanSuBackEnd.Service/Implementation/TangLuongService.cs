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
    public class TangLuongService : ITangLuongService
    {
        private readonly ITangLuongRespository _TangLuongRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public TangLuongService(ITangLuongRespository TangLuongRespository, IMapper mapper ,  IHttpContextAccessor httpContextAccessor)
        {
            _TangLuongRepository = TangLuongRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<TangLuongDto> CreateTangLuong(TangLuongDto request)
        {
            var result = new AppResponse<TangLuongDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tangLuong = _mapper.Map<TangLuong>(request);
                tangLuong.Id = Guid.NewGuid();
                // tuyendung.BoPhanId = Guid.NewGuid();
                //tuyendung.ChucVuId = Guid.NewGuid();
                tangLuong.CreatedBy = UserName;
                _TangLuongRepository.Add(tangLuong);
                //request.ChucVuId = tuyendung.ChucVuId;
                //request.BoPhanId = tuyendung.BoPhanId;
                request.Id = tangLuong.Id;
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

        public AppResponse<string> DeleteTangLuong(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tangLuong = new TangLuong();
                tangLuong = _TangLuongRepository.Get(Id);
                tangLuong.IsDeleted = true;

                _TangLuongRepository.Edit(tangLuong);

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



        public AppResponse<TangLuongDto> EditTangLuong(TangLuongDto request)
        {
            var result = new AppResponse<TangLuongDto>();
            try
            {
                var tangLuong = new TangLuong();
                tangLuong = _mapper.Map<TangLuong>(tangLuong);
                _TangLuongRepository.Edit(tangLuong);

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

        public AppResponse<List<TangLuongDto>> GetAllTangLuong()
        {
            var result = new AppResponse<List<TangLuongDto>>();
            //string userId = "";
            try
            {
                var query = _TangLuongRepository.GetAll()
                   .Include(n => n.NhanVien);

                var list = query.Select(m => new TangLuongDto
                {
                    Id = m.Id,
                   ten = m.NhanVien.Ten,
                   NhanVienId = m.NhanVienId,
                  NgayCapNhat = m.NgayCapNhat,
                  SoTien = m.SoTien,
                  HeSoCu = m.HeSoCu,
                  HeSoMoi = m.HeSoMoi,
                  NgayKetThuc = m.NgayKetThuc,

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



        public AppResponse<TangLuongDto> GetTangLuongId(Guid Id)
        {
            var result = new AppResponse<TangLuongDto>();
            try
            {
                var query = _TangLuongRepository.FindBy(x=>x.Id == Id)
                    .Include(x=>x.NhanVien);
                var data = query.Select(x => new TangLuongDto
                {
                    HeSoCu = x.HeSoCu,
                    HeSoMoi = x.HeSoMoi,
                    Id = Id,
                    NgayCapNhat = x.NgayCapNhat,
                    NgayKetThuc = x.NgayKetThuc,
                    NhanVienId = x.NhanVienId,
                    SoTien = x.SoTien,
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
