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

        public Neuron()
        {

        }

        public Neuron(Neuron parent) // constructor of copying
        {
            value = parent.value;
            type = parent.type;
        }

        public void SetType(string type)
        {
            this.type = type;
        }

        public void SetValue(float value)
        {
            this.value = value;
        }
    }
}
