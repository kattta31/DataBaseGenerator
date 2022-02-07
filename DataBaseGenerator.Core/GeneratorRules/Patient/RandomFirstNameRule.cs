using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.Patient
{
    public sealed class RandomFirstNameRule : IGeneratorRule<string>
    {
        private static readonly IDictionary<int, string> _russianName = new Dictionary<int, string>
        {
            {0, "Андрей"},
            {1, "Борис"},
            {2, "Виталик"},
            {3, "Георгий"},
            {4, "Дмитрий"},
            {5, "Егор"},
            {6, "Сергей"},
            {7, "Ярик"},
            {8, "Павел"},
            {9, "Марк"}
        };

        public string Generate()
        {
            var random = new Random();

            var rusName = _russianName[random.Next(0, _russianName.Count)];

            return rusName;
        }

        public override string ToString()
        {
            return $"{Generate()}";
        }
    }
}
