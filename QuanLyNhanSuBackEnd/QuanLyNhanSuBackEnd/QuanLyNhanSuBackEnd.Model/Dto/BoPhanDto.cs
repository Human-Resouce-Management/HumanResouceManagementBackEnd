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
    public class BoPhanDto : BaseDto
    {
        public string? TenBoPhan { get; set; }
        public string? QuanLy { get; set; }
      
        //public Guid? BoPhanChuQuanId { get; set; }
   
    }
}
