using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationLibrary.Data.Entities;

namespace WebApplicationLibrary.Data.Service
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetProducts();
        Products GetProduct(int ProductID);
        IEnumerable<Products> GetProducts(IEnumerable<int> ProductIDs);
        void AddProducts(Products products);
        void DeleteProducts(Products products);
        void UpdateProducts(Products products);
        bool ProductsExists(int ProductID);
        IEnumerable<OrderDetails> GetOrderDetailsForProducts(int ProductID);
        OrderDetails GetOrderDetailsForProducts(int ProductID, int OrderID);
        void AddOrderDetailsForProducts(int ProductID, OrderDetails orderDetail);
        void UpdateOrderDetailsForProducts(OrderDetails orderDetail);
        void DeleteOrderDetails(OrderDetails orderDetail);
        bool OrderDetailsExists(int orderDetail);
        bool Save();
    }
}
