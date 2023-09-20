using Maynghien.Common.Repository;
using Microsoft.AspNetCore.Identity;
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
    public class BoPhanRespository : GenericRepository<BoPhan, QuanLyNhanSuBDContext>, IBoPhanRespository
    {
        public BoPhanRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<BoPhan, bool>> predicate)
        {
            return _context.BoPhan.Where(predicate).Count();
        }
        public IQueryable<BoPhan> FindByPredicate(Expression<Func<BoPhan, bool>> predicate)
        {
            return _context.BoPhan.Where(predicate);
        }

    }
}
