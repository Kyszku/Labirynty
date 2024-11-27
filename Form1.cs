using System;
using System.Windows.Forms;

namespace Labirynty{
    public partial class Form1 : Form{
        private Labirynt labirynt; // Przechowywanie labiryntu
        private Poziom poziom = Poziom.Latwy; // Aktualny poziom trudności
        private Gracz gracz;   // Obiekt reprezentujący gracza
        private int szerokosc; // Szerokość labiryntu
        private int wysokosc;  // Wysokość labiryntu

        public Form1(){
            InitializeComponent();
        }

        private void UstawPoziom(Poziom poziom){
            this.poziom = poziom;
            poziom.PrzywrocCheckpointy(); // Przywrócenie checkpointów
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc);
            labirynt.GenerujLabirynt(poziom.Macierz);
            gracz = new Gracz(poziom.Start.X, poziom.Start.Y);
        }
    
        private void startButton_Click(object sender, EventArgs e){
            this.Focus();
            panelGry.Invalidate();  // Odśwież panel, aby narysować nowy labirynt
            PanelMenuMain.Hide();
            PanelPoziomy.Show();
        }

        private void exitButton_Click(object sender, EventArgs e){
            Application.Exit();
        }

        private void retrunButton_Click(object sender, EventArgs e){
            PanelPoziomy.Hide();
            PanelMenuMain.Show();
        }

        private void controlsButton_Click(object sender, EventArgs e){
            PanelSterowanie.Show();
        }

        private void returnMenu2_Click(object sender, EventArgs e){
            PanelSterowanie.Hide();
            PanelMenuMain.Show();
        }
        public bool CzyMoznaRuszac(int x, int y, Keys key){
            switch (key){
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
        private void CzyWszedlWCheckpoint(){
            // Sprawdzamy, czy gracz jest na pozycji któregoś z checkpointów
            var checkpoint = poziom.Checkpoints.Find(cp => cp.X == gracz.X && cp.Y == gracz.Y);
            // Jeśli gracz trafił na checkpoint
            if (checkpoint != default){
                poziom.Checkpoints.Remove(checkpoint); // Usuń checkpoint, aby nie wyświetlać ponownie zadania
                WyswietlZadanieMatematyczne(); // Wyświetl pytanie matematyczne
            }
        }

        private void WyswietlZadanieMatematyczne(){
            Random random = new Random();
            bool jestPoprawnaOdpowiedz = false;
            while (!jestPoprawnaOdpowiedz)
            {
                string pytanie = "";
                double poprawnaOdpowiedz = 0;

                if (poziom == Poziom.Latwy)
                {
                    int a = random.Next(1, 20);
                    int b = random.Next(1, 20);
                    if (random.Next(2) == 0)
                    {
                        pytanie = $"{a} + {b} = ?";
                        poprawnaOdpowiedz = a + b;
                    }
                    else
                    {
                        pytanie = $"{a} - {b} = ?";
                        poprawnaOdpowiedz = a - b;
                    }
                }
                else if (poziom == Poziom.Sredni)
                {
                    int a = random.Next(1, 10);
                    int b = random.Next(1, 10);
                    if (random.Next(2) == 0)
                    {
                        pytanie = $"{a} * {b} = ?";
                        poprawnaOdpowiedz = a * b;
                    }
                    else
                    {
                        pytanie = $"{a} / {b} = ?";
                        poprawnaOdpowiedz = Math.Round((double)a / b, 2);
                    }
                }
                else if (poziom == Poziom.Trudny)
                {
                    int a = random.Next(1, 10);
                    if (random.Next(2) == 0)
                    {
                        pytanie = $"{a}^2 = ?";
                        poprawnaOdpowiedz = Math.Pow(a, 2);
                    }
                    else
                    {
                        int b = a * a;
                        pytanie = $"√{b} = ?";
                        poprawnaOdpowiedz = Math.Sqrt(b);
                    }
                }
                // Wyświetlanie pytania i pobieranie odpowiedzi
                string odpowiedz = Microsoft.VisualBasic.Interaction.InputBox(pytanie, "Zadanie Matematyczne", "");

                // Sprawdzanie odpowiedzi
                if (double.TryParse(odpowiedz, out double wynik) && Math.Abs(wynik - poprawnaOdpowiedz) < 0.01){
                    MessageBox.Show("Poprawna odpowiedź! Możesz kontynuować.", "Sukces");
                    jestPoprawnaOdpowiedz = true;
                }
                else{
                    MessageBox.Show($"Błędna odpowiedź. Prawidłowy wynik to: {poprawnaOdpowiedz}.", "Błąd");
                }
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData){
            // Sprawdzenie, czy obiekt gracz jest zainicjalizowany
            if (gracz != null){
                if (CzyMoznaRuszac(gracz.X, gracz.Y, keyData)){
                    // Wywolanie metody przesuniecie gracza na podstawie wcisnietego klawisza
                    gracz.Rusz(keyData, poziom.Szerokosc, poziom.Wysokosc);
                    // Sprawdzamy, czy gracz wszedł w checkpoint
                    CzyWszedlWCheckpoint();
                    // Odswieza panel, aby zaktualizowaæ pozycje gracza
                    panelGry.Invalidate();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void panelGry_Paint(object sender, PaintEventArgs e){
            if (labirynt != null && poziom != null){
                int szerokoscKomorki = panelGry.Width / poziom.Szerokosc;
                int wysokoscKomorki = panelGry.Height / poziom.Wysokosc;
                // Wywołanie metody rysującej labirynt
                labirynt.RysujLabirynt(e.Graphics, szerokoscKomorki, wysokoscKomorki, poziom.Start, poziom.End, poziom.Checkpoints);
                // Rysowanie gracza
                if (gracz != null){
                    e.Graphics.FillRectangle(Brushes.Blue, gracz.X * szerokoscKomorki, gracz.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
                }
            }
        }

        private void easyLevel_Click(object sender, EventArgs e){
            UstawPoziom(Poziom.Latwy);
            text_poziom_Label.Hide();
            panelGry.Hide();
            MessageBox.Show("Ustawiono poziom: Łatwy");
            panelGry.Show();
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: Łatwy";
            panelGry.Invalidate();
        }

        private void mediumLevel_Click(object sender, EventArgs e){
            UstawPoziom(Poziom.Sredni);
            text_poziom_Label.Hide();
            panelGry.Hide(); 
            MessageBox.Show("Ustawiono poziom: Średni");
            panelGry.Show();
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: Średni";
            panelGry.Invalidate();
        }

        private void hardLevel_Click(object sender, EventArgs e){
            UstawPoziom(Poziom.Trudny);
            text_poziom_Label.Hide();
            panelGry.Hide();
            MessageBox.Show("Ustawiono poziom: Trudny");
            panelGry.Show();
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: Trudny";
            panelGry.Invalidate();
        }
    }
}
