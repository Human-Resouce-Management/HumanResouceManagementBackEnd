using Maynghien.Common.Repository;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Models.Context;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Implementation
{
    public class TinhLuongRespository : GenericRepository<TinhLuong, QuanLyNhanSuBDContext>, ITinhLuongRespository
    {
        public TinhLuongRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<TinhLuong, bool>> predicate)
        {
            return _context.TinhLuong.Where(predicate).Count();
        }
        public IQueryable<TinhLuong> FindByPredicate(Expression<Func<TinhLuong, bool>> predicate)
        {
            return _context.TinhLuong.Where(predicate).AsQueryable();
        }
    }
}
