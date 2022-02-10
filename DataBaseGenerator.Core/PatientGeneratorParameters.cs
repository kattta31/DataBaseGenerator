using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Core
{
    public class PatientGeneratorParameters
    {
        public PatientGeneratorParameters(
            
            IGeneratorRule<int, int> iDPatient,
            IGeneratorRule<string> lastName,
            IGeneratorRule<string> firstName,
            IGeneratorRule<string> middleName,
            IGeneratorRule<string, int> patientId,
            IGeneratorRule<DateTime> birthDate,
            IGeneratorRule<string> sex,
            IGeneratorRule<string> address,
            IGeneratorRule<string> addInfo,
            IGeneratorRule<string> occupation)

        {

            ID_Patient = iDPatient ?? throw new ArgumentNullException(nameof(iDPatient));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            MiddleName = middleName ?? throw new ArgumentNullException(nameof(middleName));
            PatientID = patientId ?? throw new ArgumentNullException(nameof(patientId));
            BirthDate = birthDate ?? throw new ArgumentNullException(nameof(birthDate));
            Sex = sex ?? throw new ArgumentNullException(nameof(sex));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            AddInfo = addInfo ?? throw new ArgumentNullException(nameof(addInfo));
            Occupation = occupation ?? throw new ArgumentNullException(nameof(occupation));
        }




        public int PatientCount { get; set; }

        public  IGeneratorRule<int, int> ID_Patient { get; }

        public IGeneratorRule<string> LastName { get; }

        public IGeneratorRule<string> FirstName { get; }

        public IGeneratorRule<string> MiddleName { get; }

        public IGeneratorRule<string, int> PatientID { get; }

        public IGeneratorRule<DateTime> BirthDate { get; }

        public IGeneratorRule<string> Sex { get; }

        public IGeneratorRule<string> Address { get; }

        public IGeneratorRule<string> AddInfo { get; }

        public IGeneratorRule<string> Occupation { get; }

    }
}
