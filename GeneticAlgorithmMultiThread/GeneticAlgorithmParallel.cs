using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GeneticAlgorithmMultiThread
{
    // Parallel implementation of the genetic algorithm
    public class GeneticAlgorithmParallel : GeneticAlgorithm
    {
        public GeneticAlgorithmParallel(double mutationRate, int tournamentSize) : base(mutationRate, tournamentSize)
        {
        }

        public override Population EvolvePopulation(Population population)
        {
            Population newPopulation = new Population(population.Routes.Count, population.GetCities());
            int elitismOffset = 0;
            if (elitismOffset > 0)
            {
                newPopulation.Routes[0] = population.GetFittest();
                elitismOffset++;
            }

            Parallel.For(elitismOffset, population.Routes.Count, i =>
            {
                Route parent1 = TournamentSelection(population);
                Route parent2 = TournamentSelection(population);
                Route child = Crossover(parent1, parent2);
                Mutate(child);
                newPopulation.Routes[i] = child;
            });

            newPopulation.Sort();
            return newPopulation;
        }
    }
}