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
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRespository _NhanVienRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public NhanVienService(INhanVienRespository NhanVienRespository, IMapper mapper,     IHttpContextAccessor httpContextAccessor)
        {
            _NhanVienRepository = NhanVienRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<NhanVienDto> CreateNhanVien(NhanVienDto request)
        {
            var result = new AppResponse<NhanVienDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                if (request.BoPhanId == null)
                {
                    return result.BuildError("Khong tim thay Bo Phan");
                }
                if (request.ChucVuId == null)
                {
                    return result.BuildError("Khong tim thay chuc vu");
                }
                var tuyendung = _mapper.Map<NhanVien>(request);
                tuyendung.Id = Guid.NewGuid();

                tuyendung.CreatedBy = UserName;
                _NhanVienRepository.Add(tuyendung);

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

        public AppResponse<string> DeleteNhanVien(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
              
              var  tuyendung = _NhanVienRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _NhanVienRepository.Edit(tuyendung);

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



        public AppResponse<NhanVienDto> EditNhanVien(NhanVienDto tuyendung)
        {
            var result = new AppResponse<NhanVienDto>();
            try
            {
                var request = new NhanVien();
                request = _mapper.Map<NhanVien>(tuyendung);
                _NhanVienRepository.Edit(request);

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

        public AppResponse<List<NhanVienDto>> GetAllNhanVien()
        {
            var result = new AppResponse<List<NhanVienDto>>();
            //string userId = "";
            try
            {
                var query = _NhanVienRepository.GetAll().Where(x => x.IsDeleted == false)
                    .Include(n=>n.BoPhan)
                    .Include(n=>n.ChucVu);
               
                var list = query.Where(x => x.IsDeleted == false).Select(m => new NhanVienDto
                {
                    Id = m.Id,
                   Ten = m.Ten,
                   CapBat = m.CapBat,
                   MucLuong = m.MucLuong,
                   HeSo = m.HeSo,
                   BoPhanId = m.BoPhanId,
                   ChucVuId = m.ChucVuId,
                   TenBoPhan = m.BoPhan.TenBoPhan,
                   TenChucVu = m.ChucVu.TenChucVu
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



        public AppResponse<NhanVienDto> GetNhanVienId(Guid Id)
        {
            var result = new AppResponse<NhanVienDto>();
            try
            {
                var query = _NhanVienRepository.FindBy(x => x.Id == Id)
                    .Include(x => x.ChucVu)
                    .Include(x => x.BoPhan);
                var data = query.Select(x=> new NhanVienDto
                {
                    BoPhanId = x.BoPhanId,
                    CapBat = x.CapBat,
                    ChucVuId =x.ChucVuId,
                    HeSo = x.HeSo,
                    Id = x.Id,
                    MucLuong = x.MucLuong,
                    Ten = x.Ten,
                    TenBoPhan = x.BoPhan.TenBoPhan,
                    TenChucVu = x.ChucVu.TenChucVu
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



        public async Task<AppResponse<SearchNhanVienRespository>> SearchNhanVien(SearchRequest request)
        {
            var result = new AppResponse<SearchNhanVienRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _NhanVienRepository.CountRecordsByPredicate(query);

                var users = _NhanVienRepository.FindByPredicate(query)
                    .Include(x => x.BoPhan) .Include(x => x.ChucVu) .Select(x => new NhanVienDto
                {
                    Id = x.Id,
                    Ten = x.Ten,
                    ChucVuId = x.ChucVuId,
                    BoPhanId = x.BoPhanId,
                    CapBat = x.CapBat,
                    MucLuong = x.MucLuong,
                    HeSo = x.HeSo,
                    TenChucVu = x.ChucVu.TenChucVu,
                    TenBoPhan = x.BoPhan.TenBoPhan,

                }).ToList(); 
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<NhanVienDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchNhanVienRespository
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
        private ExpressionStarter<NhanVien> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<NhanVien>(true);
                if(Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "ten":
                                predicate = predicate.And(m => m.Ten.Contains(filter.Value));
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
