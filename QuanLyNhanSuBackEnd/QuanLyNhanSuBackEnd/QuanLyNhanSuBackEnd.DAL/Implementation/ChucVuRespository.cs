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
    public class ChucVuRespository : GenericRepository<ChucVu, QuanLyNhanSuBDContext>, IChucVuRespository
    {
        public ChucVuRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<ChucVu, bool>> predicate)
        {
            return _context.ChucVu.Where(predicate).Count();
        }
        public IQueryable<ChucVu> FindByPredicate(Expression<Func<ChucVu, bool>> predicate)
        {
            return _context.ChucVu.Where(predicate).AsQueryable();
        }
    }
}
