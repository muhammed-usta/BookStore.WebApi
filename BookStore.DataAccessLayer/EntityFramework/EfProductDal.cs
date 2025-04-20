using BookStore.DataAccessLayer.Abstract;
using BookStore.DataAccessLayer.Context;
using BookStore.DataAccessLayer.Repositories;
using BookStore.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        private readonly BookStoreContext _context;

        public EfProductDal(BookStoreContext context) : base(context)
        {
            _context = context;

        }

        public int GetProductCount()
        {
            var context = new BookStoreContext();
            int value = context.Products.Count();
            return value;
        }

        public Product GetRandomProduct()
        {
            return _context.Products
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefault();
        }

        public List<Product> GetProductsWithCategory()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

    }
}
