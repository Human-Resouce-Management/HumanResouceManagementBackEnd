using MayNghien.Common.Models;
using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
    public class NhanVienTangCaDto : BaseDto
    {
      
        public Guid NhanVienId { get; set; }
        public string Ten { get; set; }
      
        public Guid TangCaId { get; set; }
    
       

    }
}
