using AutoMapper;
using MayNghien.Models.Response.Base;
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

        public TangLuongService(ITangLuongRespository TangLuongRespository, IMapper mapper)
        {
            _TangLuongRepository = TangLuongRespository;
            _mapper = mapper;
        }

        public AppResponse<TangLuongDto> CreateTangLuong(TangLuongDto request)
        {
            var result = new AppResponse<TangLuongDto>();
            try
            {
                var tuyendung = _mapper.Map<TangLuong>(request);
                tuyendung.Id = Guid.NewGuid();
                // tuyendung.BoPhanId = Guid.NewGuid();
                //tuyendung.ChucVuId = Guid.NewGuid();
                _TangLuongRepository.Add(tuyendung);
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

        public AppResponse<string> DeleteTangLuong(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new TangLuong();
                tuyendung = _TangLuongRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _TangLuongRepository.Edit(tuyendung);

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



        public AppResponse<TangLuongDto> EditTangLuong(TangLuongDto tuyendung)
        {
            var result = new AppResponse<TangLuongDto>();
            try
            {
                var request = new TangLuong();
                request = _mapper.Map<TangLuong>(tuyendung);
                _TangLuongRepository.Edit(request);

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
                var tuyendung = _TangLuongRepository.Get(Id);
                var data = _mapper.Map<TangLuongDto>(tuyendung);
                data.ten = tuyendung.NhanVien.Ten;
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
