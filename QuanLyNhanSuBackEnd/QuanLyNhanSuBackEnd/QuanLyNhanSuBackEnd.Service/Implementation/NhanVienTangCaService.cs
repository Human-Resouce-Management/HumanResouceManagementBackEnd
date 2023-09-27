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
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
   
        public class NhanVienTangCaService : INhanVienTangCaService
    {
            private readonly INhanVienTangCaRespository _NhanVienTangCaRepository;
            private readonly IMapper _mapper;
            private IHttpContextAccessor _httpContextAccessor;
        public NhanVienTangCaService(INhanVienTangCaRespository NhanVienTangCaRepository, IMapper mapper,   IHttpContextAccessor httpContextAccessor)
            {
            _NhanVienTangCaRepository = NhanVienTangCaRepository;
                _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            }

            public AppResponse<NhanVienTangCaDto> CreateNhanVienTangCa(NhanVienTangCaDto request)
            {
                var result = new AppResponse<NhanVienTangCaDto>();
                try
                {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var nhanVienTangCa = new NhanVienTangCa();
                    nhanVienTangCa = _mapper.Map<NhanVienTangCa>(request);
                    nhanVienTangCa.Id = Guid.NewGuid();
                     nhanVienTangCa.CreatedBy = UserName;

                _NhanVienTangCaRepository.Add(nhanVienTangCa);
     
                    request.Id = nhanVienTangCa.Id;
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

            public AppResponse<string> DeleteNhanVienTangCa(Guid Id)
            {
                var result = new AppResponse<string>();
                try
                {
                    var nhanVienTangCa = new NhanVienTangCa();
                    nhanVienTangCa = _NhanVienTangCaRepository.Get(Id);
                    nhanVienTangCa.IsDeleted = true;

                _NhanVienTangCaRepository.Edit(nhanVienTangCa);

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



            public AppResponse<NhanVienTangCaDto> EditNhanVienTangCa(NhanVienTangCaDto request)
            {
                var result = new AppResponse<NhanVienTangCaDto>();
                try
                {
                    var nhanVienTangCa = new NhanVienTangCa();
                    nhanVienTangCa = _mapper.Map<NhanVienTangCa>(request);
                _NhanVienTangCaRepository.Edit(nhanVienTangCa);

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

            public AppResponse<List<NhanVienTangCaDto>> GetAllNhanVienTangCa()
            {
                var result = new AppResponse<List<NhanVienTangCaDto>>();
                //string userId = "";
                try
                {
                    var query = _NhanVienTangCaRepository.GetAll()
                    .Include(n => n.NhanVien)
                    .Include(n => n.TangCa)
                    ;
                    var list = query.Select(m => new NhanVienTangCaDto
                    {
                        Id = m.Id,
                        Ten = m.NhanVien.Ten,
                        NhanVienId = m.NhanVienId,
                        TangCaId = m.TangCaId,
                      
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



            public AppResponse<NhanVienTangCaDto> GetNhanVienTangCaId(Guid Id)
            {
                var result = new AppResponse<NhanVienTangCaDto>();
                try
                {
                    var query = _NhanVienTangCaRepository.FindBy(x => x.Id == Id).Include(x=>x.NhanVien);
                    var data = query.Select(x => new NhanVienTangCaDto
                    {
                        Id = x.Id,
                        NhanVienId = x.NhanVienId,
                        TangCaId = x.TangCaId,
                        Ten = x.NhanVien.Ten
                    }).First();
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

        public async Task<AppResponse<SearchNhanVienTangCaRespository>> SearchNhanVienTangCa(SearchRequest request)
        {
            var result = new AppResponse<SearchNhanVienTangCaRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _NhanVienTangCaRepository.CountRecordsByPredicate(query);

                var users = _NhanVienTangCaRepository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<NhanVienTangCaDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchNhanVienTangCaRespository
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
        private ExpressionStarter<NhanVienTangCa> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<NhanVienTangCa>(true);

                foreach (var filter in Filters)
                {
                    switch (filter.FieldName)
                    {
                        case "TenChucVu":
                            predicate = predicate.And(m => m.NhanVien.Ten.Contains(filter.Value));
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

