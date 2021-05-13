using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         Factory Pattern for creation of location objects
     * Date modified:       7/12/2020
    */

    public class LocationFactory
    {
        // Decalare private variables
        private int _prevLocationID = 0;
        private string filePath = "TrackandTraceData\\idData.csv";

        //Location Factory blank constructor  
        public LocationFactory()
        {
            //Set up location id csv file
            SetupLocationIDS();
        }


        //make sure ids are up to date
        public void SetupLocationIDS()
        {
            // check file exists
            if (File.Exists(filePath))
            {

                //Read in id file and split by delimiter
                string whole_file = System.IO.File.ReadAllText(filePath);
                string[] splitData = whole_file.Split(',');

                int number = 0;
                bool checkNumber = int.TryParse(splitData[0], out number);
                // verify int has been obtained
                if (checkNumber)
                {
                    //Set private attribute _previousLocationId
                    _prevLocationID = number;
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

        /// Update id file
        public void UpdateIdFile(int LocationID)
        {
            // Update id file with specified location id
            string whole_file = System.IO.File.ReadAllText(filePath);
            string[] splitData = whole_file.Split(',');
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.Write($"{LocationID},{splitData[1]},{splitData[2]}");
            }
        }

        /// Factory method for Location Class
        public Location FactoryMethod()
        {
            // Increment location id
            _prevLocationID++;
            // Update id file
            UpdateIdFile(_prevLocationID);
            // Return the new location object
            return new Location(_prevLocationID);
        }
    }
}
