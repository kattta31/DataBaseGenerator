using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.WorkList
{
    public sealed class RandomLastNameRule : IGeneratorRule<string>
    {
        private static readonly IDictionary<int, string> _russianLastName = new Dictionary<int, string>
        {
            {0, "Покровский"},
            {1, "Лебединский"},
            {2, "Дубов"},
            {3, "Голицын"},
            {4, "Селиверстов"},
            {5, "Сибирцев"},
            {6, "Дунаевский"},
            {7, "Ржевский"},
            {8, "Чацкий"},
            {9, "Ростов"}
        };
        
        public string Generate()
        {
            var random = new Random();

            var lastName = _russianLastName[random.Next(0, _russianLastName.Count)];

            return lastName;
        }
    }
}
