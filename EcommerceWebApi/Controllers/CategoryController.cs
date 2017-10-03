using EcommerceWebApi.Entity;
using EcommerceWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EcommerceWebApi.Controllers
{
    public class CategoryController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Categories
        public IQueryable<Category> GetCategories()
        {
            return db.Category.Where(x => x.isDeleted == false);
        }

        // GET: api/Users/5
        [HttpGet]
        public IHttpActionResult getCategoryById(int id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        

        // PUT: api/Users/5
        [Route("api/updateCategory")]
        [HttpPost]
        public IHttpActionResult updateCategory(Category category)
        {
            Category oldCategory = db.Category.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (category.CategoryId != oldCategory.CategoryId)
            {
                return BadRequest();
            }

            //db.Entry(user).State = EntityState.Modified;
            oldCategory.CategoryName = category.CategoryName;
            oldCategory.CategoryCode = category.CategoryCode;
            oldCategory.CategoryDescription = category.CategoryDescription;
            db.SaveChanges();

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/saveCategory")]
        [HttpPost]
        public IHttpActionResult saveCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            category.isDeleted = false;
            db.Category.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryId }, category);
        }
    }
}
