using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Model
{
    public class TblPlayers
    {
        public int Id { get; set; }

        [StringLength(10)]
        [Required]
        public string Name { get; set; }

        [StringLength(10)]
        [Required]
        public string Password { get; set; }
        public int NumOfGames { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
        [StringLength(30)]
        [Required]
        public string Email { get; set; }
    }
}
