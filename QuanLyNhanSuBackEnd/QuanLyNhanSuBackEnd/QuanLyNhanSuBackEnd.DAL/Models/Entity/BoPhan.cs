using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class BoPhan :BaseEntity
    {
        public string? TenBoPhan { get; set; }
        public string? QuanLy { get; set; }

        //[ForeignKey("BoPhan")]
        //public Guid? BoPhanChuQuanId { get; set; }
        //[ForeignKey("BoPhanChuQuanId")]
        //public BoPhan? BoPhanChuQuan { get;set; } 
    }
}
