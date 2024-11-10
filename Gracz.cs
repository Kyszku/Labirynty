using System.Windows.Forms;
namespace Labirynty
{
    public class Gracz
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Gracz(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public void Przesun(Keys key, int maxX, int maxY)
        {
            switch (key)
            {
                case Keys.Up:
                    if (Y > 0) Y--;
                    break;
                case Keys.Down:
                    if (Y < maxY - 1) Y++;
                    break;
                case Keys.Left:
                    if (X > 0) X--;
                    break;
                case Keys.Right:
                    if (X < maxX - 1) X++;
                    break;
            }
        }
    }
}
