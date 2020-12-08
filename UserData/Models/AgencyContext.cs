using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserData.Models;

namespace UserData.Models
{
    public class AgencyContext : DbContext
    {
        public AgencyContext(DbContextOptions<AgencyContext> options) : base(options)
        {

        }

        public DbSet<Data> agencies { get; set; }

        public DbSet<UserData.Models.Agency> Agency { get; set; }
    }
}
