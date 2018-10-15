using System;
using System.Runtime.CompilerServices;
using PersonManagement.Constants;

namespace PersonManagement.Services.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public EGender? Gender { get; set; }
        public string GenderName { get; set; }
        public decimal? Salary { get; set; }
  }
}