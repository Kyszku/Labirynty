namespace Labirynty
{
    public class Poziom
    {
        /** Macierz definiujaca poziom labiryntu */
        public int[,] Macierz { get; set; }
        /** Punkt startowy w labiryncie */
        public (int X, int Y) Start { get; set; }
        /** Punkt koncowy w labiryncie */
        public (int X, int Y) End { get; set; }
        /** Slownik checkpointow i ich stanu (odwiedzony/nieodwiedzony) */
        public Dictionary<(int X, int Y), bool> Checkpoints { get; private set; }
        /** Domyslne checkpointy */
        private readonly Dictionary<(int X, int Y), bool> DomyslneCheckpoints;
        /** Czas na ukonczenie poziomu w sekundach */
        public int TimeLevel { get; private set; }
        /** Czas na zapamietanie ukladu labiryntu w sekundach */
        public int TimeShow { get; private set; }
        /** Nazwa poziomu */
        public string NameLevel { get; private set; }
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

        /** Latwy poziom - mala macierz z mala liczba scian */
        public static Poziom Easy = new Poziom(new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1},
            {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1},
            {1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
            {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1},
            {1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        },
            start: (1, 1),
            end: (9, 9),
            timeLevel: 45,
            timeShow: 20,
            checkpoints: new List<(int X, int Y)> { (5, 3), (3, 9) },
            namelevel: "Łatwy"
        );

        /** Sredni poziom - Srednia macierz z wieksza iloscia scian */
        public static Poziom Meduim = new Poziom(new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
            {1, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1},
            {1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1},
            {1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1},
            {1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
            {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1},
            {1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        },
            start: (1, 8),
            end: (1, 1),
            timeLevel: 75,
            timeShow: 30,
            checkpoints: new List<(int X, int Y)> { (4, 8), (9, 5), (14, 5), (6,1) },
            namelevel: "Średni"
        );

        /** Trudny poziom - duza macierz z wieloma scianami */
        public static Poziom Hard = new Poziom(new int[,]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1},
            {1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1},
            {1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 1, 1},
            {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1},
            {1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1},
            {1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1},
            {1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 1},
            {1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1},
            {1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1},
            {1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1},
            {1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 1},
            {1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
            {1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1},
            {1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1},
            {1, 0, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1},
            {1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        },
            start: (19, 1),
            end: (16, 7),
            timeLevel: 90,
            timeShow: 30,
            checkpoints: new List<(int X, int Y)> { (6, 2), (8, 6), (2, 7), (8, 13), (15, 9), (19, 17) },
            namelevel: "Trudny"
        );
        /** 
         * Konstruktor klasy Poziom 
         * @param macierzpoziomu - macierz definiujaca poziom
         * @param start - punkt startowy
         * @param end - punkt koncowy
         * @param timeLevel - czas na ukonczenie poziomu
         * @param timeShow - czas na zapamietanie ukladu labiryntu
         * @param checkpoints - lista checkpointow
         * @param nameLevel - nazwa poziomu
         */
        public Poziom(int[,] macierzpoziomu, (int X, int Y) start, (int X, int Y) end, int timeLevel, int timeShow, List<(int X, int Y)> checkpoints, string namelevel)
        {
            Macierz = macierzpoziomu; // Ustawienie macierzy poziomu
            Start = start;  // Ustawienie punktu startowego
            End = end;  // Ustawienie punktu koncowego 
            TimeLevel = timeLevel;  // Ustawienie czasu na ukonczenie poziomu
            TimeShow = timeShow;    // Ustawienie czasu na zapamietanie labiryntu
            NameLevel = namelevel;  // Ustawienie nazwy poziomu
            DomyslneCheckpoints = checkpoints.ToDictionary(cp => cp, cp => false);  // Tworzenie slownika checkpointow z zawartoscia (nieodwiedzony)
            PrzywrocCheckpointy();

        }
        /** 
        * Przywraca checkpointy do stanu domyslnego (ustawia wszystkie checkpointy jako nieodwiedzone).
        */
        public void PrzywrocCheckpointy()
        {
            Checkpoints = new Dictionary<(int X, int Y), bool>(DomyslneCheckpoints);
        }
        /** Szerokosc labiryntu (liczba kolumn w macierzy) */
        public int Szerokosc => Macierz.GetLength(0);
        /** Wysokosc labiryntu (liczba wierszy w macierzy) */
        public int Wysokosc => Macierz.GetLength(1);
    }
}