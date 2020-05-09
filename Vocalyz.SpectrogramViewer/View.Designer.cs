namespace Vocalyz.SpectrogramViewer
{
    partial class View
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.fftSizeTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timeStepTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox1 = new Vocalyz.SpectrogramViewer.NoAntialiasingPicturebox();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(15, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1402, 409);
            this.panel1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 465);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "Play";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 504);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(165, 34);
            this.button3.TabIndex = 4;
            this.button3.Text = "Pause";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.spectrogramToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1429, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fichierToolStripMenuItem.Text = "File";
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.ouvrirToolStripMenuItem.Text = "Open";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // spectrogramToolStripMenuItem
            // 
            this.spectrogramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.spectrogramToolStripMenuItem.Name = "spectrogramToolStripMenuItem";
            this.spectrogramToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.spectrogramToolStripMenuItem.Text = "Spectrogram";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = " MP3 File |*.mp3| WAV File |*.wav| AIFF File |*.aiff | All Files |*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = " BMP File |*.bmp";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 544);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fftSizeTxt
            // 
            this.fftSizeTxt.Location = new System.Drawing.Point(312, 465);
            this.fftSizeTxt.Name = "fftSizeTxt";
            this.fftSizeTxt.Size = new System.Drawing.Size(100, 20);
            this.fftSizeTxt.TabIndex = 7;
            this.fftSizeTxt.Text = "4096";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 468);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "FFT Size :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(418, 468);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Time Step :";
            // 
            // timeStepTxt
            // 
            this.timeStepTxt.Location = new System.Drawing.Point(496, 465);
            this.timeStepTxt.Name = "timeStepTxt";
            this.timeStepTxt.Size = new System.Drawing.Size(100, 20);
            this.timeStepTxt.TabIndex = 9;
            this.timeStepTxt.Text = "0,01";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 494);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Maximum Frequency :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(312, 491);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "4000";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(188, 544);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(408, 33);
            this.button4.TabIndex = 13;
            this.button4.Text = "Reload";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(800, 465);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(617, 112);
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.Text = "Open a file to start";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(602, 465);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(192, 33);
            this.button5.TabIndex = 17;
            this.button5.Text = "Identify";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(319, 267);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 593);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timeStepTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fftSizeTxt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vocalyz.SpectrogramViewer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox fftSizeTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox timeStepTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private NoAntialiasingPicturebox pictureBox1;
        private System.Windows.Forms.Button button5;
    }
}

