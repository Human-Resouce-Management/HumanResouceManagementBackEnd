using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
    public class ThoiViecDto : BaseDto
    {  
        public Guid NhanVienId { get; set; }
        public DateTime? NgayNghi { get; set; }
        public bool? DaThoiViec { get; set; }
        public string? Ten { get; set; } 
    }
}
