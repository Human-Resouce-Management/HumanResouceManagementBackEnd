﻿using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Model.Dto
{
    public class QuanLyNhanSuBackEndDto : BaseDto
    {
        public string? Ten { get; set; }
        public string? LienHe{ get; set; }
        public string? ViTriUngTuyen { get; set; }
        public bool? KetQua { get; set; }
    }
}
