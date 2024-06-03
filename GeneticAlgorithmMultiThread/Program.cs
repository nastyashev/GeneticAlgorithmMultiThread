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
            double mutationRate = 0.01;
            int tournamentSize = 50;
            int populationSize = 200;
            int generations = 100;
            Random Random = new Random();

            List<City> cities = new List<City>();
            for (int i = 0; i < 200; i++)
            {
                cities.Add(new City(Random.Next(1000), Random.Next(1000), i));
            }

            // Create a new instance of the genetic algorithm
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(mutationRate, tournamentSize);
            GeneticAlgorithmParallel geneticAlgorithmParallel = new GeneticAlgorithmParallel(mutationRate, tournamentSize);

            Population population1 = new Population(populationSize, cities);
            Population population2 = population1.Copy();

            // Run the genetic algorithm
            RunAlgorithm(geneticAlgorithm, population1, generations);
            Console.WriteLine("-------------------------------------------------");
            RunAlgorithm(geneticAlgorithmParallel, population2, generations);
        }

        static void RunAlgorithm(GeneticAlgorithm geneticAlgorithm, Population population, int generations)
        {
            // warmup
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < generations; j++)
                {
                    population = geneticAlgorithm.EvolvePopulation(population);
                }
            }

            // measure
            List<long> microsecondsList = new List<long>();
            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < 20; i++)
            {
                stopwatch.Start();

                for (int j = 0; j < generations; j++)
                {
                    population = geneticAlgorithm.EvolvePopulation(population);
                }

                stopwatch.Stop();

                long microsecondsTmp = stopwatch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                microsecondsList.Add(microsecondsTmp);
            }

            long microseconds = (long)microsecondsList.Average();

            Console.WriteLine("Time taken: " + microseconds + " microseconds");

            Console.WriteLine("Final distance: " + population.GetFittest().Distance);
            Console.WriteLine("Solution:");
            Console.WriteLine(population.GetFittest());
        }
    }
}
