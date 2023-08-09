using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class NhanVien :BaseEntity
    {
        public string? Ten { get; set; } 
      

        [ForeignKey("BoPhan")]
        public Guid BoPhanId { get; set; }
        [ForeignKey("BoPhanId")]
        public BoPhan? BoPhan { get; set; }

        [ForeignKey("ChucVu")]
        public Guid ChucVuId {  get; set; }
        [ForeignKey("ChucVuId")]
        public ChucVu? ChucVu { get; set; }

        public int? CapBat { get; set; }

        public double? MucLuong { get; set; }
        public int? HeSo {  get; set; }

    }
}
