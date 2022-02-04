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
            Patient newPatient = new Patient();

            newPatient.ID_Patient = 1;
            newPatient.FirstName = "Vasia";
            newPatient.LastName = "Pupkin";
            newPatient.MiddleName = "Olegich";
            newPatient.PatientID = "MXR-0001";
            newPatient.BirthDate = new DateTime(1985,01,01);
            newPatient.Sex = "M";
            newPatient.Address = "Minsk";
            newPatient.AddInfo = "No comments";
            newPatient.Occupation = "Engineer";


                

            Console.WriteLine(newPatient);

        }
    }
}
