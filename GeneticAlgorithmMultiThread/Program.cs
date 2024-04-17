using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GeneticAlgorithmMultiThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define the parameters of the genetic algorithm
            int populationSize = 10;
            double mutationRate = 0.01;
            int genomeLength = 20;

            // Create a new instance of the genetic algorithm
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(populationSize, mutationRate, genomeLength);
            GeneticAlgorithmParallel geneticAlgorithmParallel = new GeneticAlgorithmParallel(populationSize, mutationRate, genomeLength);

            var population = geneticAlgorithm.InitializePopulation();

            // Run the genetic algorithm
            //RunAlgorithm(geneticAlgorithm, population);
            Console.WriteLine("-------------------------------------------------");
            geneticAlgorithmParallel.Run(population);
        }

        static void RunAlgorithm(IGeneticAlgorithm geneticAlgorithm, List<string> population)
        {
            // warmup
            for (int i = 0; i < 20; i++)
            {
                geneticAlgorithm.Run(population);
            }

            // measure
            List<long> microsecondsList = new List<long>();
            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < 20; i++)
            {
                stopwatch.Start();

                geneticAlgorithm.Run(population);

                stopwatch.Stop();

                long microsecondsTmp = stopwatch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                microsecondsList.Add(microsecondsTmp);
            }

            long microseconds = (long)microsecondsList.Average();

            Console.WriteLine("Time taken: " + microseconds + " microseconds");
        }
    }
}
