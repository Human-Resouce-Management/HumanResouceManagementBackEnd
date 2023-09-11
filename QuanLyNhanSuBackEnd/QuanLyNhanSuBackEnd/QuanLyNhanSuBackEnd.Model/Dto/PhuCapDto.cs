using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
    public class PhuCapDto : BaseDto
    {
        public bool? ThuongCoDinh { get; set; }

        public double? SoTien { get; set; }
      
        public Guid NhanVienId { get; set; }
       
       public string? ten { get; set; }
    }
}
