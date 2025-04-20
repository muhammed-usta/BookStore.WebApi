using BookStore.EntityLayer.Concrete;
using BookStore.WebUI.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLayer.Abstract
{
    public interface IDashboardService
    {
        int GetProductCount();
        int GetAuthorCount();
        int GetTotalStock();
        Product GetMostExpensiveProduct();
        Product GetLastAddedProduct();
        int GetSubscriberCount();
        int GetCategoryCount();
        decimal GetAverageProductPrice();
        Product GetLeastStockProduct();
        Product GetProductWithLongestDescription();
        string GetMostUsedEmailDomain();
        CategoryStockDto GetCategoryWithMostStock();

        List<ResultCategoryProductCountDto> GetCategoryProductCounts();

        List<CategoryPriceDto> GetCategoryTotalPrices();
    }
}
