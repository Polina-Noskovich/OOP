using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using HospitalManagementSystem.Classes;
using HospitalManagementSystem.Managers;

namespace HospitalManagementSystem
{
    class Program
    {
        private static readonly string credentialsFilePath = "D:\\c#\\HospitalManagSystem\\OOP\\Data\\credentials.txt"; //path to the file storing user credentials
        //bool isAuthenticated = false;
        //string userRole = null;
        static string userIdforCheck;
        static void Main()
        {
            bool isAuthenticated = false;
            string userRole = null;
            Console.Clear();

            // Define the dimensions of the box
            int boxWidth = 50;
            int boxHeight = 10;

            // Calculate the width of the horizontal lines
            int horizontalLineWidth = boxWidth - 6; // Adjust for the title length

            // Create the top border
            Console.WriteLine(new string('=', boxWidth));

            // Set the cursor position for the title
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((boxWidth - "DOTNET Hospital Management System".Length) / 2, 1);
            Console.WriteLine("DOTNET Hospital Management System");
            Console.ForegroundColor = ConsoleColor.White;

            // Create the horizontal line beneath the title
            Console.SetCursorPosition(3, 2);
            Console.WriteLine(new string('=', horizontalLineWidth));

            Console.ForegroundColor = ConsoleColor.Blue;
            // Set the cursor position for the "Login" text
            Console.SetCursorPosition((boxWidth - "Login".Length) / 2, 4);
            Console.WriteLine("Login");

            Console.ForegroundColor = ConsoleColor.White;
            // Create the left and right vertical lines
            for (int i = 3; i <= boxHeight - 2; i++)
            {
                Console.SetCursorPosition(3, i);
                Console.Write("|");
                Console.SetCursorPosition(boxWidth - 4, i);
                Console.Write("|");
            }

            Console.ForegroundColor = ConsoleColor.White;
            // Create the bottom border
            Console.SetCursorPosition(0, boxHeight - 1);
            Console.WriteLine(new string('=', boxWidth));



            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("     Welcome to the Hospital Management System");
            Console.ForegroundColor = ConsoleColor.White;


            AdministratorManagement administratorManagement = new AdministratorManagement();
            DoctorManagement doctorManagement = new DoctorManagement();
            PatientManagement patientManagement = new PatientManagement();
            

            while (!isAuthenticated)
            {
                Console.WriteLine("\nlogin Menu");
                Console.Write("ID: ");
                string userId = Console.ReadLine();
                userIdforCheck = userId;

                Console.Write("Enter Password: "); 
                string password = ReadPassword();

                // Validate user credentials and determine their role
                userRole = ValidateCredentials(userId, password);

                if(userRole != null)
                {
                    isAuthenticated = true;
                    Console.WriteLine("\nLogin Successful!");
                }

                else
                {
                    Console.WriteLine("\nInvalid credentials. Please try again.");
                }

            }

            // Navigate to the appropriate menu based on the user's role
            switch (userRole)
            {
             

                case "Doctor":
                    RunDoctorMenu(patientManagement, doctorManagement, userIdforCheck);
                    break;

                case "Admin":
                    RunAdministratorMenu(doctorManagement, administratorManagement, patientManagement, ref isAuthenticated);
                    break;
                case "Patient":
                    patientManagement.LoadPatientsFromFile();
                    int id = int.Parse(userIdforCheck);
                    var patient = patientManagement.patients.Find(p => p.Id == id);
                    RunPatientMenu(patient, patientManagement, doctorManagement,  userIdforCheck);
                    break;


            }


        }

        // *** Function to read password securely ***
        private static string ReadPassword()
        {
            string password = ""; //Initialize an empty string to store the password
            ConsoleKeyInfo key; // declare a variable to store each key press

            do
            {
                key = Console.ReadKey(true); // Read a key from the console without displaying it

                //Check if the key pressed is a printable character or a symbol

                if(char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar) )
                {
                    password += key.KeyChar; // Append the character to the password string
                    Console.Write("*"); // Display an asterisk (*) on the screen to mask the character
                }

                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); // Erase the last character by moving the cursor back, clearing the character, and moving the cursor back again 
                }

            }

            while (key.Key != ConsoleKey.Enter); // Continue reading keys until the Enter key is pressed

            Console.WriteLine(); // Print a newline to move to the next line after the user presses Enter

            return password; // Return the entered password 
        }

        // *** Function to valiadate user credentials *** 
        private static string ValidateCredentials(string userId,  string password)
        {
            try
            {
                //Read user credentials from the credentials file 
                string[] lines = File.ReadAllLines(credentialsFilePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    // Check if the line has enough parts
                    if (parts.Length == 3)
                    {
                        string storedUserId = parts[0];
                        string storedPassword = parts[1];
                        string userRole = parts[2];

                        // Check if the entered userId and password match the stored credentials
                        if(userId == storedUserId && password == storedPassword)
                        {
                            return userRole; // Return the user's role if credentials are valid 
                        }
                    }
                }
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The credentials file '{credentialsFilePath}' was not found.");
            }

            catch (IOException ex)
            {
                Console.WriteLine($"Error reading the credentials file: {ex.Message}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            // Return null if an error occurred or if credentials were not found 
            return null;
        }

        //AdministratorManagement administratorManagement = new AdministratorManagement();
       

        public static void RunAdministratorMenu(DoctorManagement doctorManagement, AdministratorManagement administratorManagement, PatientManagement patientManagement, ref bool isAuthenticated)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to DOTNET Hospital Management System userName");
                Console.WriteLine();
                Console.WriteLine("1. List all doctors");
                Console.WriteLine("2. Check doctor details");
                Console.WriteLine("3. List all patients");
                Console.WriteLine("4. Check patient details");
                Console.WriteLine("5. Add doctor");
                Console.WriteLine("6. Add patient");
                Console.WriteLine("7. Delete doctor");
                Console.WriteLine("8. Delete patient");
                Console.WriteLine("9. Logout");
                Console.WriteLine("10. Exit");


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("All doctors registered to the DOTNET Hospital Management System: ");
                        doctorManagement.ListDoctors();
                        break;
                    case "2":
                        Console.WriteLine("Enter the ID of the doctor whose details you want to check: ");
                        if (int.TryParse(Console.ReadLine(), out int Id))
                        {
                            Console.WriteLine($"{Id} is the value I have received");
                            doctorManagement.ListDoctorDetails(Id);
                            Console.WriteLine("Please Enter to return to the menu");
                            Console.ReadLine(); // Wait for user input before returning to the menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid doctor ID.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("list of all patients...");
                        // Implement logic for listing all patients
                        patientManagement.ListPatients();
                        Console.WriteLine("Please Enter to return to the menu");
                        Console.ReadLine();

                        break;
                    case "4":
                        Console.WriteLine("Enter the ID of the patient whose details you want to check: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine($"{id} is the value I have received");
                            patientManagement.ListPatientDetails(id);
                            Console.WriteLine("Please Enter to return to the menu");
                            Console.ReadLine(); // Wait for user input before returning to the menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID.");
                        }

                        break;
                    case "5":
                        Console.WriteLine("Enter the details of the new doctor:");
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Phone: ");
                        string phone = Console.ReadLine();
                        Console.Write("Street Number: ");
                        if (int.TryParse(Console.ReadLine(), out int streetNumber))
                        {
                            Console.Write("Street Name: ");
                            string streetName = Console.ReadLine();
                            Console.Write("City: ");
                            string city = Console.ReadLine();

                            // Create a new Doctor object with the entered details
                            int uniqueID = doctorManagement.doctors.Count()+1;
                            Doctor newDoctor = new Doctor(uniqueID, firstName, lastName, email, phone, streetNumber, streetName, city);

                            // Add the new doctor to the DoctorManagement class
                            doctorManagement.AddDoctor(newDoctor);

                            //Console.WriteLine("Doctor added successfully!");
                            Console.ReadKey();
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid street number.");
                        }
                        break;
                    case "6":
                        Console.WriteLine("Enter the details of the new patient:");
                        Console.Write("Full Name: ");
                        string fullName = Console.ReadLine();
                        Console.Write("Address: ");
                        string address = Console.ReadLine();
                        Console.Write("Email: ");
                        string Email = Console.ReadLine();
                        Console.Write("Phone: ");
                        string Phone = Console.ReadLine();
                        // Create a new Patient object with the entered details
                        int uniqueiD = patientManagement.patients.Count() + 1;
                        Patient newPatient = new Patient(uniqueiD, fullName, address, Email, Phone);

                        // Add the new patient to the PatientManagement class
                        patientManagement.AddPatient(newPatient);

                        //Console.WriteLine("Patient added successfully!");
                        Console.ReadKey();
                        break;
                    case "7":
                        // Удаление врача
                        Console.WriteLine("Enter the ID of the doctor you want to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int doctorId))
                        {
                            if (doctorManagement.DeleteDoctor(doctorId))
                            {
                                //Console.WriteLine("Doctor deleted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Doctor with specified ID not found!");
                            }
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid doctor ID.");
                        }
                        break;
                    case "8":
                        // Удаление пациента
                        Console.WriteLine("Enter the ID of the patient you want to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int patientId))
                        {
                            if (patientManagement.DeletePatient(patientId))
                            {
                                //Console.WriteLine("Patient deleted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Patient with specified ID not found!");
                            }
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid patient ID.");
                        }
                        break;
                    case "9":
                        isAuthenticated = false;
                        running = false; //Exit the administrator menu
                        Main();
                        break;
                    case "10":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void RunDoctorMenu(PatientManagement patientManagement, DoctorManagement doctorManagement, String userId)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("\nDoctor Menu");
                Console.WriteLine("1. View Patient List");
                Console.WriteLine("2. View Appointments");
                Console.WriteLine("3. Update Patient Records");
                Console.WriteLine("4. Logout");

                Console.Write("Enter your choice (1-4): ");
                string choice = Console.ReadLine();
                string Description1 = "Pain in the heart";

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("list of all patients...");
                        // Implement logic for listing all patients
                        patientManagement.ListPatients();
                        Console.WriteLine("Please Enter to return to the menu");
                        Console.ReadLine();
                        //Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("List of all appointments:");
                        // Implement logic to list all appointments here
                        Console.WriteLine($"Doctor ID: 1\n" +
                        $"Name: John Doe\n" +
                        $"Doctor: Jane Smith\n" +
                        $"Description: {Description1}\n" +
                        $"Appointment dateTime: 20.06.2024 15:00\n");
                        Console.WriteLine("Please press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.WriteLine("Updating patient records");
                        Console.WriteLine("What do you want to update? 1 -> Description, 2 -> Appointment dateTime");
                        string choice1 = Console.ReadLine();
                        if(choice1 == "1") { Console.WriteLine("Enter a new description:");
                            Description1 = Console.ReadLine();
                            Console.WriteLine("Description successfully updated!");
                        }
                        else if(choice1 == "2") { Console.WriteLine("Enter a new date and time (in format MM/dd/yyyy HH:mm:ss):");
                            string choice2 = Console.ReadLine();
                            Console.WriteLine("Date and time successfully updated!");
                        }
                        // Implement logic for updating patient records
                        //Console.WriteLine("Description successfully updated!");
                        Console.ReadKey();
                        break;
                    case "4":
                        running = false; // Exit the doctor menu
                        Main();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void RunPatientMenu(Patient patient, PatientManagement patientManagement, DoctorManagement doctorManagement, String userId)
        {

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Patient Menu");
                Console.WriteLine();
                Console.WriteLine("1. List patient details");
                Console.WriteLine("2. List my doctor details");
                Console.WriteLine("3. List all appointments");
                Console.WriteLine("4. Book appointment");
                Console.WriteLine("5. Exit to login");
                Console.WriteLine("6. Exit System");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Your patient details:");
                        // Implement logic to display patient details here
                        
                        if(patient != null)
                        {
                            //Print the patient details
                            Console.WriteLine("Patient Details:");
                            Console.WriteLine(patient.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Patient not found!");
                        }

                        Console.WriteLine("Please press any key to return to the menu.");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("Your doctor details:");
                        Console.WriteLine("You are not registered with any doctor! Please choose a doctor to register with:");
                        doctorManagement.ListDoctors();
                        Console.WriteLine("Enter the ID of the doctor whose details you want to check: ");
                        if (int.TryParse(Console.ReadLine(), out int Id))
                        {
                            Console.WriteLine($"{Id} is the value I have received");
                            doctorManagement.ListDoctorDetails(Id);
                            Console.WriteLine("Please Enter to return to the menu");
                            Console.ReadLine(); // Wait for user input before returning to the menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid doctor ID.");
                        }
                        // Implement logic to display patient's doctor details here
                        //Console.WriteLine("Please press any key to return to the menu.");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.WriteLine("List of all appointments:");
                        // Implement logic to list all appointments here
                        Console.WriteLine($"Doctor ID: 1\n" +
                        $"Name: John Doe\n" +
                        $"Doctor: Jane Smith\n" +
                        $"Description: Pain in the heart\n" +
                        $"Appointment: 20.06.2024 15:00\n");
                        Console.WriteLine("Please press any key to return to the menu.");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.WriteLine("Booking an appointment:");
                        // Implement logic to book an appointment here
                        if(patient.appointments.Count == 0)
                        {
                            Console.WriteLine("You are not registered with any doctor! Please choose a doctor to register with:");
                            doctorManagement.ListDoctors();
                            Console.WriteLine("Please choose a doctor ID:");
                            int DoctorId = int.Parse(Console.ReadLine());
                            Doctor myDoctor = doctorManagement.GetDoctorById(DoctorId);
                            RegistrationManagement myDoctors = new RegistrationManagement(myDoctor, patient);
                            myDoctors.RegisterWithDoctor(myDoctor, patient, patient.appointments); //registering with doctor
                            patient.AddRegistration(myDoctors);
                            Console.WriteLine("Successfully Registered with the doctor!");
                            Console.WriteLine("Please Write the description of visit!");
                            Console.Write("Description:");
                            string description = Console.ReadLine();
                            GenerationRecipe(description);
                            DateTime dateTime;
                            Console.WriteLine("Enter a date and time (in format MM/dd/yyyy HH:mm:ss) for visit: ");
                            string input = Console.ReadLine();

                            if (DateTime.TryParseExact(input, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                            {
                                Console.WriteLine($"You entered: {dateTime}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date and time format. Please enter date and time in MM/dd/yyyy HH:mm:ss format.");
                            }

                            AppointmentManagement newAppointment = new AppointmentManagement(myDoctor, patient, description, dateTime);
                            patient.appointments.Add(newAppointment);
                            Console.WriteLine("           ");
                            Console.WriteLine($"You are booking a new appointment with {myDoctor.LastName}");
                            Console.WriteLine($"Description of appointment {description}");
                            Console.WriteLine("The appointment has been booked successfully");
                            Console.WriteLine("           ");
                            var timeReminder = new Managers.TimeReminder();
                            timeReminder.Init(dateTime);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("You will receive a notification an hour before the appointment");
                            Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else
                        {
                            Console.WriteLine("Available Doctors: ");
                            doctorManagement.ListDoctors();
                            Console.WriteLine("Please choose a doctor ID:");
                            int DoctorId = int.Parse(Console.ReadLine());
                            Doctor myDoctor = doctorManagement.GetDoctorById(DoctorId);
                            Console.WriteLine("Please Write the description of visit!");
                            Console.Write("Description:");
                            string description = Console.ReadLine();
                            GenerationRecipe(description);
                            DateTime date;
                            Console.WriteLine("Enter a date and time (in format MM/dd/yyyy HH:mm:ss) for visit: ");
                            string input = Console.ReadLine();

                            if (DateTime.TryParseExact(input, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                            {
                                Console.WriteLine($"You entered: {date}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date and time format. Please enter date and time in MM/dd/yyyy HH:mm:ss format.");
                            }

                            GenerationRecipe(description);
                            AppointmentManagement newAppointment = new AppointmentManagement(myDoctor, patient, description, date);
                            patient.appointments.Add(newAppointment);
                            var timeReminder = new Managers.TimeReminder();
                            timeReminder.Init(date);
                            Console.WriteLine($"You are booking a new appointment with {myDoctor.LastName}");
                            Console.WriteLine($"Description of appointment {description}");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The appointment has been booked successfully");

                        }

                        Console.WriteLine("Appointment booked successfully!");
                        Console.WriteLine("Please press any key to return to the menu.");
                        Console.ReadKey();
                        break;

                    case "5":
                        running = false; // Exit to the login screen
                        Main();
                        break;

                    case "6":
                        Environment.Exit(0); // Exit the entire system
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void GenerationRecipe(string description)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Prescription for medicines");
            if (description == "headache")
            {
                Console.WriteLine("Paracetamol, Ibuprofen, Aspirin");
            }
            else if (description == "nausea")
            {
                Console.WriteLine("Dimenhydrinate, Metoclopramide, Promethazine");
            }
            else if (description == "toothache")
            {
                Console.WriteLine("Ibuprofen, Paracetamol, Acetaminophen + Codeine");
            }
            Console.ForegroundColor = ConsoleColor.White;

        }





    }
}
