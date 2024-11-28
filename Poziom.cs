namespace Labirynty{
    public class Poziom{
        public int[,] Macierz { get; set; } // Macierz definiująca poziom
        public (int X, int Y) Start { get; set; } // Punkt startowy
        public (int X, int Y) End { get; set; } // Punkt końcowy
        /*
        public List<(int X, int Y)> Checkpoints { get; private set; } // Lista punktów kontrolnych
        private readonly List<(int X, int Y)> DomyslneCheckpoints; // Lista oryginalna 
        */
        public Dictionary<(int X, int Y), bool> Checkpoints { get; private set; } // Checkpointy i ich stan
        private readonly Dictionary<(int X, int Y), bool> DomyslneCheckpoints; // Domyślne checkpointy

        public int TimeLevel { get; private set; } // Czas w sekundach
        public int TimeShow { get; private set; } //

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
            timeShow: 10,
            checkpoints: new List<(int X, int Y)> {(2, 2)}           
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
            timeShow: 10,
            checkpoints: new List<(int X, int Y)> { (2, 2) }
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
            timeShow: 10,
            checkpoints: new List<(int X, int Y)> { (2, 2) }
        );
        public Poziom(int[,] macierzpoziomu, (int X, int Y) start, (int X, int Y) end, int timeLevel, int timeShow, List<(int X, int Y)> checkpoints){
            Macierz = macierzpoziomu;
            Start = start;
            End = end;
            TimeLevel = timeLevel;
            TimeShow = timeShow;
            DomyslneCheckpoints = checkpoints.ToDictionary(cp => cp, cp => false); /*?? new List<(int X, int Y)>();*/
            PrzywrocCheckpointy();
        }
        // Przywraca checkpointy do stanu domyślnego
        public void PrzywrocCheckpointy(){
            Checkpoints = new Dictionary<(int X, int Y), bool>(DomyslneCheckpoints);
        }
        public int Szerokosc => Macierz.GetLength(0);
        public int Wysokosc => Macierz.GetLength(1);
    }
}
