using BookStore.BusinessLayer.Abstract;
using BookStore.DataAccessLayer.Abstract;
using BookStore.EntityLayer.Concrete;
using BookStore.WebUI.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLayer.Concrete
{
    public class DashboardManager:IDashboardService
    {
        private readonly IDashboardDal _dashboardDal;

        public DashboardManager(IDashboardDal dashboardDal)
        {
            _dashboardDal = dashboardDal;
        }
        public int GetProductCount()
        {
            return _dashboardDal.GetProductCount();
        }

        public int GetAuthorCount()
        {
            return _dashboardDal.GetAuthorCount();
        }

        public int GetTotalStock()
        {
            return _dashboardDal.GetTotalStock();
        }

        public Product GetMostExpensiveProduct()
        {
            return _dashboardDal.GetMostExpensiveProduct();
        }

        public Product GetLastAddedProduct()
        {
            return _dashboardDal.GetLastAddedProduct();
        }

        public int GetSubscriberCount()
        {
            return _dashboardDal.GetSubscriberCount();
        }

        public int GetCategoryCount()
        {
            return _dashboardDal.GetCategoryCount();
        }

        public decimal GetAverageProductPrice()
        {
            return _dashboardDal.GetAverageProductPrice();
        }

        public Product GetLeastStockProduct()
        {
            return _dashboardDal.GetLeastStockProduct();
        }

        public Product GetProductWithLongestDescription()
        {
            return _dashboardDal.GetProductWithLongestDescription();
        }

        public string GetMostUsedEmailDomain()
        {
            return _dashboardDal.GetMostUsedEmailDomain();
        }

        public CategoryStockDto GetCategoryWithMostStock()
        {
            return _dashboardDal.GetCategoryWithMostStock();
        }

        public List<ResultCategoryProductCountDto> GetCategoryProductCounts()
        {
            return _dashboardDal.GetCategoryProductCounts();
        }

        public List<CategoryPriceDto> GetCategoryTotalPrices()
        {
            return _dashboardDal.GetCategoryTotalPrices();
        }


    }
}
