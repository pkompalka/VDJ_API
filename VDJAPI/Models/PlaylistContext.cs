using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDJAPI.Models
{
    public class PlaylistContext : DbContext
    {
        public DbSet<Playlist> Playlist { get; set; }

        public PlaylistContext(DbContextOptions<PlaylistContext> options) : base(options)
        {

        }
    }
}
