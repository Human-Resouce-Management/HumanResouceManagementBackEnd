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
    public class TuyenDungRespository : GenericRepository<TuyenDung, QuanLyNhanSuBDContext>, ITuyenDungRespository
    {
        public TuyenDungRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<TuyenDung, bool>> predicate)
        {
            return _context.tuyenDung.Where(predicate).Count();
        }
        public IQueryable<TuyenDung> FindByPredicate(Expression<Func<TuyenDung, bool>> predicate)
        {
            return _context.tuyenDung.Where(predicate).AsQueryable();
        }
    }
}
