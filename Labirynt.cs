namespace Labirynty
{
    public class Labirynt
    {
        /** Reprezentacja labiryntu w postaci dwuwymiarowej tablicy */
        public int[,] Siatka { get; private set; }
        /** Szerokosc labiryntu */
        public int Szerokosc { get; private set; }
        /** Wysokosc labiryntu */
        public int Wysokosc { get; private set; }
        /** 
         * Konstruktor klasy Labirynt 
         * @param szerokosc - szerokosc labiryntu
         * @param wysokosc - wysokosc labiryntu
         */
        public Labirynt(int szerokosc, int wysokosc)
        {
            Szerokosc = szerokosc;
            Wysokosc = wysokosc;
            Siatka = new int[szerokosc, wysokosc];
        }
        /** 
         * Metoda generujaca labirynt na podstawie podanej macierzy 
         * @param macierz - macierz definiujaca uklad labiryntu
         */
        public void GenerujLabirynt(int[,] macierz)
        {
            Siatka = macierz; //Ustawienie siatki na macierz
        }
        /** 
         * Metoda rysujaca labirynt na obiekcie Graphics 
         * @param g - obiekt Graphics do rysowania
         * @param szerokoscKomorki - szerokosc pojedynczej komorki labiryntu
         * @param wysokoscKomorki - wysokosc pojedynczej komorki labiryntu
         * @param start - punkt startowy w labiryncie
         * @param end - punkt koncowy w labiryncie
         * @param checkpoints - slownik checkpointow i ich stanu
         */
        public void RysujLabirynt(Graphics g, int szerokoscKomorki, int wysokoscKomorki, (int X, int Y) start, (int X, int Y) end, Dictionary<(int X, int Y), bool> checkpoints)
        {
            for (int x = 0; x < Szerokosc; x++)
            {
                for (int y = 0; y < Wysokosc; y++)
                {
                    if (Siatka[x, y] == 1) /* sciana */
                    {
                        g.FillRectangle(Brushes.Black, x * szerokoscKomorki, y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                    }
                    else /* Pusta przestrzen */
                    {
                        g.FillRectangle(Brushes.White, x * szerokoscKomorki, y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                    }
                }
            }
            // Rysowanie punktu poczatkowego
            g.FillRectangle(Brushes.Green, start.X * szerokoscKomorki, start.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            // Rysowanie punktu koncowego
            g.FillRectangle(Brushes.Red, end.X * szerokoscKomorki, end.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            foreach (var checkpoint in checkpoints)
            {
                var kolor = checkpoint.Value ? Brushes.Gray : Brushes.Yellow; // Szary dla odwiedzonych
                g.FillRectangle(kolor, checkpoint.Key.X * szerokoscKomorki, checkpoint.Key.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            }
        }
    }
}