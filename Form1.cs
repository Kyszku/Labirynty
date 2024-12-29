using System;
using System.Windows.Forms;

namespace Labirynty
{
    public partial class Form1 : Form
    {
        /** Przechowuje instancje labiryntu, ktory jest aktualnie uzywany w grze */
        private Labirynt labirynt;
        /** Reprezentuje aktualny poziom trudnosci gry, domyslnie ustawiony na poziom latwy */
        private Poziom poziom = Poziom.Easy;
        /** Obiekt reprezentujacy gracza w grze, przechowujac jego pozycje */
        private Gracz gracz;
        /** Szerokosc labiryntu w jednostkach, uzywana do obliczen graficznych */
        private int szerokosc;
        /** Wysokosc labiryntu w jednostkach, uzywana do obliczen graficznych */
        private int wysokosc;
        /** Liczba sekund pozostalych na dany poziom, uzywana do zarzadzania czasem gry */
        private int pozostalyCzas;
        /** Czas prezentacji labiryntu przed rozpoczeciem poziomu, w sekundach */
        private int czasPrezentacji;
        /** Przechowuje poprawny wynik zadania matematycznego, ktore gracz musi rozwiazac */
        private double wynikPoprawny;
        /** Flaga wskazujaca, czy gra jest wstrzymana (pauza) */
        private bool graZatrzymana = false;

        /** Konstruktor klasy Form1, inicjalizuje komponenty formularza */
        public Form1()
        {
            InitializeComponent();
        }

        /** 
        * Ustawia poziom trudnosci gry oraz inicjalizuje odpowiednie elementy 
        * @param poziom Poziom - nowy poziom trudnosci do ustawienia
        */
        private void UstawPoziom(Poziom poziom)
        {
            /** Zatrzymuje i zwalnia timer poziomu, jesli jest aktywny */
            if (timerLevel != null)
            {
                timerLevel.Stop();
                timerLevel.Dispose();
            }
            /** Zatrzymuje i zwalnia timer prezentacji, jesli jest aktywny */
            if (timerShow != null)
            {
                timerShow.Stop();
                timerShow.Dispose();
            }
            /** Ustawia nowy poziom trudnosci */
            this.poziom = poziom;
            /** Przywraca checkpointy do stanu poczatkowego */
            poziom.PrzywrocCheckpointy();
            /** Inicjalizuje nowy labirynt na podstawie szerokosci i wysokosci poziomu */
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc);
            /** Generuje labirynt na podstawie macierzy poziomu */
            labirynt.GenerujLabirynt(poziom.Macierz);
            /** Inicjalizuje gracza na pozycji startowej poziomu */
            gracz = new Gracz(poziom.Start.X, poziom.Start.Y);

            // Wyswietlenie labiryntu dla gracza przez dany czas
            /** Inicjalizuje gracza na pozycji startowej poziomu */
            czasPrezentacji = poziom.TimeShow;
            /** Inicjalizuje gracza na pozycji startowej poziomu */
            text_poziom_Label.Text = $"Poziom: {poziom.NameLevel}";
            /** Inicjalizuje gracza na pozycji startowej poziomu */
            czasLabel.Text = $"Czas na zapamiętanie: {czasPrezentacji}s";
            /** Odswieza panel gry, aby narysowac nowy labirynt */
            panelGry.Invalidate();

            // Ustawienie timera prezentacji
            /** Odswieza panel gry, aby narysowac nowy labirynt */
            timerShow = new System.Windows.Forms.Timer();
            timerShow.Interval = 1000; // 1 sekunda
            timerShow.Tick += timerShow_Tick;

            // Ustawienie tymczasowego timera opóźnienia
            /** Opoznia narysowanie labiryntu, aby przeczytac informacje przed poziomem*/
            timerDelay = new System.Windows.Forms.Timer();
            timerDelay.Interval = 5000; // 5 sekund
            timerDelay.Tick += timerDelay_Tick;
            timerDelay.Start();

            // Ukrycie gracza na czas prezentacji
            /** Odswieza panel gry, aby narysowac nowy labirynt */
            gracz = null;
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku startowego.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void startButton_Click(object sender, EventArgs e)
        {
            /** Ustawia fokus na formularz, aby umozliwic interakcje z nim */
            this.Focus();
            /** Ustawia fokus na formularz, aby umozliwic interakcje z nim */
            panelGry.Invalidate();
            /** Ukrywa glowne menu, aby przejsc do wyboru poziomu */
            PanelMenuMain.Hide();
            /** Wyswietla panel do wyboru poziomu */
            PanelPoziomy.Show();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku wyjscia.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void exitButton_Click(object sender, EventArgs e)
        {
            /** Zamyka aplikacje */
            Application.Exit();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku powrotu do menu.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void retrunButton_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel wyboru poziomu */
            PanelPoziomy.Hide();
            /** Wyswietla glowne menu */
            PanelMenuMain.Show();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku sterowania. 
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void controlsButton_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel gry */
            panelGry.Hide();
            /** Ukrywa glowne menu */
            PanelMenuMain.Hide();
            /** Ukrywa panel wyboru poziomu */
            PanelPoziomy.Hide();
            /** Resetuje czas prezentacji do zera */
            czasPrezentacji = 0;
            /** Resetuje pozostaly czas do zera */
            pozostalyCzas = 0;
            /** Ukrywa etykiete z czasem */
            czasLabel.Hide();
            /** Ukrywa etykiete z poziomem */
            text_poziom_Label.Hide();
            /** Wyswietla panel sterowania */
            PanelSterowanieMenuMain.Show();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku powrotu do menu.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void returnMenu2_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel sterowania */
            PanelSterowanieMenuMain.Hide();
            /** Ukrywa panel gry */
            panelGry.Hide();
            /** Ukrywa etykiete z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa etykiete z czasem */
            czasLabel.Hide();
            /** Wyswietla glowne menu */
            PanelMenuMain.Show();
            /** Resetuje pozostaly czas do zera */
            pozostalyCzas = 0;
            /** Resetuje czas prezentacji do zera */
            czasPrezentacji = 0;
        }

        /** 
        * Sprawdza, czy gracz moze sie poruszac w wybranym kierunku. 
        * @param x Pozycja X gracza w labiryncie.
        * @param y Pozycja Y gracza w labiryncie.
        * @param key Klawisz, ktory reprezentuje kierunek ruchu.
        * @return bool - Zwraca true, jesli ruch jest mozliwy, w przeciwnym razie false.
        */
        private bool CzyMoznaRuszac(int x, int y, Keys key)
        {
            switch (key)
            {
                /** 
                * Sprawdzamy, czy mozna ruszyc w gore.
                * Warunek: y > 0 (nie wychodzimy poza gorna krawedź) 
                * oraz labirynt.Siatka[x, y - 1] == 0 (sprawdzamy, czy pole jest puste).
                */
                case Keys.W:
                    if (y > 0 && labirynt.Siatka[x, y - 1] == 0) // Sprawdzamy w gore
                        return true;
                    break;

                /** 
                * Sprawdzamy, czy mozna ruszyc w dol.
                * Warunek: y < labirynt.Wysokosc - 1 (nie wychodzimy poza dolna krawedź) 
                * oraz labirynt.Siatka[x, y + 1] == 0 (sprawdzamy, czy pole jest puste).
                */
                case Keys.S:
                    if (y < labirynt.Wysokosc - 1 && labirynt.Siatka[x, y + 1] == 0) // Sprawdzamy w dol
                        return true;
                    break;

                /** 
                * Sprawdzamy, czy mozna ruszyc w lewo.
                * Warunek: x > 0 (nie wychodzimy poza lewa krawedź) 
                * oraz labirynt.Siatka[x - 1, y] == 0 (sprawdzamy, czy pole jest puste).
                */
                case Keys.A:
                    if (x > 0 && labirynt.Siatka[x - 1, y] == 0) // Sprawdzamy w lewo
                        return true;
                    break;

                /** 
                * Sprawdzamy, czy mozna ruszyc w prawo.
                * Warunek: x < labirynt.Szerokosc - 1 (nie wychodzimy poza prawa krawedź) 
                * oraz labirynt.Siatka[x + 1, y] == 0 (sprawdzamy, czy pole jest puste).
                */
                case Keys.D:
                    if (x < labirynt.Szerokosc - 1 && labirynt.Siatka[x + 1, y] == 0) // Sprawdzamy w prawo
                        return true;
                    break;
            }
            /** 
            * Zwracamy false, jesli nie ma drogi w wybranym kierunku.
            */
            return false; // Zwracamy false, jesli nie ma drogi
        }

        /** 
        * Sprawdza, czy gracz wszedl w checkpoint. 
        * Jesli gracz znajduje sie w punkcie kontrolnym, oznacza go jako odwiedzony
        * i wyswietla zadanie matematyczne do rozwiazania.
        */
        private void CzyWszedlWCheckpoint()
        {
            /** 
            * Sprawdzamy, czy aktualna pozycja gracza jest punktem kontrolnym oraz czy nie zostal on wczesniej odwiedzony.
            */
            if (poziom.Checkpoints.ContainsKey((gracz.X, gracz.Y)) && !poziom.Checkpoints[(gracz.X, gracz.Y)])
            {
                /** Oznacz checkpoint jako odwiedzony */
                poziom.Checkpoints[(gracz.X, gracz.Y)] = true; // Oznacz checkpoint jako odwiedzony
                /** Wyswietlenie zadania matematycznego do rozwiazania */
                WyswietlZadanieMatematyczne(); // Wyswietlenie zadania matematycznego
            }
        }

        /** 
        * Sprawdza, czy gracz ukonczyl poziom. 
        * Jesli gracz dotarl do punktu koncowego, zatrzymuje timer, 
        * wyswietla komunikat o sukcesie i przechodzi do menu poziomow.
        */
        private void CzyGraczUkonczylPoziom()
        {
            /** 
            * Sprawdzamy, czy pozycja gracza odpowiada pozycji koncowej poziomu.
            */
            if (gracz.X == poziom.End.X && gracz.Y == poziom.End.Y)
            {
                /** Zatrzymujemy timer */
                timerLevel.Stop();
                /** Wyswietlamy komunikat o ukonczeniu poziomu */
                MessageBox.Show("Gratulacje! Ukonczyłeś poziom!", "Sukces");
                /** Przejdź do menu poziomow */
                PanelPoziomy.Show();
                /** Przechodzimy do menu poziomow */
                text_poziom_Label.Hide();
                /** Ukrywamy etykiety z poziomem i czasem */
                czasLabel.Hide();
                /** Ukrywamy zadanie matematyczne, jesli jest wyswietlane */
                UkryjZadanie();
                /** Ukrywamy zadanie matematyczne, jesli jest wyswietlane */
                panelGry.Hide();
            }
        }

        /** 
        * Wyswietla zadanie matematyczne do rozwiazania w zaleznosci od poziomu trudnosci.
        * Generuje losowe pytanie matematyczne i ustawia poprawny wynik.
        */
        private void WyswietlZadanieMatematyczne()
        {
            /** Resetuje zmienna przechowujaca poprawny wynik */
            wynikPoprawny = 0;
            /** Wyswietla panel z zadaniem matematycznym */
            panelZadanie.Show();
            /** Inicjalizuje generator liczb losowych */
            Random random = new Random();
            /** Zmienna do przechowywania pytania matematycznego */
            string pytanie = "";

            // Generowanie zadania matematycznego
            /** Generowanie zadania matematycznego w zaleznosci od poziomu trudnosci */
            if (poziom == Poziom.Easy)
            {
                int a = random.Next(1, 20);
                int b = random.Next(1, 20);
                /** Losuje, czy zadanie bedzie dodawaniem czy odejmowaniem */
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
                /** Losuje, czy zadanie bedzie mnozeniem czy dzieleniem */
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
                /** Losuje, czy zadanie bedzie potega czy pierwiastkiem */
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
            * Wyswietla pytanie i aktywuje kontrolki do wprowadzenia odpowiedzi.
            */
            labelZadanie.Text = pytanie; // Ustawia tekst pytania
            labelZadanie.Visible = true; // Ustawia widocznosc etykiety z pytaniem
            textBoxZadanie.Visible = true; // Ustawia widocznosc pola tekstowego do odpowiedzi
            buttonSprawdzZadanie.Visible = true; // Ustawia widocznosc przycisku do sprawdzania odpowiedzi
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku sprawdzania odpowiedzi.
        * Sprawdza, czy odpowiedź uzytkownika jest poprawna i wyswietla odpowiedni komunikat.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void buttonSprawdzZadanie_Click(object sender, EventArgs e)
        {
            /** 
            * Probuje przekonwertowac tekst z pola odpowiedzi na liczbe typu double.
            * Sprawdza, czy roznica miedzy odpowiedzia uzytkownika a poprawnym wynikiem 
            * jest mniejsza niz 0.01 (tolerancja bledu).
            */
            if (double.TryParse(textBoxZadanie.Text, out double wynikUzytkownika) &&
                Math.Abs(wynikUzytkownika - wynikPoprawny) < 0.01)
            {
                /** Wyswietla komunikat o poprawnej odpowiedzi */
                MessageBox.Show("Poprawna odpowiedź! Możesz kontynuować.", "Sukces");
                /** Ukrywa zadanie matematyczne po poprawnej odpowiedzi */
                UkryjZadanie();
            }
            else
            {
                /** Wyswietla komunikat o blednej odpowiedzi */
                MessageBox.Show("Błędna odpowiedź. Spróbuj ponownie!", "Błąd");
            }
        }
        /** 
        * Ukrywa kontrolki zwiazane z zadaniem matematycznym.
        * Ta metoda jest uzywana do ukrywania panelu z zadaniem oraz 
        * resetowania widocznosci kontrolek, aby przygotowac interfejs 
        * do nastepnego zadania lub do powrotu do gry.
        */
        private void UkryjZadanie()
        {
            /** Ukrywa panel z zadaniem matematycznym */
            panelZadanie.Hide();
            /** Ustawia widocznosc etykiety z pytaniem na false */
            labelZadanie.Visible = false;
            /** Ustawia widocznosc pola tekstowego na false */
            textBoxZadanie.Visible = false;
            /** Ustawia widocznosc przycisku do sprawdzania odpowiedzi na false */
            buttonSprawdzZadanie.Visible = false;
            /** Resetuje tekst w polu odpowiedzi do wartosci null */
            textBoxZadanie.Text = null;
        }

        /** 
        * Przeslania metode ProcessCmdKey, aby obsluzyc nacisniecia klawiszy.
        * Umozliwia poruszanie sie gracza w labiryncie na podstawie nacisnietego klawisza.
        * @param msg Referencja do wiadomosci, ktora zawiera informacje o nacisnietym klawiszu.
        * @param keyData Klawisz, ktory zostal nacisniety.
        * @return bool - Zwraca true, jesli klawisz zostal przetworzony, w przeciwnym razie false.
        */
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Sprawdzenie, czy obiekt gracz jest zainicjalizowany
            /** Sprawdzenie, czy obiekt gracz jest zainicjalizowany */
            if (gracz != null)
            {
                /** Sprawdza, czy gracz moze sie poruszac w kierunku wskazanym przez nacisniety klawisz */
                if (CzyMoznaRuszac(gracz.X, gracz.Y, keyData))
                {
                    // Wywolanie metody przesuniecie gracza na podstawie wcisnietego klawisza
                    /** Wywolanie metody przesuniecia gracza na podstawie wcisnietego klawisza */
                    gracz.Rusz(keyData, poziom.Szerokosc, poziom.Wysokosc);
                    // Sprawdzamy, czy gracz wszedl w checkpoint
                    /** Sprawdzamy, czy gracz wszedl w checkpoint */
                    CzyWszedlWCheckpoint();
                    /** Sprawdzamy, czy gracz ukonczyl poziom */
                    CzyGraczUkonczylPoziom();
                    // Odswieza panel, aby zaktualizowaæ pozycje gracza
                    /** Odswieza panel, aby zaktualizowac pozycje gracza */
                    panelGry.Invalidate();
                }
            }
            /** Wywoluje bazowa metode ProcessCmdKey, aby zapewnic domyslna obsluge klawiszy */
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /** 
        * Obsluguje zdarzenie malowania panelu gry.
        * Rysuje labirynt oraz pozycje gracza na panelu gry.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia malowania, zawierajace informacje o grafice.
        */
        private void panelGry_Paint(object sender, PaintEventArgs e)
        {
            /** Sprawdza, czy labirynt i poziom sa zainicjalizowane */
            if (labirynt != null && poziom != null)
            {
                /** 
                * Oblicza szerokosc i wysokosc komorki na podstawie rozmiaru panelu gry 
                * oraz wymiarow poziomu.
                */
                int szerokoscKomorki = panelGry.Width / poziom.Szerokosc;
                int wysokoscKomorki = panelGry.Height / poziom.Wysokosc;
                // Wywolanie metody rysujacej labirynt
                /** Wywolanie metody rysujacej labirynt na podstawie aktualnych parametrow */
                labirynt.RysujLabirynt(e.Graphics, szerokoscKomorki, wysokoscKomorki, poziom.Start, poziom.End, poziom.Checkpoints);
                // Rysowanie gracza
                /** Rysowanie gracza na panelu, jesli obiekt gracz jest zainicjalizowany */
                if (gracz != null)
                {
                    e.Graphics.FillRectangle(Brushes.Blue, gracz.X * szerokoscKomorki, gracz.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                }
            }
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku wyboru latwego poziomu. 
        * Ustawia poziom trudnosci na latwy i przygotowuje interfejs do gry. 
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void easyLevel_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel z informacjami o grze */
            panelPodczasgry.Hide();
            /** Ustawia poziom trudnosci na latwy */
            UstawPoziom(Poziom.Easy);
            /** Ukrywa etykiete z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa panel gry przed rozpoczeciem poziomu */
            panelGry.Hide();
            /** Wyswietla komunikat informujacy o ustawieniu poziomu oraz czasie na zapamietanie labiryntu */
            MessageBox.Show($"Ustawiono poziom: Łatwy\nZadanie będzie obejmować dodawanie i odejmowanie.\nMasz {czasPrezentacji} sekund na zapamiętanie ukladu labiryntu. Po tym czasie rozpocznie sie poziom.\nZapamietaj Labirynt");
            /** Wyswietla panel gry oraz inne elementy interfejsu */
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku wyboru latwego poziomu. 
        * Ustawia poziom trudnosci na latwy i przygotowuje interfejs do gry. 
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void mediumLevel_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel z informacjami o grze */
            panelPodczasgry.Hide();
            /** Ustawia poziom trudnosci na sredni */
            UstawPoziom(Poziom.Meduim);
            /** Ukrywa etykiete z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa panel gry przed rozpoczeciem poziomu */
            panelGry.Hide();
            /** Wyswietla komunikat informujacy o ustawieniu poziomu oraz czasie na zapamietanie labiryntu */
            MessageBox.Show($"Ustawiono poziom: Średni\nZadanie będzie obejmować mnożenie i dzielenie.\nMasz {czasPrezentacji} sekund na zapamiętanie ukladu labiryntu. Po tym czasie rozpocznie sie poziom.\nZapamietaj Labirynt");
            /** Wyswietla panel gry oraz inne elementy interfejsu */
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku wyboru latwego poziomu. 
        * Ustawia poziom trudnosci na latwy i przygotowuje interfejs do gry. 
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void hardLevel_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel z informacjami o grze */
            panelPodczasgry.Hide();
            /** Ustawia poziom trudnosci na trudny */
            UstawPoziom(Poziom.Hard);
            /** Ukrywa etykiete z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa panel gry przed rozpoczeciem poziomu */
            panelGry.Hide();
            /** Wyswietla komunikat informujacy o ustawieniu poziomu oraz czasie na zapamietanie labiryntu */
            MessageBox.Show($"Ustawiono poziom: Trudny\nZadanie będzie obejmować potęgi i pierwiastki.\nMasz {czasPrezentacji} sekund na zapamiętanie układu labiryntu. Po tym czasie rozpocznie sie poziom.\nZapamietaj Labirynt");
            /** Wyswietla panel gry oraz inne elementy interfejsu */
            panelGry.Show();
            panelPodczasgry.Show();
            text_poziom_Label.Show();
            czasLabel.Show();
            panelGry.Invalidate();
        }

        /** 
        * Obsluguje zdarzenie tick timera poziomu.
        * Zmniejsza pozostaly czas i aktualizuje etykiete z czasem. 
        * Sprawdza, czy czas sie skonczyl, a jesli tak, zatrzymuje timer  i informuje gracza o przegranej.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void timerLevel_Tick(object sender, EventArgs e)
        {
            /** Zmniejsza pozostaly czas o 1 sekunde */
            pozostalyCzas--;
            /** Aktualizuje etykiete z pozostalym czasem */
            czasLabel.Text = $"Pozostały czas: {pozostalyCzas}s";
            /** Sprawdza, czy pozostaly czas wynosi 0 lub mniej */
            if (pozostalyCzas <= 0)
            {
                /** Zatrzymuje timer poziomu */
                timerLevel.Stop();
                /** Wyswietla komunikat o uplywie czasu i przegranej */
                MessageBox.Show("Czas minał! Przegrałeś.", "Koniec gry");
                /** Ukrywa zadanie matematyczne, jesli jest wyswietlane */
                UkryjZadanie();
                /** Restartuje poziom */
                RestartujPoziom();
            }
        }

        /** 
        * Restartuje aktualny poziom, przywracajac go do stanu poczatkowego. 
        * Zatrzymuje timer poziomu i przywraca checkpointy.
        */
        private void RestartujPoziom()
        {
            /** Sprawdza, czy timer poziomu jest aktywny, a jesli tak, zatrzymuje go i zwalnia zasoby */
            if (timerLevel != null)
            {
                timerLevel.Stop();
                timerLevel.Dispose();
            }
            /** Przywraca checkpointy do stanu poczatkowego */
            poziom.PrzywrocCheckpointy();
            /** Ustawia aktualny poziom do stanu poczatkowego */
            UstawPoziom(poziom);
        }

        /** 
        * Obsluguje zdarzenie tick timera prezentacji. 
        * Zmniejsza czas prezentacji i aktualizuje etykiete z pozostalym czasem. 
        * Po zakonczeniu prezentacji przywraca gracza i rozpoczyna poziom. 
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void timerShow_Tick(object sender, EventArgs e)
        {
            /** Zmniejsza czas prezentacji o 1 sekunde */
            czasPrezentacji--;
            /** Sprawdza, czy czas prezentacji wynosi 0 lub mniej */
            if (czasPrezentacji <= 0)
            {
                /** Zatrzymuje timer prezentacji */
                timerShow.Stop();
                /** Ustawia etykiete z pozostalym czasem na czas poziomu */
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
                /** Aktualizuje etykiete z czasem na zapamietanie */
                czasLabel.Text = $"Czas na zapamiętanie: {czasPrezentacji}s";
            }
            panelGry.Invalidate(); // Odswiez panel
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku powrotu do wyboru poziomu.
        * Ukrywa aktualne panele gry i sterowania, a nastepnie wyswietla panel wyboru poziomu.
        * Resetuje czasy prezentacji i pozostaly czas. 
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void returnLevel_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel gry */
            panelGry.Hide();
            /** Ukrywa panel sterowania */
            PanelSterowanieMenuMain.Hide();
            /** Wyswietla panel wyboru poziomu */
            PanelPoziomy.Show();
            /** Resetuje czas prezentacji do zera */
            czasPrezentacji = 0;
            /** Resetuje pozostaly czas do zera */
            pozostalyCzas = 0;
            /** Ukrywa etykiete z poziomem */
            text_poziom_Label.Hide();
            /** Ukrywa etykiete z czasem */
            czasLabel.Hide();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku pauzy.
        * Wstrzymuje lub wznawia gre w zaleznosci od aktualnego stanu.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void pauseButton_Click(object sender, EventArgs e)
        {
            /** Sprawdza, czy gra jest aktualnie wstrzymana */
            if (graZatrzymana)
            {
                /** Wznawiamy gre, jesli byla wstrzymana */
                WznowWlasciwyTimer();
                /** Zmienia tekst przycisku na "Pauza" */
                pauseButton.Text = "Pauza";
                /** Ustawia flage wstrzymania na false */
                graZatrzymana = false;
            }
            else
            {
                /** Zatrzymujemy gre, jesli nie byla wstrzymana */
                ZatrzymajWszystkieTimery(); // Zatrzymaj liczniki czasu
                /** Wyswietla komunikat informujacy o wstrzymaniu gry */
                MessageBox.Show("Gra jest wstrzymana. Kliknij przycisk 'Wznów', aby kontynuować.", "Pauza");
                /** Zmienia tekst przycisku na "Wznow" */
                pauseButton.Text = "Wznów";
                /** Ustawia flage wstrzymania na true */
                graZatrzymana = true;
            }
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku powrotu do wyboru poziomu.
        * Ukrywa glowne menu i wyswietla panel wyboru poziomu.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void returnLevel2_Click(object sender, EventArgs e)
        {
            /** Ukrywa glowne menu */
            PanelMenuMain.Hide();
            /** Wyswietla panel wyboru poziomu */
            PanelPoziomy.Show();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku wyjscia z gry.
        * Zamyka aplikacje.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void exitButton2_Click(object sender, EventArgs e)
        {
            /** Zamyka aplikacje */
            Application.Exit();
        }

        /** 
        * Zatrzymuje wszystkie aktywne timery w grze.
        * Sprawdza, czy timery sa zainicjalizowane i aktywne, a nastepnie je zatrzymuje.
        */
        private void ZatrzymajWszystkieTimery()
        {
            /** Zatrzymuje timer prezentacji, jesli jest aktywny */
            if (timerShow != null && timerShow.Enabled) timerShow.Stop();
            /** Zatrzymuje timer poziomu, jesli jest aktywny */
            if (timerLevel != null && timerLevel.Enabled) timerLevel.Stop();
        }

        /** 
        * Wznawia odpowiedni timer w zaleznosci od pozostalego czasu.
        * Jesli czas prezentacji jest wiekszy niz 0, wznawia timer prezentacji.
        * W przeciwnym razie, jesli pozostaly czas poziomu jest wiekszy niz 0, 
        * wznawia timer poziomu.
        */
        private void WznowWlasciwyTimer()
        {
            /** Wznawia timer prezentacji, jesli czas prezentacji jest wiekszy niz 0 i timer jest zainicjalizowany. */
            if (czasPrezentacji > 0 && timerShow != null) timerShow.Start();
            /** Wznawia timer poziomu, jesli pozostaly czas jest wiekszy niz 0  i timer jest zainicjalizowany */
            else if (pozostalyCzas > 0 && timerLevel != null) timerLevel.Start();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku do wyswietlania panelu sterowania.
        * Zatrzymuje wszystkie timery, ukrywa aktualne panele i wyswietla panel sterowania gry.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void controlsButton2_Click(object sender, EventArgs e)
        {
            /** Zatrzymuje wszystkie aktywne timery w grze */
            ZatrzymajWszystkieTimery();
            /** Ukrywa panel gry */
            panelGry.Hide();
            /** Ukrywa glowne menu */
            PanelMenuMain.Hide();
            /** Ukrywa panel wyboru poziomu */
            PanelPoziomy.Hide();
            /** Ukrywa etykiete z czasem */
            czasLabel.Hide();
            /** Ukrywa etykiete z poziomem */
            text_poziom_Label.Hide();
            /** Wyswietla panel sterowania gry */
            PanelSterowanieGra.Show();
        }

        /** 
        * Obsluguje zdarzenie klikniecia przycisku powrotu do wyboru poziomu z panelu sterowania.
        * Ukrywa panel sterowania i wyswietla panel wyboru poziomu.
        * @param sender Obiekt, ktory wywolal zdarzenie.
        * @param e Argumenty zdarzenia.
        */
        private void returnMenu3_Click(object sender, EventArgs e)
        {
            /** Ukrywa panel sterowania gry */
            PanelSterowanieGra.Hide();
            /** Wyswietla panel wyboru poziomu */
            PanelPoziomy.Show();
        }

        private void timerDelay_Tick(object sender, EventArgs e)
        {
            timerDelay.Stop(); // Zatrzymanie tymczasowego timera
            timerDelay.Dispose(); // Zwolnienie zasobów
            timerShow.Start(); // Rozpoczęcie właściwego timera prezentacji
        }
    }
}