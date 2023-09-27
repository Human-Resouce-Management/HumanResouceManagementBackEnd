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
    public class TangLuongRespository : GenericRepository<TangLuong, QuanLyNhanSuBDContext>, ITangLuongRespository
    {
        public TangLuongRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<TangLuong, bool>> predicate)
        {
            return _context.TangLuong.Where(predicate).Count();
        }
        public IQueryable<TangLuong> FindByPredicate(Expression<Func<TangLuong, bool>> predicate)
        {
            return _context.TangLuong.Where(predicate).AsQueryable();
        }
    }
}
