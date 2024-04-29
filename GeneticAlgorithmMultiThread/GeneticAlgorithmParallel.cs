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
            int numPopulations = 10;

            int size = initialPopulation.Count / numPopulations;
            int remainder = initialPopulation.Count % numPopulations;

            List<List<string>> populations = new List<List<string>>();

            for (int i = 0; i < numPopulations; i++)
            {
                List<string> population = new List<string>();

                for (int j = 0; j < size; j++)
                {
                    population.Add(initialPopulation[i * size + j]);
                }

                populations.Add(population);
            }

            if (remainder > 0)
            {
                populations[numPopulations - 1].AddRange(initialPopulation.GetRange(numPopulations * size, remainder));
            }

            ThreadPool.SetMinThreads(numPopulations, numPopulations);

            for (int i = 0; i < numPopulations; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(RunPopulation), populations[i]);
            }

            // find the fittest individual
            double maxFitness = CalculateFitness(populations[0][0]);
            string fittestIndividual = populations[0][0];

            for (int i = 0; i < numPopulations; i++)
            {
                for (int j = 0; j < populations[i].Count; j++)
                {
                    double fitness = CalculateFitness(populations[i][j]);
                    if (fitness > maxFitness)
                    {
                        maxFitness = fitness;
                        fittestIndividual = populations[i][j];
                    }
                }
            }

            Console.WriteLine($"Fittest individual: {fittestIndividual}");
        }

        private void RunPopulation(object initialPopulation)
        {
            List<string> population = (List<string>)initialPopulation;
            List<string> newPopulation = new List<string>();

            for (int i = 0; i < population.Count; i++)
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

            double maxFitness = CalculateFitness(population[0]);

            for (int i = 1; i < populationSize; i++)
            {
                double fitness = CalculateFitness(population[i]);
                if (fitness > maxFitness)
                {
                    maxFitness = fitness;
                }
            }
        }
    }
}