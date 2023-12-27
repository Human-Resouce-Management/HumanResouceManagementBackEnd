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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
   
        public class NhanVienTangCaService : INhanVienTangCaService
    {
            private readonly INhanVienTangCaRespository _NhanVienTangCaRepository;
            private readonly IMapper _mapper;
            private IHttpContextAccessor _httpContextAccessor;
        private ITinhLuongRespository _tinhLuongRespository;
        private readonly ITangCaRespository _tangCaRespository;
        public NhanVienTangCaService(INhanVienTangCaRespository NhanVienTangCaRepository, IMapper mapper,   IHttpContextAccessor httpContextAccessor,
            ITinhLuongRespository tinhLuongRespository , ITangCaRespository tangCaRespository
            )
            {
            _NhanVienTangCaRepository = NhanVienTangCaRepository;
                _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _tinhLuongRespository = tinhLuongRespository;
            _tangCaRespository = tangCaRespository;
            }

            public AppResponse<NhanVienTangCaDto> CreateNhanVienTangCa(NhanVienTangCaDto request  )
            {
                var result = new AppResponse<NhanVienTangCaDto>();
                try
                {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var nhanviencu = _NhanVienTangCaRepository.FindByPredicate(x => x.NhanVienId == request.NhanVienId).FirstOrDefault(x => x.IsDeleted == false);
                if (nhanviencu != null)
                {
                    return result.BuildError("Nhan Vien Tang Ca da ton tai ");
                }

              
                var nhanVienTangCa = new NhanVienTangCa();
                nhanVienTangCa = _mapper.Map<NhanVienTangCa>(request);
        
                     nhanVienTangCa.Id = Guid.NewGuid();
             
                    nhanVienTangCa.CreatedBy = UserName;
                //nhanVienTangCa.TangCaId = 
                var gettinhluong = _tinhLuongRespository.FindByPredicate(x => x.NhanVienId == request.NhanVienId).FirstOrDefault( x=> x.IsDeleted == false);
               if(gettinhluong != null)
                {
                    double tongLuong = 0.0;
                    if (gettinhluong != null && gettinhluong.TongLuong.HasValue)
                    {
                        tongLuong = gettinhluong.TongLuong.Value;
                    }

                    double heSoCa = 0.0;
               var tangca =     _tangCaRespository.FindByPredicate(x => x.Id == request.TangCaId).FirstOrDefault(x => x.IsDeleted == false);

                        heSoCa = tangca.HeSoCa.Value;
                

                    double soGio = 0.0;
                  
                        soGio = tangca.SoGio.Value;


                    double tongLuongMoi = tongLuong + (soGio * heSoCa);
                    gettinhluong.TongLuong = tongLuongMoi;
                    _tinhLuongRespository.Edit(gettinhluong);
                }
          
               
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
                var tinhluong = _tinhLuongRespository.FindByPredicate(x => x.NhanVienId == nhanVienTangCa.NhanVienId).FirstOrDefault(x => x.IsDeleted == false);
                var Tangca = _tangCaRespository.FindByPredicate(x => x.Id == nhanVienTangCa.TangCaId).FirstOrDefault(x => x.IsDeleted == false);
               double gettinhluong = tinhluong.TongLuong.Value - Tangca.SoGio.Value * Tangca.HeSoCa.Value;
                tinhluong.TongLuong = gettinhluong;
                _NhanVienTangCaRepository.Edit(nhanVienTangCa);
                _tinhLuongRespository.Edit(tinhluong);
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
                    HeSoCa = m.TangCa.HeSoCa,

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
                    var query = _NhanVienTangCaRepository.FindBy(x => x.Id == Id).Include(x=>x.NhanVien).Include(x => x.TangCa);
                    var data = query.Select(x => new NhanVienTangCaDto
                    {
                        Id = x.Id,
                        NhanVienId = x.NhanVienId,
                        TangCaId = x.TangCaId,
                        Ten = x.NhanVien.Ten,
                        HeSoCa = x.TangCa.HeSoCa,
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
              
                var query = BuildFilterExpression(request.Filters );
                var numOfRecords = _NhanVienTangCaRepository.CountRecordsByPredicate(query);

                var users = _NhanVienTangCaRepository.FindByPredicate(query).Include(x => x.NhanVien) .Include(x => x.TangCa).Select(x => new NhanVienTangCaDto
                {
                    Id = x.Id,
                    Ten = x.NhanVien.Ten,
                    TangCaId = x.TangCaId,
                    NhanVienId = x.NhanVienId,
                    HeSoCa = x.TangCa.HeSoCa,

                }).ToList(); ;
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
        private ExpressionStarter<NhanVienTangCa> BuildFilterExpression(IList<Filter> Filters )
        {
            try
            {
                var predicate = PredicateBuilder.New<NhanVienTangCa>(true);
                if (Filters != null)
                
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "ten":
                                predicate = predicate.And(m => m.NhanVien.Ten.Contains(filter.Value));
                                break;
                            case "TangCaId":
                                predicate = predicate.And(m => m.TangCa.Id.Equals(Guid.Parse(filter.Value)));
                                break;
                            default:
                                break;
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

