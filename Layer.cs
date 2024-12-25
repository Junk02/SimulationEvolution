using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal class Layer
    {
        public List<Neuron> neurons;

        public Layer(int size = defaulf_neurons_quantity)
        {
            neurons = new List<Neuron>(size);

            
        }
    }
}
