using BugTrackingSystem.Models;
using NLog;
using System.Data.Entity;
using System.Web.Configuration;

namespace BugTrackingSystem.Repository
{
    public class BugTrackingContext : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public BugTrackingContext() : base(BugTrackingContext.GetConnectionString())
        {
        }

        public static string GetConnectionString()
        {
            var connectionString = WebConfigurationManager.AppSettings["SessionConnectionStrings"];
            if (connectionString == null || connectionString == "")
            {
                connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                _logger.Debug("Connection string get from ConnectionString");
            }
            else
            {
                _logger.Debug("Connection string get from SessionConnectionStrings");
            }
            return connectionString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UsersTask> UsersTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}