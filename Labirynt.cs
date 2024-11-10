using System;
using System.Collections.Generic;
using System.Drawing;

namespace Labirynty
{
    public class Labirynt
    {
        private int wiersze, kolumny;
        private int[,] labirynt;
        private Random rand = new Random();

        public Labirynt(int wiersze, int kolumny)
        {
            this.wiersze = wiersze;
            this.kolumny = kolumny;
            labirynt = new int[wiersze, kolumny];
        }

        public void GenerujLabirynt()
        {
            Stack<(int, int)> stos = new Stack<(int, int)>();
            int startWiersz = rand.Next(wiersze);
            int startKolumna = rand.Next(kolumny);
            labirynt[startWiersz, startKolumna] = 1;

            stos.Push((startWiersz, startKolumna));

            while (stos.Count > 0)
            {
                var (w, k) = stos.Pop();
                var sasiedzi = PobierzNieodwiedzonychSasiadow(w, k);

                if (sasiedzi.Count > 0)
                {
                    stos.Push((w, k)); // Dodaj bieżący wierzchołek
                    var (nastepnyWiersz, nastepnaKolumna) = sasiedzi[rand.Next(sasiedzi.Count)];
                    labirynt[nastepnyWiersz, nastepnaKolumna] = 1; // Oznacz komórkę jako odwiedzoną
                    stos.Push((nastepnyWiersz, nastepnaKolumna));
                }
            }
        }

        private List<(int, int)> PobierzNieodwiedzonychSasiadow(int w, int k)
        {
            List<(int, int)> sasiedzi = new List<(int, int)>();

            if (w - 2 > 0 && labirynt[w - 2, k] == 0) sasiedzi.Add((w - 2, k));
            if (w + 2 < wiersze && labirynt[w + 2, k] == 0) sasiedzi.Add((w + 2, k));
            if (k - 2 > 0 && labirynt[w, k - 2] == 0) sasiedzi.Add((w, k - 2));
            if (k + 2 < kolumny && labirynt[w, k + 2] == 0) sasiedzi.Add((w, k + 2));

            return sasiedzi;
        }

        public void RysujLabirynt(Graphics g, int szerokoscKomorki, int wysokoscKomorki)
        {
            for (int w = 0; w < wiersze; w++)
            {
                for (int k = 0; k < kolumny; k++)
                {
                    if (labirynt[w, k] == 1)
                        g.FillRectangle(Brushes.White, k * szerokoscKomorki, w * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki); // Ścieżka
                    else
                        g.FillRectangle(Brushes.Black, k * szerokoscKomorki, w * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki); // Ściana
                }
            }
        }
    }
}