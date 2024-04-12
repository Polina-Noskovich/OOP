using HospitalManagementSystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace HospitalManagementSystem.Managers
{
    public class AdministratorManagement 
    {
        private List<Administrator> administrators;
        private  string administratorsFilePath = "D:\\c#\\HospitalManagSystem\\OOP\\Data\\adminList.txt"; //text file for storing administrator data
        public  DoctorManagement doctorManagement;
        public PatientManagement patientManagement;
        public AdministratorManagement()
        {
           administrators = new List<Administrator>();
           doctorManagement = new DoctorManagement();
            patientManagement = new PatientManagement();
        }

        // *** Function for adding new administrator ***

        public void AddAdministrator(Administrator administrator)
        {
            administrator.Id = GenerateUniqueAdminId();


            administrators.Add(administrator);  //add admin to the list

            SaveAdministratorsToFile(); //Save the updated list to the file

            Console.WriteLine("Administrator added successfully!");
        }

        // *** Fuction to list all administrator ***
        public void ListAdministrators()
        {
            foreach (Administrator administrator in administrators)
            {
                Console.WriteLine(administrator.ToString());
            }
        }

        // *** Function to list details of a specific administrator by ID ***

        public void ListAdministratorDetails(int administratorId)
        {
            var administrator = administrators.Find(a => a.Id == administratorId);

            if (administrator != null)
            {
                Console.WriteLine(administrator.ToString());
            }
            else
            {
                Console.WriteLine("Patient not found!");
            }
        }

        // *** Function to generate a unique administrator ID ***
        private int GenerateUniqueAdminId()
        {
            //Find the maximum existing admin ID and add 1
            int maxId = 0;
            foreach (var administrator in administrators)
            {
                if (administrator.Id > maxId)
                {
                    maxId = administrator.Id;
                }

            }
            return maxId + 1;
        }

        /*private void LoadAdministratorsFromFile()
        {
            if (File.Exists(administratorsFilePath))
            {
                // Read the file and create administrator objects
                string[] lines = File.ReadAllLines(administratorsFilePath);
                foreach (string line in lines)
                {
                    //Split the line into fields (assuming fields are separated by a delimiter like a comma)
                    string[] fields = line.Split(',');

                    // Check if there are enough fields to create a Administrator object
                    if (fields.Length >= 9)
                    {
                        try
                        {
                            //Parse the fields & create a new Adminisitrator object
                            int id = int.Parse(fields[0].Trim());
                            string Name = fields[1].Trim();
                            string address = fields[2].Trim();
                            string email = fields[3].Trim();
                            int phone = int.Parse(fields[4].Trim());
                            


                            //Create a new admin object
                            Administrator administrator = new Administrator(id, Name, address, email, phone);

                            //add the admin to the list

                            administrators.Add(administrator);
                        }

                        catch (FormatException ex)
                        {
                            // handle the format exception if parsing fails for any field
                            Console.WriteLine($"Error parsing administrator data: {ex.Message}");
                        }


                    }

                }
            }
        } */

        // *** Function to save doctors to the text file ***

        private void SaveAdministratorsToFile()
        {
            //Convert the list of admins to a  list of strings 
            List<string> adminStrings = new List<string>();
            foreach (var administrator in administrators)
            {
                adminStrings.Add(administrator.ToString());
            }

            //Write the list of strings to the file
            File.WriteAllLines(administratorsFilePath, adminStrings);

        }


        

        public  void ListAllDoctors()
        {
            //DoctorManagement doctorManagement = new DoctorManagement();
            doctorManagement.ListDoctors();
        }

        public void ListAllPatients()
        {
            //PatientManagement patientManagement = new PatientManagement();
            patientManagement.ListPatients();
        }

        public  void CheckDoctorDetails()
        {
            Console.WriteLine("Enter the ID of the doctor whose details you want to check: ");
            if (int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine($"{Id} is the value I have received");
                //DoctorManagement doctorManagement = new DoctorManagement();
                doctorManagement.ListDoctorDetails(Id);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid doctor ID.");
            }

        }
    }
}
