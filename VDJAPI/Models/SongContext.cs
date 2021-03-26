using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDJAPI.Models
{
    public class SongContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public SongContext(DbContextOptions<SongContext> options) : base(options)
        {

        }
    }
}
