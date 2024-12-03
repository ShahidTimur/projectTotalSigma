using System;
using System.Collections.Generic;
using System.IO; // Added for file and directory operations
using System.Text.RegularExpressions;

namespace BookJurnalLibrary
{
    public static class Customer // Definition of the Customer class
    {
        static List<string> customerIds = new List<string>(); // Creating a list of customers
        static string dataBase = @"A:\LibraryManagmentAppWPF\DataBase"; // Path to the database folder
        static string customerDirectory = @"A:\LibraryManagmentAppWPF\DataBase\Customers"; // Path to the customer data folder

        public static void AddCustomerToClub(string customerId) // Method to add a new customer to the list
        {
            IsIdValid(customerId); // Validate ID before adding
            DoesIdExistInClub(customerId); // Check if ID already exists
            customerIds.Add(customerId);
            SaveCustomer(customerId); // Save customer after adding
        }

        public static void DoesIdExistInClub(string customerId) // Check for customer's existence in the list
        {
            if (customerIds.Contains(customerId))
            {
                throw new IllegalIdException("This ID already exists in the club!");
            }
        }

        public static void ActivateDiscount(string customerId) // Method to activate discount
        {
            CheckForCustomerExistence(customerId);
            // Logic to activate discount can be added here
        }

        public static void RemoveCustomerFromClub(string customerId) // Method to remove a customer from the club
        {
            CheckForCustomerExistence(customerId);
            customerIds.Remove(customerId);
            DeleteFile(customerId); // Remove customer's data from storage
        }

        public static void IsIdValid(string customerId) // Validate customer ID format
        {
            string idPattern = @"^\d{9}$";
            Regex regex = new Regex(idPattern);
            if (!regex.IsMatch(customerId))
                throw new IllegalIdException("ID must be exactly 9 digits!");
        }

        public static void SaveCustomer(string customerId) // Method to save customer data in the database
        {
            CheckForDbExistence();

            if (!Directory.Exists(customerDirectory))
                throw new DirectoryNotFoundException("Customers directory could not be found!");

            string directory = Path.Combine(customerDirectory, "Customer " + customerId);
            string fileName = "CustomerId.txt";
            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, fileName);
            File.WriteAllText(filePath, customerId);
        }

        public static void LoadCustomers() // Method to load customers from the database
        {
            CheckForDbExistence();

            if (Directory.Exists(customerDirectory))
            {
                foreach (string filePath in Directory.GetFiles(customerDirectory, "CustomerId.txt", SearchOption.AllDirectories))
                {
                    string customerId = File.ReadAllText(filePath);
                    AddCustomerToClub(customerId);
                }
            }
            else throw new DirectoryNotFoundException("Customers directory could not be found!");
        }

        public static void DeleteFile(string customerId) // Remove folder with customer's information
        {
            CheckForDbExistence();

            string directory = Path.Combine(customerDirectory, "Customer " + customerId);
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);
            else throw new DirectoryNotFoundException("The directory could not be found!");
        }

        public static int GetCustomersCount() // Get the number of existing customers
        {
            return customerIds.Count;
        }

        public static void CheckForDbExistence() // Check for database existence
        {
            if (!Directory.Exists(dataBase))
                throw new DirectoryNotFoundException("Database directory could not be found!");
        }

        public static void CheckForCustomerExistence(string customerId) // Check for customer's existence in the list
        {
            if (!customerIds.Contains(customerId))
            {
                throw new IllegalIdException("This ID does not exist in the club!");
            }
        }
    }
}