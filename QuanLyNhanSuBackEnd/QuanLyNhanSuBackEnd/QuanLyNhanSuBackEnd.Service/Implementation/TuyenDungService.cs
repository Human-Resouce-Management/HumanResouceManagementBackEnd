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
    public class TuyenDungService : ITuyenDungService
    {
        private readonly ITuyenDungRespository _tuyenDungRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public TuyenDungService(ITuyenDungRespository tuyenDungRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _tuyenDungRepository = tuyenDungRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<TuyenDungDto> CreateTuyenDung(TuyenDungDto request)
        {
            var result = new AppResponse<TuyenDungDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = new TuyenDung();
                tuyendung = _mapper.Map<TuyenDung>(request);
                tuyendung.Id = Guid.NewGuid();
                //nhớ thêm dòng này
                tuyendung.CreatedBy = UserName;
                _tuyenDungRepository.Add(tuyendung);

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
                tuyendung = _tuyenDungRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _tuyenDungRepository.Edit(tuyendung);

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

        

        public AppResponse<TuyenDungDto> EditTuyenDung(TuyenDungDto tuyendung)
        {
            var result = new AppResponse<TuyenDungDto>();
            try
            {
                var request = new TuyenDung();
                request = _mapper.Map<TuyenDung>(tuyendung);
                _tuyenDungRepository.Edit(request);

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

        public AppResponse<List<TuyenDungDto>> GetAllTuyenDung()
        {
            var result = new AppResponse<List<TuyenDungDto>>();
            //string userId = "";
            try
            {
                var query = _tuyenDungRepository.GetAll();
                var list = query.Select(m => new TuyenDungDto
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

       

        public AppResponse<TuyenDungDto> GetTuyenDungId(Guid Id)
        {
            var result = new AppResponse<TuyenDungDto>();
            try
            {
                var tuyendung = _tuyenDungRepository.Get(Id);
                var data = _mapper.Map<TuyenDungDto>(tuyendung);
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
