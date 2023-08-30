using Microsoft.AspNetCore.Identity;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly QuanLyNhanSuBDContext _context;
        public UserRepository(QuanLyNhanSuBDContext context)
        {
            _context = context;
        }
        public List<IdentityUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public int CountRecordsByPredicate(Expression<Func<IdentityUser, bool>> predicate)
        {
            return _context.Users.Where(predicate).Count();
        }

        public IdentityUser? FindById(string id)
        {
            return _context.Users.Where(m => m.Id == id).FirstOrDefault();
        }

        public IQueryable<IdentityUser> FindByPredicate(Expression<Func<IdentityUser, bool>> predicate)
        {
            return _context.Users.Where(predicate).AsQueryable();
        }

        public IdentityUser FindUser(string? Id)
        {
            return _context.Users.FirstOrDefault(m => m.Id == Id);
        }
    }
}
