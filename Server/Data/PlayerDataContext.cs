using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Model;

namespace Server.Data
{
    public class PlayerDataContext : DbContext
    {
        public PlayerDataContext (DbContextOptions<PlayerDataContext> options)
            : base(options)
        {
        }

        public DbSet<Server.Model.TblPlayers> TblPlayers { get; set; }

        public DbSet<Server.Model.TblGames> TblGames { get; set; }
    }
}
