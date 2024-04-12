using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Classes
{
    public class Doctor
    {
        public static int NextId { get; set; } = 1;
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }

        public Doctor(int id, string firstName, string lastName, string email, string phone, int streetNumber, string streetName, string city)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            StreetNumber = streetNumber;
            StreetName = streetName;
            City = city;
        }




        public override string ToString()
        {
            return $"Doctor ID: {Id}\n" +
                $"Name: {FirstName} {LastName}\n" +
                $"Email: {Email}\n" +
                $"Phone: {Phone}\n" +
                $"Address: {StreetNumber} {StreetName}, {City}";
        }

    }
}
