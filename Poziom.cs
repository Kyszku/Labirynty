namespace Labirynty
{
    public class Poziom
    {
        // Łatwy poziom - mała macierz z małą liczbą ścian
        public static Poziom Latwy = new Poziom(new int[,]
        {
        {0, 1, 0, 0, 0},
        {0, 1, 0, 1, 0},
        {0, 0, 0, 1, 0},
        {1, 1, 0, 0, 0},
        {0, 0, 0, 1, 0}
        });

        // Średni poziom - średnia macierz z większą ilością ścian
        public static Poziom Sredni = new Poziom(new int[,]
        {
        {0, 1, 0, 1, 0, 0},
        {0, 1, 0, 1, 0, 1},
        {0, 1, 0, 0, 0, 0},
        {1, 0, 1, 0, 1, 0},
        {0, 0, 0, 0, 1, 0},
        {0, 1, 1, 0, 0, 0}
        });

        // Trudny poziom - duża macierz z wieloma ścianami
        public static Poziom Trudny = new Poziom(new int[,]
        {
        {0, 1, 1, 1, 1, 1, 0},
        {0, 0, 0, 1, 0, 1, 0},
        {1, 1, 0, 0, 0, 1, 0},
        {1, 1, 0, 1, 1, 0, 0},
        {0, 0, 0, 1, 0, 1, 0},
        {1, 0, 1, 1, 0, 1, 0},
        {0, 0, 0, 0, 1, 0, 0}
        });

        public int[,] Macierz {get; set; }
        public Poziom(int[,] macierzpoziomu)
        {
            Macierz = macierzpoziomu;
        }
        public int Szerokosc => Macierz.GetLength(0);
        public int Wysokosc => Macierz.GetLength(1);
    }
}
