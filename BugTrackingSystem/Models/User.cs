using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BugTrackingSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "Login")]
        public string UserLogin { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public ICollection<UsersTask> UsersTasks { get; set; }
        public User()
        {
            UsersTasks = new List<UsersTask>();
        }
    }
}