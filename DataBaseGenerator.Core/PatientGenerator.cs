using System;
using System.Collections.Generic;
using DataBaseGenerator.Core.GeneratorRules.Patient;

namespace DataBaseGenerator.Core
{
    public class PatientGenerator
    {
        public IEnumerable<PatientGeneratorParameters> Generate(PatientGeneratorParameters patientGeneratorParameters)
        {
            var dataBaseGenerators = new List<PatientGeneratorParameters>();


            for (var patientindex = 0; patientindex < patientGeneratorParameters.PatientCount; patientindex++)
            {
                var patients = CreatePatientModule(patientindex);

                dataBaseGenerators.Add(patients);
            }

            return dataBaseGenerators;
        }


        public PatientGeneratorParameters CreatePatientModule(int patientIndex)
        {
            var newPatient = new PatientGeneratorParameters(
                new OrderPatientIdRule(),
                new RandomFirstNameRule(),
                new RandomLastNameRule(),
                new RandomMiddleNameRule(),
                new RandomBirthDateRule(new DateTime()),
                new RandomSexRule(),
                new RandomAddressRule(),
                new RandomAddInfoRule(),
                new RandomOccupationRule()
            );

            newPatient.PatientId.Generate(patientIndex);
            newPatient.LastName.Generate();
            newPatient.FirstName.Generate();
            newPatient.MiddleName.Generate();
            newPatient.BirthDate.Generate();
            newPatient.Sex.Generate();
            newPatient.Address.Generate();
            newPatient.AddInfo.Generate();
            newPatient.Occupation.Generate();

            return newPatient;
        }
    }
}