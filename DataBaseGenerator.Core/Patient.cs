using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBaseGenerator.Core
{
    public class Patient
    {
       public int Id { get; set; }

       public string LastName { get; set; }

       public string Name { get; set; }
    }
}