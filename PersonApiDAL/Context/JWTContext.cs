using Microsoft.EntityFrameworkCore;
using PersonApiDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApiDAL.Context
{
    public class JWTContext : DbContext
    {
        public JWTContext(DbContextOptions<JWTContext> options) : base(options) { }

        public DbSet<UserInfo> UserInfo { get; set; }
    
    }
}
