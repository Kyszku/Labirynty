namespace Labirynty{
    public class Poziom{
        public int[,] Macierz { get; set; } // Macierz definiująca poziom
        public (int X, int Y) Start { get; set; } // Punkt startowy
        public (int X, int Y) End { get; set; } // Punkt końcowy
        public List<(int X, int Y)> Checkpoints { get; private set; } // Lista punktów kontrolnych
        private readonly List<(int X, int Y)> DomyslneCheckpoints; // Lista oryginalna
        public int TimeLevel { get; private set; } // Czas w sekundach

        /*
        0,1,2,3,4,5...n
        1
        2
        3
        .
        .
        .
        n       
        */

        // Łatwy poziom - mała macierz z małą liczbą ścian
        public static Poziom Latwy = new Poziom(new int[,]
        {
        {0, 1, 0, 0, 0},
        {0, 1, 0, 1, 0},
        {0, 0, 0, 1, 0},
        {1, 1, 0, 0, 0},
        {0, 0, 0, 1, 0}
        },
            start: (0,0),
            end:(3,3),
            timeLevel: 10,
            checkpoints:( new List<(int X, int Y)> {(2, 2)})            
        );

        // Średni poziom - średnia macierz z większą ilością ścian
        public static Poziom Sredni = new Poziom(new int[,]
        {
        {0, 1, 0, 1, 0, 0},
        {0, 1, 0, 1, 0, 1},
        {0, 1, 0, 0, 0, 0},
        {1, 0, 1, 0, 1, 0},
        {0, 0, 0, 0, 1, 0},
        {0, 1, 1, 0, 0, 0}
        },
            start: (1, 1),
            end: (3, 3),
            timeLevel: 10,
            checkpoints: (new List<(int X, int Y)> { (2, 2) })
        );

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
        },
            start: (1, 1),
            end: (3, 3),
            timeLevel: 10,
            checkpoints: (new List<(int X, int Y)> { (2, 2) })
        );
        public Poziom(int[,] macierzpoziomu, (int X, int Y) start, (int X, int Y) end, int timeLevel, List<(int X, int Y)> checkpoints){
            Macierz = macierzpoziomu;
            Start = start;
            End = end;
            TimeLevel = timeLevel;
            DomyslneCheckpoints = checkpoints ?? new List<(int X, int Y)>();
            PrzywrocCheckpointy();
        }
        // Przywraca checkpointy do stanu domyślnego
        public void PrzywrocCheckpointy(){
            Checkpoints = new List<(int X, int Y)>(DomyslneCheckpoints);
        }
        public int Szerokosc => Macierz.GetLength(0);
        public int Wysokosc => Macierz.GetLength(1);
    }
}
