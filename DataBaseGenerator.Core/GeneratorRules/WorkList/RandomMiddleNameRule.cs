using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.WorkList
{
    public sealed class RandomMiddleNameRule : IGeneratorRule<string>
    {
        private static readonly IDictionary<int, string> _russianMiddlename = new Dictionary<int, string>
        {
            {0, "Дмитреевич"},
            {1, "Аркадьевич"},
            {2, "Иваныч"},
            {3, "Семенович"},
            {4, "Вольфович"},
            {5, "Ильич"},
            {6, "Михайлович"},
            {7, "Григорьевич"},
            {8, "Борисович"},
            {9, "Юрьевич"}
        };

        public string Generate()
        {
            var random = new Random();

            var middleName = _russianMiddlename[random.Next(0, _russianMiddlename.Count)];

            return middleName;
        }
    }
}
