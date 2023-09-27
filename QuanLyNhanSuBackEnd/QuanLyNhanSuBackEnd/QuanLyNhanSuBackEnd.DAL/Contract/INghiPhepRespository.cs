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
    public interface INghiPhepRespository : IGenericRepository<NghiPhep, QuanLyNhanSuBDContext>
    {
        public int CountRecordsByPredicate(Expression<Func<NghiPhep, bool>> predicate);
        public IQueryable<NghiPhep> FindByPredicate(Expression<Func<NghiPhep, bool>> predicate);
    }
}
