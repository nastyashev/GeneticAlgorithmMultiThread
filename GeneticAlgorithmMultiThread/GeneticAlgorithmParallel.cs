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

        public override void Run(List<string> population)
        {
            // Find the fittest individual in the initial population
            string fittest = population[0];
            double maxFitness = CalculateFitness(population[0]);
            // counter for the number of generations
            int generation = 0;

            // Run the genetic algorithm until the fittest individual has the maximum fitness
            while (maxFitness < genomeLength)
            {
                List<string> newPopulation = new List<string>();

                // Use a parallel for loop to process the population in parallel
                Parallel.For(0, population.Count, i =>
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
                });

                population = newPopulation;

                // Find the fittest individual in the new population
                Parallel.For(0, population.Count, i =>
                {
                    double fitness = CalculateFitness(population[i]);
                    if (fitness > maxFitness)
                    {
                        lock (this)
                        {
                            if (fitness > maxFitness)
                            {
                                fittest = population[i];
                                maxFitness = fitness;
                            }
                        }
                    }
                });

                generation++;
            }

            Console.WriteLine("Generation: " + generation);
            Console.WriteLine("Fittest genome: " + fittest);
        }
    }
}
