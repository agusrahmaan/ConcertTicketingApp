using ConcertTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketingApp.Data
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions options)
        : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<DataConcert> dataConcerts { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}
