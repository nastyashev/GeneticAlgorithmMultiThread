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
            int tournamentSize = 5;
            int populationSize = 20;
            int generations = 10;
            Random Random = new Random();

            List<City> cities = new List<City>();
            for (int i = 0; i < 10; i++)
            {
                cities.Add(new City(Random.Next(1000), Random.Next(1000), i));
            }

            /*List<City> cities = new List<City>
            {
            new City(0, 0, 1),
            new City(0, 1, 2),
            new City(1, 1, 3)
            };*/

            // Create a new instance of the genetic algorithm
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(mutationRate, tournamentSize);
            GeneticAlgorithmParallel geneticAlgorithmParallel = new GeneticAlgorithmParallel(mutationRate, tournamentSize);

            Population population1 = new Population(populationSize, cities);
            Population population2 = population1.Copy();

            // Run the genetic algorithm
            Console.WriteLine("Genetic Algorithm");
            RunAlgorithm(geneticAlgorithm, population1, generations);

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Genetic Algorithm Parallel");
            RunAlgorithm(geneticAlgorithmParallel, population2, generations);
        }

        static void RunAlgorithm(GeneticAlgorithm geneticAlgorithm, Population population, int generations)
        {
            Population populationTmp = population.Copy();
            // warmup
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < generations; j++)
                {
                    populationTmp = geneticAlgorithm.EvolvePopulation(population);
                }
            }

            // measure
            List<long> milisecondsList = new List<long>();
            Stopwatch stopwatch = new Stopwatch();
            populationTmp = population.Copy();

            for (int i = 0; i < 20; i++)
            {
                stopwatch.Start();

                for (int j = 0; j < generations; j++)
                {
                    populationTmp = geneticAlgorithm.EvolvePopulation(population);
                }

                stopwatch.Stop();
                long milliseconds = stopwatch.ElapsedMilliseconds;
                milisecondsList.Add(milliseconds);

                stopwatch.Reset();
            }
            population = populationTmp.Copy();

            long miliseconds = (long)milisecondsList.Average();

            Console.WriteLine("Time taken: " + miliseconds + " milliseconds");

            Console.WriteLine("Solution:");
            Console.WriteLine(population.GetFittest());
            if (population.GetFittest().Cities.Count <= 10)
            {
                Console.WriteLine("Cities:");
                foreach (City city in population.GetFittest().Cities)
                {
                    Console.WriteLine(city);
                }
            }
        }
    }
}
