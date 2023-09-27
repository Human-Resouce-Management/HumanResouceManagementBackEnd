using Maynghien.Common.Repository;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Identity;
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
    public interface IBoPhanRespository : IGenericRepository<BoPhan, QuanLyNhanSuBDContext>
    {
        public int CountRecordsByPredicate(Expression<Func<BoPhan, bool>> predicate);
        public IQueryable<BoPhan> FindByPredicate(Expression<Func<BoPhan, bool>> predicate);
        public IQueryable<BoPhan> FindByPredicate2(Expression<Func<BoPhan, bool>> predicate, int levenshteinDistance);


    }

}

