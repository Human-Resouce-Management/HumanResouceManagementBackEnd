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
    public interface ITangLuongRespository : IGenericRepository<TangLuong, QuanLyNhanSuBDContext>
    {
        public int CountRecordsByPredicate(Expression<Func<TangLuong, bool>> predicate);
        public IQueryable<TangLuong> FindByPredicate(Expression<Func<TangLuong, bool>> predicate);
    }
}
