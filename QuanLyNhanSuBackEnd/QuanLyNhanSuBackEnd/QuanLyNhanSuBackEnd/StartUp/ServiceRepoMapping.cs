using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Implementation;
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
            builder.Services.AddScoped<IQuanLyNhanSuService, QuanLyNhanSuService>();
            builder.Services.AddScoped(typeof(IQuanLyNhanSuRespository), typeof(QuanLyNhanSuRespository));
            #endregion Service Mapping
            #region Repository Mapping
            builder.Services.AddScoped<QuanLyNhanSuService, QuanLyNhanSuService>();
            #endregion Repository Mapping
        }
    }
}
