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
    public class TangCaService : ITangCaService
    {
        private readonly ITangCaRespository _TangCaRepository;
        private readonly IMapper _mapper;

        public TangCaService(ITangCaRespository TangCaRepository, IMapper mapper)
        {
            _TangCaRepository = TangCaRepository;
            _mapper = mapper;
        }

        public AppResponse<TangCaDto> CreateTangCa(TangCaDto request)
        {
            var result = new AppResponse<TangCaDto>();
            try
            {
                var tuyendung = new TangCa();
                tuyendung = _mapper.Map<TangCa>(request);
                tuyendung.Id = Guid.NewGuid();
                _TangCaRepository.Add(tuyendung);

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

        public AppResponse<string> DeleteTangCa(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new TangCa();
                tuyendung = _TangCaRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _TangCaRepository.Edit(tuyendung);

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



        public AppResponse<TangCaDto> EditTangCa(TangCaDto tuyendung)
        {
            var result = new AppResponse<TangCaDto>();
            try
            {
                var request = new TangCa();
                request = _mapper.Map<TangCa>(tuyendung);
                _TangCaRepository.Edit(request);

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

        public AppResponse<List<TangCaDto>> GetAllTangCa()
        {
            var result = new AppResponse<List<TangCaDto>>();
            //string userId = "";
            try
            {
                var query = _TangCaRepository.GetAll();
                var list = query.Select(m => new TangCaDto
                {
                    Id = m.Id,
                    SoGio = m.SoGio,
                    Ngay = m.Ngay,
                    GioBatDau = m.GioBatDau,
                    GioKetThuc = m.GioKetThuc
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



        public AppResponse<TangCaDto> GetTangCaId(Guid Id)
        {
            var result = new AppResponse<TangCaDto>();
            try
            {
                var tuyendung = _TangCaRepository.Get(Id);
                var data = _mapper.Map<TangCaDto>(tuyendung);
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
