﻿using AutoMapper;
using MayNghien.Models.Response.Base;
using QuanLyNhanSuBackEnd.DAL.Contract;
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
   
        public class NhanVienTangCaService : INhanVienTangCaService
    {
            private readonly INhanVienTangCaRespository _NhanVienTangCaRepository;
            private readonly IMapper _mapper;

            public NhanVienTangCaService(INhanVienTangCaRespository NhanVienTangCaRepository, IMapper mapper)
            {
            _NhanVienTangCaRepository = NhanVienTangCaRepository;
                _mapper = mapper;
            }

            public AppResponse<NhanVienTangCaDto> CreateNhanVienTangCa(NhanVienTangCaDto request)
            {
                var result = new AppResponse<NhanVienTangCaDto>();
                try
                {
                    var tuyendung = new NhanVienTangCa();
                    tuyendung = _mapper.Map<NhanVienTangCa>(request);
                    tuyendung.Id = Guid.NewGuid();
           
                   _NhanVienTangCaRepository.Add(tuyendung);
     
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

            public AppResponse<string> DeleteNhanVienTangCa(Guid Id)
            {
                var result = new AppResponse<string>();
                try
                {
                    var tuyendung = new NhanVienTangCa();
                    tuyendung = _NhanVienTangCaRepository.Get(Id);
                    tuyendung.IsDeleted = true;

                _NhanVienTangCaRepository.Edit(tuyendung);

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



            public AppResponse<NhanVienTangCaDto> EditNhanVienTangCa(NhanVienTangCaDto tuyendung)
            {
                var result = new AppResponse<NhanVienTangCaDto>();
                try
                {
                    var request = new NhanVienTangCa();
                    request = _mapper.Map<NhanVienTangCa>(tuyendung);
                _NhanVienTangCaRepository.Edit(request);

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

            public AppResponse<List<NhanVienTangCaDto>> GetAllNhanVienTangCa()
            {
                var result = new AppResponse<List<NhanVienTangCaDto>>();
                //string userId = "";
                try
                {
                    var query = _NhanVienTangCaRepository.GetAll()
                    .Include(n => n.NhanVien)
                    .Include(n => n.TangCa)
                    ;
                    var list = query.Select(m => new NhanVienTangCaDto
                    {
                        Id = m.Id,
                        Ten = m.NhanVien.Ten,
                        SoGio = m.TangCa.SoGio,
                        NhanVienId = m.NhanVienId,
                        TangCaId = m.TangCaId,
                      
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



            public AppResponse<NhanVienTangCaDto> GetNhanVienTangCaId(Guid Id)
            {
                var result = new AppResponse<NhanVienTangCaDto>();
                try
                {
                    var tuyendung = _NhanVienTangCaRepository.Get(Id);
                    var data = _mapper.Map<NhanVienTangCaDto>(tuyendung);
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
