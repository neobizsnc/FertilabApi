using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FertilabApi.Models
{
    public class FertilabContext : DbContext
    {
        public FertilabContext(DbContextOptions<FertilabContext> options) : base(options)
        {
        }
        public DbSet<Center> Center { get; set; }
    }
}
