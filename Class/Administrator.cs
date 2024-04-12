﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Classes
{
    public class Administrator
    {
        public int id;
        public string userName;
        public string address;
        private string email;
        private int phone;

        public Administrator(int adminNextId, string adminUserName, string adminAddress, string adminEmail, int adminPhone)
        {
            id = adminNextId;
            userName = adminUserName;
            address = adminAddress;
            email = adminEmail;
            phone = adminPhone;

        }


        // *** ID should be generated by the system on object creation, not chosen/inputted by the user. ***
        public int Id
        {
            get { return id; }

            set
            {
                //Validation: Ensure that the ID is positive(>=0).
                if (value <= 0)
                {
                    throw new ArgumentException("ID must be a positive(>=0) integer");
                }
                id = value;
            }

        }


        public string UserName
        {
            get { return userName; }

            set
            {
                // Validation: Ensure that the first name is not null or empty.
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Username name cannot be empty.");
                }

                userName = value;
            }
        }


        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int Phone
        {
            get { return phone; }
            set { phone = value; }
        }



        public override string ToString()
        {
            //customizing the format of the patient's details.

            return $"Administrator ID: {id}\n" +
                $"Name: {userName}\n" +
                $"Email: {email}\n" +
                $"Phone: {phone}\n" +
                $"Address: {address}";


        }
    }
}
