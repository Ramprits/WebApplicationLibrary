using System.Collections.Generic;
using WebApplicationLibrary.Data.Entities;

namespace WebApplicationLibrary.Data.Service
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetProducts();
        Products GetProduct(int productId);
        IEnumerable<Products> GetProducts(IEnumerable<int> productIDs);
        void AddProducts(Products products);
        void DeleteProducts(Products products);
        void UpdateProducts(Products products);
        bool ProductsExists(int productId);
        IEnumerable<OrderDetails> GetOrderDetailsForProducts(int productId);
        OrderDetails GetOrderDetailsForProducts(int productId, int orderId);
        void AddOrderDetailsForProducts(int productId, OrderDetails orderDetail);
        void UpdateOrderDetailsForProducts(OrderDetails orderDetail);
        void DeleteOrderDetails(OrderDetails orderDetail);
        bool OrderDetailsExists(int orderDetail);
        bool Save();
    }
}
