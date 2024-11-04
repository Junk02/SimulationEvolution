using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal class Entity
    {
        public Color color;
        public int energy;
        public Cell cell; // don't forget to change when changing the location
        public bool killed;

        public Entity(Cell cell)
        {
            color = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            energy = standart_energy;
            this.cell = cell;
            killed = false;
        }

        public void Action(Simulation sim) // method which make an action, which depends on entity behaviour
        {
            Check();
            energy -= energy_for_staying;
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
    }
}
