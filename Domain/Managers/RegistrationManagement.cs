using HospitalManagementSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Managers
{
    public class RegistrationManagement
    {
        public Doctor Doctor { get; }
        public Patient Patient { get; }

        public RegistrationManagement(Doctor doctor, Patient patient)
        {
            Doctor = doctor;
            Patient = patient;
        }

        public bool RegisterWithDoctor(Doctor doctor, Patient patient, List<AppointmentManagement> appointments)
        {
            bool flag = true;
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].Doctor == doctor)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
