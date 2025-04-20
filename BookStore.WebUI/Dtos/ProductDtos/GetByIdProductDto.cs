using BookStore.EntityLayer.Concrete;

namespace BookStore.WebUI.Dtos.ProductDtos
{
    public class GetByIdProductDto
    {

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public IFormFile ImageFile { get; set; } 
    }
}
