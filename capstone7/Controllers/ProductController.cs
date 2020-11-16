using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using capstone7.Data;
using capstone7.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace capstone7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly NorthwindContext _context;
        public ProductController()
        {
            _context = new NorthwindContext();
        }

       

        // GET: api/<ProductController>
        [HttpGet]//Get items from api
        public IEnumerable<ProductModel> Get()
        {

            var products = _context.Products.Select(product => new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName
            }).ToList();

            return products;


        }
        // GET: api/ProductController/5
        [HttpGet]
        [Route("{id}")]
        public Products Get(int id)
        {
            var products = _context.Products.FirstOrDefault(x => x.ProductId == id);


            return products;
        }

        [HttpGet]
        [Route("searchByCategoryId")]
        public Products GetByCategoryId(int categoryId)
        {
            var product = _context.Products.FirstOrDefault(x => x.CategoryId == categoryId);

            return product;
        }

        [HttpGet]
        [Route("searchBySupplierId")]
        public Products GetBySupplierId(int supplierId)
        {
            var product = _context.Products.FirstOrDefault(x => x.SupplierId == supplierId);

            return product;
        }


        [HttpGet]
        [Route("searchByUnitPrice")]
        public ActionResult<IEnumerable<Products>> GetByUnitPrice(decimal maxPrice)
        {
            // LINQ - Langnguage Integrated Query
            var product = _context.Products.Where(x => x.UnitPrice <= maxPrice).ToList();

            return Ok(product);
        }

        [HttpGet]
        [Route("includeDiscontinuedItems")]
        public List<Products> IncludeDiscontinuedItems()
        {
            var products = _context.Products.ToList();

            return products;
        }


        [HttpGet]
        [Route("excludeDiscontinuedItems")]
        public IEnumerable<Products> ExcludeDiscontinuedItems()
        {
            var products = _context.Products.Where(x => !x.Discontinued).ToList();

            return products;
        }
        //public decimal? UnitPrice { get; set; }

        [HttpPut]
        [Route("{id}/update")]
        public Products UpdateProduct(int id, UpdateProductModel updateProductModel)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                product.ProductName = updateProductModel.Name;
                product.UnitPrice = updateProductModel.UnitPrice;
                _context.SaveChanges();
            }

            return product;

        }
        //is this method correct
        [HttpPost]
        [Route("add")]
        public IActionResult AddProduct(Products addProductModel)
        {
            _context.Products.Add(addProductModel);
            _context.SaveChanges();

            return Ok();

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);



            //foreach(var prod in _context.Products)
            //{
            //    if(prod.ProductId == id)
            //    {
            //        product = prod;
            //        break;
            //    }
            //}

            _context.Products.Remove(product);
            _context.SaveChanges();


            return Ok(product); return Ok();

        }
        // public string ProductName { get; set; }
        //public decimal? UnitPrice { get; set; }
        // public int ProductId { get; set; }
        // public string ProductName { get; set; }

        public class AddProductModel
        {
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }
        }


        public class UpdateProductModel
        {
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }
        }

        public class DeleteProductModel
        {
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }

}