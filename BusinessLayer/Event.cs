using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{

    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         Class for Event attributes and methods
     * Date modified:       7/12/2020
    */

    public abstract class Event
    {
        // Initialise private attributes
        public DateTime _date_time;
        public int _event_id;

        public Event(int event_id)
        {
            _event_id = event_id;
        }

        /// Event date_time Getter and Setter
        public DateTime Date_time
        {
            get
            {
                return _date_time;
            }
            set
            {
                // Update date_time private attribute
                _date_time = value;
            }
        }

        //Getter for event_id
        public int Event_id
        {
            get
            {
                return _event_id;
            }
        }

    }

    //Class for location event with inheritance
    public class Location_Event : Event
    {

        //Initialise private location id attribute
        private int _location_id;

        //Location event constructor
        public Location_Event(int event_id) : base(event_id) { }

        //Location event constructor with event_id and location_id
        public Location_Event(int event_id, int Location_id) : base(event_id) 
        {
            _location_id = Location_id;
        }


        //Constructor for location event with all attributes as constructor parameters
        public Location_Event(int Event_id, DateTime Date_time, int Location_id) : base(Event_id)
        {
            _event_id = Event_id;
            _location_id = Location_id;
            _date_time = Date_time;
        }

        //Getter and Setter for _location_id
        public int Location_id
        {
            get { return _location_id; }
            set
            {
                //Update name attribute
                _location_id = value;
            }
        }


    }

    //Class for Contact_Events with inheritance
    public class Contact_Event : Event
    {

        //Initialise private location id attribute
        private int _contact_id;

        //Location event constructor
        public Contact_Event(int event_id) : base(event_id) { }

        //Location event constructor with event and contact ids
        public Contact_Event(int event_id, int Contact_id) : base(event_id)
        {
            _contact_id = Contact_id;
        }

        //Location event constructor with all attributes as constructor parameters
        public Contact_Event(int Event_id, DateTime Date_time, int Contact_id) : base(Event_id)
        {
            _event_id = Event_id;
            _contact_id = Contact_id;
            _date_time = Date_time;
        }

        //Getter and setter for _contact_id
        public int Contact_id
        {
            get { return _contact_id; }
            set
            {
                //Update name attribute
                _contact_id = value;
            }
        }


    }

}
