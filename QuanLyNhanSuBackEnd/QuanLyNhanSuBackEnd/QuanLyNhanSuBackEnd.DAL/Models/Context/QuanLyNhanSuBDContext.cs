using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MayNghien.Common.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;

namespace QuanLyNhanSuBackEnd.DAL.Models.Context
{
    public class QuanLyNhanSuBDContext :BaseContext
    {
        public QuanLyNhanSuBDContext()
        {

        }
        public QuanLyNhanSuBDContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BoPhan> BoPhan { get; set; }
        public DbSet<ChucVu> ChucVu { get; set; }
        public DbSet<NghiPhep> NghiPhep { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<NhanVienTangCa> NhanVienTangCa { get; set; }
        public DbSet<PhuCap> PhuCap { get; set; }
        public DbSet<TangCa> TangCa { get; set; }
        public DbSet<TangLuong> TangLuong { get; set; }
        public DbSet<ThoiViec> ThoiViecs { get; set; }
        public DbSet<TinhLuong> TinhLuong { get; set; }
        public DbSet<TuyenDung> tuyenDung { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var appSetting = JsonConvert.DeserializeObject<AppSetting>(File.ReadAllText("appsettings.json"));
                optionsBuilder.UseSqlServer(appSetting.ConnectionString);
            }


        }


    }
}
