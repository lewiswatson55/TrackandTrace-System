using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer
{

    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         Factory Pattern for creation of location objects
     * Date modified:       7/12/2020
    */

    public class EventFactory
    {
        // Declare private variables
        private int _prevEventID = 0;
        private string filePath = "TrackandTraceData\\idData.csv";

        // Event blank constructor  
        public EventFactory()
        {
            //Set up location id csv file
            SetupEventIDS();
        }


        //Set up event id
        public void SetupEventIDS()
        {
            // check file exists
            if (File.Exists(filePath))
            {
                //Read file and split by delimiter
                string whole_file = System.IO.File.ReadAllText(filePath);
                string[] splitData = whole_file.Split(',');

                int number = 0;
                bool checkNumber = int.TryParse(splitData[2], out number);
                // verify int has been obtained
                if (checkNumber)
                {
                    //Set private attribute _previousEventId
                    _prevEventID = number;
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
        public void UpdateIdFile(int EventID)
        {
            // Update id file with specified event id
            string whole_file = System.IO.File.ReadAllText(filePath);
            string[] splitData = whole_file.Split(',');
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.Write($"{splitData[0]},{splitData[1]},{EventID}");
            }
        }

        /// Factory method for Event Class with type - type 0 = location event, type 1 = contact event
        public Event FactoryMethod(int type, int contact_id)
        {
            // Increment individual id
            _prevEventID++;
            // Update id file
            UpdateIdFile(_prevEventID);
            // Return the new event object

            if (type==0) { return new Location_Event(_prevEventID, contact_id); } else
            {
                return new Contact_Event(_prevEventID, contact_id);
            }
            
        }

        //Factory Method for contact event
        public Contact_Event factoryMethod_contact(int contact_id)
        {
            // Increment individual id
            _prevEventID++;
            // Update id file
            UpdateIdFile(_prevEventID);
            // Return the new event object
            return new Contact_Event(_prevEventID, contact_id);
        }

        //Factory Method for location event
        public Location_Event factoryMethod_location(int location_id)
        {
            // Increment individual id
            _prevEventID++;
            // Update id file
            UpdateIdFile(_prevEventID);
            // Return the new event object
            return new Location_Event(_prevEventID, location_id);
        }

    }
}
