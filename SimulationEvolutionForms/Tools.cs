using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolutionForms.Logging;
using static SimulationEvolutionForms.Settings;

namespace SimulationEvolution
{
    internal static class Tools
    {
        public static NeuralNetwork MutateNetwork(NeuralNetwork parent_network, ref bool is_mutated)
        {
            NeuralNetwork network = new NeuralNetwork(parent_network, null);

            if (Chance(mutation_chance))
            {
                is_mutated = true;

                int chance = ChanceArray([neuron_mutation_chance, connection_mutation_chance]);

                if (chance == 0)
                {
                    int choice = rnd.Next(1, 4);
                    if (choice == 1) // input layer neuron mutation
                    {
                        network.layers[0].neurons[rnd.Next(0, network.layers[0].neurons.Count)].SetType(input_neuron_variants[rnd.Next(0, input_neuron_variants.Count)]);
                    }
                    else if (choice == 2) // output layer neuron mutation
                    {
                        network.layers[network.layers.Count - 1].neurons[rnd.Next(0, network.layers[network.layers.Count - 1].neurons.Count)].SetType(output_neuron_variants[rnd.Next(0, output_neuron_variants.Count)]);
                    }
                    else if (choice == 3) // hidden layer neuron mutation
                    {
                        int layer = rnd.Next(1, network.layers.Count - 1);
                        network.layers[layer].neurons[rnd.Next(0, network.layers[layer].neurons.Count)].SetType(hidden_neuron_variants[rnd.Next(0, hidden_neuron_variants.Count)]);
                    }
                }
                else if (chance == 1) // this stuff doesn't work, artem try to make it next time
                {
                    network.MutateWeights(rnd.Next(min_weights_mutate_count, max_weights_mutate_count + 1));
                }
                else
                {
                    throw new Exception("neuron_mutation_chance + connection_mutation_chance is not 1f");
                }
            }

            return network;
        }

        public static Color MutateColor(Color color)
        {
            //return color;
            return Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }

        public static int ChanceArray(float[] probabilities)
        {
            float random_value = (float)rnd.NextDouble();

            float cumulative = 0f;

            for (int i = 0; i < probabilities.Length; i++)
            {
                cumulative += probabilities[i];

                if (random_value < cumulative)
                {
                    return i;
                }
            }

            Log("ERROR IN METHOD Chance() looks like probabilities sum is bigger than 1");
            return 0;
        } // generates n probabilities

        public static bool Chance(float probability)
        {
            float random_value = (float)rnd.NextDouble();

            if (random_value < probability)
            {
                return true;
            }

            return false;
        } // generates one probability

        public static float NextFloat(float minimum, float maximum)
        {
            return (float)rnd.NextDouble() * (maximum - minimum) + minimum;
        }

        public static bool IsRelatives(NeuralNetwork network1, NeuralNetwork network2)
        {
            int count = 0;
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
                                count++;
                                if (count > relative_difference)
                                {
                                    return false;
                                }
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
                                count++;
                                if (count > relative_difference)
                                {
                                    return false;
                                }
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

        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
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

        public static Color GetWaveColor(float energy)
        {
            float normalizedEnergy = Math.Clamp(energy / max_wave_value, 0f, 1f);

            int r, g, b;
            if (is_night_theme)
            {
                r = (int)(normalizedEnergy * 255);
                g = (int)(normalizedEnergy * 255);
                b = 0;
            }
            else
            {
                r = 255;
                g = 255;
                b = (int)((1 - normalizedEnergy) * 255);
            }

            return Color.FromArgb(r, g, b);
        }


    }
}
