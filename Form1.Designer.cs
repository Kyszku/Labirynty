﻿namespace Labirynty{
    partial class Form1{
        private System.Windows.Forms.Panel panelGry;
        private System.Windows.Forms.Label czasLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Panel PanelMenuMain;
        private System.Windows.Forms.Panel PanelPoziomy;
        private System.Windows.Forms.Button hardLevel;
        private System.Windows.Forms.Button mediumLevel;
        private System.Windows.Forms.Button easyLevel;
        private System.Windows.Forms.Label Text_poziomy;
        private System.Windows.Forms.Button retrunButton;
        private System.Windows.Forms.Button controlsButton;
        private System.Windows.Forms.Label textSterowanie;
        private System.Windows.Forms.Panel PanelSterowanie;
        private System.Windows.Forms.Button returnMenu2;
        private System.Windows.Forms.Label text_poziom_Label;

        private System.Windows.Forms.Panel panelZadanie;
        private System.Windows.Forms.Button buttonSprawdzZadanie;
        private System.Windows.Forms.TextBox textBoxZadanie;
        private System.Windows.Forms.Label labelZadanie;

        private System.Windows.Forms.Timer timerLevel;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelGry = new Panel();
            panelZadanie = new Panel();
            buttonSprawdzZadanie = new Button();
            textBoxZadanie = new TextBox();
            labelZadanie = new Label();
            czasLabel = new Label();
            startButton = new Button();
            exitButton = new Button();
            PanelMenuMain = new Panel();
            controlsButton = new Button();
            textSterowanie = new Label();
            PanelPoziomy = new Panel();
            retrunButton = new Button();
            Text_poziomy = new Label();
            hardLevel = new Button();
            mediumLevel = new Button();
            easyLevel = new Button();
            PanelSterowanie = new Panel();
            returnMenu2 = new Button();
            text_poziom_Label = new Label();
            timerLevel = new System.Windows.Forms.Timer(components);
            timerShow = new System.Windows.Forms.Timer(components);
            panelGry.SuspendLayout();
            panelZadanie.SuspendLayout();
            PanelMenuMain.SuspendLayout();
            PanelPoziomy.SuspendLayout();
            PanelSterowanie.SuspendLayout();
            SuspendLayout();
            // 
            // panelGry
            // 
            panelGry.Controls.Add(panelZadanie);
            panelGry.Location = new Point(10, 40);
            panelGry.Margin = new Padding(3, 2, 3, 2);
            panelGry.Name = "panelGry";
            panelGry.Size = new Size(1100, 900);
            panelGry.TabIndex = 0;
            panelGry.Visible = false;
            panelGry.Paint += panelGry_Paint;
            // 
            // panelZadanie
            // 
            panelZadanie.Controls.Add(buttonSprawdzZadanie);
            panelZadanie.Controls.Add(textBoxZadanie);
            panelZadanie.Controls.Add(labelZadanie);
            panelZadanie.Location = new Point(376, 172);
            panelZadanie.Name = "panelZadanie";
            panelZadanie.Size = new Size(400, 300);
            panelZadanie.TabIndex = 10;
            panelZadanie.Visible = false;
            // 
            // buttonSprawdzZadanie
            // 
            buttonSprawdzZadanie.Location = new Point(153, 237);
            buttonSprawdzZadanie.Name = "buttonSprawdzZadanie";
            buttonSprawdzZadanie.Size = new Size(75, 23);
            buttonSprawdzZadanie.TabIndex = 0;
            buttonSprawdzZadanie.Text = "Sprawdź";
            buttonSprawdzZadanie.UseVisualStyleBackColor = true;
            buttonSprawdzZadanie.Visible = false;
            buttonSprawdzZadanie.Click += buttonSprawdzZadanie_Click;
            // 
            // textBoxZadanie
            // 
            textBoxZadanie.Location = new Point(3, 170);
            textBoxZadanie.Name = "textBoxZadanie";
            textBoxZadanie.Size = new Size(394, 23);
            textBoxZadanie.TabIndex = 1;
            textBoxZadanie.Visible = false;
            // 
            // labelZadanie
            // 
            labelZadanie.AutoSize = true;
            labelZadanie.Location = new Point(3, 23);
            labelZadanie.Name = "labelZadanie";
            labelZadanie.Size = new Size(0, 15);
            labelZadanie.TabIndex = 2;
            labelZadanie.Visible = false;
            // 
            // czasLabel
            // 
            czasLabel.AutoSize = true;
            czasLabel.Location = new Point(1016, 23);
            czasLabel.Name = "czasLabel";
            czasLabel.Size = new Size(94, 15);
            czasLabel.TabIndex = 1;
            czasLabel.Text = "Pozostały czas: 0";
            czasLabel.Visible = false;
            // 
            // startButton
            // 
            startButton.Location = new Point(10, 17);
            startButton.Margin = new Padding(3, 2, 3, 2);
            startButton.Name = "startButton";
            startButton.Size = new Size(100, 38);
            startButton.TabIndex = 2;
            startButton.Text = "Rozpocznij Gre";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(10, 101);
            exitButton.Margin = new Padding(3, 2, 3, 2);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(100, 38);
            exitButton.TabIndex = 3;
            exitButton.Text = "Wyjście";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // PanelMenuMain
            // 
            PanelMenuMain.Controls.Add(controlsButton);
            PanelMenuMain.Controls.Add(startButton);
            PanelMenuMain.Controls.Add(exitButton);
            PanelMenuMain.Location = new Point(1132, 57);
            PanelMenuMain.Name = "PanelMenuMain";
            PanelMenuMain.Size = new Size(120, 160);
            PanelMenuMain.TabIndex = 6;
            // 
            // controlsButton
            // 
            controlsButton.Location = new Point(10, 59);
            controlsButton.Margin = new Padding(3, 2, 3, 2);
            controlsButton.Name = "controlsButton";
            controlsButton.Size = new Size(100, 38);
            controlsButton.TabIndex = 4;
            controlsButton.Text = "Sterowanie";
            controlsButton.UseVisualStyleBackColor = true;
            controlsButton.Click += controlsButton_Click;
            // 
            // textSterowanie
            // 
            textSterowanie.AutoSize = true;
            textSterowanie.Location = new Point(14, 9);
            textSterowanie.Name = "textSterowanie";
            textSterowanie.Size = new Size(176, 75);
            textSterowanie.TabIndex = 5;
            textSterowanie.Text = "Sterowanie:\r\nW - Przesunięcie gracza w góre\r\nS - Przesunięcie gracza w dół\r\nA - Przesunięcie gracza w lewo\r\nD - Przesunięcie gracza w prawo";
            textSterowanie.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PanelPoziomy
            // 
            PanelPoziomy.Controls.Add(retrunButton);
            PanelPoziomy.Controls.Add(Text_poziomy);
            PanelPoziomy.Controls.Add(hardLevel);
            PanelPoziomy.Controls.Add(mediumLevel);
            PanelPoziomy.Controls.Add(easyLevel);
            PanelPoziomy.Location = new Point(1137, 58);
            PanelPoziomy.Name = "PanelPoziomy";
            PanelPoziomy.Size = new Size(110, 200);
            PanelPoziomy.TabIndex = 7;
            PanelPoziomy.Visible = false;
            // 
            // retrunButton
            // 
            retrunButton.Font = new Font("Segoe UI", 7F);
            retrunButton.Location = new Point(5, 154);
            retrunButton.Name = "retrunButton";
            retrunButton.Size = new Size(100, 38);
            retrunButton.TabIndex = 9;
            retrunButton.Text = "Powrót do menu";
            retrunButton.UseVisualStyleBackColor = true;
            retrunButton.Click += retrunButton_Click;
            // 
            // Text_poziomy
            // 
            Text_poziomy.AutoSize = true;
            Text_poziomy.Location = new Point(30, 5);
            Text_poziomy.Name = "Text_poziomy";
            Text_poziomy.Size = new Size(50, 15);
            Text_poziomy.TabIndex = 8;
            Text_poziomy.Text = "Poziom:";
            // 
            // hardLevel
            // 
            hardLevel.Location = new Point(5, 110);
            hardLevel.Name = "hardLevel";
            hardLevel.Size = new Size(100, 38);
            hardLevel.TabIndex = 2;
            hardLevel.Text = "Trudny";
            hardLevel.UseVisualStyleBackColor = true;
            hardLevel.Click += hardLevel_Click;
            // 
            // mediumLevel
            // 
            mediumLevel.Location = new Point(5, 65);
            mediumLevel.Name = "mediumLevel";
            mediumLevel.Size = new Size(100, 38);
            mediumLevel.TabIndex = 1;
            mediumLevel.Text = "Średni";
            mediumLevel.UseVisualStyleBackColor = true;
            mediumLevel.Click += mediumLevel_Click;
            // 
            // easyLevel
            // 
            easyLevel.Location = new Point(5, 20);
            easyLevel.Name = "easyLevel";
            easyLevel.Size = new Size(100, 38);
            easyLevel.TabIndex = 0;
            easyLevel.Text = "Łatwy";
            easyLevel.UseVisualStyleBackColor = true;
            easyLevel.Click += easyLevel_Click;
            // 
            // PanelSterowanie
            // 
            PanelSterowanie.Controls.Add(returnMenu2);
            PanelSterowanie.Controls.Add(textSterowanie);
            PanelSterowanie.Location = new Point(570, 120);
            PanelSterowanie.Name = "PanelSterowanie";
            PanelSterowanie.Size = new Size(280, 160);
            PanelSterowanie.TabIndex = 8;
            PanelSterowanie.Visible = false;
            // 
            // returnMenu2
            // 
            returnMenu2.Font = new Font("Segoe UI", 7F);
            returnMenu2.Location = new Point(90, 110);
            returnMenu2.Name = "returnMenu2";
            returnMenu2.Size = new Size(100, 38);
            returnMenu2.TabIndex = 10;
            returnMenu2.Text = "Powrót do menu";
            returnMenu2.UseVisualStyleBackColor = true;
            returnMenu2.Click += returnMenu2_Click;
            // 
            // text_poziom_Label
            // 
            text_poziom_Label.AutoSize = true;
            text_poziom_Label.Location = new Point(12, 23);
            text_poziom_Label.Name = "text_poziom_Label";
            text_poziom_Label.Size = new Size(77, 15);
            text_poziom_Label.TabIndex = 9;
            text_poziom_Label.Text = "Dany poziom";
            text_poziom_Label.Visible = false;
            // 
            // timerLevel
            // 
            timerLevel.Tick += timerLevel_Tick;
            // 
            // timerShow
            // 
            timerShow.Tick += timerShow_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 985);
            Controls.Add(text_poziom_Label);
            Controls.Add(PanelSterowanie);
            Controls.Add(panelGry);
            Controls.Add(czasLabel);
            Controls.Add(PanelPoziomy);
            Controls.Add(PanelMenuMain);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Labirynty";
            panelGry.ResumeLayout(false);
            panelZadanie.ResumeLayout(false);
            panelZadanie.PerformLayout();
            PanelMenuMain.ResumeLayout(false);
            PanelPoziomy.ResumeLayout(false);
            PanelPoziomy.PerformLayout();
            PanelSterowanie.ResumeLayout(false);
            PanelSterowanie.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Timer timerShow;
    }
}
