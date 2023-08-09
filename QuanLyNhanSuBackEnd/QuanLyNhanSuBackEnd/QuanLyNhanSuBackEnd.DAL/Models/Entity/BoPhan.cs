using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class BoPhan :BaseEntity
    {
        public string? TenBoPhan { get; set; }
        public string? QuanLy { get; set; }
        public Guid? BoPhanChuQuanId { get; set; }

    }
}
