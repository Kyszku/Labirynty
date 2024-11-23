namespace Labirynty
{
    public class Gracz
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Gracz(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Rusz(Keys key, int maksX, int maksY)
        {
            //int nowyX = graczX;
            //int nowyY = graczY;
            switch (key)
            {
                case Keys.W: if (Y > 0) Y--; break;
                case Keys.S: if (Y < maksY - 1) Y++; break;
                case Keys.A: if (X > 0) X--; break;
                case Keys.D: if (X < maksX - 1) X++; break;
            }
        }
    }
}