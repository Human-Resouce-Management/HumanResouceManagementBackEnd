using AutoMapper;
using MayNghien.Models.Response.Base;
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

        public PhuCapService(IPhuCapRespository PhuCapRepository, IMapper mapper)
        {
            _PhuCapRepository = PhuCapRepository;
            _mapper = mapper;
        }

        public AppResponse<PhuCapDto> CreatePhuCap(PhuCapDto request)
        {
            var result = new AppResponse<PhuCapDto>();
            try
            {
                var tuyendung = new PhuCap();
                tuyendung = _mapper.Map<PhuCap>(request);
                tuyendung.Id = Guid.NewGuid();
                _PhuCapRepository.Add(tuyendung);

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

        public AppResponse<string> DeletePhuCap(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new PhuCap();
                tuyendung = _PhuCapRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _PhuCapRepository.Edit(tuyendung);

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



        public AppResponse<PhuCapDto> EditPhuCap(PhuCapDto tuyendung)
        {
            var result = new AppResponse<PhuCapDto>();
            try
            {
                var request = new PhuCap();
                request = _mapper.Map<PhuCap>(tuyendung);
                _PhuCapRepository.Edit(request);

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
                var tuyendung = _PhuCapRepository.Get(Id);
                var data = _mapper.Map<PhuCapDto>(tuyendung);
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
