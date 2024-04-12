using HospitalManagementSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace HospitalManagementSystem.Managers
{
    public class PatientManagement
    {
        public List<Patient> patients;
        public string patientsFilePath = "D:\\c#\\HospitalManagSystem\\OOP\\Data\\patientList.txt"; //text file for storing doctor data
        public PatientManagement()
        {
            patients = new List<Patient>();
            LoadPatientsFromFile(); // Load patients from file when the class is initialized.
        }

        // *** Function for adding new patient ***

        public void AddPatient(Patient patient)
        {
            //patient.Id = GenerateUniquePatientId();


            patients.Add(patient);  //add patient to the list

            SavePatientsToFile(); //Save the updated list to the file

            Console.WriteLine("Patient added successfully!");
        }
        public bool DeletePatient(int patientId)
        {
            Patient patientToRemove = patients.FirstOrDefault(p => p.Id == patientId);
            if (patientToRemove != null)
            {
                patients.Remove(patientToRemove);
                SavePatientsToFile(); // Save the updated list to the file after deletion
                Console.WriteLine("Patient deleted successfully!");
                return true;
            }
            else
            {
                Console.WriteLine("Patient with specified ID not found!");
                return false;
            }
        }


        // *** Fuction to list all patients ***
        public void ListPatients()
        {
            Console.Clear();
            Console.WriteLine("List of All Patients:");

            if(patients.Count == 0)
            {
                Console.WriteLine("No patients found");
            }
            else
            {
                foreach (Patient patient in patients)
                {
                    Console.WriteLine(patient.ToString());
                }
            }
        }
      



        // *** Function to list details of a specific patient by ID ***

        public void ListPatientDetails(int patientId)
        {
            var patient = patients.Find(p => p.Id == patientId);

            if (patient != null)
            {
                Console.WriteLine(patient.ToString());
            }
            else
            {
                Console.WriteLine("Patient not found!");
            }
        }

        // *** Function to generate a unique patient ID ***
        private int GenerateUniquePatientId()
        {
            //Find the maximum existing patient ID and add 1
            int maxId = 0;
            foreach (var patient in patients)
            {
                if (patient.Id > maxId)
                {
                    maxId = patient.Id;
                }

            }
            return maxId + 1;
        }

        public void LoadPatientsFromFile()
        {
            try
            {
                if (File.Exists(patientsFilePath))
                {
                    // Read the file and create doctor objects
                    string[] lines = File.ReadAllLines(patientsFilePath);
                    foreach (string line in lines)
                    {
                        //Split the line into fields (assuming fields are separated by a delimiter like a comma)
                        string[] fields = line.Split(',');

                        // Check if there are enough fields to create a Patient object
                        if (fields.Length >= 5)
                        {
                            try
                            {
                                //Parse the fields & create a new Patient object
                                int id = int.Parse(fields[0].Trim());
                                string Name = fields[1].Trim();
                                string address = fields[2].Trim();
                                string email = fields[3].Trim();
                                string phone = fields[4].Trim();



                                //Create a new patient object
                                Patient patient = new Patient(id, Name, address, email, phone);

                                //add the patient to the list

                                patients.Add(patient);
                            }

                            catch (FormatException ex)
                            {
                                // handle the format exception if parsing fails for any field
                                Console.WriteLine($"Error parsing patient data: {ex.Message}");
                            }


                        }

                    }
                }

            }
            catch (IOException ex)
            {
                // Handle IO exceptions when reading the file
                Console.WriteLine($"Error reading the patient data file: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

        }

        // *** Function to save doctors to the text file ***

        private void SavePatientsToFile()
        {
            //Convert the list of patients to a  list of strings 
            List<string> patientStrings = new List<string>();
            foreach (var patient in patients)
            {
                patientStrings.Add(patient.ToString());
            }

            //Write the list of strings to the file
            //File.WriteAllLines(patientsFilePath, patientStrings);

            using (StreamWriter writer = new StreamWriter(patientsFilePath, true))
            {
                foreach (var patientString in patientStrings)
                {
                    writer.WriteLine(patientString);
                }
            }

        }

        public void BookAppointment(Patient patient, Doctor doctor, string description, DateTime dd)
        {

            AppointmentManagement appointment = new AppointmentManagement(doctor, patient, description,dd);
            patient.AddAppointment(appointment);
        }

    }
}
