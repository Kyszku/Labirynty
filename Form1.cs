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
            gracz = new Gracz(0, 0);  // Ustawienie gracza na startow¹ pozycjê
            //pozosta³yCzas = poziom.Czas;
            //czasLabel.Text = $"Pozosta³y czas: {pozosta³yCzas} s";
            //timer.Start();
            this.Focus();
            panelGry.Invalidate();  // Odœwie¿ panel, aby narysowaæ nowy labirynt
        }

        private void latwyMenuItem_Click(object sender, EventArgs e)
        {
            poziom = Poziom.Latwy;
            MessageBox.Show("Ustawiono poziom: £atwy");
        }

        private void sredniMenuItem_Click(object sender, EventArgs e)
        {
            poziom = Poziom.Sredni;
            MessageBox.Show("Ustawiono poziom: Œredni");
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
                // Wywo³anie metody przesuniêcia gracza na podstawie wciœniêtego klawisza
                gracz.Przesun(keyData, poziom.Rozmiar, poziom.Rozmiar);

                // Odœwie¿ panel, aby zaktualizowaæ pozycjê gracza
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
