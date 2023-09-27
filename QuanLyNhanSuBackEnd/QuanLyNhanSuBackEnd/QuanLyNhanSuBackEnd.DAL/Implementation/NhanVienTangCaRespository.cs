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
    public class NhanVienTangCaRespository : GenericRepository<NhanVienTangCa, QuanLyNhanSuBDContext>, INhanVienTangCaRespository
    {
        public NhanVienTangCaRespository(QuanLyNhanSuBDContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<NhanVienTangCa, bool>> predicate)
        {
            return _context.NhanVienTangCa.Where(predicate).Count();
        }
        public IQueryable<NhanVienTangCa> FindByPredicate(Expression<Func<NhanVienTangCa, bool>> predicate)
        {
            return _context.NhanVienTangCa.Where(predicate).AsQueryable();
        }
    }
}
