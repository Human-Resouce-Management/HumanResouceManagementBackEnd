using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Model.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Contract
{
    public interface IUserManagementService
    {
     public   AppResponse<List<UserModel>> GetAllUser();
      public  Task<AppResponse<string>>ResetPassWordUser(Guid Id);
        public Task<AppResponse<string>> CreateUser(UserModel model);
        public Task<AppResponse<string>> DeleteUser(string id);
        public Task<AppResponse<string>> EditUser(UserModel model);
        public Task<AppResponse<SearchUserResponse>> Search(SearchRequest request);
        public Task<AppResponse<UserModel>> GetUser(string id);
    }
}
