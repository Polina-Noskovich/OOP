using HospitalManagementSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Managers
{
    public class AppointmentManagement
    {
        public Doctor Doctor { get; }
        public Patient Patient { get; }
        public string Description { get; }
        public DateTime Date { get; }


        public AppointmentManagement(Doctor doctor, Patient patient, string description, DateTime dd)
        {
            Doctor = doctor;
            Patient = patient;
            Description = description;
            Date = dd;  
        }

        
    }
}
