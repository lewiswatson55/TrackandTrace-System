using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{

    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         Class for Individual attributes and methods
     * Date modified:       7/12/2020
    */

    public class Individual
    {

        //Initialise Private Attributes
        private int _individual_id;
        private String _name;
        private String _telephone;
        private List<Location_Event> _locationevents = new List<Location_Event>();
        private List<Contact_Event> _contactevents = new List<Contact_Event>();


        //Constructor for individual class with id as paramater
        public Individual(int Individual_id)
        {
            _individual_id = Individual_id;
        }

        //Constructor for individual class with required attributes as parameters
        public Individual(int Individual_id, string Name, string Telephone)
        {
            _individual_id = Individual_id;
            _name = Name;
            _telephone = Telephone;
        }


        /// Individual Name Getter and Setter
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                //Check if set value is empty/null
                if (string.IsNullOrWhiteSpace(value))
                {
                    //Throw argument exception 
                    throw new ArgumentException("Individuals name can't be blank...");
                }
                else
                {
                    //Update name attribute
                    _name = value;
                }
            }
        }

        // Individual Telephone Getter and Setter
        public string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                //Check if set value is empty/null
                if (string.IsNullOrWhiteSpace(value))
                {
                    //Throw argument exception 
                    throw new ArgumentException("A telephone number is required...");
                }
                else
                {
                    //Update telephone attribute
                    _telephone = value;
                }
            }
        }

        //Getter for _individual_id
        public int Individual_id
        {
            get
            {
                return _individual_id;
            }
        }

        //Getter and Setter for individuals list of contact events
        public List<Contact_Event> contactEvents
        {
            get { return _contactevents; }
            set
            {
                // If the value is null
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Individual does not have any recorded events");
                }
                else
                {
                    // Set the customer bookings to the value
                    _contactevents = value;
                }
            }
        }

        //Getter and Setter for individuals list of location events
        public List<Location_Event> locationEvents
        {
            get { return _locationevents; }
            set
            {
                // If the value is null
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Individual does not have any recorded events");
                }
                else
                {
                    // Set the customer bookings to the value
                    _locationevents = value;
                }
            }
        }

        //Method for adding contact event to _contactevent list
        public void AddcEvent(Contact_Event newevent)
        {
            _contactevents.Add(newevent);
        }

        //Method for adding location event to _locationevent list
        public void AddlEvent(Location_Event newevent)
        {
            _locationevents.Add(newevent);
        }

    }
}
