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
    public class NghiPhepService : INghiPhepService
    {
        private readonly INghiPhepRespository _NghiPhepRepository;
        private readonly IMapper _mapper;

        public NghiPhepService(INghiPhepRespository NghiPhepRespository, IMapper mapper)
        {
            _NghiPhepRepository = NghiPhepRespository;
            _mapper = mapper;
        }

        public AppResponse<NghiPhepDto> CreateNghiPhep(NghiPhepDto request)
        {
            var result = new AppResponse<NghiPhepDto>();
            try
            {
                var tuyendung = _mapper.Map<NghiPhep>(request);
                tuyendung.Id = Guid.NewGuid();
                // tuyendung.BoPhanId = Guid.NewGuid();
                //tuyendung.ChucVuId = Guid.NewGuid();
                _NghiPhepRepository.Add(tuyendung);
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

        public AppResponse<string> DeleteNghiPhep(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new NghiPhep();
                tuyendung = _NghiPhepRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _NghiPhepRepository.Edit(tuyendung);

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



        public AppResponse<NghiPhepDto> EditNghiPhep(NghiPhepDto tuyendung)
        {
            var result = new AppResponse<NghiPhepDto>();
            try
            {
                var request = new NghiPhep();
                request = _mapper.Map<NghiPhep>(tuyendung);
                _NghiPhepRepository.Edit(request);

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

        public AppResponse<List<NghiPhepDto>> GetAllNghiPhep()
        {
            var result = new AppResponse<List<NghiPhepDto>>();
            //string userId = "";
            try
            {
                var query = _NghiPhepRepository.GetAll()
                   .Include(n => n.NhanVien);

                var list = query.Select(m => new NghiPhepDto
                {
                    Id = m.Id,
                   ten = m.NhanVien.Ten,
                   NhanVienId = m.NhanVienId,
                   NghiCoLuong = m.NghiCoLuong,
                   NgayKetThuc = m.NgayKetThuc,
                   NgayNghi = m.NgayNghi,
                   SoGioNghi = m.SoGioNghi,
                   NguoiXacNhanId = m.NguoiXacNhanId,

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



        public AppResponse<NghiPhepDto> GetNghiPhepId(Guid Id)
        {
            var result = new AppResponse<NghiPhepDto>();
            try
            {
                var tuyendung = _NghiPhepRepository.Get(Id);
                var data = _mapper.Map<NghiPhepDto>(tuyendung);
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
