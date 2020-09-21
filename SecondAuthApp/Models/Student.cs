using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAuthApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public virtual IdentityUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
