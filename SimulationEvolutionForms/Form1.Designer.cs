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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RenderingModeTrackBar).BeginInit();
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
            button1.Location = new Point(1642, 96);
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
            label1.Location = new Point(1642, 15);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1641, 41);
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
            RenderingModeTrackBar.Location = new Point(1642, 183);
            RenderingModeTrackBar.Maximum = 4;
            RenderingModeTrackBar.Name = "RenderingModeTrackBar";
            RenderingModeTrackBar.Size = new Size(382, 45);
            RenderingModeTrackBar.TabIndex = 7;
            RenderingModeTrackBar.ValueChanged += RenderingModeTrackBar_ValueChanged;
            // 
            // button2
            // 
            button2.Location = new Point(1740, 96);
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
            label3.Location = new Point(1642, 69);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 9;
            label3.Text = "label3";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2036, 1263);
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
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            KeyPress += Form1_KeyPress;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)RenderingModeTrackBar).EndInit();
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
    }
}
