using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;

namespace SimulationEvolution
{
    internal class Weights
    {
        public List<List<float>> weights;
        public int layer1_size { get; private set; }
        public int layer2_size { get; private set; }

        public Weights(int layer1, int layer2)
        {
            layer1_size = layer1;
            layer2_size = layer2;
            weights = new List<List<float>>();

            for (int i = 0; i < layer1; i++)
            {
                weights.Add(new List<float>());
                for (int j = 0; j < layer2; j++)
                {
                    float weight = (float)(rnd.NextDouble() * (max_weight_size - min_weight_size) - min_weight_size);
                    weights[i].Add(weight);
                }
            }
        }

        public Weights(Weights parent)
        {
            layer1_size = parent.layer1_size;
            layer2_size = parent.layer2_size;
            weights = parent.weights.Select(row => new List<float>(row)).ToList();
        }
    }
}
