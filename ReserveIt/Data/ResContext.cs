using Microsoft.EntityFrameworkCore;
using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Data
{
    public class ResContext : DbContext
    {
        public ResContext(DbContextOptions<ResContext> options)  : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //connection string
            //builder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ResDb;Integrated Security=True;"); 
        }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        
    }

}
