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
    public class TangCaRespository : GenericRepository<TangCa, QuanLyNhanSuBDContext>, ITangCaRespository
    {
        public TangCaRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<TangCa, bool>> predicate)
        {
            return _context.TangCa.Where(predicate).Count();
        }
        public IQueryable<TangCa> FindByPredicate(Expression<Func<TangCa, bool>> predicate)
        {
            return _context.TangCa.Where(predicate).AsQueryable();
        }
    }
}
