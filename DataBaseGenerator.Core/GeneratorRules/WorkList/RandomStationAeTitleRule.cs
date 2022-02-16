using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.WorkList
{
    public sealed class RandomStationAeTitleRule : IGeneratorRule<string>
    {
        private static IDictionary<int, string> _stationAeTitle = new Dictionary<int, string>
        {
            {0, "UniExpert001"},
            {1, "UniExpert002"}
        };

        public string Generate()
        {
            var random = new Random();

            var stationAe = _stationAeTitle[random.Next(0, _stationAeTitle.Count)];

            return stationAe;
        }
    }
}
