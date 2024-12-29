namespace Labirynty
{
    public class Gracz
    {
        /** Wspolrzedna X gracza w labiryncie */
        public int X { get; set; }
        /** Wspolrzedna Y gracza w labiryncie */
        public int Y { get; set; }
        /** 
         * Konstruktor klasy Gracz 
         * @param x - poczatkowa wspolrzedna X gracza
         * @param y - poczatkowa wspolrzedna Y gracza
         */
        public Gracz(int x, int y)
        {
            X = x; // Ustawienie wspolrzednej X
            Y = y; // Ustawienie wspolrzednej Y
        }
        /** 
         * Metoda do przesuwania gracza w labiryncie 
         * @param key - klawisz, ktory zostal nacisniety
         * @param maksX - maksymalna wartosc wspolrzednej X
         * @param maksY - maksymalna wartosc wspolrzednej Y
         */
        public void Rusz(Keys key, int maksX, int maksY)
        {
            switch (key)
            {
                case Keys.W:
                    if (Y > 0) Y--; // Przesuniecie w gore
                    break;
                case Keys.S:
                    if (Y < maksY - 1) Y++; // Przesuniecie w dol
                    break;
                case Keys.A:
                    if (X > 0) X--; // Przesuniecie w lewo
                    break;
                case Keys.D:
                    if (X < maksX - 1) X++; // Przesuniecie w prawo
                    break;
            }
        }
    }
}