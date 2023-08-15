using AutoMapper;
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
            CreateMap2();
        }

        public void CreateMap()
        {
            CreateMap<TuyenDung,QuanLyNhanSuBackEndDto>().ReverseMap();
           

        }
        public void CreateMap2()
        {
            
            CreateMap<NhanVien, UserManagementDto>().ReverseMap();
        }
    }
}
