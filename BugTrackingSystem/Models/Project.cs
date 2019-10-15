using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Project name")]
        public string Name { get; set; }
        public ICollection<UsersTask> UsersTasks { get; set; }

        public Project()
        {
            UsersTasks = new List<UsersTask>();
        }
    }
}