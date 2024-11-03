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
        Random rnd;
        public List<int> genom;
        public Cell cell; // don't forget to change when changing the location
        public bool killed;

        public Entity(Cell cell)
        {
            rnd = new Random();
            color = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            energy = energy_for_staying;
            this.cell = cell;
            killed = false;
            genom = new List<int>(genom_size);
            GenerateGenom();
        }

        public void Action(Simulation sim)
        {
            if (!killed)
            {
                energy -= energy_for_staying;
                if (killed) Die();
            }
        }

        public void GenerateGenom()
        {
            for (int i = 0; i < genom.Count; i++)
                genom[i] = rnd.Next(0, genom_size);
        }

        public void Die()
        {
            //write other code for organics and other (now it's just for testing)
            killed = true;
        }
    }
}
