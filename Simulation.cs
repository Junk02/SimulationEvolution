using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;
using static SimulationEvolution.Logging;
using System.Xml;
using System.Diagnostics;

namespace SimulationEvolution
{
    internal class Simulation
    {
        public Window win;

        public Cell[,] map;
        public int entity_count;
        private ulong simulation_turn;

        private Stopwatch stopwatch;
        private double deltaTime;
        public double fps { get; private set; }

        public Simulation(Window win)
        {
            this.win = win;
            stopwatch = new Stopwatch();
            stopwatch.Start();
            map = new Cell[cell_x, cell_y];
            for (int i = 0; i < cell_x; i++)
                for (int j = 0; j < cell_y; j++)
                    map[i, j] = new Cell(i, j);
            entity_count = 0;
            simulation_turn = 0;
        }

        public void MakeTurn() // method which makes one simulation turn
        {
            deltaTime = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();
            fps = 1.0 / deltaTime;

            for (int i = 0; i < cell_x; i++)
            {
                for (int j = 0; j < cell_y; j++)
                {
                    if (!map[i, j].IsFree() && !map[i, j].GetEntity().moved)
                    {
                        Entity ent = map[i, j].GetEntity();
                        ent.Action(this);
                        if (ent.not_exist)
                        {
                            ent.not_exist = false;
                            map[i, j].DeleteEntity();
                        }
                        if (ent.killed)
                        {
                            ent.cell.DeleteEntity(ref entity_count);
                        }
                    }
                }
            }
            for (int i = 0; i < cell_x; i++)
                for (int j = 0; j < cell_y; j++)
                    if (!map[i, j].IsFree()) map[i, j].GetEntity().moved = false;
            simulation_turn++;
        }

        public void DrawEntities() // method which draws all entities
        {
            if (rendering_mode[0] == 1) // entity_color_mode
            {
                for (int i = 0; i < cell_x; i++)
                {
                    for (int j = 0; j < cell_y; j++)
                    {
                        if (!map[i, j].IsFree())
                        {
                            win.SetColor(map[i, j].GetEntity().color);
                            win.DrawRectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size);
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
                        win.SetColor((byte)(((double)(map[i, j].organics) / (double)(max_organics)) * 255), 0, 0);
                        win.DrawRectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size);
                    }
                }
            }
            else if (rendering_mode[2] == 1) // eat_color_mode
            {
                for (int i = 0; i < cell_x; i++)
                {
                    for (int j = 0; j < cell_y; j++)
                    {
                        if (!map[i, j].IsFree())
                        {
                            win.SetColor(map[i, j].GetEntity().eat_color);
                            win.DrawRectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size);
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
                        if (!map[i, j].IsFree())
                        {
                            float normalizedEnergy = (float)Math.Clamp(((double)map[i, j].GetEntity().energy / (double)max_entity_energy), 0f, 1f);
                            float smoothEnergy = (float)Math.Sqrt(normalizedEnergy);
                            int r = 255;
                            int g = (byte)(255 * (1 - smoothEnergy));
                            int b = 0;
                            win.SetColor((byte)r, (byte)g, (byte)b);
                            win.DrawRectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size);
                        }
                    }
                }
            }

            win.SetColor(default_color);
        }

        public void DeleteAllEntities() // method which deletes all live entities
        {
            for (int i = 0; i < cell_x; i++)
            {
                for (int j = 0; j < cell_y; j++)
                {
                    if (!map[i, j].IsFree())
                    {
                        map[i, j].DeleteEntity(ref entity_count);
                    }
                }
            }
            Log("All entities were successfully deleted", message_color.suc);
        }

        public void GenerateEntities(int count) // method which randomly generates new entities
        {
            /*
             * The method may work slowly when most of the space is occupied,
             * since it randomly selects free spaces for new entities,
             * it is recommended to use it only for testing
            */

            if (count <= max_entity_count - entity_count)
            {
                for (int i = 0; i < count; i++)
                {
                    int x, y;
                    do
                    {
                        x = rnd.Next(0, cell_x);
                        y = rnd.Next(0, cell_y);
                    }
                    while (!map[x, y].IsFree());
                    map[x, y].AddEntity(ref entity_count);
                }
                Log($"{count} entities were successfully added", message_color.suc);
            }
            else
            {
                Log("Error in \"GenerateEntities\", maybe variable \"count\" is greater then free place", message_color.err);
            }
        }

        public ulong GetSimulationTurn() // returns simulation_turn
        {
            return simulation_turn;
        }

        public Cell GetCellByRotation(int x, int y, int rotation)
        {
            int new_x = x, new_y = y;
            switch (rotation)
            {
                case 0:
                    new_x -= 1;
                    new_y -= 1;
                    break;
                case 1:
                    new_y -= 1;
                    break;
                case 2:
                    new_x += 1;
                    new_y -= 1;
                    break;
                case 3:
                    new_x += 1;
                    break;
                case 4:
                    new_x += 1;
                    new_y += 1;
                    break;
                case 5:
                    new_y += 1;
                    break;
                case 6:
                    new_x -= 1;
                    new_y += 1;
                    break;
                case 7:
                    new_x -= 1;
                    break;
            }

            if (0 <= new_x && new_x < cell_x && 0 <= new_y && new_y < cell_y) return map[new_x, new_y];
            return null!;
        } // returns cell by rotation
    }
}

//номер клетки в массиве * (размер клетки + 1) + 1
//x_pos * (entity_size + 1) + 1
