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
    public class NghiPhepRespository : GenericRepository<NghiPhep, QuanLyNhanSuBDContext>, INghiPhepRespository
    {
        public NghiPhepRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<NghiPhep, bool>> predicate)
        {
            return _context.NghiPhep.Where(predicate).Count();
        }
        public IQueryable<NghiPhep> FindByPredicate(Expression<Func<NghiPhep, bool>> predicate)
        {
            return _context.NghiPhep.Where(predicate).AsQueryable();
        }
    }
}
