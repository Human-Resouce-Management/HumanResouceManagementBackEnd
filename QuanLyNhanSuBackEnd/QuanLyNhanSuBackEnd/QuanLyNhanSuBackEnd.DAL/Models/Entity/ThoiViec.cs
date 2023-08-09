using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class ThoiViec :BaseEntity
    {
        [ForeignKey("NhanVien")]
        public Guid NhanVienId { get; set; }
        [ForeignKey("NhanVienId")]
        public NhanVien? NhanVien { get; set; }
        public DateTime? NgayNghi { get; set; }
        public bool? DaThoiViec { get; set;}
    }
}
