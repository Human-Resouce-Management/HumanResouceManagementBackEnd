using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Identity;
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
    public Task<AppResponse<List<UserModel>>> GetAllUser();
        public Task<AppResponse<string>>ResetPassWordUser(string Id);
        public Task<AppResponse<string>> Password(UserModel user);
        public Task<AppResponse<string>> CreateUser(UserModel model);
        public Task<AppResponse<string>> DeleteUser(string id);
        //public Task<AppResponse<string>> EditUser(UserModel model);
        public Task<AppResponse<SearchUserResponse>> Search(SearchRequest request);


           public Task<AppResponse<UserModel>> GetUser(string email);
        public Task<AppResponse<IdentityUser>> GetUserIdentity(string Id);
        //public  Task<AppResponse<IdentityUser>> XacThuc(string? Id);
    }
}
