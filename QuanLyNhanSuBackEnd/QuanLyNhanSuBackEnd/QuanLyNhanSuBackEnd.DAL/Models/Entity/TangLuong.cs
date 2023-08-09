using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class TangLuong :BaseEntity
    {
        [ForeignKey("NhanVien")]
        public Guid NhanVienId { get; set; }
        [ForeignKey("NhanVienId")]
        public NhanVien? NhanVien { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public double SoTien { get; set; }
        public double HeSoCu { get; set; }
        public double HeSoMoi { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}
