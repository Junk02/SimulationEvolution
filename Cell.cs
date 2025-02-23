using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal class Cell
    {
        private Entity? entity;
        public int x { get; private set; }
        public int y { get; private set; }

        public int energy_for_photo;
        public int organics { get; private set; }

        public Cell(int x, int y, int energy_for_photo)
        {
            this.x = x;
            this.y = y;
            organics = 0;
            this.energy_for_photo = energy_for_photo;
        }

        public void AddEntity(ref int entity_count) // adds random entity on this cell
        {
            if (entity == null)
            {
                entity = new Entity(this, ref entity_count);
            }
        }

        public void AddEntity(Entity entity)
        {
            if (this.entity == null)
            {
                this.entity = entity;
            }
        }

        public void DeleteEntity(ref int entity_count) // deletes entity from cell
        {
            entity_count--;
            if (entity_count < 0)
            {
                is_simulation_on_pause = true;
                return;
                SimulationEvolution.Window.LogInfoAboutEntity(entity);
                entity.killed = false;
                //entity.GetEnergy(standart_energy);
                entity.color = Color.White;
                is_simulation_on_pause = true;
            }
            else
            {
                entity = null;
            }
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
            if (organics < 0)
            {
                organics = 0;
            }
        }

        public void ClearOrganics()
        {
            organics = 0;
        }

        public Entity GetEntity()
        {
            if (entity != null)
                return entity;

            return null;
        }
    }
}
