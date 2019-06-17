using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFUploadPicture.Web
{
    public class ImageContext : DbContext  
    {
        private string _connectionString;

        public ImageContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Image> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);
        }

        
    }
}
