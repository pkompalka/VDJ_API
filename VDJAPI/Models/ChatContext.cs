using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDJAPI.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {

        }
    }
}
