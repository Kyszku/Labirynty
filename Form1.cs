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
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc);
            labirynt.GenerujLabirynt(poziom.Macierz);
            WyswietlLabirynt();
        }

        private void WyswietlLabirynt(){
            if (labirynt == null) return;
            var siatka = labirynt.Siatka;
            string wynik = "";
            for (int y = 0; y < siatka.GetLength(1); y++){
                for (int x = 0; x < siatka.GetLength(0); x++){
                    wynik += siatka[x, y] == 0 ? " " : "#";
                }
                wynik += Environment.NewLine;
            }
        }
        
        private void startButton_Click(object sender, EventArgs e){
            //labirynt = new Labirynt(poziom.Rozmiar, poziom.Rozmiar);
            //labirynt.GenerujLabirynt();
            //gracz = new Gracz(0, 0);  // Ustawienie gracza na startową pozycję
            //pozostałyCzas = poziom.Czas;
            //czasLabel.Text = $"Pozostały czas: {pozostałyCzas} s";
            //timer.Start();
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData){
            // Sprawdzenie, czy obiekt gracz jest zainicjalizowany
            if (gracz != null){
                if (CzyMoznaRuszac(gracz.X, gracz.Y, keyData)){
                    // Wywolanie metody przesuniecie gracza na podstawie wcisnietego klawisza
                    gracz.Rusz(keyData, poziom.Szerokosc, poziom.Wysokosc);
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
            //poziom = Poziom.Latwy;
            MessageBox.Show("Ustawiono poziom: Łatwy");
            panelGry.Show();
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc);
            labirynt.GenerujLabirynt(poziom.Macierz);
            gracz = new Gracz(0, 0);
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: Łatwy";
            panelGry.Invalidate();
        }

        private void mediumLevel_Click(object sender, EventArgs e){
            UstawPoziom(Poziom.Sredni);
            text_poziom_Label.Hide();
            panelGry.Hide();
            //poziom = Poziom.Sredni;
            MessageBox.Show("Ustawiono poziom: Średni");
            panelGry.Show();
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc);
            labirynt.GenerujLabirynt(poziom.Macierz);
            gracz = new Gracz(0, 0);
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: Średni";
            panelGry.Invalidate();
        }

        private void hardLevel_Click(object sender, EventArgs e){
            UstawPoziom(Poziom.Trudny);
            text_poziom_Label.Hide();
            panelGry.Hide();
            //poziom = Poziom.Trudny;
            MessageBox.Show("Ustawiono poziom: Trudny");
            panelGry.Show();
            labirynt = new Labirynt(poziom.Szerokosc, poziom.Wysokosc);
            labirynt.GenerujLabirynt(poziom.Macierz);
            gracz = new Gracz(0, 0);
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: Trudny";
            panelGry.Invalidate();
        }
    }
}
