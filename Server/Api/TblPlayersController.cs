using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Model;

namespace Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPlayersController : ControllerBase
    {
        private readonly PlayerDataContext _context;

        public TblPlayersController(PlayerDataContext context)
        {
            _context = context;
        }

        // GET: api/TblPlayers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPlayers>>> GetTblPlayers()
        {
            return await _context.TblPlayers.ToListAsync();
        }

        // GET: api/TblPlayers/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPlayers>> GetTblPlayers(int id)
        {
            var tblPlayers = await _context.TblPlayers.FindAsync(id);

            if (tblPlayers == null)
            {
                return NotFound();
            }

            return tblPlayers;
        }

        // GET: api/TblPlayers/login?name=Name&password=Password
        [HttpGet("login")]
        public async Task<ActionResult<TblPlayers>> Login(string name, string password)
        {
            var tblPlayers = await _context.TblPlayers.FirstOrDefaultAsync(s => s.Name.Trim() == name && s.Password.Trim() == password);

            if (tblPlayers == null)
            {
                return NotFound();
            }

            return tblPlayers;
        }

        // PUT: api/TblPlayers/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblPlayers(int id, TblPlayers tblPlayers)
        {
            if (id != tblPlayers.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblPlayers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblPlayersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("updateGames")]
        public async Task<IActionResult> UpdateGamesAmount(int id)
        {
            var tblPlayers = await _context.TblPlayers.FindAsync(id);

            if (tblPlayers == null)
            {
                return BadRequest();
            }

            tblPlayers.NumOfGames = ++tblPlayers.NumOfGames;
            _context.Entry(tblPlayers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblPlayersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TblPlayers
        [HttpPost]
        public async Task<ActionResult<TblPlayers>> PostTblPlayers(TblPlayers tblPlayers)
        {
            _context.TblPlayers.Add(tblPlayers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblPlayers", new { id = tblPlayers.Id }, tblPlayers);
        }

        // DELETE: api/TblPlayers/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblPlayers>> DeleteTblPlayers(int id)
        {
            var tblPlayers = await _context.TblPlayers.FindAsync(id);
            if (tblPlayers == null)
            {
                return NotFound();
            }

            _context.TblPlayers.Remove(tblPlayers);
            await _context.SaveChangesAsync();

            return tblPlayers;
        }

        private bool TblPlayersExists(int id)
        {
            return _context.TblPlayers.Any(e => e.Id == id);
        }
    }
}
