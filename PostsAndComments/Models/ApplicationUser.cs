using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Fullname { get; set; }
        public DateTime? DateOfBirth { get; set; }


        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
