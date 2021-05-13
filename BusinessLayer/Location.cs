using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{

    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         Class for Location attributes and methods
     * Date modified:       7/12/2020
    */

    [Serializable]
    public class Location
    {
        //Initialise Private Attributes
        private int _location_id;
        private String _name;
        private String _address;

        //Constructor for Location with location_id
        public Location(int Location_id)
        {
            _location_id = Location_id;
        }

        //Constructor with all attributes
        public Location(int Location_id, string name, string address)
        {
            _location_id = Location_id;
            _name = name;
            _address = address;
        }

        /// Location Name Getter and Setter
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
                    throw new ArgumentException("Location name can't be blank...");
                }
                else
                {
                    //Update name attribute
                    _name = value;
                }
            }
        }


        /// Location Address Getter and Setter
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                //Check if set value is empty/null
                if (string.IsNullOrWhiteSpace(value))
                {
                    //Throw argument exception 
                    throw new ArgumentException("Location address can't be blank...");
                }
                else
                {
                    //Update name attribute
                    _address = value;
                }
            }
        }

        /// Location Id Getter
        public int Location_id
        {
            get { return _location_id; }
        }

    }
}
