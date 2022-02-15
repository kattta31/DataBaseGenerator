using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.WorkList
{
    public sealed class RandomPerformingPhysiciansNameRule : IGeneratorRule<string>
    {
        public string Generate()
        {
            return "Vasia";
        }
    }
}
