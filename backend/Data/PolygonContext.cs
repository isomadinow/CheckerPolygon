using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace backend.Data
{
    public class PolygonContext : DbContext
    {
        public PolygonContext(DbContextOptions<PolygonContext> options) : base(options) { }
        public DbSet<Polygon> Polygons { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Polygon>()
                .HasMany(p => p.Vertices)
                .WithOne(v => v.Polygon)
                .HasForeignKey(v => v.PolygonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
