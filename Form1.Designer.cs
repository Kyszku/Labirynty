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
        //private System.Windows.Forms.ToolStripMenuItem wyjscieMenuItem;

        private void InitializeComponent()
        {
            this.panelGry = new System.Windows.Forms.Panel();
            this.czasLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.poziomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.latwyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sredniMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trudnyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.wyjscieMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.poziomMenuItem });
            //this.wyjscieMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";

            // poziomMenuItem
            this.poziomMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.latwyMenuItem,
            this.sredniMenuItem,
            this.trudnyMenuItem});
            this.poziomMenuItem.Name = "poziomMenuItem";
            this.poziomMenuItem.Size = new System.Drawing.Size(58, 20);
            this.poziomMenuItem.Text = "Poziom";

            // latwyMenuItem
            this.latwyMenuItem.Name = "latwyMenuItem";
            this.latwyMenuItem.Size = new System.Drawing.Size(110, 22);
            this.latwyMenuItem.Text = "Łatwy";
            this.latwyMenuItem.Click += new System.EventHandler(this.latwyMenuItem_Click);

            // sredniMenuItem
            this.sredniMenuItem.Name = "sredniMenuItem";
            this.sredniMenuItem.Size = new System.Drawing.Size(110, 22);
            this.sredniMenuItem.Text = "Średni";
            this.sredniMenuItem.Click += new System.EventHandler(this.sredniMenuItem_Click);

            // trudnyMenuItem
            this.trudnyMenuItem.Name = "trudnyMenuItem";
            this.trudnyMenuItem.Size = new System.Drawing.Size(110, 22);
            this.trudnyMenuItem.Text = "Trudny";
            this.trudnyMenuItem.Click += new System.EventHandler(this.trudnyMenuItem_Click);

            // wyjscieMenuItem
            //this.wyjscieMenuItem.Name = "wyjscieMenuItem";
            //this.wyjscieMenuItem.Size = new System.Drawing.Size(58, 20);
            //this.wyjscieMenuItem.Text = "Wyjście";
            //this.wyjscieMenuItem.Click += new System.EventHandler(this.exitButton_Click);
            panelGry = new Panel();
            czasLabel = new Label();
            startButton = new Button();
            exitButton = new Button();
            SuspendLayout();

            // panelGry
            this.panelGry.Location = new System.Drawing.Point(12, 40);
            this.panelGry.Name = "panelGry";
            this.panelGry.Size = new System.Drawing.Size(600, 600);
            this.panelGry.TabIndex = 0;
            this.panelGry.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGry_Paint);

            // czasLabel
            this.czasLabel.AutoSize = true;
            this.czasLabel.Location = new System.Drawing.Point(630, 60);
            this.czasLabel.Name = "czasLabel";
            this.czasLabel.Size = new System.Drawing.Size(120, 20);
            this.czasLabel.TabIndex = 1;
            this.czasLabel.Text = "Pozostały czas: 0";

            // startButton
            this.startButton.Location = new System.Drawing.Point(630, 100);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 50);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);

            // exitButton
            this.exitButton.Location = new System.Drawing.Point(630, 160);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(100, 50);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Wyjście";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.czasLabel);
            this.Controls.Add(this.panelGry);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Labirynty";
            this.ResumeLayout(false);
            this.PerformLayout();

            // 
            ClientSize = new Size(1008, 681);
            Controls.Add(panelGry);
            Controls.Add(czasLabel);
            Controls.Add(startButton);
            Controls.Add(exitButton);
            Name = "Form1";
            Text = "Labirynty";
            ResumeLayout(false);
        }
    }
}
