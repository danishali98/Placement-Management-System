using WebProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebProject.EF
{
    public class PlacementsDBContext : DbContext
    {
        public PlacementsDBContext() : base("name=PlacementsDBEntities")
        {

        }

        public DbSet<StudentRecord> StudentRecord { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}