﻿using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

    public class BoPhanService : IBoPhanService
    {
        private readonly IBoPhanRespository _BoPhanRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public BoPhanService(IBoPhanRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _BoPhanRepository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<BoPhanDto> CreateBoPhan(BoPhanDto request)
        {
            var result = new AppResponse<BoPhanDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = new BoPhan();
                tuyendung = _mapper.Map<BoPhan>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;

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
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new BoPhan();
                request = _mapper.Map<BoPhan>(tuyendung);
                //request.CreatedBy = UserName;
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
                var query = _BoPhanRepository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new BoPhanDto
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
        private ExpressionStarter<BoPhan> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<BoPhan>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "tenBoPhan":
                                predicate = predicate.And(m => m.TenBoPhan.Contains(filter.Value));
                                break;

                            default:
                                break;
                        }
                    }
                }
                predicate = predicate.And(m => m.IsDeleted == false);
                return predicate;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AppResponse<SearchResponse<BoPhanDto>> SearchBoPhan(SearchRequest request)
        {

            var result = new AppResponse<SearchResponse<BoPhanDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _BoPhanRepository.CountRecordsByPredicate(query);

                var users = _BoPhanRepository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<BoPhanDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchResponse<BoPhanDto>
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


    }
}

