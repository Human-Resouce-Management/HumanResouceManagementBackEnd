﻿using AutoMapper;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
    public class ThoiViecService : IThoiViecService
    {
        private readonly IThoiViecRespository _ThoiViecRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly INhanVienRespository _nhanVienRespository;
        private readonly ITinhLuongRespository _tinhLuongRespository;
        private readonly INhanVienTangCaRespository _nhanVienTangCaRespository;
        private readonly ITangLuongRespository _tangLuongRespository;

        public ThoiViecService(IThoiViecRespository ThoiViecRespository, IMapper mapper, IHttpContextAccessor httpContextAccessor ,
            INhanVienRespository nhanVienRespository, ITinhLuongRespository tinhLuongRespository, INhanVienTangCaRespository nhanVienTangCaRespository, ITangLuongRespository tangLuongRespository)
        {
            _ThoiViecRepository = ThoiViecRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _nhanVienRespository = nhanVienRespository;
            _tinhLuongRespository = tinhLuongRespository;
            _nhanVienTangCaRespository = nhanVienTangCaRespository;
            _tangLuongRespository = tangLuongRespository;
        }

        public AppResponse<ThoiViecDto> CreateThoiViec(ThoiViecDto request)
        {
            var result = new AppResponse<ThoiViecDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }

                var tuyendung = _mapper.Map<ThoiViec>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;
                tuyendung.DaThoiViec = true;
                tuyendung.NgayNghi = DateTime.Now;
               
                var nhanvien = _nhanVienRespository.FindByPredicate(x => x.Id == tuyendung.NhanVienId).FirstOrDefault(x => x.IsDeleted == false);
                if (nhanvien != null)
                {
                    nhanvien.IsDeleted = true;
                    _nhanVienRespository.Edit(nhanvien);
                    var nhanvientangca = _nhanVienTangCaRespository.FindByPredicate(x => x.NhanVienId == nhanvien.Id).Where(m => m.IsDeleted == false).ToList();
                    if (nhanvientangca.Count != 0)
                    {
                        foreach (var i in nhanvientangca)
                        {
                            i.IsDeleted = true;
                            _nhanVienTangCaRespository.Edit(i);
                        }
                    }
                   
                    var tinhluong = _tinhLuongRespository.FindByPredicate(x => x.NhanVienId == nhanvien.Id).FirstOrDefault(x => x.IsDeleted == false);
                    if ( tinhluong != null)
                    {
                        tinhluong.IsDeleted = true;
                        _tinhLuongRespository.Edit(tinhluong);
                    }
                    var Tangluong = _tangLuongRespository.FindByPredicate(x => x.NhanVienId == nhanvien.Id).FirstOrDefault(x => x.IsDeleted == false);
                    if(Tangluong != null)
                    {
                        Tangluong.IsDeleted = true;
                        _tangLuongRespository.Edit(Tangluong);
                    }
                }
               
              
                _ThoiViecRepository.Add(tuyendung);
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

        public AppResponse<string> DeleteThoiViec(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new ThoiViec();
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



        public AppResponse<ThoiViecDto> EditThoiViec(ThoiViecDto tuyendung)
        {
            var result = new AppResponse<ThoiViecDto>();
            try
            {
                var request = new ThoiViec();
                request = _mapper.Map<ThoiViec>(tuyendung);
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

        public AppResponse<List<ThoiViecDto>> GetAllThoiViec()
        {
            var result = new AppResponse<List<ThoiViecDto>>();
            //string userId = "";
            try
            {
                var query = _ThoiViecRepository.GetAll()
                   .Include(n=>n.NhanVien);

                var list = query.Select(m => new ThoiViecDto
                {
                    Id = m.Id,
                    Ten = m.NhanVien.Ten,
                    NgayNghi = m.NgayNghi,
                    DaThoiViec = m.DaThoiViec,
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



        public AppResponse<ThoiViecDto> GetThoiViecId(Guid Id)
        {
            var result = new AppResponse<ThoiViecDto>();
            try
            {
                var tuyendung = _ThoiViecRepository.FindBy(x=>x.Id == Id)
                    .Include(x=>x.NhanVien);
                var data = tuyendung.Select(x=> new ThoiViecDto
                {
                    DaThoiViec = x.DaThoiViec,
                    Id = Id,
                    NgayNghi=x.NgayNghi,
                    NhanVienId = x.NhanVienId,
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




        public async Task<AppResponse<SearchThoiViecRespository>> SearchThoiViec(SearchRequest request)
        {
            var result = new AppResponse<SearchThoiViecRespository>();
            try
            {
                var query =  BuildFilterExpression(request.Filters) ;
                var numOfRecords = _ThoiViecRepository.CountRecordsByPredicate(query);

                var users = _ThoiViecRepository.FindByPredicate(query).Include(x => x.NhanVien).Select(x => new ThoiViecDto
                {
                    Id = x.Id,
                    Ten = x.NhanVien.Ten,
                    NhanVienId = x.NhanVienId,
                    NgayNghi = x.NgayNghi,
                    DaThoiViec = x.DaThoiViec,

                }).ToList();
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList() ;
                var dtoList = _mapper.Map<List<ThoiViecDto>>(UserList);

                
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchThoiViecRespository
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
        //public  IQueryable<ThoiViec> GetByTenNhanVien(IQueryable<ThoiViec> thoiViecs, string tenNhanVien)
        //{
        //    return thoiViecs.Where(m => m.NhanVien.Ten.Contains(tenNhanVien));
        //}
        private ExpressionStarter<ThoiViec> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<ThoiViec>(true);

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
