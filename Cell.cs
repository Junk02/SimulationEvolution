using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal class Cell
    {
        public Entity? entity;
        public int x { get; private set; }
        public int y { get; private set; }

        public int energy_for_photo;
        public int organics;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            organics = 0;
            energy_for_photo = energy_for_photosynthesis;
        }

        public void AddEntity(ref int entity_count) // adds random entity on this cell
        {
            if (entity == null)
            {
                entity = new Entity(this, ref entity_count);
            }
        }

        public void DeleteEntity(ref int entity_count) // deletes entity from cell
        {
            entity = null!;
            entity_count--;
        } 

        public void DeleteEntity() // deletes entity without changing *entity_count
        {
            entity = null;
        }

        public bool IsFree() // returns true if cell is free and false in the other case
        {
            if (entity == null) return true;
            return false;
        }

        public void AddOrganics(int amount) // add *amount organics in the cell
        {
            organics += amount;
            if (!is_organics_infinite && organics > max_organics)
            {
                organics = max_organics;
            }
        }
    }
}
