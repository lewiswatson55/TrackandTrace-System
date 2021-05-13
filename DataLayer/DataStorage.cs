using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BusinessLayer;

namespace DataLayer
{

    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         Class for storage and retrieval of data
     * Date modified:       7/12/2020
    */

    public class DataStorage
    {
        //Initialise private attributes
        private List<Location> _locations = new List<Location>();
        private List<Individual> _individual = new List<Individual>();

        private LocationFactory _locationFactory = new LocationFactory();
        private IndividualFactory _individualFactory = new IndividualFactory();
        private EventFactory _eventFactory = new EventFactory();

        private static DataStorage _instance;

        //Data Storage Constructor - loads data from files
        public DataStorage()
        {
            LoadLocations();
            LoadIndividuals();
        }


        //Instance Method
        public static DataStorage Instance
        {
            get
            {
                // If an instance doesn't exist
                if (_instance == null)
                {
                    // Create a new instance of the storage class
                    _instance = new DataStorage();
                }
                // Return the existing instance of the storage class
                return _instance;
            }
        }

        //Getter for list of individuals
        public List<Individual> Individuals
        {
            get { return _individual; }
        }

        //Getter for list of locations
        public List<Location> Locations
        {
            get { return _locations; }
        }

        //Method for logging new contact event
        public Contact_Event LogContact(int contact_id, DateTime date_time, int individual_id)
        {
            //Creates new event from factorymethod to ensure ids are correct and counted
            Contact_Event newevent = _eventFactory.factoryMethod_contact(contact_id);
            newevent.Date_time = date_time;

            foreach (Individual ind in _individual)
            {
                if (individual_id == ind.Individual_id)
                {
                    //Add event to individual
                    ind.AddcEvent(newevent);
                }
            }
            return newevent;
        }

        //Method for logging new location event
        public Location_Event LogVisit(int location_id, DateTime date_time, int individual_id)
        {
            //Creates new event from factorymethod to ensure ids are correct and counted
            Location_Event newevent = _eventFactory.factoryMethod_location(location_id);
            newevent.Date_time = date_time;

            foreach (Individual ind in _individual)
            {
                if (individual_id == ind.Individual_id)
                {
                    //Add location to individual
                    ind.AddlEvent(newevent);
                }
            }
            return newevent;
        }

        //Method for storing individuals to file
        public void StoreIndividuals()
        {
            //Define output data file
            string individualsFilePath = "TrackandTraceData\\individualsData.csv";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(individualsFilePath))
            {
                //Loop through all individuals
                foreach (Individual ind in _individual)
                {
                    //Create start of line to be written
                    string line = $"{ind.Individual_id},{ind.Name},{ind.Telephone}";

                    //Format contact event and append to line
                    foreach (Contact_Event contactevents in ind.contactEvents) {
                        line = line + $",{contactevents.Event_id};{contactevents.Date_time};{contactevents.Contact_id};1";
                    }

                    //FOrmat location event and append to line
                    foreach (Location_Event locationevents in ind.locationEvents) {
                        line = line + $",{locationevents.Event_id};{locationevents.Date_time};{locationevents.Location_id};0";
                    }
                    file.WriteLine(line);
                }

            }
        }

        //Method for loading stoed individuals from csv file
        public void LoadIndividuals()
        {
            //Define input data file
            string individualsFilePath = "TrackandTraceData\\individualsData.csv";

            //Check input file exists
            if (File.Exists(individualsFilePath))
            {

                //Read Whole file into string variable then split into array of lines
                string whole_file = System.IO.File.ReadAllText(individualsFilePath);
                string[] lines = whole_file.Split('\r');

                //Loop through each line
                foreach (string temp in lines)
                {
                    //Split said line into the individuals class data
                    string[] i_classdata = temp.Split(',');

                    if (i_classdata[0]=="\n" || i_classdata[0] == "") { break; }

                    //Parse individuals individual_id and create temp individual instance
                    int id = int.Parse(i_classdata[0]);
                    Individual tmpind = new Individual(id, i_classdata[1], i_classdata[2]);

                    //Check if individual has stored events
                    if (i_classdata.Length > 3)
                    {
                        //Loop through all events
                        for (int i = 3; i < i_classdata.Length; i++) { 
                            
                            //Console.WriteLine($"{i_classdata[i]}"); //For Debugging

                            //Split event string into array
                            string[] e_classdata = i_classdata[i].Split(';');

                            //Check event type - 0 == Location, else (ie ==1) Contact Event
                            if (int.Parse(e_classdata[3]) == 0)
                            {
                                //Get locationEvents from tmp individual, create new event, add to list of events, update event list, add individual to individual list
                                List<Location_Event> tmp_event_list = tmpind.locationEvents;
                                Location_Event current_event = new Location_Event(int.Parse(e_classdata[0]), DateTime.Parse(e_classdata[1]), int.Parse(e_classdata[2]));
                                tmp_event_list.Add(current_event);
                                tmpind.locationEvents = tmp_event_list;
                            }
                            else
                            {
                                //Get contactEvents from tmp individual, create new event, add to list of events, update event list.
                                List<Contact_Event> tmp_event_list = tmpind.contactEvents;
                                Contact_Event current_event = new Contact_Event(int.Parse(e_classdata[0]), DateTime.Parse(e_classdata[1]), int.Parse(e_classdata[2]));
                                tmp_event_list.Add(current_event);
                                tmpind.contactEvents = tmp_event_list;
                            }
                        }
                        //Add Individual to list
                        _individual.Add(tmpind);
                    } else
                    {
                        //Add Individual to list if there are no event classes
                        _individual.Add(tmpind);
                    }
                }

            } 
            //If Input file/dir doesnt exist, create blank input file/dir
            else
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(individualsFilePath)) { }
            }
        }

        //Method for storing locations
        public void StoreLocations(List<Location> _locations)
        {
            string locationFilePath = "TrackandTraceData\\locationData.csv";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(locationFilePath))
            {
                _locations.ForEach(delegate (Location loc) {
                    file.WriteLine($"{loc.Location_id},{loc.Name},{loc.Address}");
                });


            }
        }

        //method for loading locations from file if file exists and creating one if it doesnt
        public void LoadLocations()
        {
            string locationFilePath = "TrackandTraceData\\locationData.csv";
            if (File.Exists(locationFilePath))
            {
                //Read file and store as whole, then split by delimiter
                string whole_file = System.IO.File.ReadAllText(locationFilePath);
                string[] lines = whole_file.Split('\r');

                //get input for location constructor from line and create class
                foreach (string temp in lines)
                {

                    string[] classdata = temp.Split(',');
                    
                    //check if line is blank and break if so - as EOF
                    if (classdata[0] == "\n" || classdata[0] == "") { break; }

                    int id = int.Parse(classdata[0]);
                    Location tmploc = new Location(id, classdata[1], classdata[2]);
                    _locations.Add(tmploc);
                }
            } else
            {
                //Create file
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(locationFilePath)) { }
            }
        }

        //Method to create new loaction using location factorymethod - updates list and writes to file
        public Location NewLocation(string name, string address)
        {
            //Create location using location factory
            Location loc = _locationFactory.FactoryMethod();
            //Set location attributes to input
            loc.Name = name;
            loc.Address = address;

            //Save location object
            _locations.Add(loc);
            StoreLocations(_locations);
            return loc;
        }

        //Method to create new individual using individual factorymethod - updates list and writes to file
        public Individual NewIndividual(string name, string telephone)
        {
            Individual ind = _individualFactory.FactoryMethod();
            ind.Name = name;
            ind.Telephone = telephone;

            //Save Individual Object and update file
            _individual.Add(ind);
            StoreIndividuals();
            return ind;
        }

        //Method for querying an individuals contacts between a date range - returns as list
        public List<Contact_Event> QueryContacts(DateTime s_date_time, DateTime e_date_time, int individual_id)
        {
            List<Contact_Event> Events_Contact = new List<Contact_Event>();

            //Check each individual stored
            foreach (Individual ind in _individual)
            {
                //If individual is target 
                if (ind.Individual_id == individual_id)
                {
                    //Loop through all contact events
                    foreach (Contact_Event currentevent in ind.contactEvents)
                    {
                        //Add event to list if between specified date range
                        if (currentevent._date_time >= s_date_time && currentevent._date_time <= e_date_time)
                        {
                            Events_Contact.Add(currentevent);
                        }
                    }
                }
            }
            //Return log of contacts
            return Events_Contact;
        }

        //Method for querying an locations contacts between a date range - returns as list
        public List<Location_Event> QueryLocation(DateTime s_date_time, DateTime e_date_time, int location_id)
        {
            List<Location_Event> Events_Location= new List<Location_Event>();

            //Check each individual stored
            foreach (Individual ind in _individual)
            {
                //Loop through all of the individuals location events
                foreach (Location_Event currentevent in ind.locationEvents)
                {
                    //Check if between date range and location is target - add to event list
                    if (currentevent._date_time >= s_date_time && currentevent._date_time <= e_date_time && currentevent.Location_id == location_id)
                    {
                        Events_Location.Add(currentevent);
                    }
                }
            }
            //Return log of contacts
            return Events_Location;
        }

    }
}
