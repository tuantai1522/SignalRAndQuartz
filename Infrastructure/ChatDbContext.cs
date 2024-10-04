using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Infrastructure
{
    public class ChatDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }

        public ChatDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
