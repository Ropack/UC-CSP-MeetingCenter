using System.Collections.Generic;
using System.Data.Entity;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.DAL
{
    [DbConfigurationType(typeof(AppDbConfiguration))]
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DB")
        {
            
        }
        public virtual DbSet<Center> Centers { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}