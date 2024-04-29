using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMultiThread
{
    internal interface IGeneticAlgorithm
    {
        void Run(List<string> population, int generations);
    }
}
