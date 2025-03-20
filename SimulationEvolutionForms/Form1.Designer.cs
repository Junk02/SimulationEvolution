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
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(82, 91);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(1839, 124);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(105, 87);
            button1.TabIndex = 2;
            button1.Text = "Spawn entities";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1839, 16);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1838, 51);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 4;
            label2.Text = "label2";
            // 
            // ThemeCheckBox
            // 
            ThemeCheckBox.AutoSize = true;
            ThemeCheckBox.Location = new Point(2202, 16);
            ThemeCheckBox.Margin = new Padding(3, 4, 3, 4);
            ThemeCheckBox.Name = "ThemeCheckBox";
            ThemeCheckBox.Size = new Size(121, 24);
            ThemeCheckBox.TabIndex = 6;
            ThemeCheckBox.Text = "Ночная тема";
            ThemeCheckBox.UseVisualStyleBackColor = true;
            ThemeCheckBox.CheckedChanged += ThemeCheckBox_CheckedChanged;
            // 
            // RenderingModeTrackBar
            // 
            RenderingModeTrackBar.LargeChange = 1;
            RenderingModeTrackBar.Location = new Point(1839, 240);
            RenderingModeTrackBar.Margin = new Padding(3, 4, 3, 4);
            RenderingModeTrackBar.Maximum = 4;
            RenderingModeTrackBar.Name = "RenderingModeTrackBar";
            RenderingModeTrackBar.Size = new Size(437, 56);
            RenderingModeTrackBar.TabIndex = 7;
            RenderingModeTrackBar.ValueChanged += RenderingModeTrackBar_ValueChanged;
            // 
            // button2
            // 
            button2.Location = new Point(1951, 124);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(105, 87);
            button2.TabIndex = 8;
            button2.Text = "Delete all";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1839, 88);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 9;
            label3.Text = "label3";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(1839, 1173);
            richTextBox1.Margin = new Padding(3, 4, 3, 4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            richTextBox1.Size = new Size(474, 429);
            richTextBox1.TabIndex = 11;
            richTextBox1.Text = "";
            // 
            // SpawnSquareSizeTrackBar
            // 
            SpawnSquareSizeTrackBar.LargeChange = 1;
            SpawnSquareSizeTrackBar.Location = new Point(1839, 308);
            SpawnSquareSizeTrackBar.Margin = new Padding(3, 4, 3, 4);
            SpawnSquareSizeTrackBar.Minimum = 1;
            SpawnSquareSizeTrackBar.Name = "SpawnSquareSizeTrackBar";
            SpawnSquareSizeTrackBar.Size = new Size(437, 56);
            SpawnSquareSizeTrackBar.TabIndex = 12;
            SpawnSquareSizeTrackBar.Value = 1;
            SpawnSquareSizeTrackBar.ValueChanged += SpawnSquareSizeTrackBar_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1650, 51);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 13;
            label4.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1650, 88);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 14;
            label5.Text = "label5";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1055);
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
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(2343, 1720);
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
        private Label label4;
        private Label label5;
    }
}
