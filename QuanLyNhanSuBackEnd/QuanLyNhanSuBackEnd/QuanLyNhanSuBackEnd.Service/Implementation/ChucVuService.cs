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
    public class ChucVuService : IChucVuService
    {
        private readonly IChucVuRespository _ChucVuRespository;
        private readonly IMapper _mapper;

        public ChucVuService(IChucVuRespository ChucVuRespository, IMapper mapper)
        {
            _ChucVuRespository = ChucVuRespository;
            _mapper = mapper;
        }

        public AppResponse<ChucVuDto> CreateChucVu(ChucVuDto request)
        {
            var result = new AppResponse<ChucVuDto>();
            try
            {
                var tuyendung = new ChucVu();
                tuyendung = _mapper.Map<ChucVu>(request);
                tuyendung.Id = Guid.NewGuid();
                _ChucVuRespository.Add(tuyendung);

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

        public AppResponse<string> DeleteChucVu(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new ChucVu();
                tuyendung = _ChucVuRespository.Get(Id);
                tuyendung.IsDeleted = true;

                _ChucVuRespository.Edit(tuyendung);

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



        public AppResponse<ChucVuDto> EditChucVu(ChucVuDto tuyendung)
        {
            var result = new AppResponse<ChucVuDto>();
            try
            {
                var request = new ChucVu();
                request = _mapper.Map<ChucVu>(tuyendung);
                _ChucVuRespository.Edit(request);

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

        public AppResponse<List<ChucVuDto>> GetAllChucVu()
        {
            var result = new AppResponse<List<ChucVuDto>>();
            //string userId = "";
            try
            {
                var query = _ChucVuRespository.GetAll();
                var list = query.Select(m => new ChucVuDto
                {
                    Id = m.Id,
                   TenChucVu = m.TenChucVu,
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



        public AppResponse<ChucVuDto> GetChucVuId(Guid Id)
        {
            var result = new AppResponse<ChucVuDto>();
            try
            {
                var tuyendung = _ChucVuRespository.Get(Id);
                var data = _mapper.Map<ChucVuDto>(tuyendung);
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
