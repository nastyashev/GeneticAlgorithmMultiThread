using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithmMultiThread.Parallel;
using GeneticAlgorithmMultiThread.Sequential;

namespace GeneticAlgorithmMultiThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define the parameters of the genetic algorithm
            int populationSize = 100;
            double mutationRate = 0.0001;
            int genomeLength = 1000;

            // Create a new instance of the genetic algorithm
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(populationSize, mutationRate, genomeLength);
            //GeneticAlgorithmParallel geneticAlgorithmParallel = new GeneticAlgorithmParallel(populationSize, mutationRate, genomeLength);

            var population = geneticAlgorithm.InitializePopulation();

            // Run the genetic algorithm
            geneticAlgorithm.Run(population);
            Console.WriteLine("-------------------------------------------------");
        }
    }
}
