﻿using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Models.Entity
{
    public class TangCa :BaseEntity
    {
        public int? SoGio { get; set; }
        public DateTime? Ngay { get; set; }
        public string? GioBatDau { get; set; }
        public string? GioKetThuc { get; set; }
        public double? HeSoCa {  get; set; }


    }
}
