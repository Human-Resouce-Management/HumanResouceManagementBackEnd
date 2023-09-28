using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Implementation;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Model.Response.User;
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
        private IHttpContextAccessor _httpContextAccessor;

        public TangCaService(ITangCaRespository TangCaRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _TangCaRepository = TangCaRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<TangCaDto> CreateTangCa(TangCaDto request)
        {
            var result = new AppResponse<TangCaDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tangCa = new TangCa();
                tangCa = _mapper.Map<TangCa>(request);
                tangCa.Id = Guid.NewGuid();
                tangCa.CreatedBy = UserName;
                _TangCaRepository.Add(tangCa);

                request.Id = tangCa.Id;
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
                var tangCa = new TangCa();
                tangCa = _TangCaRepository.Get(Id);
                tangCa.IsDeleted = true;

                _TangCaRepository.Edit(tangCa);

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



        public AppResponse<TangCaDto> EditTangCa(TangCaDto request)
        {
            var result = new AppResponse<TangCaDto>();
            try
            {
                var tangCa = new TangCa();
                tangCa = _mapper.Map<TangCa>(request);
                _TangCaRepository.Edit(tangCa);

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
        public async Task<AppResponse<SearchTangCaRespository>> SearchTangCa(SearchRequest request)
        {
            var result = new AppResponse<SearchTangCaRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _TangCaRepository.CountRecordsByPredicate(query);

                var users = _TangCaRepository.FindByPredicate(query).Include(x => x.NguoiXacNhanId).Select(x => new TangCaDto
                {
                    Id = x.Id,
                   SoGio = x.SoGio,
                   Ngay = x.Ngay,
                   GioBatDau = x.GioBatDau,
                   GioKetThuc = x.GioKetThuc,
                   NguoiXacNhanId = x.NguoiXacNhanId,
                   HeSoCa = x.HeSoCa,

                }).ToList(); ; ;
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<TangCaDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchTangCaRespository
                {
                    TotalRows = numOfRecords,
                    TotalPages = SearchHelper.CalculateNumOfPages(numOfRecords, pageSize),
                    CurrentPage = pageIndex,
                    Data = dtoList,
                };

                result.Data = searchUserResult;
                result.IsSuccess = true;

                return result;

            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }
        private ExpressionStarter<TangCa> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<TangCa>(true);

                foreach (var filter in Filters)
                {
                    switch (filter.FieldName)
                    {
                        case "TangCa":
                            predicate = predicate.And(m => m.GioBatDau.Contains(filter.Value));
                            break;

                        default:
                            break;
                    }
                }
                return predicate;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
