using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Contract
{
    public interface IUserRepository
    {
        int CountRecordsByPredicate(Expression<Func<IdentityUser, bool>> predicate);
        IQueryable<IdentityUser> FindByPredicate(Expression<Func<IdentityUser, bool>> predicate);
        IdentityUser? FindById(string id);
    }
}
