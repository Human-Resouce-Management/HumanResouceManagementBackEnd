using MayNghien.Common.Models.Entity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class TuyenDung :BaseEntity
    {
        public string? Ten;

        public string? LienHe;

        public string? ViTriUngTuyen;

        public bool? KetQua;

    }
}
