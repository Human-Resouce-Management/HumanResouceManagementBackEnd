using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class TinhLuong :BaseEntity
    {
        //public double? SoLuong { get; set; }
        public double? MucLuong { get; set; }
        public int? CacKhoangTru { get; set; }
        [ForeignKey("NhanVien")]
        public Guid NhanVienId { get; set; }
        [ForeignKey("NhanVienId")]
        public NhanVien? NhanVien { get; set; }
        public int? CacKhoangThem { get; set; }
        public double? HeSoLuong { get; set; }
        public double? TongLuong { get; set; }
        
    }
}
