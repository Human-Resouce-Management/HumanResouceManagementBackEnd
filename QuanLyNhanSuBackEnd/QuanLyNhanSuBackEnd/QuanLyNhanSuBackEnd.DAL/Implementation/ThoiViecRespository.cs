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
    public class ThoiViecRespository : GenericRepository<ThoiViec, QuanLyNhanSuBDContext>, IThoiViecRespository
    {
        public ThoiViecRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<ThoiViec, bool>> predicate)
        {
            return _context.ThoiViec.Where(predicate).Count();
        }
        public IQueryable<ThoiViec> FindByPredicate(Expression<Func<ThoiViec, bool>> predicate)
        {
            return _context.ThoiViec.Where(predicate).AsQueryable();
        }
    }
}
