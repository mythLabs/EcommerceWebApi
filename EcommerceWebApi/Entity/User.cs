using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceWebApi.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string profileImageName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? isDeleted {get; set; }
    }

    public class File
    {
        public string filename { get; set; }
    }
}