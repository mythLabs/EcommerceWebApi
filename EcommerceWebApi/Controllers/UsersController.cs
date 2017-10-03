using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EcommerceWebApi.Entity;
using EcommerceWebApi.Models;
using System.Web;

namespace EcommerceWebApi.Controllers
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.User.Where(x=>x.isDeleted == false);
        }

        // GET: api/Users/5
        [HttpGet]
        public IHttpActionResult getUserById(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [HttpGet]
        public IHttpActionResult getUserforauthentication(string username,string password)
        {
            User user = db.User.Where(x => x.Username == username && x.Password == password && x.isDeleted == false).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [Route("api/updateUser")]
        [HttpPost]
        public IHttpActionResult updateUser(User user)
        {
            User oldUser = db.User.Where(x => x.UserId == user.UserId).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user.UserId != oldUser.UserId)
            {
                return BadRequest();
            }

            //db.Entry(user).State = EntityState.Modified;
            oldUser.Username = user.Username;
            oldUser.profileImageName = user.profileImageName;
            oldUser.PhoneNumber = user.PhoneNumber;
            oldUser.Password = user.Password;
            oldUser.LastName = user.LastName;
            oldUser.FirstName = user.FirstName;
            oldUser.Country = user.Country;
            oldUser.City = user.City;
            db.SaveChanges();

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/saveUser")]
        [HttpPost]
        public IHttpActionResult saveUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.isDeleted = false;
            db.User.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        [Route("api/deleteUser")]
        [HttpGet]
        public IHttpActionResult deleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User.Where(x => x.UserId == id).FirstOrDefault().isDeleted = true;
            db.SaveChanges();

            return Ok(0);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        [Route("api/uploadFile")]
        [HttpPost]
        public IHttpActionResult uploadFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;

            File LocalFile = new File();
            if (httpRequest.Files.Count > 0)
            {
                foreach(string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/ProfileImage/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    LocalFile.filename = postedFile.FileName;
                }
            }

            return Ok(LocalFile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.UserId == id) > 0;
        }
    }
}