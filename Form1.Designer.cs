namespace Labirynty
{
    partial class Form1
    {
        private System.Windows.Forms.Panel panelGry;
        private System.Windows.Forms.Label czasLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button ControlsButton;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.Panel PanelPoziomy;
        private System.Windows.Forms.Button hardLevel;
        private System.Windows.Forms.Button mediumLevel;
        private System.Windows.Forms.Button easyLevel;

        private void InitializeComponent()
        {
            panelGry = new Panel();
            czasLabel = new Label();
            startButton = new Button();
            exitButton = new Button();
            PanelMenu = new Panel();
            PanelPoziomy = new Panel();
            retrunButton = new Button();
            ControlsButton = new Button();
            Text_poziomy = new Label();
            hardLevel = new Button();
            mediumLevel = new Button();
            easyLevel = new Button();
            PanelMenu.SuspendLayout();
            PanelPoziomy.SuspendLayout();
            SuspendLayout();
            // 
            // panelGry
            // 
            panelGry.Location = new Point(10, 30);
            panelGry.Margin = new Padding(3, 2, 3, 2);
            panelGry.Name = "panelGry";
            panelGry.Size = new Size(525, 450);
            panelGry.TabIndex = 0;
            panelGry.Visible = false;
            panelGry.Paint += panelGry_Paint;
            // 
            // czasLabel
            // 
            czasLabel.AutoSize = true;
            czasLabel.Location = new Point(1142, 39);
            czasLabel.Name = "czasLabel";
            czasLabel.Size = new Size(94, 15);
            czasLabel.TabIndex = 1;
            czasLabel.Text = "Pozostały czas: 0";
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
            // PanelMenu
            // 
            PanelMenu.Controls.Add(startButton);
            PanelMenu.Controls.Add(exitButton);
            PanelMenu.Location = new Point(1132, 57);
            PanelMenu.Name = "PanelMenu";
            PanelMenu.Size = new Size(120, 160);
            PanelMenu.TabIndex = 6;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 985);
            Controls.Add(PanelMenu);
            Controls.Add(panelGry);
            Controls.Add(czasLabel);
            Controls.Add(PanelPoziomy);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Labirynty";
            PanelMenu.ResumeLayout(false);
            PanelPoziomy.ResumeLayout(false);
            PanelPoziomy.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label Text_poziomy;
        private Button retrunButton;
    }
}
