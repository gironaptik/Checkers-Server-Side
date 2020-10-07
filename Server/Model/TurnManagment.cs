namespace Server.Model
{
    public class TurnManagment
    {
        public int IFromAnswer { get; set; }
        public int JFromAnswer { get; set; }
        public int IToAnswer { get; set; }
        public int JToAnswer { get; set; }
        public bool EndOfRoad { get; set; }

        public bool IsInsideBorders(int boardRows, int boardColumns, int ti, int tj)
        {
            if (ti >= boardRows || tj >= boardColumns || ti < 0 || tj < 0)
            {
                return false;
            }
            return true;
        }

    }

}
