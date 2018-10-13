using System;

namespace PersonManagement.Data.DbModels
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? Gender { get; set; }
        public decimal? Salary { get; set; }
    }
}