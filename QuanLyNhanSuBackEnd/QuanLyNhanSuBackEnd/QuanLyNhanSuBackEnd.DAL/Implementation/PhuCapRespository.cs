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
    public class PhuCapRespository : GenericRepository<PhuCap, QuanLyNhanSuBDContext>, IPhuCapRespository
    {
        public PhuCapRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<PhuCap, bool>> predicate)
        {
            return _context.PhuCap.Where(predicate).Count();
        }
        public IQueryable<PhuCap> FindByPredicate(Expression<Func<PhuCap, bool>> predicate)
        {
            return _context.PhuCap.Where(predicate).AsQueryable();
        }
    }
}
