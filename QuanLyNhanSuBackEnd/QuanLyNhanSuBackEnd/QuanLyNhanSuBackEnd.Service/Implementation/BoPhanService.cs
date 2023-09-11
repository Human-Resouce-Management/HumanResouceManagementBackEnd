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
   
        public class BoPhanService : IBoPhanService
        {
            private readonly IBoPhanRespository _BoPhanRepository;
            private readonly IMapper _mapper;

            public BoPhanService(IBoPhanRespository BoPhanRepository, IMapper mapper)
            {
                 _BoPhanRepository = BoPhanRepository;
                _mapper = mapper;
            }

            public AppResponse<BoPhanDto> CreateBoPhan(BoPhanDto request)
            {
                var result = new AppResponse<BoPhanDto>();
                try
                {
                    var tuyendung = new BoPhan();
                    tuyendung = _mapper.Map<BoPhan>(request);
                    tuyendung.Id = Guid.NewGuid();
           
                   _BoPhanRepository.Add(tuyendung);
     
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

            public AppResponse<string> DeleteBoPhan(Guid Id)
            {
                var result = new AppResponse<string>();
                try
                {
                    var tuyendung = new BoPhan();
                    tuyendung = _BoPhanRepository.Get(Id);
                    tuyendung.IsDeleted = true;

                _BoPhanRepository.Edit(tuyendung);

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



            public AppResponse<BoPhanDto> EditBoPhan(BoPhanDto tuyendung)
            {
                var result = new AppResponse<BoPhanDto>();
                try
                {
                    var request = new BoPhan();
                    request = _mapper.Map<BoPhan>(tuyendung);
                    _BoPhanRepository.Edit(request);

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

            public AppResponse<List<BoPhanDto>> GetAllBoPhan()
            {
                var result = new AppResponse<List<BoPhanDto>>();
                //string userId = "";
                try
                {
                    var query = _BoPhanRepository.GetAll();
                    var list = query.Select(m => new BoPhanDto
                    {
                        Id = m.Id,
                        TenBoPhan = m.TenBoPhan,
                        QuanLy = m.QuanLy,
                      
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



            public AppResponse<BoPhanDto> GetBoPhanId(Guid Id)
            {
                var result = new AppResponse<BoPhanDto>();
                try
                {
                    var tuyendung = _BoPhanRepository.Get(Id);
                    var data = _mapper.Map<BoPhanDto>(tuyendung);
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

