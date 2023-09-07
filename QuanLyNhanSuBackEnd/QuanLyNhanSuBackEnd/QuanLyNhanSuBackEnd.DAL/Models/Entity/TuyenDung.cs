using MayNghien.Common.Models.Entity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetManBackEnd.DAL.Models.Entity;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class TuyenDung :BaseAccountEntity
    {
        public string? Ten { get; set; } 

        public string? LienHe { get; set; }

        public string? ViTriUngTuyen { get; set; }

        public bool? KetQua { get; set; }

    }
}
