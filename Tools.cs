using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Logging;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal static class Tools
    {
        public static NeuralNetwork MutateNetwork(NeuralNetwork parent_network, ref bool is_mutated)
        {
            NeuralNetwork network = new NeuralNetwork(parent_network, null);

            if (rnd.Next(1, 20) == 1)
            {
                network.layers[0].neurons[rnd.Next(0, network.layers[0].neurons.Count)].SetType(input_neuron_variants[rnd.Next(0, input_neuron_variants.Count)]);
                is_mutated = true;
            }
            if (rnd.Next(1, 20) == 1)
            {
                network.layers[network.layers.Count - 1].neurons[rnd.Next(0, network.layers[network.layers.Count - 1].neurons.Count)].SetType(output_neuron_variants[rnd.Next(0, output_neuron_variants.Count)]);
                is_mutated = true;
            }

            return network;
        }

        public static Color MutateColor(Color color)
        {
            return color;
            //return Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }

        public static bool IsRelatives(NeuralNetwork network1, NeuralNetwork network2)
        {
            if (network1.layers.Count != network2.layers.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < network1.layers.Count; i++)
                {
                    if (network1.layers[i].neurons.Count != network2.layers[i].neurons.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int j = 0; j < network1.layers[i].neurons.Count; j++)
                        {
                            if (network1.layers[i].neurons[j].type != network2.layers[i].neurons[j].type)
                            {
                                return false;
                            }
                        }
                    }
                } // check if neurons are not equal

                for (int i = 0; i < network1.weights.Count; i++)
                {
                    for (int j = 0; j < network1.weights[i].layer1_size; j++)
                    {
                        for (int k = 0; k < network1.weights[i].layer2_size; k++)
                        {
                            if (network1.weights[i].weights[j][k] != network2.weights[i].weights[j][k])
                            {
                                return false;
                            }
                        }
                    }
                } // check if weights are not equal
            }
            return true;
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
