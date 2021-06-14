using System;
using System.IO;
using System.Collections.Generic;

namespace NameSorter
{
    class MainClass
    {
        #region Variables
        static List<string> listOfSortedNames = new List<string>();
        static List<string> listOfNames = new List<string>();
        List<string> listOfLastNames = new List<string>();
        List<string> listOfGivenNames = new List<string>();
        List<string> sortedListFirstName = new List<string>();
        List<string> sortedListLastName = new List<string>();
        List<string> lastNameAtFrontList = new List<string>();
        string space = " ";
        #endregion

        public static void Main(string[] args)
        {
            MainClass myClass = new MainClass();
            string[] names = File.ReadAllLines("./unsorted-names-list.txt");

            Console.WriteLine("Names from text file\n");

            // Adding names to listOfNames list
            foreach (string name in names)
            {
                Console.WriteLine("\t" + name);
                if (name != null) listOfNames.Add(name);

            }

            myClass.GetLastNamesAndSort();
            myClass.ConcatenateSortedLastName();
            myClass.WriteIntoTxtFile();

        }

        public void GetLastNamesAndSort()
        {
            foreach (string name in listOfNames)
            {
                // Checking spaces on names in the list.
                if (name.Contains(space))
                {
                    // Separating last name from given names.
                    // Starting from the end of the string, get the first " " and create a substring from it.
                    string lastName = name.Substring(name.LastIndexOf(" ") + 1);
                    listOfLastNames.Add(lastName);

                    // Everything before the last " ".
                    string givenNames = name.Substring(0, name.LastIndexOf(" "));
                    listOfGivenNames.Add(givenNames);

                    //concactunate last name and given names
                    lastName = $"{lastName} {givenNames}";

                    lastNameAtFrontList.Add(lastName);

                    //sorting from the first object in the lastNameAtFrontList which is the lastName
                    lastNameAtFrontList.Sort();

                }
            }
        }

        public void ConcatenateSortedLastName()
        {
            Console.WriteLine("\n\nSorted Names\n");

            // Reversing
            foreach (string name in lastNameAtFrontList)
            {
                // Checking spaces on names in the list to get given and last name.
                if (name.Contains(space))
                {
                    string lastName = name.Substring(0, name.IndexOf(" "));
                    sortedListLastName.Add(lastName);
                    string givenNames = name.Substring(name.IndexOf(" ") + 1);
                    sortedListFirstName.Add(givenNames);

                    string sortedNames = $"{givenNames} {lastName}";
                    Console.WriteLine("\t" + sortedNames);

                    //adding to a new list so it can be written on the txt file
                    listOfSortedNames.Add(sortedNames);
                }
            }
        }

        public void WriteIntoTxtFile()
        {
            // Writing/Ovewriting to txt file
            string sortedList_FileName = @"./sorted-names-list.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(sortedList_FileName))
                {
                    foreach (string sortedName in listOfSortedNames) writer.Write(sortedName + "\n");
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
        }
    }
}
