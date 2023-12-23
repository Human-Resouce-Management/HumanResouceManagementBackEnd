using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Implementation;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Model.Response.User;
using QuanLyNhanSuBackEnd.Service.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
    public class TangCaService : ITangCaService
    {
        private readonly ITangCaRespository _TangCaRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly INhanVienTangCaRespository _NhanVienTangCaRespository;
        private readonly ITinhLuongRespository _tinhLuongRespository;
        private readonly INhanVienRespository _nhanVienRespository;
        private readonly IBoPhanRespository _boPhanRespository;
        private readonly IChucVuRespository _chucVuRespository;

        public TangCaService(ITangCaRespository TangCaRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor , INhanVienTangCaRespository NhanVienTangCaRespository ,
            ITinhLuongRespository tinhLuongRespository , INhanVienRespository nhanVienRespository 
            , IChucVuRespository chucVuRespository , IBoPhanRespository boPhanRespository)
        {
            _TangCaRepository = TangCaRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _NhanVienTangCaRespository = NhanVienTangCaRespository;
            _tinhLuongRespository = tinhLuongRespository;
            _nhanVienRespository = nhanVienRespository;
            _chucVuRespository = chucVuRespository;
            _boPhanRespository = boPhanRespository;
        }

        public  AppResponse<TangCaDto> CreateTangCa(TangCaDto request)
        {
            var result = new AppResponse<TangCaDto>();
            try
            {
                var UserName =  ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                //var nhanVienTangCaList = new List<NhanVienTangCa>();
                
                var tangCa = new TangCa();
              
                tangCa = _mapper.Map<TangCa>(request);
                tangCa.Id = Guid.NewGuid();
                tangCa.CreatedBy = UserName;
                tangCa.Ngay = DateTime.Now;

                int soGio = int.Parse(tangCa.GioKetThuc) - int.Parse(tangCa.GioBatDau);
                tangCa.SoGio = soGio;
     
                _TangCaRepository.Add(tangCa);
               // request.TangCaList.ForEach( item =>
               // {
                    
                    

               //     var nhanVienTangCa = new NhanVienTangCa()
               //     {
               //         Id = Guid.NewGuid(),
               //         TangCaId =  tangCa.Id,                 
               //         CreatedBy = UserName ,
               //         NhanVienId = item.NhanVienId,   
               //         CreatedOn = DateTime.Now,
               //         Modifiedby = null,
               //         ModifiedOn = null,
               //         IsDeleted = false,
                       
               //     };
               //     nhanVienTangCaList.Add(nhanVienTangCa);
               // } );
          
               //_NhanVienTangCaRespository.AddRange(nhanVienTangCaList);
                request.Id = tangCa.Id;
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

        public AppResponse<string> DeleteTangCa(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tangCa = new TangCa();
                tangCa = _TangCaRepository.Get(Id);
                tangCa.IsDeleted = true;

                _TangCaRepository.Edit(tangCa);

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

        public static double TinhLuong2(TinhLuong thongKe)
        {
            // Lấy số lượng sản phẩm


            // Tính lương cơ bản
            double luongCoBan = (double)(thongKe.MucLuong * thongKe.HeSoLuong);

            // Tính tiền lương
            double tienLuong = (double)(luongCoBan - thongKe.CacKhoangTru + thongKe.CacKhoangThem);

            return tienLuong;
        }

        public AppResponse<TangCaDto> EditTangCa(TangCaDto request)
        {
            var result = new AppResponse<TangCaDto>();
            try
            {
                var tangCa = new TangCa();
                tangCa = _mapper.Map<TangCa>(request);
          

                var nhanvientangca = _NhanVienTangCaRespository.FindByPredicate(x => x.TangCaId == tangCa.Id).Where(x => x.IsDeleted == false).ToList();
                foreach(var i in nhanvientangca)
                {
                    //double tongLuongMoi = tongLuong + (soGio * heSoCa)
                    var tinhluong = _tinhLuongRespository.FindByPredicate(x => x.NhanVienId == i.NhanVienId).FirstOrDefault(m => m.IsDeleted == false);
                    double tongluongmoi = TinhLuong2(tinhluong) + (tangCa.SoGio.Value * tangCa.HeSoCa.Value);
                    tinhluong.TongLuong = tongluongmoi;
                    _tinhLuongRespository.Edit(tinhluong);
                }
                _TangCaRepository.Edit(tangCa);
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

        public AppResponse<List<TangCaDto>> GetAllTangCa()
        {
            var result = new AppResponse<List<TangCaDto>>();
            //string userId = "";
            try
            {
                var query = _TangCaRepository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new TangCaDto
                {
                    Id = m.Id,
                    SoGio = m.SoGio,
                    Ngay = m.Ngay,
                    GioBatDau = m.GioBatDau,
                    GioKetThuc = m.GioKetThuc
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



        public AppResponse<TangCaDto> GetTangCaId(Guid Id)
        {
            var result = new AppResponse<TangCaDto>();
            try
            {
                var tuyendung = _TangCaRepository.Get(Id);
                var data = _mapper.Map<TangCaDto>(tuyendung);
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
        public async Task<AppResponse<SearchTangCaRespository>> SearchTangCa(SearchRequest request)
        {
            var result = new AppResponse<SearchTangCaRespository>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _TangCaRepository.CountRecordsByPredicate(query);

                var users = _TangCaRepository.FindByPredicate(query).Select(x => new TangCaDto
                {
                    Id = x.Id,
                   SoGio = x.SoGio,
                   Ngay = x.Ngay,
                   GioBatDau = x.GioBatDau,
                   GioKetThuc = x.GioKetThuc,
           
                   HeSoCa = x.HeSoCa,

                }).ToList(); ; ;
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<TangCaDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchTangCaRespository
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
        private ExpressionStarter<TangCa> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<TangCa>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "gioBatDau":
                                predicate = predicate.And(m => m.GioBatDau.Contains(filter.Value));
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

        public async Task<byte[]> ExportToExcel(SearchRequest request)
        {
            var data = await this.SearchTangCa(request);
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SelectedRows");

                int row = 1;

                foreach (var caTangCa in data.Data.Data)
                {
                    worksheet.Cells[row, 1].Value = $"Gio Bắt Đầu: {caTangCa.GioBatDau}";
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 3].Value = $"Gio Kết Thúc: {caTangCa.GioKetThuc}";
                    worksheet.Cells[row, 3].Style.Font.Bold = true;
                    row++;
                    worksheet.Cells[row, 1].Value = "Tên Nhân Viên";
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 2].Value = "Mã Nhân Viên";
                    worksheet.Cells[row, 2].Style.Font.Bold = true;
                    worksheet.Cells[row, 3].Value = "Bộ Phận";
                    worksheet.Cells[row, 3].Style.Font.Bold = true;
                    worksheet.Cells[row, 4].Value = "Chức Vụ";
                    worksheet.Cells[row, 4].Style.Font.Bold = true;
                    worksheet.Cells[row, 5].Value = "Hệ Số Lương";
                    worksheet.Cells[row, 5].Style.Font.Bold = true;
                    // Ghi thông tin nhân viên tăng ca trong ca này
                    var listNhanvientangca = _NhanVienTangCaRespository.FindByPredicate(x => x.TangCaId == caTangCa.Id).Where(x => x.IsDeleted == false).ToList();
                    foreach (var nhanVien in listNhanvientangca)
                    {
                        var nhanviens = _nhanVienRespository.FindByPredicate(x => x.Id == nhanVien.NhanVienId).FirstOrDefault();
                        worksheet.Cells[row + 1, 1].Value = nhanviens.Ten;
                        worksheet.Cells[row + 1, 2].Value = nhanVien.NhanVienId;
                        var bophan = _boPhanRespository.FindByPredicate(x => x.Id == nhanviens.BoPhanId).FirstOrDefault();
                        worksheet.Cells[row + 1, 3].Value = bophan.TenBoPhan;
                        var chucvu = _chucVuRespository.FindByPredicate(x => x.Id == nhanviens.ChucVuId).FirstOrDefault();
                        worksheet.Cells[row + 1, 4].Value = chucvu.TenChucVu;
                        worksheet.Cells[row + 1, 5].Value = nhanviens.HeSo;
      
                        // Các thông tin khác của nhân viên tăng ca (nếu có)
                        row++;
                    }

                    // Khoảng trắng giữa các ca tăng ca
                    row++;
                }


                return package.GetAsByteArray();
            }
        }
    }
}
