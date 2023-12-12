using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
    public class TangLuongDto : BaseDto
    {
      
        public Guid NhanVienId { get; set; }
        public string? ten { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public double? SoTien { get; set; }
        public double? HeSoCu { get; set; }
        public double? HeSoMoi { get; set; }
        public DateTime? NgayKetThuc { get; set; }

    }
}
