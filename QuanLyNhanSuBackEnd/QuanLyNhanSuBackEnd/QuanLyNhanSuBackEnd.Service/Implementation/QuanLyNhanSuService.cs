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
    public class QuanLyNhanSuService : IQuanLyNhanSuService
    {
        private readonly IQuanLyNhanSuRespository _QuanLyNhanSuBackEndRepository;
        private readonly IMapper _mapper;

        public QuanLyNhanSuService(IQuanLyNhanSuRespository QuanLyNhanSuBackEndRepository, IMapper mapper)
        {
            _QuanLyNhanSuBackEndRepository = QuanLyNhanSuBackEndRepository;
            _mapper = mapper;
        }

        public AppResponse<QuanLyNhanSuBackEndDto> CreateTuyenDung(QuanLyNhanSuBackEndDto request)
        {
            var result = new AppResponse<QuanLyNhanSuBackEndDto>();
            try
            {
                var tuyendung = new TuyenDung();
                tuyendung = _mapper.Map<TuyenDung>(request);
                tuyendung.Id = Guid.NewGuid();
                _QuanLyNhanSuBackEndRepository.Add(tuyendung);

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

        public AppResponse<string> DeleteTuyenDung(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung= new TuyenDung();
                tuyendung = _QuanLyNhanSuBackEndRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _QuanLyNhanSuBackEndRepository.Edit(tuyendung);

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

        

        public AppResponse<QuanLyNhanSuBackEndDto> EditTuyenDung(QuanLyNhanSuBackEndDto tuyendung)
        {
            var result = new AppResponse<QuanLyNhanSuBackEndDto>();
            try
            {
                var request = new TuyenDung();
                request = _mapper.Map<TuyenDung>(tuyendung);
                _QuanLyNhanSuBackEndRepository.Edit(request);

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

        public AppResponse<List<QuanLyNhanSuBackEndDto>> GetAllTuyenDung()
        {
            var result = new AppResponse<List<QuanLyNhanSuBackEndDto>>();
            //string userId = "";
            try
            {
                var query = _QuanLyNhanSuBackEndRepository.GetAll();
                var list = query.Select(m => new QuanLyNhanSuBackEndDto
                {
                    Id =  m.Id,
                    Ten = m.Ten,
                    LienHe = m.LienHe,
                    ViTriUngTuyen = m.ViTriUngTuyen,
                    KetQua = m.KetQua
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

       

        public AppResponse<QuanLyNhanSuBackEndDto> GetTuyenDungId(Guid Id)
        {
            var result = new AppResponse<QuanLyNhanSuBackEndDto>();
            try
            {
                var tuyendung = _QuanLyNhanSuBackEndRepository.Get(Id);
                var data = _mapper.Map<QuanLyNhanSuBackEndDto>(tuyendung);
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
