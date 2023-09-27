using Maynghien.Common.Repository;
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
    public interface IChucVuRespository: IGenericRepository<ChucVu, QuanLyNhanSuBDContext>
    {
        public int CountRecordsByPredicate(Expression<Func<ChucVu, bool>> predicate);
        public IQueryable<ChucVu> FindByPredicate(Expression<Func<ChucVu, bool>> predicate);
    }
}
