﻿using AutoMapper;
using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Implementation;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using QuanLyNhanSuBackEnd.Model.Dto;
using QuanLyNhanSuBackEnd.Service.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.Service.Implementation
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRespository _NhanVienRepository;
        private readonly IMapper _mapper;

        public NhanVienService(INhanVienRespository NhanVienRespository, IMapper mapper)
        {
            _NhanVienRepository = NhanVienRespository;
            _mapper = mapper;
        }

        public AppResponse<NhanVienDto> CreateNhanVien(NhanVienDto request)
        {
            var result = new AppResponse<NhanVienDto>();
            try
            {
                var tuyendung = _mapper.Map<NhanVien>(request);
                tuyendung.Id = Guid.NewGuid();
                _NhanVienRepository.Add(tuyendung);

                request.Id = tuyendung.Id;
                result.IsSuccess = true;
                result.Data = request;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }

        public AppResponse<string> DeleteNhanVien(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new NhanVien();
                tuyendung = _NhanVienRepository.Get(Id);
                tuyendung.IsDeleted = true;

                _NhanVienRepository.Edit(tuyendung);

                result.IsSuccess = true;
                result.Data = "Delete Sucessfuly";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ":" + ex.StackTrace;
                return result;

            }
        }



        public AppResponse<NhanVienDto> EditNhanVien(NhanVienDto tuyendung)
        {
            var result = new AppResponse<NhanVienDto>();
            try
            {
                var request = new NhanVien();
                request = _mapper.Map<NhanVien>(tuyendung);
                _NhanVienRepository.Edit(request);

                result.IsSuccess = true;
                result.Data = tuyendung;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }

        public AppResponse<List<NhanVienDto>> GetAllNhanVien()
        {
            var result = new AppResponse<List<NhanVienDto>>();
            //string userId = "";
            try
            {
                var query = _NhanVienRepository.GetAll()
                    .Include(n=>n.BoPhan)
                    .Include(n=>n.ChucVu);
               
                var list = query.Select(m => new NhanVienDto
                {
                    Id = m.Id,
                   Ten = m.Ten,
                   CapBat = m.CapBat,
                   MucLuong = m.MucLuong,
                   HeSo = m.HeSo,
                   BoPhanId = m.BoPhanId,
                   ChucVuId = m.ChucVuId,
                   TenBoPhan = m.BoPhan.TenBoPhan,
                   TenChucVu = m.ChucVu.TenChucVu
                }).ToList();
                result.IsSuccess = true;
                result.Data = list;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }



        public AppResponse<NhanVienDto> GetNhanVienId(Guid Id)
        {
            var result = new AppResponse<NhanVienDto>();
            try
            {
                var query = _NhanVienRepository.FindBy(x => x.Id == Id)
                    .Include(x => x.ChucVu)
                    .Include(x => x.BoPhan);
                var data = query.Select(x=> new NhanVienDto
                {
                    BoPhanId = x.BoPhanId,
                    CapBat = x.CapBat,
                    ChucVuId =x.ChucVuId,
                    HeSo = x.HeSo,
                    Id = x.Id,
                    MucLuong = x.MucLuong,
                    Ten = x.Ten,
                    TenBoPhan = x.BoPhan.TenBoPhan,
                    TenChucVu = x.ChucVu.TenChucVu
                }).First();
                result.IsSuccess = true;
                result.Data = data;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;

            }
        }
    }
}
