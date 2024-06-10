using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMultiThread
{
    public class Route
    {
        public List<City> Cities { get; set; }
        public double Distance { get; set; }

        public Route(List<City> cities)
        {
            Cities = cities;
            Distance = CalculateDistance();
        }

        public double CalculateDistance()
        {
            double distance = 0;
            for (int i = 0; i < Cities.Count - 1; i++)
            {
                distance += Cities[i].DistanceTo(Cities[i + 1]);
            }
            distance += Cities[Cities.Count - 1].DistanceTo(Cities[0]);

            return distance;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Route: ");
            foreach (City city in Cities)
            {
                sb.Append(city.Index + " ");
            }
            sb.Append("\nDistance: " + Distance);

            return sb.ToString();
        }
    }
}
