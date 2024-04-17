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
            // Number of populations
            int numPopulations = 4;

            int populationSize = initialPopulation.Count / numPopulations;
            int remainder = initialPopulation.Count % numPopulations;

            // Create multiple populations from the initial population
            List<List<string>> populations = new List<List<string>>();
            int currentIndex = 0;
            for (int i = 0; i < numPopulations; i++)
            {
                int currentPopulationSize = populationSize + (i < remainder ? 1 : 0);
                List<string> population = initialPopulation.Skip(currentIndex).Take(currentPopulationSize).ToList();
                populations.Add(population);
                currentIndex += currentPopulationSize;
            }

            // List to store the fittest individual from each population
            List<string> fittestIndividuals = new List<string>();
            List<double> maxFitnesses = new List<double>();

            // Initialize the lists
            for (int i = 0; i < numPopulations; i++)
            {
                fittestIndividuals.Add(populations[i][0]);
                maxFitnesses.Add(CalculateFitness(populations[i][0]));
            }

            // Run the genetic algorithm until the fittest individual has the maximum fitness
            while (maxFitnesses.Max() < genomeLength)
            {
                // Use a parallel for loop to process each population in parallel
                Parallel.For(0, numPopulations, i =>
                {
                    List<string> population = populations[i];
                    List<string> newPopulation = new List<string>();

                    // Process the population
                    for (int j = 0; j < population.Count; j++)
                    {
                        string parent1 = Selection(population);
                        string parent2 = Selection(population);

                        (string child1, string child2) = Crossover(parent1, parent2);

                        child1 = Mutation(child1);
                        child2 = Mutation(child2);

                        // Add the new individual to the new population
                        lock (newPopulation)
                        {
                            newPopulation.Add(child1);
                        }
                    }

                    populations[i] = newPopulation;

                    // Find the fittest individual in the new population
                    for (int j = 0; j < population.Count; j++)
                    {
                        double fitness = CalculateFitness(population[j]);
                        if (fitness > maxFitnesses[i])
                        {
                            lock (this)
                            {
                                if (fitness > maxFitnesses[i])
                                {
                                    fittestIndividuals[i] = population[j];
                                    maxFitnesses[i] = fitness;
                                }
                            }
                        }
                    }
                });
            }
            // Find the fittest individual among the fittest individuals
            int fittestIndex = maxFitnesses.IndexOf(maxFitnesses.Max());
            Console.WriteLine("Fittest genome: " + fittestIndividuals[fittestIndex]);
        }
    }
}