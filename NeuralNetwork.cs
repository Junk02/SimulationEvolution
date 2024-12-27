using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;
using static SimulationEvolution.Logging;

namespace SimulationEvolution
{
    internal class NeuralNetwork
    {
        //class for entity neuralNetwork

        // test commit for neuralNetwork branch!
        private List<Layer> layers;
        private List<List<float>> weights;
        public Entity entity;

        public NeuralNetwork(Entity entity, int layers_quantity = default_layers_quantity)
        {
            this.entity = entity;

            this.layers = new List<Layer>();
            this.weights = new List<List<float>>();

            for (int i = 0; i < layers_quantity; i++)
            {
                layers.Add(new Layer());
            } // layers initialization

            for (int i = 0; i < layers_quantity - 1; i++)
            {
                weights.Add(new List<float>());
                for (int j = 0; j < layers[i + 1].neurons.Count; j++)
                {
                    float weight = (float)(rnd.NextDouble() * (max_weight_size - min_weight_size) - min_weight_size);
                    weights[i].Add(weight);
                }
            } // weights initialization


            //LAYERS INITIALIZATION

            for (int i = 0; i < layers[0].neurons.Count; i++)
            {
                layers[0].neurons[i].SetType(input_neuron_variants[rnd.Next(0, input_neuron_variants.Count)]);
            } // input layer initialization

            for (int i = 1; i < layers.Count - 1; i++)
            {
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    layers[i].neurons[j].SetType("basic");
                    layers[i].neurons[j].SetActivation(activation_variants[rnd.Next(0, activation_variants.Count)]);
                }
            } // hidden layers initialization

            for (int i = 0; i < layers[layers_quantity - 1].neurons.Count; i++)
            {
                layers[layers_quantity - 1].neurons[i].SetType(output_neuron_variants[rnd.Next(0, output_neuron_variants.Count)]);
            } // output layer initialization
        }

        public string GetInfoAboutNeuralNetwork() // returns string with information about NeuralNetwork
        {
            string info = "";
            
            for (int i = 0; i < layers.Count; i++)
            {
                info += $"layer:{i}\n";
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    Neuron neuron = layers[i].neurons[j];
                    info += $"  {neuron.type}\t--- {neuron.activation} | {neuron.value}";
                    if (i != layers.Count - 1)
                    {
                        info += $"\t | {weights[i][j]}";
                    }
                    info += "\n";
                }
            }
            return info;
        }
    }
}
