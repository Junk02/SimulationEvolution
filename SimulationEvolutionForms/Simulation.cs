using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolutionForms.Settings;
using static SimulationEvolutionForms.Logging;
using static SimulationEvolution.Tools;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;

namespace SimulationEvolution
{
    internal class Simulation
    {

        public Cell[,] map;
        public int entity_count;
        private ulong simulation_turn;
        public float middle_age;
        public float middle_energy;

        public Simulation()
        {
            map = new Cell[cell_x, cell_y];

            for (int i = 0; i < cell_x; i++)
                for (int j = 0; j < cell_y; j++)
                    map[i, j] = new Cell(i, j, energy_for_photosynthesis); // start initialization of the map

            int temp = start_wave_value;
            int count = 0;

            int start = (start_wave_position < cell_y) ? (start_wave_position >= 0) ? start_wave_position : 0 : 0;

            for (int i = start; i < cell_y; i++) // wave photosynthesis initialization
            {
                for (int j = 0; j < cell_x; j++)
                {
                    map[j, i].energy_for_photo = temp;
                }
                count++;
                temp += delta_wave_value;
                if (count == waves_count || temp <= 0)
                {
                    break;
                }
            }

            entity_count = 0;
            simulation_turn = 0;
            middle_age = 0;
        }

        public void MakeTurn() // method which makes one simulation turn
        {
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
                    if (!map[i, j].IsFree())
                    {
                        map[i, j].GetEntity().moved = false;
                        middle_age += map[i, j].GetEntity().age;
                        middle_energy += map[i, j].GetEntity().energy;
                    }
            simulation_turn++;
            if (entity_count != 0)
            {
                middle_age /= entity_count;
                middle_energy /= entity_count;
            }
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
            Log(message_color.suc, "All entities were successfully deleted");
        }

        public void DeleteAllOrganics() // method which deletes all organics from world
        {
            for (int i = 0; i < cell_x; i++)
            {
                for (int j = 0; j < cell_y; j++)
                {
                    map[i, j].ClearOrganics();
                }
            }
            Log(message_color.suc, "All organic was successfully deleted");
        }

        public void DeleteAll() // method which deletes all
        {
            DeleteAllEntities();
            DeleteAllOrganics();
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
                Log(message_color.suc, $"{count} entities were successfully added");
            }
            else
            {
                Log(message_color.err, "Error in \"GenerateEntities\", maybe variable \"count\" is greater then free place");
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

        public void Pause()
        {
            is_simulation_on_pause = !is_simulation_on_pause;
        }
    }
}

//номер клетки в массиве * (размер клетки + 1) + 1
//x_pos * (entity_size + 1) + 1
