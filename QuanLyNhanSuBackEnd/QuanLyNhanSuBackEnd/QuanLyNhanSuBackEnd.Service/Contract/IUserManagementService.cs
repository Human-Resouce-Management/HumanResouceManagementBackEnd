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
       AppResponse<List<IdentityUser>> GetAllUser();
      Task<AppResponse<string>>ResetPassWordUser(string Id);
        Task<AppResponse<string>> CreateUser(UserModel model);
         Task<AppResponse<string>> DeleteUser(string id);
        //public Task<AppResponse<string>> EditUser(UserModel model);
         Task<AppResponse<SearchUserResponse>> Search(SearchRequest request);
        Task<AppResponse<UserModel>> GetUser(string email);
        Task<AppResponse<IdentityUser>> GetUserIdentity(string Id);
        //public  Task<AppResponse<IdentityUser>> XacThuc(string? Id);
    }
}
