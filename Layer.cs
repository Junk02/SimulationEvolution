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

        public Layer(int size = defaulf_neurons_input_layer_quantity)
        {
            neurons = new List<Neuron>();

            for (int i = 0; i < size; i++)
                neurons.Add(new Neuron());
        }

        public Layer(Layer parent) // constructor of copying
        {
            neurons = parent.neurons.Select(neuron => new Neuron(neuron)).ToList();
        }
    }
}
