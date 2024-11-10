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
            labirynt = new Labirynt(poziom.Rozmiar, poziom.Rozmiar);
            labirynt.GenerujLabirynt();
            gracz = new Gracz(0, 0);  // Ustawienie gracza na startow� pozycj�
            //pozosta�yCzas = poziom.Czas;
            //czasLabel.Text = $"Pozosta�y czas: {pozosta�yCzas} s";
            //timer.Start();
            this.Focus();
            panelGry.Invalidate();  // Od�wie� panel, aby narysowa� nowy labirynt
        }

        private void latwyMenuItem_Click(object sender, EventArgs e)
        {
            poziom = Poziom.Latwy;
            MessageBox.Show("Ustawiono poziom: �atwy");
        }

        private void sredniMenuItem_Click(object sender, EventArgs e)
        {
            poziom = Poziom.Sredni;
            MessageBox.Show("Ustawiono poziom: �redni");
        }

        private void trudnyMenuItem_Click(object sender, EventArgs e)
        {
            poziom = Poziom.Trudny;
            MessageBox.Show("Ustawiono poziom: Trudny");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}
