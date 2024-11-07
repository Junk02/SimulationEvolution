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

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            energy_for_photo = energy_for_photosynthesis;
        }

        public void AddEntity()
        {
            if (entity == null) entity = new Entity(this);
        }

        public void DeleteEntity(ref int entity_count)
        {
            entity = null!;
            entity_count--;
        }

        public void DeleteEntity()
        {
            entity = null;
        }

        public bool IsFree()
        {
            if (entity == null) return true;
            return false;
        }
    }
}
