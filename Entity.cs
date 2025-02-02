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
using System.Collections.Specialized;

namespace SimulationEvolution
{
    internal class Entity
    {
        public Color color;
        public Color eat_color;
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
            eat_color = Color.White;
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
            energy = parent.energy;
            this.cell = cell;
            killed = false;
            not_exist = false;
            moved = false;
            rotation = parent.rotation;
            bool is_mutated = false;
            brain = MutateNetwork(parent.brain, ref is_mutated);
            brain.entity = this;
            eat_color = parent.eat_color;
            if (is_mutated)
            {
                color = MutateColor(parent.color);
            }
            else
            {
                color = parent.color;
            }
        }

        public void Action(Simulation sim) // makes an action, which depends on entity behaviour
        {
            Check();

            if (killed)
            {
                return;
            }

            int counter = 0; // counter for rotating IMPORTANT
            string action = "rotr";

            while ((action == "rotr" || action == "rotl") && counter != 5)
            {
                counter++;

                action = brain.Prediction(sim);

                switch (action)
                {
                    case "move":
                        Move(sim);
                        break;
                    case "rotl":
                        Rotate("left");
                        break;
                    case "rotr":
                        Rotate("right");
                        break;
                    case "photo":
                        Photosynthesis();
                        break;
                    case "produ":
                        Reproduction(sim);
                        break;
                    case "bite":
                        Bite(sim);
                        break;
                    case "recyc":
                        Organics(sim);
                        break;
                }
            }

            if (!moved)
            {
                energy -= energy_for_staying;
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

        public void Rotate(string side = "left") // rotates entity
        {
            if (energy >= energy_for_rotating)
            {
                if (side == "left")
                {
                    rotation--;
                }
                else if (side == "right")
                {
                    rotation++;
                }

                if (rotation < 0) rotation = 7;
                if (rotation > 7) rotation = 0;

                energy -= energy_for_rotating;
                moved = true;
            }
        }

        public void Photosynthesis() // makes entity do photosynthes
        {
            GetEnergy(cell.energy_for_photo);
            ChangeEatColor("photo");
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
                        //bite_cell.GetEntity().energy -= bite_power;
                        //bite_cell.GetEntity().Check();
                        bite_cell.DeleteEntity(ref sim.entity_count);
                    }
                    moved = true;
                    ChangeEatColor("bite");
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
                ChangeEatColor("organics");
            }
        }

        public void ChangeEatColor(string type)
        {
            int r = eat_color.R, g = eat_color.G, b = eat_color.B;
            if (type == "photo")
            {
                g++;
                r--;
                b--;
            }
            else if (type == "organics")
            {
                g--;
                r--;
                b++;
            }
            else if (type == "bite")
            {
                g--;
                r++;
                b--;
            }

            if (r > 255) r = 255;
            else if (r < 0) r = 0;

            if (g > 255) g = 255;
            else if (g < 0) g = 0;

            if (b > 255) b = 255;
            else if (b < 0) b = 0;

            eat_color = Color.FromArgb(r, g, b);
        }
    }
}
