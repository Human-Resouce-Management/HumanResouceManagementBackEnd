using Maynghien.Common.Repository;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSuBackEnd.DAL.Contract;
using QuanLyNhanSuBackEnd.DAL.Models.Context;
using QuanLyNhanSuBackEnd.DAL.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSuBackEnd.DAL.Implementation
{
    public class BoPhanRespository : GenericRepository<BoPhan, QuanLyNhanSuBDContext>, IBoPhanRespository
    {
        private readonly SearchRequest _searchRequest;
        public BoPhanRespository(QuanLyNhanSuBDContext unitOfWork , SearchRequest request) : base(unitOfWork)
        {
            _context = unitOfWork;
            _searchRequest = request;
        }
        public int CountRecordsByPredicate(Expression<Func<BoPhan, bool>> predicate)
        {
            return _context.BoPhan.Where(predicate).Count();
        }
        public IQueryable<BoPhan> FindByPredicate(Expression<Func<BoPhan, bool>> predicate)
        {
            return _context.BoPhan.Where(predicate).AsQueryable();
        }
        public static int LevenshteinDistance(string str1, string str2)
        {
            // Tạo mảng hai chiều để lưu trữ khoảng cách giữa các ký tự
            int[,] distance = new int[str1.Length + 1, str2.Length + 1];

            // Khởi tạo khoảng cách
            for (int i = 0; i <= str1.Length; i++)
            {
                distance[i, 0] = i;
            }
            for (int i = 0; i <= str2.Length; i++)
            {
                distance[0, i] = i;
            }

            // Tính khoảng cách Levenshtein
            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        distance[i, j] = distance[i - 1, j - 1];
                    }
                    else
                    {
                        distance[i, j] = Math.Min(distance[i - 1, j - 1] + 1, Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1));
                    }
                }
            }

            // Trả về khoảng cách
            return distance[str1.Length, str2.Length];
        }
        public IQueryable<BoPhan> FindByPredicate2(Expression<Func<BoPhan, bool>> predicate, int levenshteinDistance)
        {

            return _context.BoPhan.Include(x => x.TenBoPhan).AsNoTracking().Where(predicate).Where(x => LevenshteinDistance(_searchRequest.Filters.First(f => f.FieldName == "TenBoPhan").Value, x.TenBoPhan) <= levenshteinDistance);
        }

    }
}
