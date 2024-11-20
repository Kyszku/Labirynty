using System;
using System.Windows.Forms;

namespace Labirynty
{
    public partial class Form1 : Form
    {
        private Poziom poziom = Poziom.Latwy;
        private Labirynt labirynt;
        private Gracz gracz;

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //labirynt = new Labirynt(poziom.Rozmiar, poziom.Rozmiar);
            //labirynt.GenerujLabirynt();
            //gracz = new Gracz(0, 0);  // Ustawienie gracza na startow� pozycj�
            //pozosta�yCzas = poziom.Czas;
            //czasLabel.Text = $"Pozosta�y czas: {pozosta�yCzas} s";
            //timer.Start();
            this.Focus();
            panelGry.Invalidate();  // Od�wie� panel, aby narysowa� nowy labirynt
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
            PanelSterowanie.Show();
        }

        private void returnMenu2_Click(object sender, EventArgs e)
        {
            PanelSterowanie.Hide();
            PanelMenuMain.Show();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Sprawdzenie, czy obiekt gracz jest zainicjalizowany
            if (gracz != null)
            {
                // Wywo�anie metody przesuni�cia gracza na podstawie wci�ni�tego klawisza
                gracz.Przesun(keyData, poziom.Rozmiar, poziom.Rozmiar);

                // Od�wie� panel, aby zaktualizowa� pozycj� gracza
                panelGry.Invalidate();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void panelGry_Paint(object sender, PaintEventArgs e)
        {
            if (labirynt != null)
            {
                int szerokoscKomorki = panelGry.Width / poziom.Rozmiar;
                int wysokoscKomorki = panelGry.Height / poziom.Rozmiar;

                labirynt.RysujLabirynt(e.Graphics, szerokoscKomorki, wysokoscKomorki);

                // Rysowanie gracza
                e.Graphics.FillRectangle(Brushes.Blue, gracz.X * szerokoscKomorki, gracz.Y * wysokoscKomorki, szerokoscKomorki, wysokoscKomorki);
            }
        }

        private void easyLevel_Click(object sender, EventArgs e)
        {
            text_poziom_Label.Hide();
            panelGry.Hide();
            poziom = Poziom.Latwy;
            MessageBox.Show("Ustawiono poziom: �atwy");
            panelGry.Show();
            labirynt = new Labirynt(poziom.Rozmiar, poziom.Rozmiar);
            labirynt.GenerujLabirynt();
            gracz = new Gracz(0, 0);
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: �atwy";
        }

        private void mediumLevel_Click(object sender, EventArgs e)
        {
            text_poziom_Label.Hide();
            panelGry.Hide();
            poziom = Poziom.Sredni;
            MessageBox.Show("Ustawiono poziom: �redni");
            panelGry.Show();
            labirynt = new Labirynt(poziom.Rozmiar, poziom.Rozmiar);
            labirynt.GenerujLabirynt();
            gracz = new Gracz(0, 0);
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: �redni";
        }

        private void hardLevel_Click(object sender, EventArgs e)
        {
            text_poziom_Label.Hide();
            panelGry.Hide();
            poziom = Poziom.Trudny;
            MessageBox.Show("Ustawiono poziom: Trudny");
            panelGry.Show();
            labirynt = new Labirynt(poziom.Rozmiar, poziom.Rozmiar);
            labirynt.GenerujLabirynt();
            gracz = new Gracz(0, 0);
            text_poziom_Label.Show();
            text_poziom_Label.Text = "Poziom: Trudny";
        }
    }
}
