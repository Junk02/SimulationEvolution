using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEvolution
{
    internal class Cell
    {
        public Entity? entity;
        private int x; private int y;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
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

        public bool IsFree()
        {
            if (entity == null) return true;
            return false;
        }

        public int GetXPos()
        {
            return x;
        }

        public int GetYPos()
        {
            return y;
        }
    }
}
