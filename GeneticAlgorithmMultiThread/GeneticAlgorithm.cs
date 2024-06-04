using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMultiThread
{
    // Sequentual implementation of the genetic algorithm
    public class GeneticAlgorithm : IGeneticAlgorithm
    {
        protected readonly Random Random = new Random();
        private readonly double _mutationRate;
        private readonly int _tournamentSize;

        public GeneticAlgorithm(double mutationRate, int tournamentSize)
        {
            _mutationRate = mutationRate;
            _tournamentSize = tournamentSize;
        }

        protected Route Crossover(Route parent1, Route parent2)
        {
            Route child = new Route(parent1.Cities);
            int startPos = Random.Next(parent1.Cities.Count);
            int endPos = Random.Next(parent1.Cities.Count);

            for (int i = 0; i < child.Cities.Count; i++)
            {
                if (startPos < endPos && i > startPos && i < endPos)
                {
                    child.Cities[i] = parent1.Cities[i];
                }
                else if (startPos > endPos)
                {
                    if (!(i < startPos && i > endPos))
                    {
                        child.Cities[i] = parent1.Cities[i];
                    }
                }
            }

            for (int i = 0; i < parent2.Cities.Count; i++)
            {
                if (!child.Cities.Contains(parent2.Cities[i]))
                {
                    for (int j = 0; j < child.Cities.Count; j++)
                    {
                        if (child.Cities[j] == null)
                        {
                            child.Cities[j] = parent2.Cities[i];
                            break;
                        }
                    }
                }
            }

            return child;
        }

        protected void Mutate(Route route)
        {
            for (int routePos1 = 0; routePos1 < route.Cities.Count; routePos1++)
            {
                if (Random.NextDouble() < _mutationRate)
                {
                    int routePos2 = Random.Next(route.Cities.Count);

                    City city1 = route.Cities[routePos1];
                    City city2 = route.Cities[routePos2];

                    route.Cities[routePos2] = city1;
                    route.Cities[routePos1] = city2;
                }
            }
        }

        protected Route TournamentSelection(Population population)
        {
            Population tournament = new Population(_tournamentSize, population.GetCities());
            for (int i = 0; i < _tournamentSize; i++)
            {
                Route tournamentRoute = population.Routes[Random.Next(population.Routes.Count)];
                tournament.Routes.Add(tournamentRoute);
            }

            return tournament.GetFittest();
        }

        public virtual Population EvolvePopulation(Population population)
        {
            Population newPopulation = new Population(population.Routes.Count, population.GetCities());

            newPopulation.Routes[0] = population.Routes.FirstOrDefault();

            for (int i = 1; i < newPopulation.Routes.Count; i++)
            {
                Route parent1 = TournamentSelection(population);
                Route parent2 = TournamentSelection(population);
                Route child = Crossover(parent1, parent2);
                Mutate(child);
                newPopulation.Routes[i] = child;
            }

            newPopulation.Sort();
            return newPopulation;
        }
    }
}
