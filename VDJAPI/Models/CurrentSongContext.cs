using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDJAPI.Models
{
    public class CurrentSongContext : DbContext
    {
        public DbSet<CurrentSong> CurrentSongs { get; set; }

        public CurrentSongContext(DbContextOptions<CurrentSongContext> options) : base(options)
        {

        }
    }
}
