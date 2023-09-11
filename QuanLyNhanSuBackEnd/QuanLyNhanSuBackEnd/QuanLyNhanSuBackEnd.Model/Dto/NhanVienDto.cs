using MayNghien.Common.Models;
using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
    public class NhanVienDto : BaseDto
    {
        public string? Ten { get; set; }
        
        public Guid BoPhanId { get; set; }       
     
        public Guid ChucVuId { get; set; }
          
        public int? CapBat { get; set; }

        public double? MucLuong { get; set; }

        public int? HeSo { get; set; }

        public string? TenBoPhan { get; set; }

        public string? TenChucVu { get; set; }
    }
}
