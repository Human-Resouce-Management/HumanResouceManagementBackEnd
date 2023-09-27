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
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new BoPhan();
                    request = _mapper.Map<BoPhan>(tuyendung);
                    request.CreatedBy = UserName;
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
        private ExpressionStarter<BoPhan> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<BoPhan>(true);

                foreach (var filter in Filters)
                {
                    switch (filter.FieldName)
                    {
                        case "TenBoPhan":
                            predicate = predicate.And(m => m.TenBoPhan.Contains(filter.Value));
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
        public static int LevenshteinDistance(string str1, string str2)
        {
            // Tạo mảng hai chiều để lưu trữ khoảng cách giữa các ký tự
            int[,] distance = new int[str1.Length + 1, str2.Length + 1];

            // Khởi tạo khoảng cách
            for (int i = 0; i <= str1.Length; i++)
            {
                distance[i, 0] = i;
            }
            for (int i = 0; i <= str2.Length; i++)
            {
                distance[0, i] = i;
            }

            // Tính khoảng cách Levenshtein
            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        distance[i, j] = distance[i - 1, j - 1];
                    }
                    else
                    {
                        distance[i, j] = Math.Min(distance[i - 1, j - 1] + 1, Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1));
                    }
                }
            }

            // Trả về khoảng cách
            return distance[str1.Length, str2.Length];
        }

        public async Task<AppResponse<SearchBoPhanRespository>> SearchBoPhan(SearchRequest request)
        {

            var result = new AppResponse<SearchBoPhanRespository>();

            try
            {

                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _BoPhanRepository.CountRecordsByPredicate(query);
                var levenshteinDistance = 10;
                var users = _BoPhanRepository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;

                if (request.Filters == null || request.Filters.Count == 0)
                {
                    // Trả về một kết quả rỗng
                    return result.BuildResult(new SearchBoPhanRespository());
                }
                //var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                //var dtoList = _mapper.Map<List<BoPhanDto>>(UserList);
             // Khoảng cách Levenshtein tối đa được chấp nhận
               
                // Tìm kiếm gần đúng TenBoPhan
                var nearTenBoPhanList = new List<BoPhan>();
                foreach (var user in users)
                {
                    var distance = LevenshteinDistance(request.Filters.First(f => f.FieldName == "TenBoPhan").Value, user.TenBoPhan);
                    if (distance <= levenshteinDistance)
                    {
                        nearTenBoPhanList.Add(user);
                    }
                }

                // Sắp xếp danh sách kết quả theo khoảng cách Levenshtein
                nearTenBoPhanList.Sort((x, y) => LevenshteinDistance(request.Filters.First(f => f.FieldName == "TenBoPhan").Value, x.TenBoPhan).CompareTo(LevenshteinDistance(request.Filters.First(f => f.FieldName == "TenBoPhan").Value, y.TenBoPhan)));

                // Trả về kết quả
                var searchUserResult = new SearchBoPhanRespository
                {
                    TotalRows = nearTenBoPhanList.Count,
                    TotalPages = SearchHelper.CalculateNumOfPages(nearTenBoPhanList.Count, pageSize),
                    CurrentPage = pageIndex,
                    Data = _mapper.Map<List<BoPhanDto>>(nearTenBoPhanList),
                };

                return result.BuildResult(searchUserResult);

            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }

        }

        }


    }

