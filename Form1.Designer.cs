namespace Labirynty
{
    partial class Form1
    {
        private System.Windows.Forms.Panel panelGry;
        private System.Windows.Forms.Label czasLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem poziomMenuItem;
        private System.Windows.Forms.ToolStripMenuItem latwyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sredniMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trudnyMenuItem;
        private System.Windows.Forms.Button ControlsButton;
        //private System.Windows.Forms.ToolStripMenuItem wyjscieMenuItem;

        private void InitializeComponent()
        {
            panelGry = new Panel();
            czasLabel = new Label();
            startButton = new Button();
            exitButton = new Button();
            menuStrip1 = new MenuStrip();
            poziomMenuItem = new ToolStripMenuItem();
            latwyMenuItem = new ToolStripMenuItem();
            sredniMenuItem = new ToolStripMenuItem();
            trudnyMenuItem = new ToolStripMenuItem();
            ControlsButton = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panelGry
            // 
            panelGry.Location = new Point(10, 30);
            panelGry.Margin = new Padding(3, 2, 3, 2);
            panelGry.Name = "panelGry";
            panelGry.Size = new Size(525, 450);
            panelGry.TabIndex = 0;
            panelGry.Paint += panelGry_Paint;
            // 
            // czasLabel
            // 
            czasLabel.AutoSize = true;
            czasLabel.Location = new Point(1158, 39);
            czasLabel.Name = "czasLabel";
            czasLabel.Size = new Size(94, 15);
            czasLabel.TabIndex = 1;
            czasLabel.Text = "Pozostały czas: 0";
            // 
            // startButton
            // 
            startButton.Location = new Point(1164, 69);
            startButton.Margin = new Padding(3, 2, 3, 2);
            startButton.Name = "startButton";
            startButton.Size = new Size(88, 38);
            startButton.TabIndex = 2;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(1164, 153);
            exitButton.Margin = new Padding(3, 2, 3, 2);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(88, 38);
            exitButton.TabIndex = 3;
            exitButton.Text = "Wyjście";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { poziomMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(1264, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // poziomMenuItem
            // 
            poziomMenuItem.DropDownItems.AddRange(new ToolStripItem[] { latwyMenuItem, sredniMenuItem, trudnyMenuItem });
            poziomMenuItem.Name = "poziomMenuItem";
            poziomMenuItem.Size = new Size(59, 20);
            poziomMenuItem.Text = "Poziom";
            // 
            // latwyMenuItem
            // 
            latwyMenuItem.Name = "latwyMenuItem";
            latwyMenuItem.Size = new Size(110, 22);
            latwyMenuItem.Text = "Łatwy";
            latwyMenuItem.Click += latwyMenuItem_Click;
            // 
            // sredniMenuItem
            // 
            sredniMenuItem.Name = "sredniMenuItem";
            sredniMenuItem.Size = new Size(110, 22);
            sredniMenuItem.Text = "Średni";
            sredniMenuItem.Click += sredniMenuItem_Click;
            // 
            // trudnyMenuItem
            // 
            trudnyMenuItem.Name = "trudnyMenuItem";
            trudnyMenuItem.Size = new Size(110, 22);
            trudnyMenuItem.Text = "Trudny";
            trudnyMenuItem.Click += trudnyMenuItem_Click;
            // 
            // ControlsButton
            // 
            ControlsButton.Location = new Point(1164, 111);
            ControlsButton.Margin = new Padding(3, 2, 3, 2);
            ControlsButton.Name = "ControlsButton";
            ControlsButton.Size = new Size(88, 38);
            ControlsButton.TabIndex = 5;
            ControlsButton.Text = "Sterowanie";
            ControlsButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 985);
            Controls.Add(ControlsButton);
            Controls.Add(menuStrip1);
            Controls.Add(panelGry);
            Controls.Add(czasLabel);
            Controls.Add(startButton);
            Controls.Add(exitButton);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Labirynty";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
