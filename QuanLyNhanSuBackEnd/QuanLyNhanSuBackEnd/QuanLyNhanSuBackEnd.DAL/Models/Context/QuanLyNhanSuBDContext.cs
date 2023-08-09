using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MayNghien.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSuBackEnd.DAL.Models.Context
{
    public class QuanLyNhanSuBDContext :BaseContext
    {
        public QuanLyNhanSuBDContext()
        {

        }
        public QuanLyNhanSuBDContext(DbContextOptions options) : base(options)
        {

        }


    }
}
