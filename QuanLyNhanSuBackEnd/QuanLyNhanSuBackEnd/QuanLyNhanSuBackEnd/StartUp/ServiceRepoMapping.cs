using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Implementation;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;
using QuanLyNhanSuBackEnd.Service.Implementation;
using static QuanLyNhanSuBackEnd.Service.Implementation.LoginService;

namespace QuanLyNhanSuBackEnd.API.StartUp
{
    public class ServiceRepoMapping
    {
        public ServiceRepoMapping() { }

        public void Mapping(WebApplicationBuilder builder)
        {
            #region Service Mapping
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<ITuyenDungService, TuyenDungService>();
            builder.Services.AddScoped(typeof(ITuyenDungRespository), typeof(TuyenDungRespository));
            builder.Services.AddScoped<IUserManagementService, UserManagementService>();
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddScoped<ITangCaService, TangCaService>();
            builder.Services.AddScoped(typeof(ITangCaRespository), typeof(TangCaRespository));

            builder.Services.AddScoped<IChucVuService, ChucVuService>();
            builder.Services.AddScoped(typeof(IChucVuRespository), typeof(ChucVuRespository));

            builder.Services.AddScoped<IBoPhanService, BoPhanService>();
            builder.Services.AddScoped(typeof(IBoPhanRespository), typeof(BoPhanRespository));

            builder.Services.AddScoped<INhanVienService, NhanVienService>();
            builder.Services.AddScoped(typeof(INhanVienRespository), typeof(NhanVienRespository));

            builder.Services.AddScoped<IThoiViecService, ThoiViecService>();
            builder.Services.AddScoped(typeof(IThoiViecRespository), typeof(ThoiViecRespository));

            builder.Services.AddScoped<INghiPhepService, NghiPhepService>();
            builder.Services.AddScoped(typeof(INghiPhepRespository), typeof(NghiPhepRespository));


            builder.Services.AddScoped<ITangLuongService, TangLuongService>();
            builder.Services.AddScoped(typeof(ITangLuongRespository), typeof(TangLuongRespository));

            builder.Services.AddScoped<IPhuCapService, PhuCapService>();
            builder.Services.AddScoped(typeof(IPhuCapRespository), typeof(PhuCapRespository));

            builder.Services.AddScoped<ITinhLuongService, TinhLuongService>();
            builder.Services.AddScoped(typeof(ITinhLuongRespository), typeof(TinhLuongRespository));

             builder.Services.AddScoped(typeof(SearchRequest));

            #endregion Service Mapping
            #region Repository Mapping
            builder.Services.AddScoped<ITuyenDungService, TuyenDungService>();
            builder.Services.AddScoped<ITangCaService, TangCaService>();
            builder.Services.AddScoped<IChucVuService, ChucVuService>();
            builder.Services.AddScoped<IBoPhanService, BoPhanService>();
            builder.Services.AddScoped<INhanVienService, NhanVienService>();
            builder.Services.AddScoped<IThoiViecService, ThoiViecService>();
            builder.Services.AddScoped<INghiPhepService, NghiPhepService>();
            builder.Services.AddScoped<ITangLuongService, TangLuongService>();
            builder.Services.AddScoped<IPhuCapService, PhuCapService>();
            builder.Services.AddScoped<ITinhLuongService, TinhLuongService>();
            #endregion Repository Mapping
        }

    }
}
