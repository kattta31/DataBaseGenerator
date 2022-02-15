using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.WorkList
{
    public sealed class RandomCreateTimeRule : IGeneratorRule<TimeSpan>
    {
        public TimeSpan Generate()
        {
            return new TimeSpan(15, 20, 30);
        }
    }
}
