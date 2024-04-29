using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GeneticAlgorithmMultiThread
{
    // Parallel implementation of the genetic algorithm
    internal class GeneticAlgorithmParallel : GeneticAlgorithm
    {
        public GeneticAlgorithmParallel(int populationSize, double mutationRate, int genomeLength) : base(populationSize, mutationRate, genomeLength)
        {
        }

        public override void Run(List<string> initialPopulation)
        {
            // Number of populations to create in parallel
            int numPopulations = Environment.ProcessorCount;

            int size = initialPopulation.Count / numPopulations;
            int remainder = initialPopulation.Count % numPopulations;

            // Split the initial population into multiple sub-populations
            List<List<string>> subPopulations = new List<List<string>>();
            for (int i = 0; i < numPopulations; i++)
            {
                //subPopulations.Add(initialPopulation.Skip(i * initialPopulation.Count / numPopulations).Take(initialPopulation.Count / numPopulations).ToList());
                int start = i * size;
                int end = (i == numPopulations - 1) ? start + size + remainder : start + size;
                subPopulations.Add(initialPopulation.GetRange(start, end - start));
            }

            // Create and evolve populations in parallel
            List<string> bestIndividuals = new List<string>(new string[numPopulations]);
            Parallel.For(0, numPopulations, i =>
            {
                List<string> population = subPopulations[i];
                double maxFitness = CalculateFitness(population[0]);

                while (maxFitness < genomeLength)
                {
                    List<string> newPopulation = new List<string>();

                    for (int j = 0; j < populationSize / 2; j++)
                    {
                        string parent1 = Selection(population);
                        string parent2 = Selection(population);

                        (string child1, string child2) = Crossover(parent1, parent2);

                        child1 = Mutation(child1);
                        child2 = Mutation(child2);

                        newPopulation.Add(child1);
                        newPopulation.Add(child2);
                    }

                    population = newPopulation;

                    for (int j = 1; j < populationSize; j++)
                    {
                        double fitness = CalculateFitness(population[j]);
                        if (fitness > maxFitness)
                        {
                            maxFitness = fitness;
                        }
                    }
                }

                string bestIndividual = population.Aggregate((i1, i2) => CalculateFitness(i1) > CalculateFitness(i2) ? i1 : i2);
                bestIndividuals[i] = bestIndividual;
            });

            // Select the best individual from the best individuals
            string overallBestIndividual = bestIndividuals.Aggregate((i1, i2) => CalculateFitness(i1) > CalculateFitness(i2) ? i1 : i2);
            Console.WriteLine("Overall best individual: " + overallBestIndividual);
        }
    }
}