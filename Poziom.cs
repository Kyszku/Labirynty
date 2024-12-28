namespace Labirynty{
    public class Poziom{
        public int[,] Macierz { get; set; } /** Macierz definiująca poziom labiryntu */
        public (int X, int Y) Start { get; set; } /** Punkt startowy w labiryncie */
        public (int X, int Y) End { get; set; } /** Punkt końcowy w labiryncie */
        public Dictionary<(int X, int Y), bool> Checkpoints { get; private set; } /** Słownik checkpointów i ich stanu (odwiedzony/nieodwiedzony) */
        private readonly Dictionary<(int X, int Y), bool> DomyslneCheckpoints; /** Domyślne checkpointy */
        public int TimeLevel { get; private set; } /** Czas na ukończenie poziomu w sekundach */
        public int TimeShow { get; private set; } /** Czas na zapamiętanie układu labiryntu w sekundach */
        public string NameLevel {get; private set; } /** Nazwa poziomu */
        /* kolumna w maicerzy to wiersz labiryntu
        0,1,2,3,4,5...n
        1
        2
        3
        .
        .
        .
        n       
        */

        /** Łatwy poziom - mała macierz z małą liczbą ścian */
        public static Poziom Easy = new Poziom(new int[,]
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
            checkpoints: new List<(int X, int Y)> {(2, 2)},
            namelevel: "Latwy"
        );

        /** Średni poziom - średnia macierz z większą ilością ścian */
        public static Poziom Meduim = new Poziom(new int[,]
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
            checkpoints: new List<(int X, int Y)> { (2, 2) },
            namelevel: "Sredni"
        );

        /** Trudny poziom - duża macierz z wieloma ścianami */
        public static Poziom Hard = new Poziom(new int[,]
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
            checkpoints: new List<(int X, int Y)> { (2, 2) },
            namelevel: "Trudny"
        );
        /** 
         * Konstruktor klasy Poziom 
         * @param macierzpoziomu - macierz definiująca poziom
         * @param start - punkt startowy
         * * @param end - punkt końcowy
         * @param timeLevel - czas na ukończenie poziomu
         * @param timeShow - czas na zapamiętanie układu labiryntu
         * @param checkpoints - lista checkpointów
         * @param nameLevel - nazwa poziomu
         */
        public Poziom(int[,] macierzpoziomu, (int X, int Y) start, (int X, int Y) end, int timeLevel, int timeShow, List<(int X, int Y)> checkpoints, string namelevel)
        {
            Macierz = macierzpoziomu; // Ustawienie macierzy poziomu
            Start = start;  // Ustawienie punktu startowego
            End = end;  // Ustawienie punktu końcowego 
            TimeLevel = timeLevel;  // Ustawienie czasu na ukończenie poziomu
            TimeShow = timeShow;    // Ustawienie czasu na zapamiętanie labiryntu
            NameLevel = namelevel;  // Ustawienie nazwy poziomu
            DomyslneCheckpoints = checkpoints.ToDictionary(cp => cp, cp => false);  // Tworzenie słownika checkpointów z zawartością (nieodwiedzony)
            PrzywrocCheckpointy();
            
        }
        // Przywraca checkpointy do stanu domyślnego
        /** 
        * Przywraca checkpointy do stanu domyślnego 
        * (ustawia wszystkie checkpointy jako nieodwiedzone).
        */
        public void PrzywrocCheckpointy(){
            Checkpoints = new Dictionary<(int X, int Y), bool>(DomyslneCheckpoints);
        }
        public int Szerokosc => Macierz.GetLength(0);   /** Szerokość labiryntu (liczba kolumn w macierzy) */
        public int Wysokosc => Macierz.GetLength(1);    /** Wysokość labiryntu (liczba wierszy w macierzy) */
    }
}