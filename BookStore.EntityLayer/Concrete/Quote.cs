using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EntityLayer.Concrete
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
    }
}
