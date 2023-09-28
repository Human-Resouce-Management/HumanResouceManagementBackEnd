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
    public class TinhLuongService : ITinhLuongService
    {
        private readonly ITinhLuongRespository _ThoiViecRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public TinhLuongService(ITinhLuongRespository ThoiViecRespository, IMapper mapper , IHttpContextAccessor httpContextAccessor)
        {
            _ThoiViecRepository = ThoiViecRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<TinhLuongDto> CreateTinhLuong(TinhLuongDto request)
        {
            var result = new AppResponse<TinhLuongDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = _mapper.Map<TinhLuong>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;
                // tuyendung.BoPhanId = Guid.NewGuid();
                //tuyendung.ChucVuId = Guid.NewGuid();
                _ThoiViecRepository.Add(tuyendung);
                //request.ChucVuId = tuyendung.ChucVuId;
                //request.BoPhanId = tuyendung.BoPhanId;
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

        public AppResponse<string> DeleteTinhLuong(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new TinhLuong();
                tuyendung = _ThoiViecRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _ThoiViecRepository.Edit(tuyendung);

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



        public AppResponse<TinhLuongDto> EditTinhLuong(TinhLuongDto tuyendung)
        {
            var result = new AppResponse<TinhLuongDto>();
            try
            {
                var request = new TinhLuong();
                request = _mapper.Map<TinhLuong>(tuyendung);
                _ThoiViecRepository.Edit(request);

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

        public AppResponse<List<TinhLuongDto>> GetAllTinhLuong()
        {
            var result = new AppResponse<List<TinhLuongDto>>();
            //string userId = "";
            try
            {
                var query = _ThoiViecRepository.GetAll()
                   .Include(n=>n.NhanVien);

                var list = query.Select(m => new TinhLuongDto
                {
                    Id = m.Id,
                    ten = m.NhanVien.Ten,
                   SoLuong = m.SoLuong,
                   MucLuong = m.MucLuong,
                   CacKhoangThem = m.CacKhoangThem,
                   CacKhoangTru = m.CacKhoangTru,
                   NhanVienId = m.NhanVienId,
                   
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



        public AppResponse<TinhLuongDto> GetTinhLuongId(Guid Id)
        {
            var result = new AppResponse<TinhLuongDto>();
            try
            {
                var tuyendung = _ThoiViecRepository.FindBy(x=>x.Id == Id).Include(x=>x.NhanVien);
                var data = tuyendung.Select(x => new TinhLuongDto
                {
                    CacKhoangThem = x.CacKhoangThem,
                    CacKhoangTru =x.CacKhoangTru,
                    Id = Id,
                    MucLuong = x.MucLuong,
                    NhanVienId=x.NhanVienId,
                    SoLuong = x.SoLuong,
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


        public async Task<AppResponse<SearchTinhLuongRespository>> SearchTinhLuong(SearchRequest request)
        {
            var result = new AppResponse<SearchTinhLuongRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _ThoiViecRepository.CountRecordsByPredicate(query);

                var users = _ThoiViecRepository.FindByPredicate(query).Include(x => x.NhanVien).Select(x => new TinhLuongDto
                {
                    Id = x.Id,
                    ten = x.NhanVien.Ten,
                   CacKhoangThem = x.CacKhoangThem,
                    NhanVienId = x.NhanVienId,
                    CacKhoangTru = x.CacKhoangTru,
                    MucLuong = x.MucLuong,
                    SoLuong = x.SoLuong,
                    

                }).ToList(); ; ;
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<TinhLuongDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchTinhLuongRespository
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
        private ExpressionStarter<TinhLuong> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<TinhLuong>(true);

                foreach (var filter in Filters)
                {
                    switch (filter.FieldName)
                    {
                        case "TinhLuong":
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
