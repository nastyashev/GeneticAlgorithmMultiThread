using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using GeneticAlgorithmMultiThread.MultiThread;
using GeneticAlgorithmMultiThread.Sequential;

namespace GeneticAlgorithmMultiThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define the parameters of the genetic algorithm
            int populationSize = 100;
            double mutationRate = 0.00001;
            int genomeLength = 500;

            // Create a new instance of the genetic algorithm
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(populationSize, mutationRate, genomeLength);

            // Run the genetic algorithm
            string result = geneticAlgorithm.Run(1000);
            Console.WriteLine(result);
        }
    }
}
