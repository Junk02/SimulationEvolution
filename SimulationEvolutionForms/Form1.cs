using System.Drawing;
using static SimulationEvolutionForms.Settings;
using static SimulationEvolutionForms.Logging;
using static SimulationEvolution.Tools;
using SimulationEvolution;

namespace SimulationEvolutionForms
{
    public partial class Form1 : Form
    {
        // Создаём контекст для буферной графики
        BufferedGraphicsContext myContext = new BufferedGraphicsContext();
        Graphics graphics;
        // Получаем текущий глобальный контекст буферной графики
        BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
        BufferedGraphics myBuffer;
        SolidBrush brush;
        Random rand;
        int x = 200;

        Simulation sim;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Включаем двойную буферизацию для уменьшения мерцания
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Инициализируем собственный контекст буферной графики
            myContext = new BufferedGraphicsContext();

            // Получаем графику с компонента pictureBox1
            graphics = pictureBox1.CreateGraphics();

            // Используем глобальный контекст буферной графики
            currentContext = BufferedGraphicsManager.Current;

            // Создаём буфер на основе области отображения формы
            myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            rand = new Random();


            sim = new Simulation();

            if (sim == null) MessageBox.Show("All is wrong");

            myBuffer.Render();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sim.GenerateEntities(100);
        }

        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            // Создаём кисть светло-серого цвета для очистки области
            brush = new SolidBrush(Color.White);

            // Очищаем весь экран (условно "заливка фона")
            brush.Color = panel_color;
            myBuffer.Graphics.FillRectangle(brush, new Rectangle(0, 0, 5000, 5000));
            brush.Color = simulation_color;
            myBuffer.Graphics.FillRectangle(brush, new Rectangle(0, 0, x_size, y_size));

            // Меняем цвет кисти на чёрный для рисования квадрата

            // Рисуем чёрный квадрат размером 100x100 пикселей



            label1.Text = $"{x_size} : {y_size}";


            myBuffer.Graphics.DrawLine(new Pen(lines_color), 0, y_size, x_size, y_size);
            myBuffer.Graphics.DrawLine(new Pen(lines_color), x_size, 0, x_size, y_size);

            DrawEntities();
            sim.MakeTurn();
            label2.Text = "Кол-во сущностей: " + sim.entity_count.ToString();

            // Прорисовываем буфер на экране (два раза для надёжности)
            myBuffer.Render();
            //myBuffer.Render(graphics);
        }

        public void DrawEntities() // method which draws all entities
        {
            if (rendering_mode[0] == 1) // entity_color_mode
            {
                for (int i = 0; i < cell_x; i++)
                {
                    for (int j = 0; j < cell_y; j++)
                    {
                        if (!sim.map[i, j].IsFree())
                        {
                            SolidBrush brush = new SolidBrush(sim.map[i, j].GetEntity().color);
                            myBuffer.Graphics.FillRectangle(brush, new Rectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size, cell_size));
                        }
                    }
                }
            }
            else if (rendering_mode[1] == 1) // organics_mode
            {
                for (int i = 0; i < cell_x; i++)
                {
                    for (int j = 0; j < cell_y; j++)
                    {
                        Color cell_color;
                        if (is_night_theme)
                        {
                            cell_color = Color.FromArgb((byte)(((double)(sim.map[i, j].organics) / (double)(max_organics)) * 255), 0, 0);
                        }
                        else
                        {
                            byte red = (byte)(((double)(sim.map[i, j].organics) / (double)(max_organics)) * 255);
                            byte whiteComponent = (byte)(255 - red);
                            cell_color = Color.FromArgb(255, 255, whiteComponent, whiteComponent);
                        }

                        SolidBrush brush = new SolidBrush(cell_color);
                        myBuffer.Graphics.FillRectangle(brush, new Rectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size, cell_size));
                    }
                }
            }
            else if (rendering_mode[2] == 1) // eat_color_mode
            {
                for (int i = 0; i < cell_x; i++)
                {
                    for (int j = 0; j < cell_y; j++)
                    {
                        if (!sim.map[i, j].IsFree())
                        {
                            SolidBrush brush = new SolidBrush(sim.map[i, j].GetEntity().eat_color);
                            myBuffer.Graphics.FillRectangle(brush, new Rectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size, cell_size));
                        }
                    }
                }
            }
            else if (rendering_mode[3] == 1) // energy_mode
            {
                for (int i = 0; i < cell_x; i++)
                {
                    for (int j = 0; j < cell_y; j++)
                    {
                        if (!sim.map[i, j].IsFree())
                        {
                            float normalizedEnergy = (float)Math.Clamp(((double)sim.map[i, j].GetEntity().energy / (double)max_entity_energy), 0f, 1f);
                            float smoothEnergy = (float)Math.Sqrt(normalizedEnergy);
                            int r = 255;
                            int g = (byte)(255 * (1 - smoothEnergy));
                            int b = 0;
                            SolidBrush brush = new SolidBrush(Color.FromArgb((byte)r, (byte)g, (byte)b));
                            myBuffer.Graphics.FillRectangle(brush, new Rectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size, cell_size));
                        }
                    }
                }
            }

            else if (rendering_mode[4] == 1) // wave_mode
            {
                for (int i = 0; i < cell_x; i++)
                {
                    for (int j = 0; j < cell_y; j++)
                    {
                        Color color = GetWaveColor(sim.map[i, j].energy_for_photo);
                        SolidBrush brush = new SolidBrush(color);
                        myBuffer.Graphics.FillRectangle(brush, new Rectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size, cell_size));
                    }
                }
            }

            //win.SetColor(default_color);
            myBuffer.Render(graphics);
        }

        private void ThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (is_night_theme)
            {
                simulation_color = Color.White;
                panel_color = Color.White;
                lines_color = Color.Black;
                is_night_theme = false;
            }
            else
            {
                simulation_color = Color.Black;
                panel_color = Color.Black;
                lines_color = Color.White;
                is_night_theme = true;
            }
        }

        private void RenderingModeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ChangeRenderingMode(RenderingModeTrackBar.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sim.DeleteAll();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            label1.Text = e.KeyChar.ToString();
            if (e.KeyChar == ' ')
            {
                label3.Text = e.KeyChar.ToString();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            label1.Text = e.KeyCode.ToString();
        }
    }
}
