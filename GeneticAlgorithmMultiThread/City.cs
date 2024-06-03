using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMultiThread
{
    public class City
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Index { get; set; }

        public City(int x, int y, int index)
        {
            X = x;
            Y = y;
            Index = index;
        }

        public double DistanceTo(City city)
        {
            int xDistance = Math.Abs(X - city.X);
            int yDistance = Math.Abs(Y - city.Y);
            double distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

            return distance;
        }

        public override string ToString()
        {
            return Index.ToString() + " (" + X + ", " + Y + ")";
        }
    }
}
