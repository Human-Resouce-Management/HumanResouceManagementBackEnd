using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;
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
        private readonly INhanVienRespository _nhanVienRespository;
        private readonly ITangLuongRespository _tangLuongRespository;

        public TinhLuongService(ITinhLuongRespository ThoiViecRespository, IMapper mapper , IHttpContextAccessor httpContextAccessor, INhanVienRespository nhanVienRespository, ITangLuongRespository tangLuongRespository)
        {
            _ThoiViecRepository = ThoiViecRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _nhanVienRespository = nhanVienRespository;
            _tangLuongRespository = tangLuongRespository;
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
                var IdNhanVien = _ThoiViecRepository.FindByPredicate(x => x.NhanVienId == request.NhanVienId && !x.IsDeleted).ToList();
                if (IdNhanVien.Any())
                {
                    return result.BuildError("Nhan Vien da ton tai");
                }

                var nhanvien = _nhanVienRespository.FindByPredicate(x => x.Id == request.NhanVienId).FirstOrDefault(x => x.IsDeleted == false);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;
                tuyendung.MucLuong = nhanvien.MucLuong.Value;
                tuyendung.HeSoLuong = nhanvien.HeSo.Value;
                request.MucLuong = nhanvien.MucLuong;
                request.HeSoLuong = nhanvien.HeSo;
                tuyendung.TongLuong =  TinhLuong2(request);
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

        public AppResponse<string> DeleteTinhLuong(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {             
                var tuyendung = _ThoiViecRepository.Get(Id);
                var TangLuongid = _tangLuongRespository.FindByPredicate(x => x.NhanVienId == tuyendung.NhanVienId).FirstOrDefault();
                tuyendung.IsDeleted = true;     
                if(TangLuongid != null)
                {
                    TangLuongid.IsDeleted = true;
                    _tangLuongRespository.Edit(TangLuongid);
                }         
                _ThoiViecRepository.Edit(tuyendung);
                result.BuildResult("Delete Sucessfuly");
            }
            catch (Exception ex)
            {
                result.BuildError(ex.Message);

            }
            return result;
        }



        public AppResponse<TinhLuongDto> EditTinhLuong(TinhLuongDto tuyendung)
        {
            var result = new AppResponse<TinhLuongDto>();
            try
            {
                var TangLuongid = _tangLuongRespository.FindByPredicate(x => x.NhanVienId == tuyendung.NhanVienId).FirstOrDefault();
                var request = new TinhLuong();
                request = _mapper.Map<TinhLuong>(tuyendung);
                if (TangLuongid != null)
                {
                    TangLuongid.HeSoCu = TangLuongid.HeSoMoi;
                    TangLuongid.HeSoMoi = (double)tuyendung.HeSoLuong;
                    _tangLuongRespository.Edit(TangLuongid);
                }
                 request.TongLuong = TinhLuong2(tuyendung);
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
                   .Include(m=>m.NhanVien);

                var list = query.Select(m => new TinhLuongDto
                {
                    Id = m.Id,
                    ten = m.NhanVien.Ten,
              
                   MucLuong = m.MucLuong,
                   CacKhoangThem = m.CacKhoangThem,
                   CacKhoangTru = m.CacKhoangTru,
                   NhanVienId = m.NhanVienId,
                   HeSoLuong = m.HeSoLuong,
                   TongLuong = m.TongLuong,
                   
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
                  
                    ten = x.NhanVien.Ten,
                    HeSoLuong = x.HeSoLuong,
                    TongLuong = x.TongLuong
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
                
                    HeSoLuong = x.HeSoLuong,
                    TongLuong = x.TongLuong,

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
                if (Filters != null)
                {
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
                }
                predicate = predicate.And(m => m.IsDeleted == false);
                return predicate;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static double TinhLuong(TinhLuong thongKe)
        {
            // Lấy số lượng sản phẩm
         

            // Tính lương cơ bản
            double luongCoBan =(double)(thongKe.MucLuong*thongKe.HeSoLuong);

            // Tính tiền lương
            double tienLuong = (double)(luongCoBan - thongKe.CacKhoangTru + thongKe.CacKhoangThem);

            return tienLuong;
        }


        public static double TinhLuong2(TinhLuongDto thongKe)
        {
            // Lấy số lượng sản phẩm


            // Tính lương cơ bản
            double luongCoBan = (double)(thongKe.MucLuong * thongKe.HeSoLuong);

            // Tính tiền lương
            double tienLuong = (double)(luongCoBan - thongKe.CacKhoangTru + thongKe.CacKhoangThem);

            return tienLuong;
        }
        public AppResponse<double> TinhLuongs(Guid id)
         {
            var result = new AppResponse<double>();
            try
            {
                var request = new TinhLuong();
                request = _ThoiViecRepository.Get(id);
                request.TongLuong = TinhLuong(request);
                _ThoiViecRepository.Edit(request);
                var sum = request.TongLuong;
                result.IsSuccess = true;
                result.Data = (double) sum;
                return result;
            }
            catch(Exception ex) 
            {
                return result.BuildError(ex.ToString());
            }
        }


        public async Task<byte[]> ExportToExcel(SearchRequest request)
        {
            var data = await this.SearchTinhLuong(request);
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SelectedRows");
              

                worksheet.Cells[1, 1].Value = "Tên Nhân Viên";
                worksheet.Cells[1, 2].Value = "Mã Nhân Viên";
                worksheet.Cells[1, 3].Value = "Các Khoảng Trừ";
                worksheet.Cells[1, 4].Value = "Các Khoảng Thêm";
                worksheet.Cells[1, 5].Value = "Mức Lương";
                worksheet.Cells[1, 6].Value = "Hệ Số Lương";
                worksheet.Cells[1, 7].Value = "Tổng Lương";
                              

                for (int i = 0; i < data.Data.Data.Count; i++)
                {
                    var dto = data.Data.Data[i];
                    worksheet.Cells[i + 2, 1].Value = dto.ten;
                    worksheet.Cells[i + 2, 2].Value = dto.NhanVienId;
                    worksheet.Cells[i + 2, 3].Value = dto.CacKhoangTru;
                    worksheet.Cells[i + 2, 4].Value = dto.CacKhoangThem;
                    worksheet.Cells[i + 2, 5].Value = dto.MucLuong;
                    worksheet.Cells[i + 2, 6].Value = dto.HeSoLuong;
                    worksheet.Cells[i + 2, 7].Value = dto.TongLuong;
                   

                }


                return package.GetAsByteArray();
            }
        }

    }
}
