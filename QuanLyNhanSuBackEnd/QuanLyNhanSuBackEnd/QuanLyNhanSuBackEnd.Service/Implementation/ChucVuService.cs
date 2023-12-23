using AutoMapper;
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
    public class ChucVuService : IChucVuService
    {
        private readonly IChucVuRespository _ChucVuRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public ChucVuService(IChucVuRespository ChucVuRespository, IMapper mapper,  IHttpContextAccessor httpContextAccessor)
        {
            _ChucVuRespository = ChucVuRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<ChucVuDto> CreateChucVu(ChucVuDto request)
        {
            var result = new AppResponse<ChucVuDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var chucVu = new ChucVu();
                chucVu = _mapper.Map<ChucVu>(request);
                chucVu.Id = Guid.NewGuid();
                chucVu.CreatedBy = UserName;
                _ChucVuRespository.Add(chucVu);

                request.Id = chucVu.Id;
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
                var chucVu = new ChucVu();
                chucVu = _ChucVuRespository.Get(Id);
                chucVu.IsDeleted = true;

                _ChucVuRespository.Edit(chucVu);

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



        public AppResponse<ChucVuDto> EditChucVu(ChucVuDto request)
        {
            var result = new AppResponse<ChucVuDto>();
            try
            {
                var chucVu = new ChucVu();
                chucVu = _mapper.Map<ChucVu>(request);
                _ChucVuRespository.Edit(chucVu);

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

        public AppResponse<List<ChucVuDto>> GetAllChucVu()
        {
            var result = new AppResponse<List<ChucVuDto>>();
            //string userId = "";
            try
            {
                var query = _ChucVuRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new ChucVuDto
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
                var chucVu = _ChucVuRespository.Get(Id);
                var data = _mapper.Map<ChucVuDto>(chucVu);
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
        public async Task<AppResponse<SearchChucVuRespository>> SearchChucVu(SearchRequest request)
        {
            var result = new AppResponse<SearchChucVuRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _ChucVuRespository.CountRecordsByPredicate(query);

                var users = _ChucVuRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<ChucVuDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchChucVuRespository
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
        private ExpressionStarter<ChucVu> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<ChucVu>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                {
              
                        switch (filter.FieldName)
                        {
                            case "tenChucVu":
                                predicate = predicate.And(m => m.TenChucVu.Contains(filter.Value));
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
    }
}
