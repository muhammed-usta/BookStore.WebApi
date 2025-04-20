using BookStore.DataAccessLayer.Abstract;
using BookStore.DataAccessLayer.Context;
using BookStore.EntityLayer.Concrete;
using BookStore.WebUI.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.EntityFramework
{
    public class EfDashboardDal : IDashboardDal
    {
        private readonly BookStoreContext _context;

        public EfDashboardDal(BookStoreContext context)
        {
            _context = context;
        }
        public int GetProductCount()
        {
            return _context.Products.Count();
        }

        public int GetAuthorCount()
        {
            return _context.Products
                .Select(p => p.Author)
                .Distinct()
                .Count();
        }

        public int GetTotalStock()
        {
            return _context.Products.Sum(p => p.Stock);
        }

        public Product GetMostExpensiveProduct()
        {
            return _context.Products
                .OrderByDescending(p => p.Price)
                .FirstOrDefault();
        }

        public Product GetLastAddedProduct()
        {
            return _context.Products
                .OrderByDescending(p => p.ProductId) // ya da CreatedDate varsa onu kullan
                .FirstOrDefault();
        }

        public int GetSubscriberCount()
        {
            return _context.Subscribers.Count();
        }

        public int GetCategoryCount()
        {
            return _context.Categories.Count();
        }

        public decimal GetAverageProductPrice()
        {
            return _context.Products.Average(p => p.Price);
        }

        public Product GetLeastStockProduct()
        {
            return _context.Products
                .OrderBy(p => p.Stock)
                .FirstOrDefault();
        }

        public Product GetProductWithLongestDescription()
        {
            return _context.Products
                .OrderByDescending(p => p.Description.Length)
                .FirstOrDefault();
        }

        public string GetMostUsedEmailDomain()
        {
            var emails = _context.Subscribers
                .Where(x => !string.IsNullOrEmpty(x.Email) && x.Email.Contains("@"))
                .Select(x => x.Email)
                .ToList(); 

            var domain = emails
                .Select(email => email.Substring(email.IndexOf('@') + 1))
                .GroupBy(domain => domain)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            return domain;
        }

        public CategoryStockDto GetCategoryWithMostStock()
        {
            var grouped = _context.Products
                .GroupBy(p => p.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    TotalStock = g.Sum(p => p.Stock)
                })
                .OrderByDescending(g => g.TotalStock)
                .FirstOrDefault();

            if (grouped == null)
                return null;

            var categoryName = _context.Categories
                .Where(c => c.CategoryId == grouped.CategoryId)
                .Select(c => c.CategoryName)
                .FirstOrDefault();

            return new CategoryStockDto
            {
                CategoryName = categoryName,
                TotalStock = grouped.TotalStock
            };


        }

        public List<ResultCategoryProductCountDto> GetCategoryProductCounts()
        {
            var result = _context.Categories
                .Select(c => new ResultCategoryProductCountDto
                {
                    CategoryName = c.CategoryName,
                    TotalStock = c.Products.Sum(p => p.Stock)
                })
                .ToList();

            return result;
        }

        public List<CategoryPriceDto> GetCategoryTotalPrices()
        {
            var values = _context.Products
                .GroupBy(b => b.Category.CategoryName)
                .Select(g => new CategoryPriceDto
                {
                    CategoryName = g.Key,
                    TotalPrice = g.Sum(x => x.Price)
                })
                .ToList();

            return values;
        }

    }
}