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

        public void Move(Simulation sim) // method which use rotation and move entity
        {
            if (energy >= energy_for_moving)
            {
                Cell moving_cell = sim.GetCellByRotation(cell.x, cell.y, rotation);

                if (moving_cell != null)
                {
                    if (moving_cell.IsFree())
                    {
                        moving_cell.entity = this;
                        energy -= energy_for_moving;
                        cell = moving_cell;
                        not_exist = true;
                        moved = true;
                    }
                }
            }
        }

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
