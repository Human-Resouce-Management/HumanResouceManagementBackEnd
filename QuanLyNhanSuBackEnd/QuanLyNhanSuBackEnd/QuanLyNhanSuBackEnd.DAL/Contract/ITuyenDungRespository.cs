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
   public interface ITuyenDungRespository : IGenericRepository<TuyenDung, QuanLyNhanSuBDContext>
    {
        public int CountRecordsByPredicate(Expression<Func<TuyenDung, bool>> predicate);
        public IQueryable<TuyenDung> FindByPredicate(Expression<Func<TuyenDung, bool>> predicate);
    }
}
