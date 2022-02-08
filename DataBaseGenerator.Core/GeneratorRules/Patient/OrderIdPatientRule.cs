using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.Patient
{
    public sealed class OrderIdPatientRule : IGeneratorRule<string, int>
    {
        public int ID_Patient { get; set; }

        public string Generate(int index)
        {
            return $"{index}";
        }

        //public override string ToString()
        //{
        //    return $"{Generate($"{}")}";
        //}
    }
}
