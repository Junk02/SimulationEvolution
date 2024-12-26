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
        public Entity entity;

        public NeuralNetwork(Entity entity, int layers_quantity = default_layers_quantity)
        {
            this.entity = entity;

            this.layers = new List<Layer>();

            for (int i = 0; i < layers_quantity; i++)
                layers.Add(new Layer());

            for (int i = 0; i < layers[0].neurons.Count; i++)
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
            } // input layer initialization

            for (int i = 1; i < layers.Count - 1; i++)
            {
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    layers[i].neurons[j].SetType("basic");
                }
            } // hidden layers initialization

            for (int i = 0; i < layers[layers_quantity - 1].neurons.Count; i++)
            {
                int choice = rnd.Next(1, 4);
                string type = "basic";

                switch (choice)
                {
                    case 1:
                        type = "move";
                        break;
                    case 2:
                        type = "rotate";
                        break;
                    case 3:
                        type = "bite";
                        break;
                }

                layers[layers_quantity - 1].neurons[i].SetType(type);
            } // output layer initialization
        }
    }
}
