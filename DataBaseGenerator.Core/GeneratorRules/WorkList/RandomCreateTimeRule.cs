using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.WorkList
{
    public sealed class RandomCreateTimeRule : IGeneratorRule<TimeSpan>
    {
        public TimeSpan Generate()
        {
            TimeSpan timeNow = DateTime.Now.TimeOfDay;

            return timeNow;
        }
    }
}
