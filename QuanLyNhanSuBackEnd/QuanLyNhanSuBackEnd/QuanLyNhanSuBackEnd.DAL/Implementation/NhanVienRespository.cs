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
    public class NhanVienRespository : GenericRepository<NhanVien, QuanLyNhanSuBDContext>, INhanVienRespository
    {
        public NhanVienRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<NhanVien, bool>> predicate)
        {
            return _context.NhanVien.Where(predicate).Count();
        }
        public IQueryable<NhanVien> FindByPredicate(Expression<Func<NhanVien, bool>> predicate)
        {
            return _context.NhanVien.Where(predicate).AsQueryable();
        }
    }
}
