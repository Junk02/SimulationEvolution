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
        public static NeuralNetwork MutateNetwork(NeuralNetwork old_network)
        {
            return old_network;
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
