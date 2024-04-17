using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMultiThread.Sequential
{
    // Sequentual implementation of the genetic algorithm
    internal class GeneticAlgorithm
    {
        protected int populationSize;
        protected double mutationRate;
        protected int genomeLength;
        protected Random random;

        public GeneticAlgorithm(int populationSize, double mutationRate, int genomeLength)
        {
            this.populationSize = populationSize;
            this.mutationRate = mutationRate;
            this.genomeLength = genomeLength;
            this.random = new Random();
        }

        // Initialize population with random genomes
        public List<string> InitializePopulation()
        {
            List<string> population = new List<string>();

            for (int i = 0; i < populationSize; i++)
            {
                population.Add(GenerateRandomGenome());
            }

            return population;
        }

        // Generate a random genome of given length
        protected string GenerateRandomGenome()
        {
            StringBuilder genome = new StringBuilder();

            for (int i = 0; i < genomeLength; i++)
            {
                genome.Append(random.Next(2)); // Append 0 or 1
            }

            return genome.ToString();
        }

        // Selection process
        protected string Selection(List<string> population)
        {
            int tournamentSize = 5; // Define the tournament size
            List<string> tournament = new List<string>();

            // Select random individuals for the tournament
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomId = random.Next(population.Count);
                tournament.Add(population[randomId]);
            }

            // Select the fittest individual
            string fittest = tournament[0];
            double maxFitness = CalculateFitness(fittest);

            for (int i = 1; i < tournamentSize; i++)
            {
                double fitness = CalculateFitness(tournament[i]);
                if (fitness > maxFitness)
                {
                    fittest = tournament[i];
                    maxFitness = fitness;
                }
            }

            return fittest;
        }

        // Crossover process
        protected (string, string) Crossover(string parent1, string parent2)
        {
            // Choose a random crossover point
            int crossoverPoint = random.Next(genomeLength);

            // Swap bits after the crossover point
            string child1 = parent1.Substring(0, crossoverPoint) + parent2.Substring(crossoverPoint);
            string child2 = parent2.Substring(0, crossoverPoint) + parent1.Substring(crossoverPoint);

            return (child1, child2);
        }

        // Mutation process
        protected string Mutation(string genome)
        {
            StringBuilder mutatedGenome = new StringBuilder(genome);

            for (int i = 0; i < genomeLength; i++)
            {
                // Flip the bit with a probability equal to the mutation rate
                if (random.NextDouble() < mutationRate)
                {
                    mutatedGenome[i] = genome[i] == '0' ? '1' : '0';
                }
            }

            return mutatedGenome.ToString();
        }

        // Fitness calculation
        protected double CalculateFitness(string genome)
        {
            // Count the number of 1s in the genome
            int count = genome.Count(c => c == '1');

            return count;
        }

        // Run the genetic algorithm
        public virtual void Run(List<string> population)
        {
            // Find the fittest individual in the initial population
            string fittest = population[0];
            double maxFitness = CalculateFitness(fittest);
            // counter for the number of generations
            int generation = 0;

            // Run the genetic algorithm until the fittest individual has the maximum fitness
            while (maxFitness < genomeLength)
            {
                List<string> newPopulation = new List<string>();

                // Create a new population by selecting, crossing over, and mutating individuals
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

                // Find the fittest individual in the new population
                for (int i = 1; i < populationSize; i++)
                {
                    double fitness = CalculateFitness(population[i]);
                    if (fitness > maxFitness)
                    {
                        fittest = population[i];
                        maxFitness = fitness;
                    }
                }

                generation++;
            }

            Console.WriteLine("Generation: " + generation);
            Console.WriteLine("Fittest genome: " + fittest);
        }
    }
}
