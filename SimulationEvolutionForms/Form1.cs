using System.Drawing;
using static SimulationEvolutionForms.Settings;
using static SimulationEvolutionForms.Logging;
using static SimulationEvolution.Tools;
using SimulationEvolution;
using System.Xml;
using System.Security.Cryptography;

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
        public static RichTextBox logs;
        Entity selected_entity;

        Simulation sim;


        public Form1()
        {
            InitializeComponent();
            logs = richTextBox1; // Log textBox for Logging class
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
            sim.GenerateEntities(1000);
            this.ActiveControl = null;
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

            label1.Text = $"{x_size} : {y_size}";

            myBuffer.Graphics.DrawLine(new Pen(lines_color), 0, y_size, x_size, y_size);
            myBuffer.Graphics.DrawLine(new Pen(lines_color), x_size, 0, x_size, y_size);

            string text = sim.GetSimulationTurn().ToString();

            if (!is_simulation_on_pause)
            {
                sim.MakeTurn();
            }
            DrawEntities();



            label2.Text = "Entity count: " + sim.entity_count;
            label3.Text = "Simulation turn: " + sim.GetSimulationTurn();
            label4.Text = "Middle age: " + Math.Round(sim.middle_age, 3);
            label5.Text = "Middle energy: " + Math.Round(sim.middle_energy, 3);

            if (fixed_window) this.Location = new Point(0, 0);
            myBuffer.Render();
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
            this.ActiveControl = null;
        }

        private void RenderingModeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ChangeRenderingMode(RenderingModeTrackBar.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sim.DeleteAll();
            this.ActiveControl = null;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    sim.Pause();
                    break;
                case Keys.A:
                    fixed_window = !fixed_window;
                    break;
                case Keys.S:
                    sim.DeleteAll();
                    break;
                case Keys.D:
                    sim.GenerateEntities(entity_to_spawn_by_click);
                    break;
                case Keys.Down:
                    SimulationTimer.Interval++;
                    break;
                case Keys.Up:
                    if (SimulationTimer.Interval > 1) SimulationTimer.Interval--;
                    break;
                case Keys.X:
                    is_spawn_checker = !is_spawn_checker;
                    break;
                case Keys.D1:
                    ChangeRenderingMode(0);
                    RenderingModeTrackBar.Value = 0;
                    break;
                case Keys.D2:
                    ChangeRenderingMode(1);
                    RenderingModeTrackBar.Value = 1;
                    break;
                case Keys.D3:
                    ChangeRenderingMode(2);
                    RenderingModeTrackBar.Value = 2;
                    break;
                case Keys.D4:
                    ChangeRenderingMode(3);
                    RenderingModeTrackBar.Value = 3;
                    break;
                case Keys.D5:
                    ChangeRenderingMode(4);
                    RenderingModeTrackBar.Value = 4;
                    break;
                case Keys.C:
                    ChangeMouseMode(0);
                    break;
                case Keys.V:
                    ChangeMouseMode(1);
                    break;
                case Keys.B:
                    ChangeMouseMode(2);
                    break;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (mouse_mode[0] == 1)
            {
                Cell cell = GetCellByCoordinates(e.X, e.Y);
                Log($"{e.X} : {e.Y}");

                if (cell == null)
                {
                    Log(message_color.warn, "Not found");
                }
                else
                {
                    Log(cell.IsFree().ToString());
                    LogInfoAboutCell(cell);
                    if (!cell.IsFree())
                    {
                        selected_entity = cell.GetEntity();
                        LogInfoAboutEntity(selected_entity);
                    }
                }
            }
            else if (mouse_mode[1] == 1)
            {
                SpawnEntities(e.X, e.Y);
            }
        }

        private Cell GetCellByCoordinates(int x, int y)
        {
            int x_ind = (x - 1) / 8;
            int y_ind = (y - 1) / 8;

            // Проверка выхода за границы поля
            if (x >= cell_x * (cell_size + 1) + 1 || y > cell_y * (cell_size + 1) + 1)
            {
                return null;
            }

            // Если попали на перегородку — округляем координаты
            if ((x - 1) % 8 == 7) x_ind = Math.Min(x_ind + 1, cell_x - 1);
            if ((y - 1) % 8 == 7) y_ind = Math.Min(y_ind + 1, cell_y - 1);

            return sim.map[x_ind, y_ind];
        }


        private static void LogInfoAboutCell(Cell cell)
        {
            Log($"Organics: {cell.organics}\n" +
                $"Energy for photo: {cell.energy_for_photo}");
        } // logs info about cell

        private static void LogInfoAboutEntity(Entity entity)
        {
            Log(message_color.suc, $"Energy: {entity.energy}\nColor: {entity.color}" +
                $"\nRotation: {entity.rotation} | X: {entity.cell.x} Y: {entity.cell.y}\n" +
                $"Killed: {entity.killed} | Moved: {entity.moved} | Age: {entity.age}\n" +
                $"Photo: {entity.photo} | Bited: {entity.bited} | Recycled: {entity.recycled} | Produced: {entity.reproduced}\n");

            LogInfoAboutNeuralNetwork(entity.brain);
        } // logs info about entity

        private static void LogInfoAboutNeuralNetwork(NeuralNetwork network)
        {
            Log(network.GetInfoAboutNeuralNetwork(), message_color.suc);

            for (int i = 0; i < network.weights.Count; i++)
            {
                string info = "";
                for (int j = 0; j < network.weights[i].layer1_size; j++)
                {
                    for (int k = 0; k < network.weights[i].layer2_size; k++)
                    {
                        info += $"{network.weights[i].weights[j][k]:F5} ";
                    }
                    info += "\n";
                }
                Log(info);
            } // info about weights
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (is_spawn_checker)
            {
                if (mouse_mode[1] == 1)
                {
                    SpawnEntities(e.X, e.Y);
                }
                else if (mouse_mode[2] == 1)
                {
                    EraseEntities(e.X, e.Y);
                }
            }
        }

        private void SpawnSquareSizeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            mouse_square_size = SpawnSquareSizeTrackBar.Value;
        }

        private void SpawnEntities(int x, int y)
        {
            Cell cell = GetCellByCoordinates(x, y);
            if (cell != null)
            {
                if (mouse_square_size == 1)
                {
                    if (cell.IsFree()) cell.AddEntity(ref sim.entity_count);
                }
                else
                {
                    int delta = mouse_square_size / 2;
                    int start_x = cell.x - delta;
                    int start_y = cell.y - delta;
                    if (mouse_square_size % 2 != 0)
                    {
                        start_x--;
                        start_y--;
                    }
                    int end_x = cell.x + delta;
                    int end_y = cell.y + delta;

                    start_x = (0 > start_x) ? 0 : start_x;
                    start_y = (0 > start_y) ? 0 : start_y;
                    end_x = (end_x > cell_x) ? cell_x : end_x;
                    end_y = (end_y > cell_y) ? cell_y : end_y;

                    for (int i = start_x; i < end_x; i++)
                    {
                        for (int j = start_y; j < end_y; j++)
                        {
                            if (sim.map[i, j].IsFree()) sim.map[i, j].AddEntity(ref sim.entity_count);
                        }
                    }
                }
            }
        }
        private void EraseEntities(int x, int y)
        {
            Cell cell = GetCellByCoordinates(x, y);
            if (cell != null)
            {
                if (mouse_square_size == 1)
                {
                    if (!cell.IsFree()) cell.DeleteEntity(ref sim.entity_count);
                }
                else
                {
                    int delta = mouse_square_size / 2;
                    int start_x = cell.x - delta;
                    int start_y = cell.y - delta;
                    if (mouse_square_size % 2 != 0)
                    {
                        start_x--;
                        start_y--;
                    }
                    int end_x = cell.x + delta;
                    int end_y = cell.y + delta;

                    start_x = (0 > start_x) ? 0 : start_x;
                    start_y = (0 > start_y) ? 0 : start_y;
                    end_x = (end_x > cell_x) ? cell_x : end_x;
                    end_y = (end_y > cell_y) ? cell_y : end_y;

                    for (int i = start_x; i < end_x; i++)
                    {
                        for (int j = start_y; j < end_y; j++)
                        {
                            if (!sim.map[i, j].IsFree()) sim.map[i, j].DeleteEntity(ref sim.entity_count);
                        }
                    }
                }
            }
        }
    }
}
