using Maynghien.Common.Repository;
using QuanLyNhanSuBackEnd.DAL.Models.Context;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Contract
{
    public interface INhanVienRespository : IGenericRepository<NhanVien, QuanLyNhanSuBDContext>
    {
        public int CountRecordsByPredicate(Expression<Func<NhanVien, bool>> predicate);
        public IQueryable<NhanVien> FindByPredicate(Expression<Func<NhanVien, bool>> predicate);
    }
}
