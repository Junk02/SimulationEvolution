﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolutionForms.Settings;
using static SimulationEvolutionForms.Logging;
using static SimulationEvolution.Tools;
using System.Diagnostics.CodeAnalysis;

namespace SimulationEvolution
{
    internal class NeuralNetwork
    {
        //class for entity neuralNetwork

        // test commit for neuralNetwork branch!
        public List<Layer> layers;
        public List<Weights> weights { get; private set; }
        public Entity entity;

        public NeuralNetwork(Entity entity, int layers_quantity = default_layers_quantity)
        {
            this.entity = entity;

            this.layers = new List<Layer>();
            this.weights = new List<Weights>();

            layers.Add(new Layer(defaulf_neurons_input_layer_quantity));

            for (int i = 1; i < layers_quantity - 1; i++)
            {
                layers.Add(new Layer(defaulf_neurons_hidden_layer_quantity));
            }

            layers.Add(new Layer(defaulf_neurons_output_layer_quantity));
            // layers initialization


            for (int i = 0; i < layers_quantity - 1; i++)
            {
                weights.Add(new Weights(layers[i].neurons.Count, layers[i + 1].neurons.Count));
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
                    layers[i].neurons[j].SetType(hidden_neuron_variants[rnd.Next(0, hidden_neuron_variants.Count)]);
                }
            } // hidden layers initialization

            for (int i = 0; i < layers[layers_quantity - 1].neurons.Count; i++)
            {
                layers[layers_quantity - 1].neurons[i].SetType(output_neuron_variants[rnd.Next(0, output_neuron_variants.Count)]);
            } // output layer initialization
        }

        public NeuralNetwork(NeuralNetwork parent, Entity entity) // constructor of copying
        {
            this.entity = entity;

            layers = parent.layers.Select(layer => new Layer(layer)).ToList();

            weights = parent.weights.Select(weight => new Weights(weight)).ToList();

        }

        public string Prediction(Simulation sim)
        {
            float vision_cell_value = 100;
            for (int i = 0; i < layers[0].neurons.Count; i++)
            {
                string type = layers[0].neurons[i].type;
                Neuron neuron = layers[0].neurons[i];

                if (type == "x")
                {
                    neuron.SetValue(Formalize(entity.cell.x, 0, cell_x - 1));
                }
                else if (type == "y")
                {
                    neuron.SetValue(Formalize(entity.cell.y, 0, cell_y - 1));
                }
                else if (type == "energ")
                {
                    neuron.SetValue(Formalize(entity.cell.y, 0, max_entity_energy));
                }
                else if (type == "visio")
                {
                    if (vision_cell_value == 100)
                    {
                        vision_cell_value = GetNumberByVision(sim.GetCellByRotation(entity.cell.x, entity.cell.y, entity.rotation));
                    }
                    neuron.SetValue(vision_cell_value);             
                }
            } // input layer values initialization

            for (int i = 1; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < layers[i - 1].neurons.Count; k++)
                    {
                        sum += layers[i - 1].neurons[k].value * weights[i - 1].weights[k][j];
                    }
                    if (i != layers.Count - 1)
                    {
                        if (layers[i].neurons[j].type == "relu")
                        {
                            sum = ReLu(sum);
                        }
                        else if (layers[i].neurons[j].type == "line")
                        {
                            sum = Linear(sum);
                        }
                        else if (layers[i].neurons[j].type == "tanh")
                        {
                            sum = Tanh(sum);
                        }
                        else if (layers[i].neurons[j].type == "rand")
                        {
                            sum = Rand(sum);
                        }
                    }
                    layers[i].neurons[j].SetValue(sum);
                }
            }

            int max = 0;
            bool rotated_flag = false;
            for (int i = 0; i < layers[layers.Count - 1].neurons.Count; i++)
            {
                Neuron neuron = layers[layers.Count - 1].neurons[i];
                if (neuron.type == "rota" && !rotated_flag)
                {
                    rotated_flag = true;
                    neuron.SetValue((float)Sigmoid(neuron.value));
                    entity.RotateAbsolute(neuron.value);
                    continue;
                } // check for absolute turn
                else if (neuron.type == "rote" && !rotated_flag)
                {
                    rotated_flag = true;
                    neuron.SetValue((float)Sigmoid(neuron.value));
                    entity.RotateRelative(neuron.value);
                    continue;
                } // check for relative turn
                if (layers[layers.Count - 1].neurons[max].value < neuron.value)
                {
                    max = i;
                }
            } // finds the largest output neuron value

            return layers[layers.Count - 1].neurons[max].type;
        
        }

        public float GetNumberByVision(Cell cell)
        {
            if (cell == null)
            {
                return -1;
            }
            if (!cell.IsFree())
            {
                if (IsRelatives(this, cell.GetEntity().brain))
                {
                    return 1;
                }
                return 0.5f;
            }
            return 0;
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
                    info += $"  {neuron.type}\t--- {neuron.value:F7}";
                    info += "\n";
                }
            }
            return info;
        }

        public void MutateWeights(int count)
        {
            if (can_weights_mutate_in_different_layers)
            {
                for (int x = 0; x < count; x++)
                {
                    weights[rnd.Next(0, weights.Count)].Mutate(1);
                }
            }
            else
            {
                weights[rnd.Next(0, weights.Count)].Mutate(count);
            }
        }
    }
}
