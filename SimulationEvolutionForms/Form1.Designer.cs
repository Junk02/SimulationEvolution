namespace SimulationEvolutionForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SimulationTimer = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            ThemeCheckBox = new CheckBox();
            RenderingModeTrackBar = new TrackBar();
            button2 = new Button();
            label3 = new Label();
            richTextBox1 = new RichTextBox();
            SpawnSquareSizeTrackBar = new TrackBar();
            label4 = new Label();
            label5 = new Label();
            MutationChanceTrackBar = new TrackBar();
            label6 = new Label();
            SunTrackBar = new TrackBar();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RenderingModeTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpawnSquareSizeTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MutationChanceTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SunTrackBar).BeginInit();
            SuspendLayout();
            // 
            // SimulationTimer
            // 
            SimulationTimer.Enabled = true;
            SimulationTimer.Interval = 16;
            SimulationTimer.Tick += SimulationTimer_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(72, 68);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(1609, 93);
            button1.Name = "button1";
            button1.Size = new Size(92, 65);
            button1.TabIndex = 2;
            button1.Text = "Spawn entities";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1609, 12);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1608, 38);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 4;
            label2.Text = "label2";
            // 
            // ThemeCheckBox
            // 
            ThemeCheckBox.AutoSize = true;
            ThemeCheckBox.Location = new Point(1927, 12);
            ThemeCheckBox.Name = "ThemeCheckBox";
            ThemeCheckBox.Size = new Size(97, 19);
            ThemeCheckBox.TabIndex = 6;
            ThemeCheckBox.Text = "Ночная тема";
            ThemeCheckBox.UseVisualStyleBackColor = true;
            ThemeCheckBox.CheckedChanged += ThemeCheckBox_CheckedChanged;
            // 
            // RenderingModeTrackBar
            // 
            RenderingModeTrackBar.LargeChange = 1;
            RenderingModeTrackBar.Location = new Point(1609, 180);
            RenderingModeTrackBar.Maximum = 4;
            RenderingModeTrackBar.Name = "RenderingModeTrackBar";
            RenderingModeTrackBar.Size = new Size(382, 45);
            RenderingModeTrackBar.TabIndex = 7;
            RenderingModeTrackBar.ValueChanged += RenderingModeTrackBar_ValueChanged;
            // 
            // button2
            // 
            button2.Location = new Point(1707, 93);
            button2.Name = "button2";
            button2.Size = new Size(92, 65);
            button2.TabIndex = 8;
            button2.Text = "Delete all";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1609, 66);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 9;
            label3.Text = "label3";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(1609, 880);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            richTextBox1.Size = new Size(415, 323);
            richTextBox1.TabIndex = 11;
            richTextBox1.Text = "";
            // 
            // SpawnSquareSizeTrackBar
            // 
            SpawnSquareSizeTrackBar.LargeChange = 1;
            SpawnSquareSizeTrackBar.Location = new Point(1609, 231);
            SpawnSquareSizeTrackBar.Minimum = 1;
            SpawnSquareSizeTrackBar.Name = "SpawnSquareSizeTrackBar";
            SpawnSquareSizeTrackBar.Size = new Size(382, 45);
            SpawnSquareSizeTrackBar.TabIndex = 12;
            SpawnSquareSizeTrackBar.Value = 1;
            SpawnSquareSizeTrackBar.ValueChanged += SpawnSquareSizeTrackBar_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1749, 12);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 13;
            label4.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1749, 38);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 14;
            label5.Text = "label5";
            // 
            // MutationChanceTrackBar
            // 
            MutationChanceTrackBar.LargeChange = 1;
            MutationChanceTrackBar.Location = new Point(1609, 291);
            MutationChanceTrackBar.Maximum = 100;
            MutationChanceTrackBar.Name = "MutationChanceTrackBar";
            MutationChanceTrackBar.Size = new Size(382, 45);
            MutationChanceTrackBar.TabIndex = 15;
            MutationChanceTrackBar.Scroll += MutationChanceTrackBar_Scroll;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1997, 291);
            label6.Name = "label6";
            label6.Size = new Size(13, 15);
            label6.TabIndex = 16;
            label6.Text = "0";
            // 
            // SunTrackBar
            // 
            SunTrackBar.LargeChange = 1;
            SunTrackBar.Location = new Point(1609, 342);
            SunTrackBar.Maximum = 100;
            SunTrackBar.Name = "SunTrackBar";
            SunTrackBar.Size = new Size(382, 45);
            SunTrackBar.TabIndex = 17;
            SunTrackBar.Scroll += SunTrackBar_Scroll;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1997, 342);
            label7.Name = "label7";
            label7.Size = new Size(13, 15);
            label7.TabIndex = 18;
            label7.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1608, 390);
            label8.Name = "label8";
            label8.Size = new Size(38, 15);
            label8.TabIndex = 19;
            label8.Text = "label8";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1608, 414);
            label9.Name = "label9";
            label9.Size = new Size(38, 15);
            label9.TabIndex = 20;
            label9.Text = "label9";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1609, 438);
            label10.Name = "label10";
            label10.Size = new Size(44, 15);
            label10.TabIndex = 21;
            label10.Text = "label10";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2036, 1261);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(SunTrackBar);
            Controls.Add(label6);
            Controls.Add(MutationChanceTrackBar);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(SpawnSquareSizeTrackBar);
            Controls.Add(richTextBox1);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(RenderingModeTrackBar);
            Controls.Add(ThemeCheckBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            ForeColor = SystemColors.ControlText;
            KeyPreview = true;
            MaximumSize = new Size(2052, 1300);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            MouseClick += Form1_MouseClick;
            MouseMove += Form1_MouseMove;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)RenderingModeTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpawnSquareSizeTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)MutationChanceTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)SunTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer SimulationTimer;
        private PictureBox pictureBox1;
        private Button button1;
        private Label label1;
        private Label label2;
        private CheckBox ThemeCheckBox;
        private TrackBar RenderingModeTrackBar;
        private Button button2;
        private Label label3;
        private RichTextBox richTextBox1;
        private TrackBar SpawnSquareSizeTrackBar;
        private Label label4;
        private Label label5;
        private TrackBar MutationChanceTrackBar;
        private Label label6;
        private TrackBar SunTrackBar;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
    }
}
