using System;
using System.Windows.Forms;

namespace Labirynty
{
    public partial class Form1 : Form
    {
        /** Przechowuje instancję labiryntu, który jest aktualnie używany w grze */
        public Labirynt labirynt;
        /** Reprezentuje aktualny poziom trudności gry, domyślnie ustawiony na poziom łatwy */
        public Poziom poziom = Poziom.Easy;
        /** Obiekt reprezentujący gracza w grze, przechowując jego pozycję */
        public Gracz gracz;
        /** Szerokość labiryntu w jednostkach, używana do obliczeń graficznych */
        public int szerokosc;
        /** Wysokość labiryntu w jednostkach, używana do obliczeń graficznych */
        public int wysokosc;
        /** Liczba sekund pozostałych na dany poziom, używana do zarządzania czasem gry */
        public int pozostalyCzas;
        /** Czas prezentacji labiryntu przed rozpoczęciem poziomu, w sekundach */
        public int czasPrezentacji;
        /** Przechowuje poprawny wynik zadania matematycznego, które gracz musi rozwiązać */
        public double wynikPoprawny;
        /** Flaga wskazująca, czy gra jest wstrzymana (pauza) */
        public bool graZatrzymana = false;

        /** Konstruktor klasy Form1, inicjalizuje komponenty formularza */
        public Form1()
        {
            InitializeComponent();
        }

        /** 
        * Ustawia poziom trudności gry oraz inicjalizuje odpowiednie elementy 
        * @param poziom Poziom - nowy poziom trudności do ustawienia
        */
        public void UstawPoziom(Poziom poziom)
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
            /** Ustawia nowy poziom trudności */
            this.poziom = poziom;
            /** Przywraca checkpointy do stanu początkowego */
            poziom.PrzywrocCheckpointy();
            /** Inicjalizuje nowy labirynt na podstawie szerokości i wysokości poziomu */
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc);
            /** Generuje labirynt na podstawie macierzy poziomu */
            labirynt.GenerujLabirynt(poziom.Macierz);
            /** Inicjalizuje gracza na pozycji startowej poziomu */
            gracz = new Gracz(poziom.Start.X, poziom.Start.Y);

            // Wyświetlenie labiryntu dla gracza przez dany czas
            /** Inicjalizuje gracza na pozycji startowej poziomu */
            czasPrezentacji = poziom.TimeShow;
            /** Inicjalizuje gracza na pozycji startowej poziomu */
            text_poziom_Label.Text = $"Poziom: {poziom.NameLevel}";
            /** Inicjalizuje gracza na pozycji startowej poziomu */
            czasLabel.Text = $"Czas na zapamiętanie: {czasPrezentacji}s";
            /** Odświeża panel gry, aby narysować nowy labirynt */
            panelGry.Invalidate();

            // Ustawienie timera prezentacji
            /** Odświeża panel gry, aby narysować nowy labirynt */
            timerShow = new System.Windows.Forms.Timer();
            timerShow.Interval = 1000; // 1 sekunda
            timerShow.Tick += timerShow_Tick;
            timerShow.Start();

            // Ukrycie gracza na czas prezentacji
            /** Odświeża panel gry, aby narysować nowy labirynt */
            gracz = null;
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku startowego.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void startButton_Click(object sender, EventArgs e)
        {
            /** Ustawia fokus na formularz, aby umożliwić interakcję z nim */
            this.Focus();
            /** Ustawia fokus na formularz, aby umożliwić interakcję z nim */
            panelGry.Invalidate();
            /** Ukrywa główne menu, aby przejść do wyboru poziomu */
            PanelMenuMain.Hide();
            /** Wyświetla panel do wyboru poziomu */
            PanelPoziomy.Show();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku wyjścia.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void exitButton_Click(object sender, EventArgs e)
        {
            /** Zamyka aplikacje */
            Application.Exit();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do menu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void retrunButton_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel wyboru poziomu */
            PanelPoziomy.Hide();
            /** Wyświetla główne menu */
            PanelMenuMain.Show();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku sterowania. 
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void controlsButton_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel gry */
            panelGry.Hide();
            /** Ukrywa główne menu */
            PanelMenuMain.Hide();
            /** Ukrywa panel wyboru poziomu */
            PanelPoziomy.Hide();
            /** Resetuje czas prezentacji do zera */
            czasPrezentacji = 0;
            /** Resetuje pozostały czas do zera */
            pozostalyCzas = 0;
            /** Ukrywa etykietę z czasem */
            czasLabel.Hide();
            /** Ukrywa etykietę z poziomem */
            text_poziom_Label.Hide();
            /** Wyświetla panel sterowania */
            PanelSterowanieMenuMain.Show();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do menu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void returnMenu2_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel sterowania */
            PanelSterowanieMenuMain.Hide();
            /** Ukrywa panel gry */
            panelGry.Hide();
            /** Ukrywa etykietę z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa etykietę z czasem */
            czasLabel.Hide();
            /** Wyświetla główne menu */
            PanelMenuMain.Show();
            /** Resetuje pozostały czas do zera */
            pozostalyCzas = 0;
            /** Resetuje czas prezentacji do zera */
            czasPrezentacji = 0;
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
        public void CzyWszedlWCheckpoint()
        {
            /** 
            * Sprawdzamy, czy aktualna pozycja gracza jest punktem kontrolnym oraz czy nie został on wcześniej odwiedzony.
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
        public void CzyGraczUkonczylPoziom()
        {
            /** 
            * Sprawdzamy, czy pozycja gracza odpowiada pozycji końcowej poziomu.
            */
            if (gracz.X == poziom.End.X && gracz.Y == poziom.End.Y)
            {
                /** Zatrzymujemy timer */
                timerLevel.Stop();
                /** Wyświetlamy komunikat o ukończeniu poziomu */
                MessageBox.Show("Gratulacje! Ukończyłeś poziom!", "Sukces");
                /** Przejdź do menu poziomów */
                PanelPoziomy.Show();
                /** Przechodzimy do menu poziomów */
                text_poziom_Label.Hide();
                /** Ukrywamy etykiety z poziomem i czasem */
                czasLabel.Hide();
                /** Ukrywamy zadanie matematyczne, jeśli jest wyświetlane */
                UkryjZadanie();
                /** Ukrywamy zadanie matematyczne, jeśli jest wyświetlane */
                panelGry.Hide();
            }
        }

        /** 
        * Wyświetla zadanie matematyczne do rozwiązania w zależności od poziomu trudności.
        * Generuje losowe pytanie matematyczne i ustawia poprawny wynik.
        */
        public void WyswietlZadanieMatematyczne()
        {
            /** Resetuje zmienną przechowującą poprawny wynik */
            wynikPoprawny = 0;
            /** Wyświetla panel z zadaniem matematycznym */
            panelZadanie.Show();
            /** Inicjalizuje generator liczb losowych */
            Random random = new Random();
            /** Zmienna do przechowywania pytania matematycznego */
            string pytanie = "";

            // Generowanie zadania matematycznego
            /** Generowanie zadania matematycznego w zależności od poziomu trudności */
            if (poziom == Poziom.Easy)
            {
                int a = random.Next(1, 20);
                int b = random.Next(1, 20);
                /** Losuje, czy zadanie będzie dodawaniem czy odejmowaniem */
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
                /** Losuje, czy zadanie będzie mnożeniem czy dzieleniem */
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
                /** Losuje, czy zadanie będzie potęgą czy pierwiastkiem */
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
        public void buttonSprawdzZadanie_Click(object sender, EventArgs e)
        {
            /** 
            * Próbuje przekonwertować tekst z pola odpowiedzi na liczbę typu double.
            * Sprawdza, czy różnica między odpowiedzią użytkownika a poprawnym wynikiem 
            * jest mniejsza niż 0.01 (tolerancja błędu).
            */
            if (double.TryParse(textBoxZadanie.Text, out double wynikUzytkownika) &&
                Math.Abs(wynikUzytkownika - wynikPoprawny) < 0.01)
            {
                /** Wyświetla komunikat o poprawnej odpowiedzi */
                MessageBox.Show("Poprawna odpowiedź! Możesz kontynuować.", "Sukces");
                /** Ukrywa zadanie matematyczne po poprawnej odpowiedzi */
                UkryjZadanie();
            }
            else
            {
                /** Wyświetla komunikat o błędnej odpowiedzi */
                MessageBox.Show("Błędna odpowiedź. Spróbuj ponownie!", "Błąd");
            }
        }
        /** 
        * Ukrywa kontrolki związane z zadaniem matematycznym.
        * Ta metoda jest używana do ukrywania panelu z zadaniem oraz 
        * resetowania widoczności kontrolek, aby przygotować interfejs 
        * do następnego zadania lub do powrotu do gry.
        */
        public void UkryjZadanie()
        {
            /** Ukrywa panel z zadaniem matematycznym */
            panelZadanie.Hide();
            /** Ustawia widoczność etykiety z pytaniem na false */
            labelZadanie.Visible = false;
            /** Ustawia widoczność pola tekstowego na false */
            textBoxZadanie.Visible = false;
            /** Ustawia widoczność przycisku do sprawdzania odpowiedzi na false */
            buttonSprawdzZadanie.Visible = false;
            /** Resetuje tekst w polu odpowiedzi do wartości null */
            textBoxZadanie.Text = null;
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
            /** Sprawdzenie, czy obiekt gracz jest zainicjalizowany */
            if (gracz != null)
            {
                /** Sprawdza, czy gracz może się poruszać w kierunku wskazanym przez naciśnięty klawisz */
                if (CzyMoznaRuszac(gracz.X, gracz.Y, keyData))
                {
                    // Wywolanie metody przesuniecie gracza na podstawie wcisnietego klawisza
                    /** Wywołanie metody przesunięcia gracza na podstawie wciśniętego klawisza */
                    gracz.Rusz(keyData, poziom.Szerokosc, poziom.Wysokosc);
                    // Sprawdzamy, czy gracz wszedł w checkpoint
                    /** Sprawdzamy, czy gracz wszedł w checkpoint */
                    CzyWszedlWCheckpoint();
                    /** Sprawdzamy, czy gracz ukończył poziom */
                    CzyGraczUkonczylPoziom();
                    // Odswieza panel, aby zaktualizowaæ pozycje gracza
                    /** Odświeża panel, aby zaktualizować pozycję gracza */
                    panelGry.Invalidate();
                }
            }
            /** Wywołuje bazową metodę ProcessCmdKey, aby zapewnić domyślną obsługę klawiszy */
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /** 
        * Obsługuje zdarzenie malowania panelu gry.
        * Rysuje labirynt oraz pozycję gracza na panelu gry.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia malowania, zawierające informacje o grafice.
        */
        public void panelGry_Paint(object sender, PaintEventArgs e)
        {
            /** Sprawdza, czy labirynt i poziom są zainicjalizowane */
            if (labirynt != null && poziom != null)
            {
                /** 
                * Oblicza szerokość i wysokość komórki na podstawie rozmiaru panelu gry 
                * oraz wymiarów poziomu.
                */
                int szerokoscKomorki = panelGry.Width / poziom.Szerokosc;
                int wysokoscKomorki = panelGry.Height / poziom.Wysokosc;
                // Wywołanie metody rysującej labirynt
                /** Wywołanie metody rysującej labirynt na podstawie aktualnych parametrów */
                labirynt.RysujLabirynt(e.Graphics, szerokoscKomorki, wysokoscKomorki, poziom.Start, poziom.End, poziom.Checkpoints);
                // Rysowanie gracza
                /** Rysowanie gracza na panelu, jeśli obiekt gracz jest zainicjalizowany */
                if (gracz != null)
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
        public void easyLevel_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel z informacjami o grze */
            panelPodczasgry.Hide();
            /** Ustawia poziom trudności na łatwy */
            UstawPoziom(Poziom.Easy);
            /** Ukrywa etykietę z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa panel gry przed rozpoczęciem poziomu */
            panelGry.Hide();
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
        public void mediumLevel_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel z informacjami o grze */
            panelPodczasgry.Hide();
            /** Ustawia poziom trudności na średni */
            UstawPoziom(Poziom.Meduim);
            /** Ukrywa etykietę z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa panel gry przed rozpoczęciem poziomu */
            panelGry.Hide();
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
        public void hardLevel_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel z informacjami o grze */
            panelPodczasgry.Hide();
            /** Ustawia poziom trudności na trudny */
            UstawPoziom(Poziom.Hard);
            /** Ukrywa etykietę z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa panel gry przed rozpoczęciem poziomu */
            panelGry.Hide();
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
        public void timerLevel_Tick(object sender, EventArgs e)
        {
            /** Zmniejsza pozostały czas o 1 sekundę */
            pozostalyCzas--;
            /** Aktualizuje etykietę z pozostałym czasem */
            czasLabel.Text = $"Pozostały czas: {pozostalyCzas}s";
            /** Sprawdza, czy pozostały czas wynosi 0 lub mniej */
            if (pozostalyCzas <= 0)
            {
                /** Zatrzymuje timer poziomu */
                timerLevel.Stop();
                /** Wyświetla komunikat o upływie czasu i przegranej */
                MessageBox.Show("Czas minął! Przegrałeś.", "Koniec gry");
                /** Ukrywa zadanie matematyczne, jeśli jest wyświetlane */
                UkryjZadanie();
                /** Restartuje poziom */
                RestartujPoziom();
            }
        }

        /** 
        * Restartuje aktualny poziom, przywracając go do stanu początkowego. 
        * Zatrzymuje timer poziomu i przywraca checkpointy.
        */
        public void RestartujPoziom()
        {
            /** Sprawdza, czy timer poziomu jest aktywny, a jeśli tak, zatrzymuje go i zwalnia zasoby */
            if (timerLevel != null)
            {
                timerLevel.Stop();
                timerLevel.Dispose();
            }
            /** Przywraca checkpointy do stanu początkowego */
            poziom.PrzywrocCheckpointy();
            /** Ustawia aktualny poziom do stanu początkowego */
            UstawPoziom(poziom);
        }

        /** 
        * Obsługuje zdarzenie tick timera prezentacji. 
        * Zmniejsza czas prezentacji i aktualizuje etykietę z pozostałym czasem. 
        * Po zakończeniu prezentacji przywraca gracza i rozpoczyna poziom. 
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void timerShow_Tick(object sender, EventArgs e)
        {
            /** Zmniejsza czas prezentacji o 1 sekundę */
            czasPrezentacji--;
            /** Sprawdza, czy czas prezentacji wynosi 0 lub mniej */
            if (czasPrezentacji <= 0)
            {
                /** Zatrzymuje timer prezentacji */
                timerShow.Stop();
                /** Ustawia etykietę z pozostałym czasem na czas poziomu */
                czasLabel.Text = $"Pozostały czas: {poziom.TimeLevel}s";
                /** Przywraca gracza do pozycji startowej poziomu */
                gracz = new Gracz(poziom.Start.X, poziom.Start.Y);
                /** Inicjalizuje nowy timer poziomu */
                timerLevel = new System.Windows.Forms.Timer();
                timerLevel.Interval = 1000; // 1 sekunda
                timerLevel.Tick += timerLevel_Tick;
                pozostalyCzas = poziom.TimeLevel;
                timerLevel.Start();
            }
            else
            {
                /** Aktualizuje etykietę z czasem na zapamiętanie */
                czasLabel.Text = $"Czas na zapamiętanie: {czasPrezentacji}s";
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
        public void returnLevel_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel gry */
            panelGry.Hide();
            /** Ukrywa panel sterowania */
            PanelSterowanieMenuMain.Hide();
            /** Wyświetla panel wyboru poziomu */
            PanelPoziomy.Show();
            /** Resetuje czas prezentacji do zera */
            czasPrezentacji = 0;
            /** Resetuje pozostały czas do zera */
            pozostalyCzas = 0;
            /** Ukrywa etykietę z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa etykietę z czasem */
            czasLabel.Hide();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku pauzy.
        * Wstrzymuje lub wznawia grę w zależności od aktualnego stanu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void pauseButton_Click(object sender, EventArgs e)
        {
            /** Sprawdza, czy gra jest aktualnie wstrzymana */
            if (graZatrzymana)
            {
                /** Wznawiamy grę, jeśli była wstrzymana */
                WznowWlasciwyTimer();
                /** Zmienia tekst przycisku na "Pauza" */
                pauseButton.Text = "Pauza";
                /** Ustawia flagę wstrzymania na false */
                graZatrzymana = false;
            }
            else
            {
                /** Zatrzymujemy grę, jeśli nie była wstrzymana */
                ZatrzymajWszystkieTimery(); // Zatrzymaj liczniki czasu
                /** Wyświetla komunikat informujący o wstrzymaniu gry */
                MessageBox.Show("Gra jest wstrzymana. Kliknij przycisk 'Wznów', aby kontynuować.", "Pauza");
                /** Zmienia tekst przycisku na "Wznów" */
                pauseButton.Text = "Wznów";
                /** Ustawia flagę wstrzymania na true */
                graZatrzymana = true;
            }
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do wyboru poziomu.
        * Ukrywa główne menu i wyświetla panel wyboru poziomu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void returnLevel2_Click(object sender, EventArgs e)
        {
            /** Ukrywa główne menu */
            PanelMenuMain.Hide();
            /** Wyświetla panel wyboru poziomu */
            PanelPoziomy.Show();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku wyjścia z gry.
        * Zamyka aplikację.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void exitButton2_Click(object sender, EventArgs e)
        {
            /** Zamyka aplikację */
            Application.Exit();
        }

        /** 
        * Zatrzymuje wszystkie aktywne timery w grze.
        * Sprawdza, czy timery są zainicjalizowane i aktywne, a następnie je zatrzymuje.
        */
        public void ZatrzymajWszystkieTimery()
        {
            /** Zatrzymuje timer prezentacji, jeśli jest aktywny */
            if (timerShow != null && timerShow.Enabled) timerShow.Stop();
            /** Zatrzymuje timer poziomu, jeśli jest aktywny */
            if (timerLevel != null && timerLevel.Enabled) timerLevel.Stop();
        }

        /** 
        * Wznawia odpowiedni timer w zależności od pozostałego czasu.
        * Jeśli czas prezentacji jest większy niż 0, wznawia timer prezentacji.
        * W przeciwnym razie, jeśli pozostały czas poziomu jest większy niż 0, 
        * wznawia timer poziomu.
        */
        public void WznowWlasciwyTimer()
        {
            /** Wznawia timer prezentacji, jeśli czas prezentacji jest większy niż 0 i timer jest zainicjalizowany. */
            if (czasPrezentacji > 0 && timerShow != null) timerShow.Start();
            /** Wznawia timer poziomu, jeśli pozostały czas jest większy niż 0  i timer jest zainicjalizowany */
            else if (pozostalyCzas > 0 && timerLevel != null) timerLevel.Start();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku do wyświetlania panelu sterowania.
        * Zatrzymuje wszystkie timery, ukrywa aktualne panele i wyświetla panel sterowania gry.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void controlsButton2_Click(object sender, EventArgs e)
        {
            /** Zatrzymuje wszystkie aktywne timery w grze */
            ZatrzymajWszystkieTimery();
            /** Ukrywa panel gry */
            panelGry.Hide();
            /** Ukrywa główne menu */
            PanelMenuMain.Hide();
            /** Ukrywa panel wyboru poziomu */
            PanelPoziomy.Hide();
            /** Ukrywa etykietę z czasem */
            czasLabel.Hide();
            /** Ukrywa etykietę z poziomem */
            text_poziom_Label.Hide();
            /** Wyświetla panel sterowania gry */
            PanelSterowanieGra.Show();
        }

        /** 
        * Obsługuje zdarzenie kliknięcia przycisku powrotu do wyboru poziomu z panelu sterowania.
        * Ukrywa panel sterowania i wyświetla panel wyboru poziomu.
        * @param sender Obiekt, który wywołał zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        public void returnMenu3_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel sterowania gry */
            PanelSterowanieGra.Hide();
            /** Wyświetla panel wyboru poziomu */
            PanelPoziomy.Show();
        }
    }
}