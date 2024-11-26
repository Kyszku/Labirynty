namespace Labirynty{
    public class Labirynt{
        public int[,] Siatka { get; private set; } // Reprezentacja labiryntu
        public int Szerokosc { get; private set; }
        public int Wysokosc { get; private set; }
        public Labirynt(int szerokosc, int wysokosc){
            Szerokosc = szerokosc;
            Wysokosc = wysokosc;
            Siatka = new int[szerokosc, wysokosc];
        }
        public void GenerujLabirynt(int[,] macierz){
            Siatka = macierz;
        }
        public void RysujLabirynt(Graphics g, int szerokoscKomorki, int wysokoscKomorki, (int X, int Y) start, (int X, int Y) end, List<(int X, int Y)> checkpoints)
        {
            for (int x = 0; x < Szerokosc; x++){
                for (int y = 0; y < Wysokosc; y++){
                    if (Siatka[x, y] == 1) /* Ściana */ {
                        g.FillRectangle(Brushes.Black, x * szerokoscKomorki, y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                    }
                    else /* Pusta przestrzeń */ {
                        g.FillRectangle(Brushes.White, x * szerokoscKomorki, y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                    }
                }
            }
            g.FillRectangle(Brushes.Green, start.X * szerokoscKomorki, start.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            // Rysowanie punktu końcowego
            g.FillRectangle(Brushes.Red, end.X * szerokoscKomorki, end.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            // Rysowanie checkpointów
            foreach (var checkpoint in checkpoints){
                g.FillRectangle(Brushes.Yellow, checkpoint.X * szerokoscKomorki, checkpoint.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            }
        }
    }
}
