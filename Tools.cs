using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Logging;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal static class Tools
    {
        public static NeuralNetwork MutateNetwork(NeuralNetwork parent_network)
        {
            NeuralNetwork network = new NeuralNetwork(parent_network, null);

            if (rnd.Next(1, 3) == 1)
            {
                network.layers[0].neurons[rnd.Next(0, network.layers[0].neurons.Count)].SetType(input_neuron_variants[rnd.Next(0, input_neuron_variants.Count)]);
            }

            return network;
        }

        public static float FormalizeNegative(float value, float min, float max)
        {
            return 2 * (value - min) / (max - min) - 1;
        }

        public static float Formalize(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        public static float ReLu(float value)
        {
            return Math.Max(0, value);
        }

        public static float Linear(float value)
        {
            return value;
        }

        public static float Tanh(float value)
        {
            return value / (1 + Math.Abs(value));
        }

        public static float Rand(float value)
        {
            return (value > rnd.NextDouble()) ? 1 : 0;
        }
    }
}
