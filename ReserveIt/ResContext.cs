using Microsoft.EntityFrameworkCore;
using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt
{
    public class ResContext : DbContext
    {
        public ResContext() : base()
        {
            
        }
        public override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //connection string
            builder.UseSqlServer("connection string"); 
        }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ConferenceRoom> ConferenceRooms { get; set; }
    }
}
