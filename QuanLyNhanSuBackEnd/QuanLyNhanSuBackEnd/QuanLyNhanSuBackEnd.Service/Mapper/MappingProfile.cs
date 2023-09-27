using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using QuanLyNhanSuBackEnd.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap();
            
        }

        public void CreateMap()
        {
            CreateMap<TuyenDung,TuyenDungDto>().ReverseMap();
            CreateMap<IdentityUser, UserModel>().ReverseMap();
            CreateMap<TangCa, TangCaDto>().ReverseMap();
            CreateMap<ChucVu, ChucVuDto>().ReverseMap();
            CreateMap<BoPhan, BoPhanDto>().ReverseMap();
            CreateMap<NhanVien, NhanVienDto>().ReverseMap();
            CreateMap<ThoiViec, ThoiViecDto>().ReverseMap();
            CreateMap<NghiPhep, NghiPhepDto>().ReverseMap();
            CreateMap<NhanVienTangCa, NhanVienTangCaDto>().ReverseMap();
            CreateMap<TangLuong, TangLuongDto>().ReverseMap();
            CreateMap<PhuCap, PhuCapDto>().ReverseMap();
            CreateMap<TinhLuong, TinhLuongDto>().ReverseMap();
            CreateMap<ThoiViec, ThoiViecDto>();
        }
       
    }
}
