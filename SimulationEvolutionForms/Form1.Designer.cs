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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RenderingModeTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpawnSquareSizeTrackBar).BeginInit();
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2036, 1263);
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
            MaximumSize = new Size(2052, 1302);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            MouseClick += Form1_MouseClick;
            MouseMove += Form1_MouseMove;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)RenderingModeTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpawnSquareSizeTrackBar).EndInit();
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
    }
}
