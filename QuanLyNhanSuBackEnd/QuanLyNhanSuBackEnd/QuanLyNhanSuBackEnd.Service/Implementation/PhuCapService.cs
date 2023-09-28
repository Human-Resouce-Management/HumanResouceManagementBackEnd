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
    public class PhuCapService : IPhuCapService
    {
        private readonly IPhuCapRespository _PhuCapRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public PhuCapService(IPhuCapRespository PhuCapRepository, IMapper mapper,  IHttpContextAccessor httpContextAccessor)
        {
            _PhuCapRepository = PhuCapRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<PhuCapDto> CreatePhuCap(PhuCapDto request)
        {
            var result = new AppResponse<PhuCapDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var phuCap = new PhuCap();
                phuCap = _mapper.Map<PhuCap>(request);
                phuCap.Id = Guid.NewGuid();
                phuCap.CreatedBy = UserName;
                _PhuCapRepository.Add(phuCap);

                request.Id = phuCap.Id;
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

        public AppResponse<string> DeletePhuCap(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var phuCap = new PhuCap();
                phuCap = _PhuCapRepository.Get(Id);
                phuCap.IsDeleted = true;

                _PhuCapRepository.Edit(phuCap);

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



        public AppResponse<PhuCapDto> EditPhuCap(PhuCapDto request)
        {
            var result = new AppResponse<PhuCapDto>();
            try
            {
                var phuCap = new PhuCap();
                phuCap = _mapper.Map<PhuCap>(request);
                _PhuCapRepository.Edit(phuCap);

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

        public AppResponse<List<PhuCapDto>> GetAllPhuCap()
        {
            var result = new AppResponse<List<PhuCapDto>>();
            //string userId = "";
            try
            {
                var query = _PhuCapRepository.GetAll()
                    .Include(n => n.NhanVien)
                    ;
                var list = query.Select(m => new PhuCapDto
                {
                    Id = m.Id,
                  ThuongCoDinh = m.ThuongCoDinh,
                  SoTien = m.SoTien,
                  NhanVienId = m.NhanVienId,
                  ten = m.NhanVien.Ten,
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



        public AppResponse<PhuCapDto> GetPhuCapId(Guid Id)
        {
            var result = new AppResponse<PhuCapDto>();
            try
            {
                var query = _PhuCapRepository.FindBy(x => x.Id == Id).Include(x => x.NhanVien);
                var data = query.Select(x=> new PhuCapDto
                {
                    Id=x.Id,
                    NhanVienId=x.NhanVienId,
                    SoTien =x.SoTien,
                    ten = x.NhanVien.Ten,
                    ThuongCoDinh = x.ThuongCoDinh
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



        public async Task<AppResponse<SearchPhuCapRespository>> SearchPhuCap(SearchRequest request)
        {
            var result = new AppResponse<SearchPhuCapRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _PhuCapRepository.CountRecordsByPredicate(query);

                var users = _PhuCapRepository.FindByPredicate(query).Include(x => x.NhanVien).Select(x => new PhuCapDto
                {
                    Id = x.Id,
                    ten = x.NhanVien.Ten,
                   ThuongCoDinh = x.ThuongCoDinh,
                    NhanVienId = x.NhanVienId,
                    SoTien = x.SoTien,

                }).ToList(); ;
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<PhuCapDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchPhuCapRespository
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
        private ExpressionStarter<PhuCap> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<PhuCap>(true);

                foreach (var filter in Filters)
                {
                    switch (filter.FieldName)
                    {
                        case "TenPhuCap":
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
