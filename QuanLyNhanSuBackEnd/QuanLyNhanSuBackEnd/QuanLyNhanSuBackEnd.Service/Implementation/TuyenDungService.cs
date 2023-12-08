using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
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


        public async Task<AppResponse<SearchTuyenDungRespository>> SearchTuyenDung(SearchRequest request)
        {
            var result = new AppResponse<SearchTuyenDungRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _tuyenDungRepository.CountRecordsByPredicate(query);

                var users = _tuyenDungRepository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<TuyenDungDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchTuyenDungRespository
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
        private ExpressionStarter<TuyenDung> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<TuyenDung>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "lienHe":
                                predicate = predicate.And(m => m.LienHe.Contains(filter.Value));
                                break;

                            default:
                                break;
                        }
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
