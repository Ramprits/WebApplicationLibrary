using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationLibrary.Data.Entities;

namespace WebApplicationLibrary.Data.Service
{
    public class ProductRepository : IProductRepository
    {

        private NorthwndContext _context;

        public ProductRepository(NorthwndContext context)
        {
            _context = context;
        }
        public void AddOrderDetailsForProducts(int productId, OrderDetails orderDetail)
        {
            var products = GetProduct(productId);
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

        public IEnumerable<OrderDetails> GetOrderDetailsForProducts(int productId)
        {
            return _context.OrderDetails.Where(x => x.ProductId == productId).OrderBy(x => x.ProductId).ToList();
        }

        public OrderDetails GetOrderDetailsForProducts(int productId, int orderId)
        {
            return _context.OrderDetails.Where(x => x.ProductId == productId && x.OrderId == orderId).FirstOrDefault();
        }

        public IEnumerable<Products> GetProducts()
        {
            return _context.Products.OrderBy(x => x.ProductName).ToList();
        }

        public Products GetProduct(int productId)
        {
            return _context.Products.FirstOrDefault(x => x.ProductId == productId);
        }

        public IEnumerable<Products> GetProducts(IEnumerable<int> productIDs)
        {
            throw new NotImplementedException();
        }

        public bool OrderDetailsExists(int orderDetail)
        {
            return _context.OrderDetails.Any(x => x.OrderId == orderDetail);
        }

        public bool ProductsExists(int productId)
        {
            return _context.Products.Any(x => x.ProductId == productId);
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
