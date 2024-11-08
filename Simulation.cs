using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;
using static SimulationEvolution.Logging;

namespace SimulationEvolution
{
    internal class Simulation
    {
        public Window win;

        public Cell[,] map;
        public int entity_count;
        private ulong simulation_turn;

        public Simulation(Window win)
        {
            this.win = win;
            map = new Cell[cell_x, cell_y];
            for (int i = 0; i < cell_x; i++)
                for (int j = 0; j < cell_y; j++)
                    map[i, j] = new Cell(i, j);
            entity_count = 0;
            simulation_turn = 0;
        }

        public void MakeTurn() // method which makes one simulation turn
        {
            for (int i = 0; i < cell_x; i++)
            {
                for (int j = 0; j < cell_y; j++)
                {
                    if (!map[i, j].IsFree() && !map[i, j].entity.moved)
                    {
                        Entity ent = map[i, j].entity;
                        ent.Action(this);
                        if (ent.killed) ent.cell.DeleteEntity(ref entity_count);
                        if (ent.not_exist)
                        {
                            ent.not_exist = false;
                            map[i, j].DeleteEntity();
                        }
                    }
                }
            }
            for (int i = 0; i < cell_x; i++)
                for (int j = 0; j < cell_y; j++)
                    if (!map[i, j].IsFree()) map[i, j].entity.moved = false;
            simulation_turn++;
        }

        public void DrawEntities() // method which draws all entities
        {
            for (int i = 0; i < cell_x; i++)
            {
                for (int j = 0; j < cell_y; j++)
                {
                    if (!map[i, j].IsFree())
                    {
                        if (((int)rendering_mode.entity_color) == 1) win.SetColor(map[i, j].entity.color);
                        win.DrawRectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size);
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

            if (count < max_entity_count - entity_count)
            {
                for (int i = 0; i < count; i++)
                {
                    int x, y;
                    do
                    {
                        x = rnd.Next(0, cell_x);
                        y = rnd.Next(0, cell_y);
                    }
                    while (map[x, y].entity != null);
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
        }
    }
}

//номер клетки в массиве * (размер клетки + 1) + 1
//x_pos * (entity_size + 1) + 1
