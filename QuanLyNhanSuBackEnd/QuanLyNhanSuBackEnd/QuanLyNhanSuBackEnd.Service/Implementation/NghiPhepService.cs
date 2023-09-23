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
                var nghiPhep = _mapper.Map<NghiPhep>(request);
                nghiPhep.Id = Guid.NewGuid();
                // tuyendung.BoPhanId = Guid.NewGuid();
                //tuyendung.ChucVuId = Guid.NewGuid();
                _NghiPhepRepository.Add(nghiPhep);
                //request.ChucVuId = tuyendung.ChucVuId;
                //request.BoPhanId = tuyendung.BoPhanId;
                request.Id = nghiPhep.Id;
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
                var nghiPhep = new NghiPhep();
                nghiPhep = _NghiPhepRepository.Get(Id);
                nghiPhep.IsDeleted = true;

                _NghiPhepRepository.Edit(nghiPhep);

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



        public AppResponse<NghiPhepDto> EditNghiPhep(NghiPhepDto request)
        {
            var result = new AppResponse<NghiPhepDto>();
            try
            {
                var nghiPhep = new NghiPhep();
                nghiPhep = _mapper.Map<NghiPhep>(request);
                _NghiPhepRepository.Edit(nghiPhep);

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
                var query = _NghiPhepRepository.FindBy(x => x.Id == Id).Include(x=>x.NhanVien);
                var data =  query.Select(x=>new NghiPhepDto
                {
                    Id = x.Id,
                    NgayKetThuc = x.NgayKetThuc,
                    NgayNghi = x.NgayNghi,
                    NghiCoLuong = x.NghiCoLuong,
                    NguoiXacNhanId =x.NguoiXacNhanId,
                    NhanVienId = x.NhanVienId,
                    SoGioNghi = x.SoGioNghi,
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
