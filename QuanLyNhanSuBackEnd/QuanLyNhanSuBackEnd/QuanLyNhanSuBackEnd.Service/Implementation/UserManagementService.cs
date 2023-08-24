using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSuBackEnd.Common.Enum;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Models.Context;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Model.Response.User;
using QuanLyNhanSuBackEnd.Service.Contract;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MayNghien.Common.CommonMessage.AuthResponseMessage;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private QuanLyNhanSuBDContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserManagementService( IMapper mapper,QuanLyNhanSuBDContext context , UserManager<IdentityUser>userManager, RoleManager<IdentityRole> roleManager , IUserRepository userRepository)
        {
           
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
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
                             Id = user.Id,
                             UserName = user.UserName,
                             Password = user.PasswordHash,
                             Role = role.Name,
                             Email = user.Email,
                             LockoutEnabled = user.LockoutEnabled,
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
                var user = _userRepository.FindUser(Id.ToString());
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, "sairoi");
                result.IsSuccess = true;
                result.Data = "sairoi";
                return result;
            }
            catch(Exception ex) 
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }
        public async Task<AppResponse<string>> CreateUser(UserModel user)
        {
           
            var result = new AppResponse<string>();
            try
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    return result.BuildError(ERR_MSG_EmailIsNullOrEmpty);
                }
                var identityUser = await _userManager.FindByNameAsync(user.UserName);
                if (identityUser != null)
                {
                    return result.BuildError(ERR_MSG_UserExisted);
                }
                var newIdentityUser = new IdentityUser { Email = user.Email, UserName = user.Email };
               
                var createResult = await _userManager.CreateAsync(newIdentityUser);
                await _userManager.AddPasswordAsync(newIdentityUser, user.Password);

                newIdentityUser = await _userManager.FindByEmailAsync(user.Email);
                return result.BuildResult(INFO_MSG_UserCreated);
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }


        public async Task<AppResponse<string>> DeleteUser(string id)
        {
            var result = new AppResponse<string>();
            try
            {

                IdentityUser identityUser = new IdentityUser();

                identityUser = await _userManager.FindByIdAsync(id);
                if (identityUser != null)
                {
                    if (await _userManager.IsInRoleAsync(identityUser, "tenant"))
                    {
                       
                      var user = _context.Users.FirstOrDefault(x => x.Id == id);
                        _context.Users.Remove(user);    
                    }

                }
                return result.BuildResult(INFO_MSG_UserDeleted);
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }

        public async Task<AppResponse<string>> EditUser(UserModel model)
        {
            var result = new AppResponse<string>();
            if (model.Id == null)
            {
                return result.BuildError(ERR_MSG_EmailIsNullOrEmpty);
            }
            try
            {
                var identityUser = await _userManager.FindByIdAsync(model.Id);
               
                if (identityUser != null)
                {
                    
                    model.Id = identityUser.Id;
                    model.UserName= identityUser.UserName ;
                    model.Email= identityUser.Email ;
                    model.LockoutEnabled  =  identityUser.LockoutEnabled ;
                  
                    
                }
                return result.BuildResult("ok");
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }

        public async Task<AppResponse<UserModel>> GetUser(string id)
        {
            var result = new AppResponse<UserModel>();
            try
            {
                List<Filter> Filters = new List<Filter>();
                var query = BuildFilterExpression(Filters);

                var identityUser = _userRepository.FindById(id);

                if (identityUser == null)
                {
                    return result.BuildError("User not found");
                }
                var dtouser = _mapper.Map<UserModel>(identityUser);

                dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();

                return result.BuildResult(dtouser);
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }

        private ExpressionStarter<IdentityUser> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<IdentityUser>(true);

                foreach (var filter in Filters)
                {
                    switch (filter.FieldName)
                    {
                        case "Email":
                            predicate = predicate.And(m => m.Email.Equals(filter.Value));
                            break;

                        default:
                            break;
                    }
                }
                return predicate;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AppResponse<SearchUserResponse>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchUserResponse>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _userRepository.CountRecordsByPredicate(query);

                var users = _userRepository.FindByPredicate(query);
                int pageIndex = request.PageSize ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<UserModel>>(UserList);
                if (dtoList != null && dtoList.Count > 0)
                {
                    for (int i = 0; i < UserList.Count; i++)
                    {
                        var dtouser = dtoList[i];
                        var identityUser = UserList[i];
                        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                    }
                }
                var searchUserResult = new SearchUserResponse
                {
                    TotalRows = numOfRecords,
                    TotalPages = SearchHelper.CalculateNumOfPages(numOfRecords, pageSize),
                    CurrentPage = pageIndex,
                    Data = dtoList,
                };

                return result.BuildResult(searchUserResult);

            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }
    }
}
