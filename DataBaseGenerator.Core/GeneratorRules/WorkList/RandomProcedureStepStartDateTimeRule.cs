using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.WorkList
{
    public sealed class RandomProcedureStepStartDateTimeRule : IGeneratorRule<DateTime>
    {
        public DateTime Generate()
        {
            return DateTime.Now;
        }
    }
}
