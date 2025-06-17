using Microsoft.EntityFrameworkCore;
using FileStoringService.Models;
using System.Collections.Generic;

namespace FileStoringService.Data
{
   
    public class AppDbContext : DbContext
    {
        public DbSet<FileMetadata> Files { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }

}
