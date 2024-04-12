using HospitalManagementSystem.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Classes
{
    public class Patient
    {
        public static int nextId { get; set; } = 1;
        public int Id { get;  set; }
        public string fullName { get; set; }
        public string address { get; set; } 
        private string email { get; set; }
        private string phone { get; set; }
     

        public Patient(int patientNextId, string patientFullName, string patientAddress, string patientEmail, string patientPhone)
        {
            Id = patientNextId;
            FullName = patientFullName;
            address = patientAddress;
            email = patientEmail;
            phone = patientPhone;
           


        }



        public string FullName
        {
            get { return fullName; }

            set
            {
                // Validation: Ensure that the first name is not null or empty.
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Full name cannot be empty.");
                }

                fullName = value;
            }
        }

        

       

        public override string ToString()
        {
            //customizing the format of the patient's details.

            return $"Patient ID: {Id}\n" +
                $"Name: {fullName}\n" +
                $"Email: {email}\n" +
                $"Phone: {phone}\n" +
                $"Address: {address}";


        }

        

        public List<AppointmentManagement> appointments = new List<AppointmentManagement>();  // List to store patient's appointments
        public List<RegistrationManagement> registration = new List<RegistrationManagement>();  // List to store patient's registration

     
        public void AddAppointment(AppointmentManagement appointment)
        {
            appointments.Add(appointment); // Method to add an appointment to the patient's list
        }
        public void AddRegistration(RegistrationManagement myDoctor)
        {
            registration.Add(myDoctor);
        }

    }
}
