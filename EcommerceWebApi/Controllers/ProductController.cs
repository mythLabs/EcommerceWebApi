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
    public class ProductController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public IQueryable<Product> GetProducts()
        {
            return db.Product.Where(x => x.isDeleted == false);
        }

        
        [HttpGet]
        public IHttpActionResult getProductById(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        

        // PUT: api/Users/5
        [Route("api/updateProduct")]
        [HttpPost]
        public IHttpActionResult updateProduct(Product product)
        {
            Product oldProduct = db.Product.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product.ProductId != oldProduct.ProductId)
            {
                return BadRequest();
            }

            //db.Entry(user).State = EntityState.Modified;
            oldProduct.ProductName = product.ProductName;
            oldProduct.ProductDescription = product.ProductDescription;
            oldProduct.SellerName = product.SellerName;
            oldProduct.StockSize = product.StockSize;
            oldProduct.CategoryId = product.CategoryId;
            
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

        [Route("api/saveProduct")]
        [HttpPost]
        public IHttpActionResult saveProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.isDeleted = false;
            db.Product.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        [Route("api/deleteProduct")]
        [HttpGet]
        public IHttpActionResult deleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Where(x => x.ProductId == id).FirstOrDefault().isDeleted = true;
            db.SaveChanges();

            return Ok(0);
        }
    }
}
