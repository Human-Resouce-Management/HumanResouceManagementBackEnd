﻿using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Contract
{
    public interface ILoginService
    {
        Task<AppResponse<string>> AuthenticateUser(UserModel user);
        Task<AppResponse<string>> CreateUser(UserModel user);

    }
}
