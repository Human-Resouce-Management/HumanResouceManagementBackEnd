using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class PhuCap :BaseEntity
    {
        public bool? ThuongCoDinh { get; set; }

        public double? SoTien { get; set; }
        [ForeignKey("NhanVien")]
        public Guid NhanVienId { get; set; }
        [ForeignKey("NhanVienId")]
        public NhanVien? NhanVien { get; set; }
       
    }
}
