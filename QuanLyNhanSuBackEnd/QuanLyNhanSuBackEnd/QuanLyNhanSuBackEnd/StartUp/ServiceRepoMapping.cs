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
            builder.Services.AddScoped<ILoginService, LoginService>();
        }
    }
}
