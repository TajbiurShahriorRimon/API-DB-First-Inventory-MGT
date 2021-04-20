using API_DB_First.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_DB_First.Models;

namespace API_DB_First.Controllers
{
    public class ProductController : ApiController
    {
        //http://localhost:65381/api/product
        //Note that it is singular (product) not plural (products)
        [HttpGet]
        public IHttpActionResult ProductList()
        {
            ProductRepository productRepository = new ProductRepository();
            var allProducts = productRepository.GetAll();
            return Ok(allProducts);
        }

        //http://localhost:65381/api/products
        //Note that it is plural (products) not singular (product)
        //If we run in the Postman to get all the product by invoking the Get, Both the above and the following method will
        //conflict and will give an error, that is An exception will be thrown stating that "Multiple Actions were matched"
        //And also if the above method were not there, the still the following method will not get executed because
        //the URI will not work, it is plural format products. but the controller is not plural, that is, It is not ProductsController
        /*public IHttpActionResult Get()
        {
            ProductRepository productRepository = new ProductRepository();
            return Ok(productRepository.GetAll());
        }*/
        
        //Now if the above ProductList() method exists still both will work because of course their URIs are different.
        //In the Postman the based on the Uri any of the method will be executed.
        [Route("api/products")]//Explicitly defined
        public IHttpActionResult Get()
        {
            ProductRepository productRepository = new ProductRepository();
            return Ok(productRepository.GetAll());
        }

        [HttpGet]
        public IHttpActionResult SingleProduct(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            Product singleProduct = productRepository.Get(id);
            return Ok(singleProduct);
        }

        [HttpPost]
        public IHttpActionResult AddProduct(Product product)
        {
            ProductRepository productRepository = new ProductRepository();
            productRepository.Insert(product);
            return Created("abc", product);
        }

        
        //In the method parameter we have explicitly specified the which will bind from which portion.
        //The first parameter id will be bind from the URI and the second parameter product will be bind from the body from Postman
        [HttpPut]
        public IHttpActionResult EditProduct([FromUri]int id, [FromBody]Product product)
        {
            product.ProductId = id;
            ProductRepository productRepository = new ProductRepository();
            productRepository.Update(product);
            return Ok(product);
        }

        //[HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            productRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
