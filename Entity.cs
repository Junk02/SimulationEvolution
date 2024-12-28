using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;
using static SimulationEvolution.Logging;
using static SimulationEvolution.Tools;
using System.IO.Compression;
using System.Xml;

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
        public NeuralNetwork brain;

        public Entity(Cell cell, ref int entity_count) // standart constructor
        {
            entity_count++;
            color = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            energy = standart_energy;
            this.cell = cell;
            killed = false;
            not_exist = false;
            moved = false;
            rotation = rnd.Next(0, 8);
            brain = new NeuralNetwork(this);
        }

        public Entity(Cell cell, Entity parent, ref int entity_count) // overloading of constructor for borned entities
        {
            entity_count++;
            color = parent.color;
            energy = parent.energy;
            this.cell = cell;
            killed = false;
            not_exist = false;
            moved = false;
            rotation = parent.rotation;
            brain = MutateNetwork(parent.brain);
        }

        public void Action(Simulation sim) // makes an action, which depends on entity behaviour
        {
            Check();

            string action = brain.Prediction(sim);

            switch (action)
            {
                case "move":
                    Move(sim);
                    break;
                case "rotate":
                    Rotate();
                    break;
                case "protosynthesis":
                    Photosynthesis();
                    break;
                case "reproduction":
                    Reproduction(sim);
                    break;
                case "bite":
                    Bite(sim);
                    break;
                case "organics":
                    Organics(sim);
                    break;
            }

            int choice = rnd.Next(1, 7);
            if (choice == 1) Move(sim);
            else if (choice == 2) Rotate();
            else if (choice == 3) Photosynthesis();
            else if (choice == 4) Reproduction(sim);
            else if (choice == 5) Bite(sim);
            else if (choice == 6) Organics(sim);

            if (!moved)
            {
                energy -= energy_for_moving;
                moved = true;
            }

            Check();
        }

        public void GetEnergy(int amount) // increases entity energy by amount
        {
            energy += amount;
            if (!is_entity_energy_infinite && energy > max_entity_energy)
            {
                energy = max_entity_energy;
            }
        }

        public void Die() // makes the entity state killed
        {
            //write other code for organics and other (now it's just for testing)
            killed = true;
            cell.AddOrganics(organics_after_dying);
        }

        public void Check() // checks if entity still alive and kills it in the other case
        {
            if (energy <= 0) Die();
        }

        public void Move(Simulation sim) // moves entity
        {
            if (energy >= energy_for_moving)
            {
                Cell moving_cell = sim.GetCellByRotation(cell.x, cell.y, rotation);

                if (moving_cell != null)
                {
                    if (moving_cell.IsFree())
                    {
                        moving_cell.AddEntity(this);
                        energy -= energy_for_moving;
                        cell = moving_cell;
                        not_exist = true;
                        moved = true;
                    }
                }
            }
        }

        public void Rotate() // rotates entity
        {
            if (energy >= energy_for_rotating)
            {
                rotation = rnd.Next(0, 8);
                energy -= energy_for_rotating;
                moved = true;
            }
        }

        public void Photosynthesis() // makes entity do photosynthes
        {
            GetEnergy(cell.energy_for_photo);
            moved = true;
        }

        public void Reproduction(Simulation sim) // makes entity reproduct
        {
            if (energy >= energy_for_reproduction)
            {
                Cell reproduction_cell = sim.GetCellByRotation(cell.x, cell.y, rotation);
                if (reproduction_cell != null)
                {
                    if (reproduction_cell.IsFree())
                    {
                        energy /= 2;
                        reproduction_cell.AddEntity(new Entity(reproduction_cell, this, ref sim.entity_count));
                        moved = true;
                    }
                }
            }
        }

        public void Bite(Simulation sim) // makes entity bite another one
        {
            Cell bite_cell = sim.GetCellByRotation(cell.x, cell.y, rotation);
            if (bite_cell != null)
            {
                if (!bite_cell.IsFree())
                {
                    if (bite_cell.GetEntity().energy > bite_power)
                    {
                        GetEnergy(bite_power);
                        bite_cell.GetEntity().energy -= bite_power;
                    }
                    else
                    {
                        GetEnergy(bite_cell.GetEntity().energy);
                        bite_cell.GetEntity().energy -= bite_power;
                        bite_cell.GetEntity().Check();
                        if (bite_cell.GetEntity().killed)
                        {
                            bite_cell.DeleteEntity(ref sim.entity_count);
                        }
                        moved = true;
                    }
                }
            }
        }

        public void Organics(Simulation sim) // makes entity recycle organics
        {
            Cell organics_cell = sim.GetCellByRotation(cell.x, cell.y, rotation);
            if (organics_cell != null)
            {
                if (organics_cell.organics < organics_bite_power)
                {
                    GetEnergy(organics_cell.organics);
                    organics_cell.organics = 0;
                }
                else
                {
                    GetEnergy(organics_bite_power);
                    organics_cell.organics -= organics_bite_power;
                }
                moved = true;
            }
        }
    }
}
