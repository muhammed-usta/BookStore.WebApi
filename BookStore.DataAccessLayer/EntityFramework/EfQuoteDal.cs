using BookStore.DataAccessLayer.Abstract;
using BookStore.DataAccessLayer.Context;
using BookStore.DataAccessLayer.Repositories;
using BookStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.EntityFramework
{
    public class EfQuoteDal : GenericRepository<Quote>, IQuoteDal
    {
        private readonly BookStoreContext _context;
        public EfQuoteDal(BookStoreContext context) : base(context)
        {
            _context = context;
        }

        public Quote GetLastQuote()
        {
            return _context.Quotes
                .OrderByDescending(x => x.QuoteId)
                .FirstOrDefault();
        }

    }
}
