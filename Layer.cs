using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;
using static SimulationEvolution.Logging;

namespace SimulationEvolution
{
    internal class Layer
    {
        public List<Neuron> neurons;

        public Layer(int size = defaulf_neurons_quantity)
        {
            neurons = new List<Neuron>();

            for (int i = 0; i < defaulf_neurons_quantity; i++)
                neurons.Add(new Neuron());
        }
    }
}
