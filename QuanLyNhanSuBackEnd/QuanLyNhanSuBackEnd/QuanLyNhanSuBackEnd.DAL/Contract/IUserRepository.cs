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
       public int CountRecordsByPredicate(Expression<Func<IdentityUser, bool>> predicate);
       public IQueryable<IdentityUser> FindByPredicate(Expression<Func<IdentityUser, bool>> predicate);
       public IdentityUser? FindById(string id);
        public IdentityUser? FindUser(string Id);
        
    }
}
