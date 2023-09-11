using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
    public class TangCaDto:BaseDto
    {
        public int? SoGio { get; set; }
        public DateTime? Ngay { get; set; }
        public string? GioBatDau { get; set; }
        public string? GioKetThuc { get; set; }
        public double? HeSoCa { get; set; }
        public Guid? NguoiXacNhanId { get; set; }
    }
}
