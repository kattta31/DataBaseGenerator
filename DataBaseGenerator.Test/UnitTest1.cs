using System;
using DataBaseGenerator.Core;
using DataBaseGenerator.Core.GeneratorRules.Patient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBaseGenerator.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RandomNameGenerate()
        {
            var name = new RandomFirstNameRule();
            var result = name.Generate();

            Console.WriteLine(result);
        }

        [TestMethod]
        public void RandomLastNameGenerate()
        {
            var lastName = new RandomLastNameRule();
            var result = lastName.Generate();

            Console.WriteLine(result);
        }

        [TestMethod]
        public void CreateNewPatient()
        {
            var newPatient = new PatientGeneratorParameters(
                new OrderIdPatientRule(),
                new RandomFirstNameRule(),
                new RandomLastNameRule(),
                new RandomMiddleNameRule(),
                new OrderPatientIdRule(),
                new RandomBirthDateRule(new DateTime()),
                new RandomSexRule(),
                new RandomAddressRule(),
                new RandomAddInfoRule(),
                new RandomOccupationRule()
            );

            newPatient.ID_Patient.Generate(1);
            newPatient.LastName.Generate();
            newPatient.FirstName.Generate();
            newPatient.MiddleName.Generate();
            newPatient.PatientID.Generate(1);
            newPatient.BirthDate.Generate();
            newPatient.Sex.Generate();
            newPatient.Address.Generate();
            newPatient.AddInfo.Generate();
            newPatient.Occupation.Generate();

            var result = newPatient;

            Console.WriteLine(result);
        }

        [TestMethod]
        public void GeneratePatient()
        {
            var patient = new PatientGenerator();

            var newPatient = new PatientGeneratorParameters(
                new OrderIdPatientRule(),
                new RandomFirstNameRule(),
                new RandomLastNameRule(),
                new RandomMiddleNameRule(),
                new OrderPatientIdRule(),
                new RandomBirthDateRule(new DateTime()),
                new RandomSexRule(),
                new RandomAddressRule(),
                new RandomAddInfoRule(),
                new RandomOccupationRule());


            var result = patient.Generate(newPatient);

            Console.WriteLine(result);
        }
    }
}
