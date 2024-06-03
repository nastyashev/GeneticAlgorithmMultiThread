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
            for (int i = 0; i < Cities.Count; i++)
            {
                City fromCity = Cities[i];
                City toCity = i + 1 < Cities.Count ? Cities[i + 1] : Cities[0];
                distance += fromCity.DistanceTo(toCity);
            }

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
            sb.Append("Distance: " + Distance);

            return sb.ToString();
        }
    }
}
