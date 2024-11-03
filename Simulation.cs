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
        public Random rnd;

        public Simulation(Window win)
        {
            this.win = win;
            map = new Cell[cell_x, cell_y];
            for (int i = 0; i < cell_x; i++)
                for (int j = 0; j < cell_y; j++)
                    map[i, j] = new Cell(i, j);
            entity_count = 0;
            rnd = new Random();
            GenerateEntities(100);
        }

        public void MakeTurn()
        {
            for (int i = 0; i < cell_x; i++)
                for (int j = 0; j < cell_y; j++)
                {
                    if (!map[i, j].IsFree())
                    {
                        map[i, j].entity.Action(this);
                        if (map[i, j].entity.killed) map[i, j].DeleteEntity(ref entity_count);
                    }
                }
        }

        public void DrawEntities()
        {
            for (int i = 0; i < cell_x; i++)
            {
                for (int j = 0; j < cell_y; j++)
                {
                    if (!map[i, j].IsFree())
                    {
                        win.SetColor(map[i, j].entity.color);
                        win.DrawRectangle(i * (cell_size + 1) + 1, j * (cell_size + 1) + 1, cell_size);
                    }
                }
            }
            win.SetColor(default_color);
        }

        public void DeleteAllEntities()
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

        public void GenerateEntities(int count)
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
                    map[x, y].AddEntity();
                }
                entity_count += count;
                Log($"{count} entities were successfully added", message_color.suc);
            }
            else
            {
                Log("Error in \"GenerateEntities\", maybe variable \"count\" is greater then free place", message_color.err);
            }
        }
    }
}

//номер клетки в массиве * (размер клетки + 1) + 1
//x_pos * (entity_size + 1) + 1
