using System;
using System.Windows.Forms;

namespace Labirynty{
    public partial class Form1 : Form
    {
        private Labirynt labirynt; // Przechowywanie labiryntu
        private Poziom poziom = Poziom.Easy; // Aktualny poziom trudności
        private Gracz gracz;   // Obiekt reprezentujący gracza
        private int szerokosc; // Szerokość labiryntu
        private int wysokosc;  // Wysokość labiryntu;
        private int pozostalyCzas; // Liczba sekund pozostałych na dany poziom
        private int czasPrezentacji;
        private double wynikPoprawny;
        private bool graZatrzymana = false; // Flaga pauzy
        public Form1()
        {
            InitializeComponent();
        }

        private void UstawPoziom(Poziom poziom)
        {
            if (timerLevel != null)
            {
                timerLevel.Stop();
                timerLevel.Dispose();
            }

            if (timerShow != null)
            {
                timerShow.Stop();
                timerShow.Dispose();
            }

            this.poziom = poziom;
            poziom.PrzywrocCheckpointy(); // Przywrócenie checkpointów
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc);
            labirynt.GenerujLabirynt(poziom.Macierz);
            gracz = new Gracz(poziom.Start.X, poziom.Start.Y);

            // Wyświetlenie labiryntu dla gracza przez 30 sekund
            czasPrezentacji = poziom.TimeShow;
            text_poziom_Label.Text = $"Poziom: {poziom.NameLevel}";
            czasLabel.Text = $"Czas na zapamiętanie: {czasPrezentacji}s";
            panelGry.Invalidate(); // Rysowanie labiryntu

            // Ustawienie timera prezentacji
            timerShow = new System.Windows.Forms.Timer();
            timerShow.Interval = 1000; // 1 sekunda
            timerShow.Tick += timerShow_Tick;
            timerShow.Start();

            // Ukrycie gracza na czas prezentacji
            gracz = null;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Focus();
            panelGry.Invalidate();  // Odśwież panel, aby narysować nowy labirynt
            PanelMenuMain.Hide();
            PanelPoziomy.Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void retrunButton_Click(object sender, EventArgs e)
        {
            PanelPoziomy.Hide();
            PanelMenuMain.Show();
        }

        private void controlsButton_Click(object sender, EventArgs e)
        {
            panelGry.Hide();
            PanelMenuMain.Hide();
            PanelPoziomy.Hide();
            czasPrezentacji = 0;
            pozostalyCzas = 0;
            czasLabel.Hide();
            text_poziom_Label.Hide();
            PanelSterowanieMenuMain.Show();
        }

        private void returnMenu2_Click(object sender, EventArgs e)
        {
            PanelSterowanieMenuMain.Hide();
            panelGry.Hide();
            text_poziom_Label.Hide();
            czasLabel.Hide();
            PanelMenuMain.Show();
            pozostalyCzas = 0;
            czasPrezentacji = 0;
        }
        public bool CzyMoznaRuszac(int x, int y, Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    if (y > 0 && labirynt.Siatka[x, y - 1] == 0) // Sprawdzamy w górę
                        return true;
                    break;

                case Keys.S:
                    if (y < labirynt.Wysokosc - 1 && labirynt.Siatka[x, y + 1] == 0) // Sprawdzamy w dół
                        return true;
                    break;

                case Keys.A:
                    if (x > 0 && labirynt.Siatka[x - 1, y] == 0) // Sprawdzamy w lewo
                        return true;
                    break;

                case Keys.D:
                    if (x < labirynt.Szerokosc - 1 && labirynt.Siatka[x + 1, y] == 0) // Sprawdzamy w prawo
                        return true;
                    break;
            }
            return false; // Zwracamy false, jeśli nie ma drogi
        }

        private void CzyWszedlWCheckpoint()
        {
            if (poziom.Checkpoints.ContainsKey((gracz.X, gracz.Y)) && !poziom.Checkpoints[(gracz.X, gracz.Y)])
            {
                poziom.Checkpoints[(gracz.X, gracz.Y)] = true; // Oznacz checkpoint jako odwiedzony
                WyswietlZadanieMatematyczne(); // Wyświetlenie zadania matematycznego
            }
        }

        private void CzyGraczUkonczylPoziom()
        {
            if (gracz.X == poziom.End.X && gracz.Y == poziom.End.Y)
            {
                timerLevel.Stop(); // Zatrzymujemy timer
                MessageBox.Show("Gratulacje! Ukończyłeś poziom!", "Sukces");
                PanelPoziomy.Show(); // Przejdź do menu poziomów
                text_poziom_Label.Hide();
                czasLabel.Hide();
                UkryjZadanie();
                panelGry.Hide();
            }
        }

        private void WyswietlZadanieMatematyczne()
        {
            wynikPoprawny = 0;
            panelZadanie.Show();
            Random random = new Random();
            string pytanie = "";

            // Generowanie zadania matematycznego
            if (poziom == Poziom.Easy)
            {
                int a = random.Next(1, 20);
                int b = random.Next(1, 20);
                if (random.Next(2) == 0)
                {
                    pytanie = $"{a} + {b} = ?";
                    wynikPoprawny = a + b;
                }
                else
                {
                    pytanie = $"{a} - {b} = ?";
                    wynikPoprawny = a - b;
                }
            }
            else if (poziom == Poziom.Meduim)
            {
                int a = random.Next(1, 10);
                int b = random.Next(1, 10);
                if (random.Next(2) == 0)
                {
                    pytanie = $"{a} * {b} = ?";
                    wynikPoprawny = a * b;
                }
                else
                {
                    pytanie = $"{a} / {b} = ?";
                    wynikPoprawny = Math.Round((double)a / b, 2);
                }
            }
            else if (poziom == Poziom.Hard)
            {
                int a = random.Next(1, 10);
                if (random.Next(2) == 0)
                {
                    pytanie = $"{a}^2 = ?";
                    wynikPoprawny = Math.Pow(a, 2);
                }
                else
                {
                    int b = a * a;
                    pytanie = $"√{b} = ?";
                    wynikPoprawny = Math.Sqrt(b);
                }
            }

            // Wyświetlenie pytania i aktywowanie kontrolek
            labelZadanie.Text = pytanie;
            labelZadanie.Visible = true;
            textBoxZadanie.Visible = true;
            buttonSprawdzZadanie.Visible = true;
        }
        private void buttonSprawdzZadanie_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxZadanie.Text, out double wynikUzytkownika) &&
                Math.Abs(wynikUzytkownika - wynikPoprawny) < 0.01)
            {
                MessageBox.Show("Poprawna odpowiedź! Możesz kontynuować.", "Sukces");
                UkryjZadanie(); // Ukryj zadanie po poprawnej odpowiedzi
            }
            else
            {
                MessageBox.Show("Błędna odpowiedź. Spróbuj ponownie!", "Błąd");
            }
        }
        // Metoda do ukrywania kontrolek zadania matematycznego
        private void UkryjZadanie()
        {
            panelZadanie.Hide();
            labelZadanie.Visible = false;
            textBoxZadanie.Visible = false;
            buttonSprawdzZadanie.Visible = false;
            textBoxZadanie.Text = null;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Sprawdzenie, czy obiekt gracz jest zainicjalizowany
            if (gracz != null)
            {
                if (CzyMoznaRuszac(gracz.X, gracz.Y, keyData))
                {
                    // Wywolanie metody przesuniecie gracza na podstawie wcisnietego klawisza
                    gracz.Rusz(keyData, poziom.Szerokosc, poziom.Wysokosc);
                    // Sprawdzamy, czy gracz wszedł w checkpoint
                    CzyWszedlWCheckpoint();
                    CzyGraczUkonczylPoziom();
                    // Odswieza panel, aby zaktualizowaæ pozycje gracza
                    panelGry.Invalidate();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void panelGry_Paint(object sender, PaintEventArgs e)
        {
            if (labirynt != null && poziom != null)
            {
                int szerokoscKomorki = panelGry.Width / poziom.Szerokosc;
                int wysokoscKomorki = panelGry.Height / poziom.Wysokosc;
                // Wywołanie metody rysującej labirynt
                labirynt.RysujLabirynt(e.Graphics, szerokoscKomorki, wysokoscKomorki, poziom.Start, poziom.End, poziom.Checkpoints);
                // Rysowanie gracza
                if (gracz != null)
                {
                    e.Graphics.FillRectangle(Brushes.Blue, gracz.X * szerokoscKomorki, gracz.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                }
            }
        }

        private void easyLevel_Click(object sender, EventArgs e)
        {
            panelPodczasgry.Hide();
            UstawPoziom(Poziom.Easy);
            text_poziom_Label.Hide();
            panelGry.Hide();
            MessageBox.Show("Ustawiono poziom: Łatwy\nMasz 30 sekund na zapamiętanie układu labiryntu. Po tym czasie rozpocznie się poziom.\", \"Zapamiętaj Labirynt");
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
        }

        private void mediumLevel_Click(object sender, EventArgs e)
        {
            panelPodczasgry.Hide();
            UstawPoziom(Poziom.Meduim);
            text_poziom_Label.Hide();
            panelGry.Hide();
            MessageBox.Show("Ustawiono poziom: Średni\nMasz 30 sekund na zapamiętanie układu labiryntu. Po tym czasie rozpocznie się poziom.\", \"Zapamiętaj Labirynt");
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
        }

        private void hardLevel_Click(object sender, EventArgs e)
        {
            panelPodczasgry.Hide();
            UstawPoziom(Poziom.Hard);
            text_poziom_Label.Hide();
            panelGry.Hide();
            MessageBox.Show("Ustawiono poziom: Trudny\nMasz 30 sekund na zapamiętanie układu labiryntu. Po tym czasie rozpocznie się poziom.\", \"Zapamiętaj Labirynt");
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
        }

        private void timerLevel_Tick(object sender, EventArgs e)
        {
            pozostalyCzas--;
            // Aktualizacja etykiety
            czasLabel.Text = $"Pozostały czas: {pozostalyCzas}s";
            // Sprawdzenie, czy czas się skończył
            if (pozostalyCzas <= 0)
            {
                timerLevel.Stop();
                MessageBox.Show("Czas minął! Przegrałeś.", "Koniec gry");
                UkryjZadanie();
                RestartujPoziom();
            }
        }
        private void RestartujPoziom()
        {
            if (timerLevel != null)
            {
                timerLevel.Stop();
                timerLevel.Dispose();
            }
            //timerLevel.Stop(); // Zatrzymujemy licznik czasu
            poziom.PrzywrocCheckpointy();
            UstawPoziom(poziom); // Przywracamy aktualny poziom do stanu początkowego
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            czasPrezentacji--;

            if (czasPrezentacji <= 0)
            {
                timerShow.Stop();
                czasLabel.Text = $"Pozostały czas: {poziom.TimeLevel}s";

                // Po zakończeniu prezentacji przywróć gracza i rozpocznij poziom
                gracz = new Gracz(poziom.Start.X, poziom.Start.Y);
                timerLevel = new System.Windows.Forms.Timer();
                timerLevel.Interval = 1000; // 1 sekunda
                timerLevel.Tick += timerLevel_Tick;
                pozostalyCzas = poziom.TimeLevel;
                timerLevel.Start();
            }
            else
            {
                czasLabel.Text = $"Czas na zapamiętanie: {czasPrezentacji}s";
            }

            panelGry.Invalidate(); // Odśwież panel
        }

        private void returnLevel_Click(object sender, EventArgs e)
        {
            panelGry.Hide();
            PanelSterowanieMenuMain.Hide();
            PanelPoziomy.Show();
            czasPrezentacji = 0;
            pozostalyCzas = 0;
            text_poziom_Label.Hide();
            czasLabel.Hide();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (graZatrzymana)
            {
                // Wznawiamy grę
                WznowWlasciwyTimer();
                pauseButton.Text = "Pauza"; // Zmień tekst przycisku
                graZatrzymana = false;
            }
            else
            {
                // Zatrzymujemy grę
                ZatrzymajWszystkieTimery(); // Zatrzymaj liczniki czasu
                MessageBox.Show("Gra jest wstrzymana. Kliknij przycisk 'Wznów', aby kontynuować.", "Pauza");
                pauseButton.Text = "Wznów"; // Zmień tekst przycisku
                graZatrzymana = true;
            }
        }

        private void returnLevel2_Click(object sender, EventArgs e)
        {
            PanelMenuMain.Hide();
            PanelPoziomy.Show();
        }

        private void exitButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void ZatrzymajWszystkieTimery()
        {
            if (timerShow != null && timerShow.Enabled) timerShow.Stop();
            if (timerLevel != null && timerLevel.Enabled) timerLevel.Stop();
        }
        private void WznowWlasciwyTimer()
        {
            if (czasPrezentacji > 0 && timerShow != null) timerShow.Start();
            else if (pozostalyCzas > 0 && timerLevel != null) timerLevel.Start();
        }


        private void controlsButton2_Click(object sender, EventArgs e)
        {
            ZatrzymajWszystkieTimery();
            panelGry.Hide();
            PanelMenuMain.Hide();
            PanelPoziomy.Hide();
            czasLabel.Hide();
            text_poziom_Label.Hide();
            PanelSterowanieGra.Show();
        }

        private void returnMenu3_Click(object sender, EventArgs e)
        {
            PanelSterowanieGra.Hide();
            PanelPoziomy.Show();
        }
    }
}
