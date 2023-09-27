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
    public interface IPhuCapRespository : IGenericRepository<PhuCap, QuanLyNhanSuBDContext>
    {
        public int CountRecordsByPredicate(Expression<Func<PhuCap, bool>> predicate);
        public IQueryable<PhuCap> FindByPredicate(Expression<Func<PhuCap, bool>> predicate);
    }
}
