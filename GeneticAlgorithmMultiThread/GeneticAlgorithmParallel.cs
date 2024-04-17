using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMultiThread
{
    // Parallel implementation of the genetic algorithm
    internal class GeneticAlgorithmParallel : GeneticAlgorithm
    {
        public GeneticAlgorithmParallel(int populationSize, double mutationRate, int genomeLength) : base(populationSize, mutationRate, genomeLength)
        {
        }


    }
}
