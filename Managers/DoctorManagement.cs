using HospitalManagementSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace HospitalManagementSystem.Managers
{
    public class DoctorManagement
    {
        public List<Doctor> doctors;
        public string doctorsFilePath = "D:\\c#\\HospitalManagSystem\\OOP\\Data\\doctorList.txt"; //text file for storing doctor data

        //private int nextId = 1; //// Declare nextId as a static variable

        public DoctorManagement()
        {
            doctors = new List<Doctor>();
            LoadDoctorsFromFile(); // Load doctors from file when the class is initialized. 
        }

        // *** Function for adding new doctor ***

        public void AddDoctor(Doctor doctor)
        {
            //doctor.Id = GenerateUniqueDoctorId();


            doctors.Add(doctor);  //add doctor to the list

            SaveDoctorsToFile(); //Save the updated list to the file

            Console.WriteLine("Doctor added successfully!");
        }

        public bool DeleteDoctor(int doctorId)
        {
            Doctor doctorToRemove = doctors.FirstOrDefault(d => d.Id == doctorId);
            if (doctorToRemove != null)
            {
                doctors.Remove(doctorToRemove);
                SaveDoctorsToFile(); // Save the updated list to the file after deletion
                Console.WriteLine("Doctor deleted successfully!");
                return true;
            }
            else
            {
                Console.WriteLine("Doctor with specified ID not found!");
                return false;
            }
        }
      



        // *** Fuction to list all doctors ***
        public void ListDoctors()
        {
            Console.Clear();
            Console.WriteLine("List of All Doctors:");
            if(doctors.Count == 0)
            {
                Console.WriteLine("No doctors found.");
            }

            else
            {
                foreach (Doctor doctor in doctors)
                {
                    Console.WriteLine(doctor.ToString());
                }
            }
            Console.WriteLine("\nPress Enter to return to the menu...");
            Console.ReadLine(); // Wait for user input before returning to the menu
        }

        // *** Function to list details of a specific doctor by ID ***

        public void ListDoctorDetails(int doctorId)
        {
            Console.WriteLine($"Searching for doctor with ID {doctorId}...");
            var doctor = doctors.Find(d => d.Id == doctorId);

            if(doctor != null)
            {
                Console.WriteLine(doctor.ToString());
            }
            else
            {
                Console.WriteLine("Doctor not found!");
            }
        }

        // *** Function to generate a unique doctor ID ***
        public int GenerateUniqueDoctorId()
        {
            //Find the maximum existing doctor ID and add 1
            int maxId = 0;
            foreach (var doctor in doctors)
            {
                if (doctor.Id > maxId)
                { 
                    maxId = doctor.Id;
                }

            }
            return maxId + 1;
        }

        public void LoadDoctorsFromFile()
        {
            if(File.Exists(doctorsFilePath))
            {
                // Read the file and create doctor objects
                string[] lines = File.ReadAllLines(doctorsFilePath);
                foreach (string line in lines)
                {
                    //Split the line into fields (assuming fields are separated by a delimiter like a comma)
                    string[] fields = line.Split(',');

                    // Check if there are enough fields to create a Doctor object
                    if(fields.Length >= 8)
                    {
                        try
                        {
                            //Parse the fields & create a new Doctor object
                            string firstName = fields[0].Trim();
                            string lastName = fields[1].Trim();
                            string email = fields[2].Trim();
                            string phone = fields[3].Trim();
                            int streetNumber = int.Parse(fields[4].Trim());
                            string streetName = fields[5].Trim();
                            string city = fields[6].Trim();
                            int id = int.Parse(fields[7].Trim());


                            //Create a new doctor object
                            Doctor doctor = new Doctor(id, firstName, lastName, email, phone, streetNumber, streetName, city);

                            //add the doctor to the list

                            doctors.Add(doctor);
                            /*if(id>=doctor.)
                            {
                                nextId = id + 1;
                            }*/
                        }

                        catch (FormatException ex)
                        {
                            // handle the format exception if parsing fails for any field
                            Console.WriteLine($"Error parsing doctor data: {ex.Message}");
                        }


                    }

                }
            }
        }

        // *** Function to save doctors to the text file ***

        public void SaveDoctorsToFile()
        {
            //Convert the list of doctors to a  list of strings 
            List<string> doctorStrings = new List<string>();
            foreach (var doctor in doctors)
            {
                doctorStrings.Add(doctor.ToString());
            }

            //Write the list of strings to the file
           // File.WriteAllLines(doctorsFilePath, doctorStrings);

            using (StreamWriter writer = new StreamWriter(doctorsFilePath, true))
            {
                foreach (var doctorString in doctorStrings)
                {
                    writer.WriteLine(doctorString);
                }
            }


        }

        public Doctor GetDoctorById(int doctorId)
        {
            return doctors.FirstOrDefault(d => d.Id == doctorId);
        }

    }
}
