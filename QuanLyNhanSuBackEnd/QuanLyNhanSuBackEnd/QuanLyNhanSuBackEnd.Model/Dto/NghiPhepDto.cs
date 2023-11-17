using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
    public class NghiPhepDto : BaseDto
    {
        public DateTime? NgayNghi { get; set; }

        public bool? NghiCoLuong { get; set; }
       
        public Guid NhanVienId { get; set; }
         
        public Guid? NguoiXacNhanId { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? SoGioNghi { get; set; }
        public string? ten { get; set; }
    }
}
