using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlTower01.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ControlTower01.DAL
{
    class ControlTowerContext : DbContext
    {
        public DbSet<AirCraft> AirCrafts { get; set; }
        public DbSet<ArrivalRecord> ArrivalRecords { get; set; }
        public DbSet<DepartureRecord> DepartureRecords { get; set; }

        public DbSet<LoginUser>LoginUsers { get; set; }

        public ControlTowerContext() : base("ControlTowerContext")
        {
            Database.SetInitializer<ControlTowerContext>(new DropCreateDatabaseIfModelChanges<ControlTowerContext>());
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }
}
