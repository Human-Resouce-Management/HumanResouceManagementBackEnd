﻿using Maynghien.Common.Repository;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Models.Context;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Implementation
{
    public class TuyenDungRespository : GenericRepository<TuyenDung, QuanLyNhanSuBDContext>, ITuyenDungRespository
    {
        public TuyenDungRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
    }
}