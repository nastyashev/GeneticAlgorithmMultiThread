using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMultiThread
{
    public interface IGeneticAlgorithm
    {
        Population EvolvePopulation(Population population);
    }
}
