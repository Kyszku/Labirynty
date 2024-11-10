namespace Labirynty
{
    public class Poziom
    {
        public int Rozmiar { get; set; }
        public int Czas { get; set; }

        public Poziom(int rozmiar, int czas)
        {
            Rozmiar = rozmiar;
            Czas = czas;
        }

        public static Poziom Latwy => new Poziom(10, 60);    // Labirynt 10x10, czas 60 sekund
        public static Poziom Sredni => new Poziom(15, 45);   // Labirynt 15x15, czas 45 sekund
        public static Poziom Trudny => new Poziom(20, 30);   // Labirynt 20x20, czas 30 sekund
    }
}