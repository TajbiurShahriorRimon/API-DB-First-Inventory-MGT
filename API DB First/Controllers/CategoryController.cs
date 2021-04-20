using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_DB_First.Models;
using API_DB_First.Repositories;
using API_DB_First.Attributes;

namespace API_DB_First.Controllers
{
    //This is a route for the whole class. If any route is configured for any method then the URL will be the class route plus 
    //the method route
    [RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
        [Route("")]//Empty Route. But the Data Annotation must be mentioned. Without it there will be error
        //There is another annotation give beside route. This is a custom annotation. If the following method is to be called, first
        //the custom data annotation's method will be called, which is in the Attributes folder. And then the following method
        //This custom annotation is used for Authentication
        public IHttpActionResult GetAllCategories()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            var list = categoryRepository.GetAll();
            return Ok(list);
        }
        
        //If No Data Attribute for Route, that is Data Annotation is not give for the following method the it will give an error
        //For the following method the URL will be the method route plus the class route
        [Route("{id}", Name = "GetCategoryById"), HttpGet] 
        //In the Route there is Name which gives unique Route Name for the whole Application
        public IHttpActionResult SingleCategory(int id)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            Category singleCategory = categoryRepository.Get(id);
            if (singleCategory == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            singleCategory.Links.Add(new Link()
            {
                Url = "http://localhost:65381/api/categories",
                Method = "POST",
                Relation = "Create a new Category Resource"
            });
            
            singleCategory.Links.Add(new Link()
            {
                Url = "http://localhost:65381/api/categories/"+singleCategory.CategoryId,
                Method = "PUT",
                Relation = "Modify an existing category resource"
            });
            
            singleCategory.Links.Add(new Link()
            {
                Url = "http://localhost:65381/api/categories/"+singleCategory.CategoryId,
                Method = "Delete",
                Relation = "Delete an existing category resource"
            });
            return Ok(singleCategory);
        }

        [Route("")/*, BasicAuthentication*/]
        public IHttpActionResult Post(Category category)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            /*Category category = new Category();
            category.CategoryName = categoryName;*/
            categoryRepository.Insert(category);

            //Creating a link. In the Link method we are passing 2 arguments. First one is the Route Name, Second one is the
            //unknown value. So here as a second parameter we are passing an anonymous object.
            string url = Url.Link("GetCategoryById", new{id = category.CategoryId});
            return Created(url, category);
        }

        [Route("{id}")]
        public IHttpActionResult Put(Category category, int id)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            category.CategoryId = id;
            categoryRepository.Update(category);
            return Ok();
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            categoryRepository.Delete(id); 
            return StatusCode(HttpStatusCode.NoContent); 
        }
    }
}
