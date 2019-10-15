using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models
{
    public class UsersTask
    {
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Topic { get; set; }
        public string Type { get; set; }
        public PriorityType Priority { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}