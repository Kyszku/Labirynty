namespace Labirynty
{
    public class Gracz
    {
        /** Współrzędna X gracza w labiryncie */
        public int X { get; set; }
        /** Współrzędna Y gracza w labiryncie */
        public int Y { get; set; }
        /** 
         * Konstruktor klasy Gracz 
         * @param x - początkowa współrzędna X gracza
         * @param y - początkowa współrzędna Y gracza
         */
        public Gracz(int x, int y)
        {
            X = x; // Ustawienie współrzędnej X
            Y = y; // Ustawienie współrzędnej Y
        }
        /** 
         * Metoda do przesuwania gracza w labiryncie 
         * @param key - klawisz, który został naciśnięty
         * @param maksX - maksymalna wartość współrzędnej X
         * @param maksY - maksymalna wartość współrzędnej Y
         */
        public void Rusz(Keys key, int maksX, int maksY)
        {
            switch (key)
            {
                case Keys.W:
                    if (Y > 0) Y--; // Przesunięcie w górę
                    break;
                case Keys.S:
                    if (Y < maksY - 1) Y++; // Przesunięcie w dół
                    break;
                case Keys.A:
                    if (X > 0) X--; // Przesunięcie w lewo
                    break;
                case Keys.D:
                    if (X < maksX - 1) X++; // Przesunięcie w prawo
                    break;
            }
        }
    }
}