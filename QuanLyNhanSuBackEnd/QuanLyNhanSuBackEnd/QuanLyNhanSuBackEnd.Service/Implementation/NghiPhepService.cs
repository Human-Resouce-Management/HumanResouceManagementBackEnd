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
    public class NghiPhepService : INghiPhepService
    {
        private readonly INghiPhepRespository _NghiPhepRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public NghiPhepService(INghiPhepRespository NghiPhepRespository, IMapper mapper,  IHttpContextAccessor httpContextAccessor)
        {
            _NghiPhepRepository = NghiPhepRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<NghiPhepDto> CreateNghiPhep(NghiPhepDto request)
        {
            var result = new AppResponse<NghiPhepDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var nghiPhep = _mapper.Map<NghiPhep>(request);
                nghiPhep.Id = Guid.NewGuid();
                // tuyendung.BoPhanId = Guid.NewGuid();
                //tuyendung.ChucVuId = Guid.NewGuid();
                nghiPhep.CreatedBy = UserName;
                _NghiPhepRepository.Add(nghiPhep);
                //request.ChucVuId = tuyendung.ChucVuId;
                //request.BoPhanId = tuyendung.BoPhanId;
                request.Id = nghiPhep.Id;
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

        public AppResponse<string> DeleteNghiPhep(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var nghiPhep = new NghiPhep();
                nghiPhep = _NghiPhepRepository.Get(Id);
                nghiPhep.IsDeleted = true;

                _NghiPhepRepository.Edit(nghiPhep);

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



        public AppResponse<NghiPhepDto> EditNghiPhep(NghiPhepDto request)
        {
            var result = new AppResponse<NghiPhepDto>();
            try
            {
                var nghiPhep = new NghiPhep();
                nghiPhep = _mapper.Map<NghiPhep>(request);
                _NghiPhepRepository.Edit(nghiPhep);

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

        public AppResponse<List<NghiPhepDto>> GetAllNghiPhep()
        {
            var result = new AppResponse<List<NghiPhepDto>>();
            //string userId = "";
            try
            {
                var query = _NghiPhepRepository.GetAll()
                   .Include(n => n.NhanVien);

                var list = query.Select(m => new NghiPhepDto
                {
                    Id = m.Id,
                   ten = m.NhanVien.Ten,
                   NhanVienId = m.NhanVienId,
                   NghiCoLuong = m.NghiCoLuong,
                   NgayKetThuc = m.NgayKetThuc,
                   NgayNghi = m.NgayNghi,
                   SoGioNghi = m.SoGioNghi,
                   NguoiXacNhanId = m.NguoiXacNhanId,

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



        public AppResponse<NghiPhepDto> GetNghiPhepId(Guid Id)
        {
            var result = new AppResponse<NghiPhepDto>();
            try
            {
                var query = _NghiPhepRepository.FindBy(x => x.Id == Id).Include(x=>x.NhanVien);
                var data =  query.Select(x=>new NghiPhepDto
                {
                    Id = x.Id,
                    NgayKetThuc = x.NgayKetThuc,
                    NgayNghi = x.NgayNghi,
                    NghiCoLuong = x.NghiCoLuong,
                    NguoiXacNhanId =x.NguoiXacNhanId,
                    NhanVienId = x.NhanVienId,
                    SoGioNghi = x.SoGioNghi,
                    ten = x.NhanVien.Ten
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


        public async Task<AppResponse<SearchNghiPhepRespository>> SearchNghiPhep(SearchRequest request)
        {
            var result = new AppResponse<SearchNghiPhepRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _NghiPhepRepository.CountRecordsByPredicate(query);

                var users = _NghiPhepRepository.FindByPredicate(query).Include(x => x.NhanVien).Select(x => new NghiPhepDto
                {
                    Id = x.Id,
                    ten = x.NhanVien.Ten,
                    NgayNghi = x.NgayNghi,
                    NguoiXacNhanId = x.NguoiXacNhanId,
                    NgayKetThuc = x.NgayKetThuc,
                    SoGioNghi = x.SoGioNghi,
                    NghiCoLuong = x.NghiCoLuong,
                    NhanVienId = x.NhanVienId,

                }).ToList(); 
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<NghiPhepDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchNghiPhepRespository
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
        private ExpressionStarter<NghiPhep> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<NghiPhep>(true);

                foreach (var filter in Filters)
                {
                    switch (filter.FieldName)
                    {
                        case "TenNhanVienNghiPhep":
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
