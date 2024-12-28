namespace Labirynty
{
    public class Labirynt
    {
        /** Reprezentacja labiryntu w postaci dwuwymiarowej tablicy */
        public int[,] Siatka { get; private set; }
        /** Szerokość labiryntu */
        public int Szerokosc { get; private set; }
        /** Wysokość labiryntu */
        public int Wysokosc { get; private set; }
        /** 
         * Konstruktor klasy Labirynt 
         * @param szerokosc - szerokość labiryntu
         * @param wysokosc - wysokość labiryntu
         */
        public Labirynt(int szerokosc, int wysokosc)
        {
            Szerokosc = szerokosc;
            Wysokosc = wysokosc;
            Siatka = new int[szerokosc, wysokosc];
        }
        /** 
         * Metoda generująca labirynt na podstawie podanej macierzy 
         * @param macierz - macierz definiująca układ labiryntu
         */
        public void GenerujLabirynt(int[,] macierz)
        {
            Siatka = macierz; //Ustawienie siatki na macierz
        }
        /** 
         * Metoda rysująca labirynt na obiekcie Graphics 
         * @param g - obiekt Graphics do rysowania
         * @param szerokoscKomorki - szerokość pojedynczej komórki labiryntu
         * @param wysokoscKomorki - wysokość pojedynczej komórki labiryntu
         * @param start - punkt startowy w labiryncie
         * @param end - punkt końcowy w labiryncie
         * @param checkpoints - słownik checkpointów i ich stanu
         */
        public void RysujLabirynt(Graphics g, int szerokoscKomorki, int wysokoscKomorki, (int X, int Y) start, (int X, int Y) end, Dictionary<(int X, int Y), bool> checkpoints)
        {
            for (int x = 0; x < Szerokosc; x++)
            {
                for (int y = 0; y < Wysokosc; y++)
                {
                    if (Siatka[x, y] == 1) /* Ściana */
                    {
                        g.FillRectangle(Brushes.Black, x * szerokoscKomorki, y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                    }
                    else /* Pusta przestrzeń */
                    {
                        g.FillRectangle(Brushes.White, x * szerokoscKomorki, y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                    }
                }
            }
            // Rysowanie punktu początkowego
            g.FillRectangle(Brushes.Green, start.X * szerokoscKomorki, start.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            // Rysowanie punktu końcowego
            g.FillRectangle(Brushes.Red, end.X * szerokoscKomorki, end.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            foreach (var checkpoint in checkpoints)
            {
                var kolor = checkpoint.Value ? Brushes.Gray : Brushes.Yellow; // Szary dla odwiedzonych
                g.FillRectangle(kolor, checkpoint.Key.X * szerokoscKomorki, checkpoint.Key.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            }
        }
    }
}