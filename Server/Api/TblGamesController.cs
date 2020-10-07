using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Model;

namespace Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblGamesController : ControllerBase
    {
        private readonly PlayerDataContext _context;

        public TblGamesController(PlayerDataContext context)
        {
            _context = context;
        }

        // GET: api/TblGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGames>>> GetTblGames()
        {
            return await _context.TblGames.ToListAsync();
        }

        // GET: api/TblGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGames>> GetTblGames(int id)
        {
            var tblGames = await _context.TblGames.FindAsync(id);

            if (tblGames == null)
            {
                return NotFound();
            }

            return tblGames;
        }

        // PUT: api/TblGames/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblGames(int id, TblGames tblGames)
        {
            if (id != tblGames.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblGames).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblGamesExists(id))
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

        // POST: api/TblGames
        [HttpPost]
        public async Task<ActionResult<TblGames>> PostTblGames(TblGames tblGames)
        {
            _context.TblGames.Add(tblGames);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblGames", new { id = tblGames.Id }, tblGames);
        }

        // DELETE: api/TblGames/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblGames>> DeleteTblGames(int id)
        {
            var tblGames = await _context.TblGames.FindAsync(id);
            if (tblGames == null)
            {
                return NotFound();
            }

            _context.TblGames.Remove(tblGames);
            await _context.SaveChangesAsync();

            return tblGames;
        }

        private bool TblGamesExists(int id)
        {
            return _context.TblGames.Any(e => e.Id == id);
        }

        [HttpPost("{nextstep}")]
        public TurnManagment NextStep(int[][] board)
        {
            TurnManagment turnManagment = new TurnManagment();
            turnManagment.EndOfRoad = false;
            int boardRows = board.Length;
            int boardColumns = board[0].Length;
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    if (board[i][j] == 1 && turnManagment.IsInsideBorders(boardRows, boardColumns, i+1, j+1))
                    {
                        //Eat from right
                        if(board[i+1][j+1] == 2 && turnManagment.IsInsideBorders(boardRows, boardColumns, i + 2, j + 2))
                        {
                            //Eat from right
                            if (board[i + 2][j + 2] == 0){
                                turnManagment.IFromAnswer = i;
                                turnManagment.JFromAnswer = j;
                                turnManagment.IToAnswer = i + 2;
                                turnManagment.JToAnswer = j + 2;
                                return turnManagment;
                            }
                        }
                        if (board[i + 1][j+1] == 0)
                        {
                            turnManagment.IFromAnswer = i;
                            turnManagment.JFromAnswer = j;
                            turnManagment.IToAnswer = i + 1;
                            turnManagment.JToAnswer = j + 1;
                            return turnManagment;
                        }
                    }

                    if (board[i][j] == 1 && turnManagment.IsInsideBorders(boardRows, boardColumns, i + 1, j - 1))
                    {
                        if (board[i + 1][j - 1] == 2 && turnManagment.IsInsideBorders(boardRows, boardColumns, i + 2, j - 2))
                        {
                            if (board[i + 2][j - 2] == 0)
                            {
                                //Eat from left
                                turnManagment.IFromAnswer = i;
                                turnManagment.JFromAnswer = j;
                                turnManagment.IToAnswer = i + 2;
                                turnManagment.JToAnswer = j - 2;
                                return turnManagment;
                            }
                        }
                        if (board[i + 1][j - 1] == 0)
                        {
                            turnManagment.IFromAnswer = i;
                            turnManagment.JFromAnswer = j;
                            turnManagment.IToAnswer = i + 1;
                            turnManagment.JToAnswer = j - 1;
                            return turnManagment;
                        }
                    }
                }
            }
            turnManagment.EndOfRoad = true;
            return turnManagment;
        }


    }
}
