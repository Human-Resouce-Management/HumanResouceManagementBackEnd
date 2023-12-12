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
    public class TangLuongService : ITangLuongService
    {
        private readonly ITangLuongRespository _TangLuongRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ITinhLuongRespository _TinhLuongRepository;

        public TangLuongService(ITangLuongRespository TangLuongRespository, IMapper mapper , 
            IHttpContextAccessor httpContextAccessor , ITinhLuongRespository tinhLuongRespository)
        {
            _TangLuongRepository = TangLuongRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _TinhLuongRepository = tinhLuongRespository;
        }

        public AppResponse<TangLuongDto> CreateTangLuong(TangLuongDto request)
        {
            var result = new AppResponse<TangLuongDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tangLuong = _mapper.Map<TangLuong>(request);
                tangLuong.Id = Guid.NewGuid();
                var nhanvien = _TinhLuongRepository.FindByPredicate(x => x.NhanVienId == request.NhanVienId).First();
                tangLuong.HeSoCu = nhanvien.HeSoLuong.Value;
                tangLuong.CreatedBy = UserName;
                tangLuong.NgayCapNhat = DateTime.Now;
                nhanvien.HeSoLuong = request.HeSoMoi;
                _TinhLuongRepository.Edit(nhanvien);
                _TangLuongRepository.Add(tangLuong);     
                request.Id = tangLuong.Id;
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

        public AppResponse<string> DeleteTangLuong(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tangLuong = new TangLuong();
                tangLuong = _TangLuongRepository.Get(Id);
                tangLuong.IsDeleted = true;

                _TangLuongRepository.Edit(tangLuong);

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



        public AppResponse<TangLuongDto> EditTangLuong(TangLuongDto request)
        {
            var result = new AppResponse<TangLuongDto>();
            try
            {
                var tangLuong = new TangLuong();
                tangLuong = _mapper.Map<TangLuong>(request);
                var nhanvien = _TinhLuongRepository.FindByPredicate(x => x.NhanVienId == request.NhanVienId).First();
                tangLuong.HeSoCu = nhanvien.HeSoLuong.Value;
                nhanvien.HeSoLuong = request.HeSoMoi;
                _TinhLuongRepository.Edit(nhanvien);
                _TangLuongRepository.Edit(tangLuong);
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

        public AppResponse<List<TangLuongDto>> GetAllTangLuong()
        {
            var result = new AppResponse<List<TangLuongDto>>();
            //string userId = "";
            try
            {
                var query = _TangLuongRepository.GetAll()
                   .Include(n => n.NhanVien);

                var list = query.Select(m => new TangLuongDto
                {
                    Id = m.Id,
                   ten = m.NhanVien.Ten,
                   NhanVienId = m.NhanVienId,
                  NgayCapNhat = m.NgayCapNhat,
                  SoTien = m.SoTien,
                  HeSoCu = m.HeSoCu,
                  HeSoMoi = m.HeSoMoi,
                  NgayKetThuc = m.NgayKetThuc,

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



        public AppResponse<TangLuongDto> GetTangLuongId(Guid Id)
        {
            var result = new AppResponse<TangLuongDto>();
            try
            {
                var query = _TangLuongRepository.FindBy(x=>x.Id == Id)
                    .Include(x=>x.NhanVien);
                var data = query.Select(x => new TangLuongDto
                {
                    HeSoCu = x.HeSoCu,
                    HeSoMoi = x.HeSoMoi,
                    Id = Id,
                    NgayCapNhat = x.NgayCapNhat,
                    NgayKetThuc = x.NgayKetThuc,
                    NhanVienId = x.NhanVienId,
                    SoTien = x.SoTien,
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

        public async Task<AppResponse<SearchTangLuongRespository>> SearchTangLuong(SearchRequest request)
        {
            var result = new AppResponse<SearchTangLuongRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _TangLuongRepository.CountRecordsByPredicate(query) ;

                var users = _TangLuongRepository.FindByPredicate(query).Include(x => x.NhanVien) /*.Include(x => x.NgayCapNhat)*/ .Select(x => new TangLuongDto
                {
                    Id = x.Id,
                    ten = x.NhanVien.Ten,
                    NgayCapNhat = x.NgayCapNhat,
                    NgayKetThuc = x.NgayKetThuc,
                    HeSoCu = x.HeSoCu,
                    HeSoMoi = x.HeSoMoi,
                    SoTien = x.SoTien,
                    NhanVienId = x.NhanVienId,

                }).ToList() ;
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<TangLuongDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchTangLuongRespository
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
        private ExpressionStarter<TangLuong> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<TangLuong>(true);

                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "ten":

                                predicate = predicate.And(m => m.NhanVien.Ten.Contains(filter.Value));
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
