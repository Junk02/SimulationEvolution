using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal class NeuralNetwork
    {
        //class for entity neuralNetwork

        // test commit for neuralNetwork branch!
        private List<Layer> layers;
        public Entity entity;

        public NeuralNetwork(Entity entity, int layers_quantity = default_layers_quantity)
        {
            this.entity = entity;

            for (int i = 0; i < layers_quantity; i++)
                layers[i] = new Layer();

            for (int i = 0; i < layers[0].neurons.Count; i++) // input layer initialization
            {
                int choice = rnd.Next(1, 4);
                string type = "basic";

                switch (choice)
                {
                    case 1:
                        type = "x";
                        break;
                    case 2:
                        type = "y";
                        break;
                    case 3:
                        type = "energy";
                        break;
                }

                layers[0].neurons[i].SetType(type);
            }

            // make other layers initializations
        }
    }
}
