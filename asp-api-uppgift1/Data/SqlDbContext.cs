using asp_api_uppgift1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Issue> Issues { get; set; }    

        public DbSet<Comment> Comments { get; set; }    

        public DbSet<SessionToken> SessionTokens { get; set; }
    }
}
