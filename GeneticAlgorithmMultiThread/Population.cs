using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMultiThread
{
    public class Population
    {
        public List<Route> Routes { get; set; }

        public Population(int populationSize, List<City> cities)
        {
            Routes = new List<Route>();
            for (int i = 0; i < populationSize; i++)
            {
                List<City> shuffledCities = cities.OrderBy(x => Guid.NewGuid()).ToList();
                Routes.Add(new Route(shuffledCities));
            }
        }

        public Route GetFittest()
        {
            Route fittest = Routes[0];
            foreach (Route route in Routes)
            {
                if (route.Distance < fittest.Distance)
                {
                    fittest = route;
                }
            }

            return fittest;
        }

        public void Sort()
        {
            Routes = Routes.OrderBy(x => x.Distance).ToList();
        }

        public List<City> GetCities()
        {
            return Routes[0].Cities;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Route route in Routes)
            {
                sb.Append(route + "\n");
            }

            return sb.ToString();
        }

        public Population Copy()
        {
            Population newPopulation = new Population(Routes.Count, GetCities());
            for (int i = 0; i < Routes.Count; i++)
            {
                newPopulation.Routes[i] = Routes[i];
            }

            return newPopulation;
        }
    }
}
