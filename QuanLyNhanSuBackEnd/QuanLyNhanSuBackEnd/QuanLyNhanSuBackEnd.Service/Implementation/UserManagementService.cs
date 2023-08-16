using AutoMapper;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Models.Context;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private QuanLyNhanSuBDContext _context;
        private readonly IUserManagementRespository _userManagementRespository ;
        private readonly IMapper _mapper;

        public UserManagementService(IUserManagementRespository userManagementRespository, IMapper mapper,QuanLyNhanSuBDContext context , UserManager<IdentityUser>userManager , RoleManager<IdentityRole> roleManager)
        {
            _userManagementRespository = userManagementRespository;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public AppResponse<List<UserModel>> GetAllUser()
        {
            var result = new AppResponse<List<UserModel>>();
            try
            {
 
            var query = (from user in _context.Users
                         join userRole in _context.UserRoles on user.Id equals userRole.UserId
                         join role in _context.Roles on userRole.RoleId equals role.Id
                         select new UserModel
                         {
                             Id = Guid.Parse(user.Id),
                             UserName = user.UserName,
                             Password = user.PasswordHash,
                             Role = role.Name,
                             Email = user.Email,
                         }).ToList();
            result.IsSuccess = true;
            result.Data = query;
            return result;
            }catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
           
           
        }
        public async Task<AppResponse<string>>ResetPassWordUser(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var user = _context.Users.FirstOrDefault(m => m.Id == Id.ToString());
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, "baodepzai");
                result.IsSuccess = true;
                result.Data = "baodepzai";
                return result;
            }
            catch(Exception ex) 
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }


    }
}
