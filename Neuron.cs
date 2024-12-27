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
        public float value { get; private set; }
        public string type { get; private set; }
        public string activation { get; private set; }

        public Neuron()
        {
            activation = "none";
            value = (float)rnd.NextDouble();
        }

        public void SetType(string type)
        {
            this.type = type;
        }

        public void SetActivation(string activation)
        {
            this.activation = activation;
        }
    }
}
