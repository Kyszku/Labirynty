namespace Labirynty
{
    public class Labirynt
    {
        public int[,] Siatka { get; private set; } // Reprezentacja labiryntu
        public int Szerokosc { get; private set; }
        public int Wysokosc { get; private set; }

        public Labirynt(int szerokosc, int wysokosc)
        {
            Szerokosc = szerokosc;
            Wysokosc = wysokosc;
            Siatka = new int[szerokosc, wysokosc];
        }

        public void GenerujLabirynt(int[,] macierz)
        {
            Siatka = macierz;
        }
        public void RysujLabirynt(Graphics g, int szerokoscKomorki, int wysokoscKomorki)
        {
            for (int x = 0; x < Szerokosc; x++)
            {
                for (int y = 0; y < Wysokosc; y++)
                {
                    if (Siatka[x, y] == 1) // Ściana
                    {
                        g.FillRectangle(Brushes.Black, x * szerokoscKomorki, y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                    }
                    else // Pusta przestrzeń
                    {
                        g.FillRectangle(Brushes.White, x * szerokoscKomorki, y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                    }
                }
            }
        }


    }
}
