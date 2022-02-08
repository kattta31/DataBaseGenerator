using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core.GeneratorRules.Patient
{
    public sealed class OrderIdPatientRule : IGeneratorRule<int, int>
    {
        //public string ID_Patient { get; set; } = "Nomer";

        public int Generate(int parameter)
        {
            var iDPatient = parameter;

            return iDPatient;
        }

        //public override string ToString()
        //{
        //    return $"{Generate($"{}")}";
        //}
    }
}
