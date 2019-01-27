using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASPDotNetWebApi01.Models;

namespace ASPDotNetWebApi01.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        #region GET

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        #endregion


        #region POST

        public bool PostAsync()
        {
            return true;
        }

        #endregion

        #region PUT

        public bool PutAsync()
        {
            return true;
        }

        #endregion

        #region DELETE

        public bool DeleteAsync()
        {
            return true;
        }

        #endregion
    }
}
