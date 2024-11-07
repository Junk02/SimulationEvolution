using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;
using static SimulationEvolution.Logging;

namespace SimulationEvolution
{
    internal class Entity
    {
        public Color color;
        public int energy;
        public Cell cell; // don't forget to change when changing the location
        public bool killed; // this is for situation when entity died and we need to delete it
        public bool not_exist; // this is for situation when entity moved but still exist on that cell
        public bool moved;
        public int rotation;

        public Entity(Cell cell)
        {
            color = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            energy = standart_energy;
            this.cell = cell;
            killed = false;
            not_exist = false;
            moved = false;
            rotation = rnd.Next(0, 8);
        }

        public void Action(Simulation sim) // method which make an action, which depends on entity behaviour
        {
            Check();

            int choice = rnd.Next(1, 4);
            if (choice == 1) Move(sim);
            else if (choice == 2) Rotate();
            else if (choice == 3) Photosynthesis();

            if (!moved)
            {
                energy -= energy_for_moving;
                moved = true;
            }

            Check();
        }

        public void Die()
        {
            //write other code for organics and other (now it's just for testing)
            killed = true;
        }

        public void Check() // method which checks if entity still alive and kills it in the other case
        {
            if (energy <= 0) Die();
        }

        public void Move(Simulation sim)
        {
            if (energy >= energy_for_moving)
            {
                int new_x = cell.x, new_y = cell.y;
                if (rotation == 0)
                {
                    new_x = cell.x - 1;
                    new_y = cell.y - 1;
                }
                else if (rotation == 1)
                {
                    new_y = cell.y - 1;
                }
                else if (rotation == 2)
                {
                    new_x = cell.x + 1;
                    new_y = cell.y - 1;
                }
                else if (rotation == 3)
                {
                    new_x = cell.x + 1;
                }
                else if (rotation == 4)
                {
                    new_x = cell.x + 1;
                    new_y = cell.y + 1;
                }
                else if (rotation == 5)
                {
                    new_y = cell.y + 1;
                }
                else if (rotation == 6)
                {
                    new_x = cell.x - 1;
                    new_y = cell.y + 1;
                }
                else if (rotation == 7)
                {
                    new_x = cell.x - 1;
                }

                if (0 <= new_x && new_x < cell_x && 0 <= new_y && new_y < cell_y)
                {
                    if (sim.map[new_x, new_y].IsFree())
                    {
                        sim.map[new_x, new_y].entity = this;
                        energy -= energy_for_moving;
                        cell = sim.map[new_x, new_y];
                        not_exist = true;
                        moved = true;
                    }
                }
            }
        } // method which use rotation and move entity

        public void Rotate()
        {
            if (energy >= energy_for_rotating)
            {
                rotation = rnd.Next(0, 8);
                energy -= energy_for_rotating;
                moved = true;
            }
        }

        public void Photosynthesis()
        {
            energy += cell.energy_for_photo;
            if (!infinite_entity_energy && energy > max_entity_energy)
            {
                energy = max_entity_energy;
            }
            moved = true;
        }
    }
}
