using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationLibrary.Data.Entities;

namespace WebApplicationLibrary.Data.Service
{
    public class ProductRepository : IProductRepository
    {

        private NORTHWNDContext _context;

        public ProductRepository(NORTHWNDContext context)
        {
            _context = context;
        }
        public void AddOrderDetailsForProducts(int ProductID, OrderDetails orderDetail)
        {
            var products = GetProduct(ProductID);
            if (products != null)
            {
                products.OrderDetails.Add(orderDetail);
            }
        }

        public void AddProducts(Products products)
        {
            _context.Products.Add(products);
            if (products.OrderDetails.Any())
            {
                foreach (var oderDetail in products.OrderDetails)
                {

                }
            }

        }

        public void DeleteOrderDetails(OrderDetails orderDetail)
        {

        }

        public void DeleteProducts(Products products)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetails> GetOrderDetailsForProducts(int ProductID)
        {
            return _context.OrderDetails.Where(x => x.ProductId == ProductID).OrderBy(x => x.ProductId).ToList();
        }

        public OrderDetails GetOrderDetailsForProducts(int ProductID, int OrderID)
        {
            return _context.OrderDetails.Where(x => x.ProductId == ProductID && x.OrderId == OrderID).FirstOrDefault();
        }

        public IEnumerable<Products> GetProducts()
        {
            return _context.Products.OrderBy(x => x.ProductName).ToList();
        }

        public Products GetProduct(int ProductID)
        {
            return _context.Products.FirstOrDefault(x => x.ProductId == ProductID);
        }

        public IEnumerable<Products> GetProducts(IEnumerable<int> ProductIDs)
        {
            throw new NotImplementedException();
        }

        public bool OrderDetailsExists(int orderDetail)
        {
            return _context.OrderDetails.Any(x => x.OrderId == orderDetail);
        }

        public bool ProductsExists(int ProductID)
        {
            return _context.Products.Any(x => x.ProductId == ProductID);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateOrderDetailsForProducts(OrderDetails orderDetail)
        {
            throw new NotImplementedException();
        }

        public void UpdateProducts(Products products)
        {
            throw new NotImplementedException();
        }
    }
}
