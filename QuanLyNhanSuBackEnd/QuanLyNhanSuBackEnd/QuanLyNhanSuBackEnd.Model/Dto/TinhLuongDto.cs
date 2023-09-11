using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
   public class TinhLuongDto : BaseDto
    {
        public double? SoLuong { get; set; }
        public double? MucLuong { get; set; }
        public int? CacKhoangTru { get; set; }
        public Guid NhanVienId { get; set; }
        public string? ten { get; set; }
        public int? CacKhoangThem { get; set; }

    }
}
