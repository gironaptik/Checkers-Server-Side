using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Model;

namespace Server.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly Server.Data.GameDataContext _context;
        private readonly Server.Data.PlayerDataContext _context2;


        public IndexModel(Server.Data.GameDataContext context, Server.Data.PlayerDataContext context2)
        {
            _context = context;
            _context2 = context2;
        }

        public IList<TblGames> TblGames { get;set; }

        public IList<TblPlayers> TblPlayers { get; set; }


        public async Task OnGetAsync()
        {
            TblGames = await _context.TblGames.ToListAsync();
            TblPlayers = await _context2.TblPlayers.ToListAsync();
        }
    }
}
