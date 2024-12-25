using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal class Neuron
    {
        public int value { get; private set; }
        public string type { get; private set; }

        public Neuron(string type)
        {
            this.type = type;
        }

        public void SetType(string type)
        {
            this.type = type;
        }
    }
}
