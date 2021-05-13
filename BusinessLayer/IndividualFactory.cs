using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer
{

    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         Factory Pattern for creation of individual objects
     * Date modified:       7/12/2020
    */

    public class IndividualFactory
    {
        // Decalare private variables
        private int _prevIndividualID = 0;
        private string filePath = "TrackandTraceData\\idData.csv";

        //Location Factory blank constructor  
        public IndividualFactory()
        {
            //Set up location id csv file
            SetupIndividualIDS();
        }


        //Set up individual id
        public void SetupIndividualIDS()
        {
            // check file exists
            if (File.Exists(filePath))
            {
                //Read id file and split by delimiter
                string whole_file = System.IO.File.ReadAllText(filePath);
                string[] splitData = whole_file.Split(',');

                int number = 0;
                bool checkNumber = int.TryParse(splitData[1], out number);
                // verify int has been obtained
                if (checkNumber)
                {
                    //Set private attribute _previousIndividualId
                    _prevIndividualID = number;
                }
            }
            else
            {
                // Create the file
                Directory.CreateDirectory("TrackandTraceData");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                {
                    file.WriteLine("0,0,0");
                }
            }
        }

        /// Update ID file
        public void UpdateIdFile(int IndividualID)
        {
            // Update id file with specified individual id
            string whole_file = System.IO.File.ReadAllText(filePath);
            string[] splitData = whole_file.Split(',');
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.Write($"{splitData[0]},{IndividualID},{splitData[2]}");
            }
        }

        /// Factory method for Individual Class
        public Individual FactoryMethod()
        {
            // Increment individual id
            _prevIndividualID++;
            // Update id file
            UpdateIdFile(_prevIndividualID);
            // Return the new individual object
            return new Individual(_prevIndividualID);
        }
    }
}
