using System;
using System.Windows.Forms;

namespace Labirynty{
    public partial class Form1 : Form
    {
        private Labirynt labirynt; /** Przechowuje instancję labiryntu, który jest aktualnie używany w grze */
        private Poziom poziom = Poziom.Easy; /** Reprezentuje aktualny poziom trudności gry, domyślnie ustawiony na poziom łatwy */
        private Gracz gracz;   /** Obiekt reprezentujący gracza w grze, przechowując jego pozycję */
        private int szerokosc; /** Szerokość labiryntu w jednostkach, używana do obliczeń graficznych */
        private int wysokosc;  /** Wysokość labiryntu w jednostkach, używana do obliczeń graficznych */
        private int pozostalyCzas; /** Liczba sekund pozostałych na dany poziom, używana do zarządzania czasem gry */
        private int czasPrezentacji; /** Czas prezentacji labiryntu przed rozpoczęciem poziomu, w sekundach */
        private double wynikPoprawny; /** Przechowuje poprawny wynik zadania matematycznego, które gracz musi rozwiązać */
        private bool graZatrzymana = false; /** Flaga wskazująca, czy gra jest wstrzymana (pauza) */

        /** Konstruktor klasy Form1, inicjalizuje komponenty formularza */
        public Form1()
        {
            InitializeComponent();
        }

        /** 
        * Ustawia poziom trudności gry oraz inicjalizuje odpowiednie elementy
        * 
        * @param poziom Poziom - nowy poziom trudności do ustawienia
        */
        private void UstawPoziom(Poziom poziom)
        {
            /** Zatrzymuje i zwalnia timer poziomu, jeśli jest aktywny */
            if (timerLevel != null)
            {
                timerLevel.Stop();
                timerLevel.Dispose();
            }
            /** Zatrzymuje i zwalnia timer prezentacji, jeśli jest aktywny */
            if (timerShow != null)
            {
                timerShow.Stop();
                timerShow.Dispose();
            }

            this.poziom = poziom;   /** Ustawia nowy poziom trudności */
            poziom.PrzywrocCheckpointy(); /** Przywraca checkpointy do stanu początkowego */
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc); /** Inicjalizuje nowy labirynt na podstawie szerokości i wysokości poziomu */
            labirynt.GenerujLabirynt(poziom.Macierz);   /** Generuje labirynt na podstawie macierzy poziomu */
            gracz = new Gracz(poziom.Start.X, poziom.Start.Y);  /** Inicjalizuje gracza na pozycji startowej poziomu */

            // Wyświetlenie labiryntu dla gracza przez dany czas
            czasPrezentacji = poziom.TimeShow; /** Inicjalizuje gracza na pozycji startowej poziomu */
            text_poziom_Label.Text = $"Poziom: {poziom.NameLevel}"; /** Inicjalizuje gracza na pozycji startowej poziomu */
            czasLabel.Text = $"Czas na zapamiętanie: {czasPrezentacji}s";   /** Inicjalizuje gracza na pozycji startowej poziomu */
            panelGry.Invalidate(); /** Odświeża panel gry, aby narysować nowy labirynt */

            // Ustawienie timera prezentacji
            timerShow = new System.Windows.Forms.Timer();   /** Odświeża panel gry, aby narysować nowy labirynt */
            timerShow.Interval = 1000; // 1 sekunda
            timerShow.Tick += timerShow_Tick;
            timerShow.Start();

            // Ukrycie gracza na czas prezentacji
            gracz = null;   /** Odświeża panel gry, aby narysować nowy labirynt */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku startowego.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void startButton_Click(object sender, EventArgs e)
        {
            this.Focus();   /** Ustawia fokus na formularz, aby umożliwić interakcję z nim */
            panelGry.Invalidate();  /** Ustawia fokus na formularz, aby umożliwić interakcję z nim */
            PanelMenuMain.Hide();   /** Ukrywa główne menu, aby przejść do wyboru poziomu */
            PanelPoziomy.Show();    /** Wyświetla panel do wyboru poziomu */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku wyjścia.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); /** Zamyka aplikacje */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do menu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void retrunButton_Click(object sender, EventArgs e)
        {
            PanelPoziomy.Hide();    /** Ukrywa panel wyboru poziomu */
            PanelMenuMain.Show();   /** Wyświetla główne menu */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku sterowania. 
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void controlsButton_Click(object sender, EventArgs e)
        {
            panelGry.Hide();    /** Ukrywa panel gry */
            PanelMenuMain.Hide();   /** Ukrywa główne menu */
            PanelPoziomy.Hide();    /** Ukrywa panel wyboru poziomu */
            czasPrezentacji = 0;    /** Resetuje czas prezentacji do zera */
            pozostalyCzas = 0;  /** Resetuje pozostały czas do zera */
            czasLabel.Hide();   /** Ukrywa etykietę z czasem */
            text_poziom_Label.Hide();   /** Ukrywa etykietę z poziomem */
            PanelSterowanieMenuMain.Show(); /** Wyświetla panel sterowania */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do menu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void returnMenu2_Click(object sender, EventArgs e)
        {
            PanelSterowanieMenuMain.Hide(); /** Ukrywa panel sterowania */
            panelGry.Hide();    /** Ukrywa panel gry */
            text_poziom_Label.Hide();   /** Ukrywa etykietę z poziomem */
            czasLabel.Hide();   /** Ukrywa etykietę z czasem */
            PanelMenuMain.Show();   /** Wyświetla główne menu */
            pozostalyCzas = 0;  /** Resetuje pozostały czas do zera */
            czasPrezentacji = 0;    /** Resetuje czas prezentacji do zera */
        }

        /** 
        * Sprawdza, czy gracz może się poruszać w wybranym kierunku. 
        * @param x Pozycja X gracza w labiryncie.
        * @param y Pozycja Y gracza w labiryncie.
        * @param key Klawisz, który reprezentuje kierunek ruchu.
        * @return bool - Zwraca true, jeśli ruch jest możliwy, w przeciwnym razie false.
        */
        public bool CzyMoznaRuszac(int x, int y, Keys key)
        {
            switch (key)
            {
                /** 
                * Sprawdzamy, czy można ruszyć w górę.
                * Warunek: y > 0 (nie wychodzimy poza górną krawędź) 
                * oraz labirynt.Siatka[x, y - 1] == 0 (sprawdzamy, czy pole jest puste).
                */
                case Keys.W:
                    if (y > 0 && labirynt.Siatka[x, y - 1] == 0) // Sprawdzamy w górę
                        return true;
                    break;

                /** 
                * Sprawdzamy, czy można ruszyć w dół.
                * Warunek: y < labirynt.Wysokosc - 1 (nie wychodzimy poza dolną krawędź) 
                * oraz labirynt.Siatka[x, y + 1] == 0 (sprawdzamy, czy pole jest puste).
                */
                case Keys.S:
                    if (y < labirynt.Wysokosc - 1 && labirynt.Siatka[x, y + 1] == 0) // Sprawdzamy w dół
                        return true;
                    break;

                /** 
                * Sprawdzamy, czy można ruszyć w lewo.
                * Warunek: x > 0 (nie wychodzimy poza lewą krawędź) 
                * oraz labirynt.Siatka[x - 1, y] == 0 (sprawdzamy, czy pole jest puste).
                */
                case Keys.A:
                    if (x > 0 && labirynt.Siatka[x - 1, y] == 0) // Sprawdzamy w lewo
                        return true;
                    break;

                /** 
                * Sprawdzamy, czy można ruszyć w prawo.
                * Warunek: x < labirynt.Szerokosc - 1 (nie wychodzimy poza prawą krawędź) 
                * oraz labirynt.Siatka[x + 1, y] == 0 (sprawdzamy, czy pole jest puste).
                */
                case Keys.D:
                    if (x < labirynt.Szerokosc - 1 && labirynt.Siatka[x + 1, y] == 0) // Sprawdzamy w prawo
                        return true;
                    break;
            }
            /** 
            * Zwracamy false, jeśli nie ma drogi w wybranym kierunku.
            */
            return false; // Zwracamy false, jeśli nie ma drogi
        }

        /** 
        * Sprawdza, czy gracz wszedł w checkpoint. 
        * Jeśli gracz znajduje się w punkcie kontrolnym, oznacza go jako odwiedzony
        * i wyświetla zadanie matematyczne do rozwiązania.
        */
        private void CzyWszedlWCheckpoint()
        {
            /** 
            * Sprawdzamy, czy aktualna pozycja gracza jest punktem kontrolnym 
            * oraz czy nie został on wcześniej odwiedzony.
            */
            if (poziom.Checkpoints.ContainsKey((gracz.X, gracz.Y)) && !poziom.Checkpoints[(gracz.X, gracz.Y)])
            {
                /** Oznacz checkpoint jako odwiedzony */
                poziom.Checkpoints[(gracz.X, gracz.Y)] = true; // Oznacz checkpoint jako odwiedzony
                /** Wyświetlenie zadania matematycznego do rozwiązania */
                WyswietlZadanieMatematyczne(); // Wyświetlenie zadania matematycznego
            }
        }

        /** 
        * Sprawdza, czy gracz ukończył poziom. 
        * Jeśli gracz dotarł do punktu końcowego, zatrzymuje timer, 
        * wyświetla komunikat o sukcesie i przechodzi do menu poziomów.
        */
        private void CzyGraczUkonczylPoziom()
        {
            /** 
            * Sprawdzamy, czy pozycja gracza odpowiada pozycji końcowej poziomu.
            */
            if (gracz.X == poziom.End.X && gracz.Y == poziom.End.Y)
            {
                timerLevel.Stop(); /** Zatrzymujemy timer */
                MessageBox.Show("Gratulacje! Ukończyłeś poziom!", "Sukces");    /** Wyświetlamy komunikat o ukończeniu poziomu */
                PanelPoziomy.Show(); /** Przejdź do menu poziomów */
                text_poziom_Label.Hide();   /** Przechodzimy do menu poziomów */
                czasLabel.Hide();   /** Ukrywamy etykiety z poziomem i czasem */
                UkryjZadanie(); /** Ukrywamy zadanie matematyczne, jeśli jest wyświetlane */
                panelGry.Hide();    /** Ukrywamy zadanie matematyczne, jeśli jest wyświetlane */
            }
        }

        /** 
        * Wyświetla zadanie matematyczne do rozwiązania w zależności od poziomu trudności.
        * Generuje losowe pytanie matematyczne i ustawia poprawny wynik.
        */
        private void WyswietlZadanieMatematyczne()
        {
            wynikPoprawny = 0;  /** Resetuje zmienną przechowującą poprawny wynik */
            panelZadanie.Show();    /** Wyświetla panel z zadaniem matematycznym */
            Random random = new Random();   /** Inicjalizuje generator liczb losowych */
            string pytanie = "";    /** Zmienna do przechowywania pytania matematycznego */

            // Generowanie zadania matematycznego
            if (poziom == Poziom.Easy)  /** Generowanie zadania matematycznego w zależności od poziomu trudności */
            {
                int a = random.Next(1, 20);
                int b = random.Next(1, 20);
                if (random.Next(2) == 0)    /** Losuje, czy zadanie będzie dodawaniem czy odejmowaniem */
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
                if (random.Next(2) == 0)    /** Losuje, czy zadanie będzie mnożeniem czy dzieleniem */
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
                if (random.Next(2) == 0) /** Losuje, czy zadanie będzie potęgą czy pierwiastkiem */
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
            /** 
            * Wyświetla pytanie i aktywuje kontrolki do wprowadzenia odpowiedzi.
            */
            labelZadanie.Text = pytanie; // Ustawia tekst pytania
            labelZadanie.Visible = true; // Ustawia widoczność etykiety z pytaniem
            textBoxZadanie.Visible = true; // Ustawia widoczność pola tekstowego do odpowiedzi
            buttonSprawdzZadanie.Visible = true; // Ustawia widoczność przycisku do sprawdzania odpowiedzi
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku sprawdzania odpowiedzi.
        * Sprawdza, czy odpowiedź użytkownika jest poprawna i wyświetla odpowiedni komunikat.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void buttonSprawdzZadanie_Click(object sender, EventArgs e)
        {
            /** 
            * Próbuje przekonwertować tekst z pola odpowiedzi na liczbę typu double.
            * Sprawdza, czy różnica między odpowiedzią użytkownika a poprawnym wynikiem 
            * jest mniejsza niż 0.01 (tolerancja błędu).
            */
            if (double.TryParse(textBoxZadanie.Text, out double wynikUzytkownika) &&
                Math.Abs(wynikUzytkownika - wynikPoprawny) < 0.01)
            {
                MessageBox.Show("Poprawna odpowiedź! Możesz kontynuować.", "Sukces");   /** Wyświetla komunikat o poprawnej odpowiedzi */
                UkryjZadanie(); /** Ukrywa zadanie matematyczne po poprawnej odpowiedzi */
            }
            else
            {
                MessageBox.Show("Błędna odpowiedź. Spróbuj ponownie!", "Błąd"); /** Wyświetla komunikat o błędnej odpowiedzi */
            }
        }
        /** 
        * Ukrywa kontrolki związane z zadaniem matematycznym.
        * Ta metoda jest używana do ukrywania panelu z zadaniem oraz 
        * resetowania widoczności kontrolek, aby przygotować interfejs 
        * do następnego zadania lub do powrotu do gry.
        */
        private void UkryjZadanie()
        {
            panelZadanie.Hide();    /** Ukrywa panel z zadaniem matematycznym */
            labelZadanie.Visible = false;   /** Ustawia widoczność etykiety z pytaniem na false */
            textBoxZadanie.Visible = false; /** Ustawia widoczność pola tekstowego na false */
            buttonSprawdzZadanie.Visible = false;   /** Ustawia widoczność przycisku do sprawdzania odpowiedzi na false */
            textBoxZadanie.Text = null; /** Resetuje tekst w polu odpowiedzi do wartości null */
        }

        /** 
        * Przesłania metodę ProcessCmdKey, aby obsłużyć naciśnięcia klawiszy.
        * Umożliwia poruszanie się gracza w labiryncie na podstawie naciśniętego klawisza.
        * @param msg Referencja do wiadomości, która zawiera informacje o naciśniętym klawiszu.
        * @param keyData Klawisz, który został naciśnięty.
        * @return bool - Zwraca true, jeśli klawisz został przetworzony, w przeciwnym razie false.
        */
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Sprawdzenie, czy obiekt gracz jest zainicjalizowany
            if (gracz != null)  /** Sprawdzenie, czy obiekt gracz jest zainicjalizowany */
            {
                if (CzyMoznaRuszac(gracz.X, gracz.Y, keyData)) /** Sprawdza, czy gracz może się poruszać w kierunku wskazanym przez naciśnięty klawisz */
                {
                    // Wywolanie metody przesuniecie gracza na podstawie wcisnietego klawisza
                    gracz.Rusz(keyData, poziom.Szerokosc, poziom.Wysokosc); /** Wywołanie metody przesunięcia gracza na podstawie wciśniętego klawisza */
                    // Sprawdzamy, czy gracz wszedł w checkpoint
                    CzyWszedlWCheckpoint(); /** Sprawdzamy, czy gracz wszedł w checkpoint */
                    CzyGraczUkonczylPoziom();   /** Sprawdzamy, czy gracz ukończył poziom */
                    // Odswieza panel, aby zaktualizowaæ pozycje gracza
                    panelGry.Invalidate();  /** Odświeża panel, aby zaktualizować pozycję gracza */
                }
            }
            return base.ProcessCmdKey(ref msg, keyData); /** Wywołuje bazową metodę ProcessCmdKey, aby zapewnić domyślną obsługę klawiszy */
        }

        /** 
        * Obsługuje zdarzenie malowania panelu gry.
        * Rysuje labirynt oraz pozycję gracza na panelu gry.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia malowania, zawierające informacje o grafice.
        */
        private void panelGry_Paint(object sender, PaintEventArgs e)
        {
            if (labirynt != null && poziom != null) /** Sprawdza, czy labirynt i poziom są zainicjalizowane */
            {
                /** 
                * Oblicza szerokość i wysokość komórki na podstawie rozmiaru panelu gry 
                * oraz wymiarów poziomu.
                */
                int szerokoscKomorki = panelGry.Width / poziom.Szerokosc;
                int wysokoscKomorki = panelGry.Height / poziom.Wysokosc;
                // Wywołanie metody rysującej labirynt
                labirynt.RysujLabirynt(e.Graphics, szerokoscKomorki, wysokoscKomorki, poziom.Start, poziom.End, poziom.Checkpoints);    /** Wywołanie metody rysującej labirynt na podstawie aktualnych parametrów */
                // Rysowanie gracza
                if (gracz != null)  /** Rysowanie gracza na panelu, jeśli obiekt gracz jest zainicjalizowany */
                {
                    e.Graphics.FillRectangle(Brushes.Blue, gracz.X * szerokoscKomorki, gracz.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                }
            }
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku wyboru łatwego poziomu. 
        * Ustawia poziom trudności na łatwy i przygotowuje interfejs do gry. 
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void easyLevel_Click(object sender, EventArgs e)
        {
            panelPodczasgry.Hide(); /** Ukrywa panel z informacjami o grze */
            UstawPoziom(Poziom.Easy);   /** Ustawia poziom trudności na łatwy */
            text_poziom_Label.Hide();   /** Ukrywa etykietę z poziomem */
            panelGry.Hide();    /** Ukrywa panel gry przed rozpoczęciem poziomu */
            /** Wyświetla komunikat informujący o ustawieniu poziomu oraz czasie na zapamiętanie labiryntu */
            MessageBox.Show("Ustawiono poziom: Łatwy\nMasz 30 sekund na zapamiętanie układu labiryntu. Po tym czasie rozpocznie się poziom.\", \"Zapamiętaj Labirynt");
            /** Wyświetla panel gry oraz inne elementy interfejsu */
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
}

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku wyboru łatwego poziomu. 
        * Ustawia poziom trudności na łatwy i przygotowuje interfejs do gry. 
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void mediumLevel_Click(object sender, EventArgs e)
        {
            panelPodczasgry.Hide(); /** Ukrywa panel z informacjami o grze */
            UstawPoziom(Poziom.Meduim); /** Ustawia poziom trudności na średni */
            text_poziom_Label.Hide();   /** Ukrywa etykietę z poziomem */
            panelGry.Hide();/** Ukrywa panel gry przed rozpoczęciem poziomu */
            /** Wyświetla komunikat informujący o ustawieniu poziomu oraz czasie na zapamiętanie labiryntu */
            MessageBox.Show("Ustawiono poziom: Średni\nMasz 30 sekund na zapamiętanie układu labiryntu. Po tym czasie rozpocznie się poziom.\", \"Zapamiętaj Labirynt");
            /** Wyświetla panel gry oraz inne elementy interfejsu */
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku wyboru łatwego poziomu. 
        * Ustawia poziom trudności na łatwy i przygotowuje interfejs do gry. 
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void hardLevel_Click(object sender, EventArgs e)
        {
            panelPodczasgry.Hide(); /** Ukrywa panel z informacjami o grze */
            UstawPoziom(Poziom.Hard);   /** Ustawia poziom trudności na trudny */
            text_poziom_Label.Hide();   /** Ukrywa etykietę z poziomem */
            panelGry.Hide();    /** Ukrywa panel gry przed rozpoczęciem poziomu */
            /** Wyświetla komunikat informujący o ustawieniu poziomu oraz czasie na zapamiętanie labiryntu */
            MessageBox.Show("Ustawiono poziom: Trudny\nMasz 30 sekund na zapamiętanie układu labiryntu. Po tym czasie rozpocznie się poziom.\", \"Zapamiętaj Labirynt");
            /** Wyświetla panel gry oraz inne elementy interfejsu */
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
        }

        /** 
        * Obsługuje zdarzenie tick timera poziomu.
        * Zmniejsza pozostały czas i aktualizuje etykietę z czasem. 
        * Sprawdza, czy czas się skończył, a jeśli tak, zatrzymuje timer  i informuje gracza o przegranej.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void timerLevel_Tick(object sender, EventArgs e)
        {
            pozostalyCzas--;    /** Zmniejsza pozostały czas o 1 sekundę */
            // Aktualizacja etykiety
            czasLabel.Text = $"Pozostały czas: {pozostalyCzas}s";   /** Aktualizuje etykietę z pozostałym czasem */
            // Sprawdzenie, czy czas się skończył
            if (pozostalyCzas <= 0) /** Sprawdza, czy pozostały czas wynosi 0 lub mniej */
            {
                timerLevel.Stop();  /** Zatrzymuje timer poziomu */
                MessageBox.Show("Czas minął! Przegrałeś.", "Koniec gry");   /** Wyświetla komunikat o upływie czasu i przegranej */
                UkryjZadanie(); /** Ukrywa zadanie matematyczne, jeśli jest wyświetlane */
                RestartujPoziom();  /** Restartuje poziom */
            }
        }

        /** 
        * Restartuje aktualny poziom, przywracając go do stanu początkowego. 
        * Zatrzymuje timer poziomu i przywraca checkpointy.
        */
        private void RestartujPoziom()
        {
            if (timerLevel != null) /** Sprawdza, czy timer poziomu jest aktywny, a jeśli tak, zatrzymuje go i zwalnia zasoby */
            {
                timerLevel.Stop();
                timerLevel.Dispose();
            }
            poziom.PrzywrocCheckpointy(); /** Przywraca checkpointy do stanu początkowego */
            UstawPoziom(poziom); /** Ustawia aktualny poziom do stanu początkowego */
        }

        /** 
        * Obsługuje zdarzenie tick timera prezentacji. 
        * Zmniejsza czas prezentacji i aktualizuje etykietę z pozostałym czasem. 
        * Po zakończeniu prezentacji przywraca gracza i rozpoczyna poziom. 
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void timerShow_Tick(object sender, EventArgs e)
        {
            czasPrezentacji--;  /** Zmniejsza czas prezentacji o 1 sekundę */

            if (czasPrezentacji <= 0)   /** Sprawdza, czy czas prezentacji wynosi 0 lub mniej */
            {
                timerShow.Stop();   /** Zatrzymuje timer prezentacji */
                czasLabel.Text = $"Pozostały czas: {poziom.TimeLevel}s";    /** Ustawia etykietę z pozostałym czasem na czas poziomu */

                // Po zakończeniu prezentacji przywróć gracza i rozpocznij poziom
                gracz = new Gracz(poziom.Start.X, poziom.Start.Y);  /** Przywraca gracza do pozycji startowej poziomu */
                timerLevel = new System.Windows.Forms.Timer();  /** Inicjalizuje nowy timer poziomu */
                timerLevel.Interval = 1000; // 1 sekunda
                timerLevel.Tick += timerLevel_Tick;
                pozostalyCzas = poziom.TimeLevel;
                timerLevel.Start();
            }
            else
            {
                czasLabel.Text = $"Czas na zapamiętanie: {czasPrezentacji}s";   /** Aktualizuje etykietę z czasem na zapamiętanie */
            }
            panelGry.Invalidate(); // Odśwież panel
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do wyboru poziomu.
        * Ukrywa aktualne panele gry i sterowania, a następnie wyświetla panel wyboru poziomu.
        * Resetuje czasy prezentacji i pozostały czas. 
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void returnLevel_Click(object sender, EventArgs e)
        {
            panelGry.Hide();    /** Ukrywa panel gry */
            PanelSterowanieMenuMain.Hide(); /** Ukrywa panel sterowania */
            PanelPoziomy.Show();    /** Wyświetla panel wyboru poziomu */
            czasPrezentacji = 0;    /** Resetuje czas prezentacji do zera */
            pozostalyCzas = 0;  /** Resetuje pozostały czas do zera */
            text_poziom_Label.Hide();   /** Ukrywa etykietę z poziomem */
            czasLabel.Hide();   /** Ukrywa etykietę z czasem */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku pauzy.
        * Wstrzymuje lub wznawia grę w zależności od aktualnego stanu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (graZatrzymana)  /** Sprawdza, czy gra jest aktualnie wstrzymana */
            {
                // Wznawiamy grę
                WznowWlasciwyTimer();   /** Wznawiamy grę, jeśli była wstrzymana */
                pauseButton.Text = "Pauza"; /** Zmienia tekst przycisku na "Pauza" */
                graZatrzymana = false;  /** Ustawia flagę wstrzymania na false */
            }
            else
            {
                /** Zatrzymujemy grę, jeśli nie była wstrzymana */
                ZatrzymajWszystkieTimery(); // Zatrzymaj liczniki czasu
                MessageBox.Show("Gra jest wstrzymana. Kliknij przycisk 'Wznów', aby kontynuować.", "Pauza");    /** Wyświetla komunikat informujący o wstrzymaniu gry */
                pauseButton.Text = "Wznów"; /** Zmienia tekst przycisku na "Wznów" */
                graZatrzymana = true;   /** Ustawia flagę wstrzymania na true */
            }
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do wyboru poziomu.
        * Ukrywa główne menu i wyświetla panel wyboru poziomu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void returnLevel2_Click(object sender, EventArgs e)
        {
            PanelMenuMain.Hide();   /** Ukrywa główne menu */
            PanelPoziomy.Show();    /** Wyświetla panel wyboru poziomu */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku wyjścia z gry.
        * Zamyka aplikację.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void exitButton2_Click(object sender, EventArgs e)
        {
            Application.Exit(); /** Zamyka aplikację */
        }

        /** 
        * Zatrzymuje wszystkie aktywne timery w grze.
        * Sprawdza, czy timery są zainicjalizowane i aktywne, a następnie je zatrzymuje.
        */
        private void ZatrzymajWszystkieTimery()
        {
            if (timerShow != null && timerShow.Enabled) timerShow.Stop();   /** Zatrzymuje timer prezentacji, jeśli jest aktywny */
            if (timerLevel != null && timerLevel.Enabled) timerLevel.Stop();    /** Zatrzymuje timer poziomu, jeśli jest aktywny */
        }

        /** 
        * Wznawia odpowiedni timer w zależności od pozostałego czasu.
        * Jeśli czas prezentacji jest większy niż 0, wznawia timer prezentacji.
        * W przeciwnym razie, jeśli pozostały czas poziomu jest większy niż 0, 
        * wznawia timer poziomu.
        */
        private void WznowWlasciwyTimer()
        {
            if (czasPrezentacji > 0 && timerShow != null) timerShow.Start(); /** Wznawia timer prezentacji, jeśli czas prezentacji jest większy niż 0 i timer jest zainicjalizowany. */
            else if (pozostalyCzas > 0 && timerLevel != null) timerLevel.Start(); /** Wznawia timer poziomu, jeśli pozostały czas jest większy niż 0  i timer jest zainicjalizowany */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku do wyświetlania panelu sterowania.
        * Zatrzymuje wszystkie timery, ukrywa aktualne panele i wyświetla panel sterowania gry.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void controlsButton2_Click(object sender, EventArgs e)
        {
            ZatrzymajWszystkieTimery(); /** Zatrzymuje wszystkie aktywne timery w grze */
            panelGry.Hide();    /** Ukrywa panel gry */
            PanelMenuMain.Hide();   /** Ukrywa główne menu */
            PanelPoziomy.Hide();    /** Ukrywa panel wyboru poziomu */
            czasLabel.Hide();   /** Ukrywa etykietę z czasem */
            text_poziom_Label.Hide();   /** Ukrywa etykietę z poziomem */
            PanelSterowanieGra.Show();  /** Wyświetla panel sterowania gry */
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do wyboru poziomu z panelu sterowania.
        * Ukrywa panel sterowania i wyświetla panel wyboru poziomu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void returnMenu3_Click(object sender, EventArgs e)
        {
            PanelSterowanieGra.Hide();  /** Ukrywa panel sterowania gry */
            PanelPoziomy.Show();    /** Wyświetla panel wyboru poziomu */
        }
    }
}