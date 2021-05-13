using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer;

namespace DataLayer
{

    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         DataFacade for interfacing between storage and the business layer
     * Date modified:       7/12/2020
    */

    public class DataFacade
    {
        private DataStorage _dataStorage;

        //Constructor for datafacade
        public DataFacade()
        {
            // Return the singleton instance of the data storage class
            _dataStorage = DataStorage.Instance;
        }

        //Method for creating new location and returns as object
        public Location NewLocation(string name, string address)
        {
            Location loc = _dataStorage.NewLocation(name, address);
            return loc;
        }

        //Method for creating new individual and returns as object
        public Individual NewIndividual(string name, string telephone)
        {
            Individual ind = _dataStorage.NewIndividual(name, telephone);
            return ind;
        }

        //Method for logging a individual contact event and returns as object
        public Contact_Event LogContact(int contact_id, DateTime date_time, int individual_id)
        {
            Contact_Event newevent = _dataStorage.LogContact(contact_id, date_time, individual_id);
            _dataStorage.StoreIndividuals();
            return newevent;
        }

        //Method for logging a location-individual contact event and returns object
        public Location_Event LogVisit(int location_id, DateTime date_time, int individual_id)
        {
            Location_Event newevent = _dataStorage.LogVisit(location_id, date_time, individual_id);
            _dataStorage.StoreIndividuals();
            return newevent;
        }

        //Method for returning list of formatted lines from query response - contact
        public List<string> QueryContacts(DateTime s_date_time, DateTime e_date_time, int individual_id)
        {
            List<Contact_Event> events = _dataStorage.QueryContacts(s_date_time, e_date_time, individual_id);
            List<string> contactResults = new List<string>();
            foreach (Contact_Event currentevent in events)
            {
                contactResults.Add($"Individual Contact Event Found!\nEvent ID: {currentevent.Event_id}\nDate/Time: {currentevent.Date_time}\nContact with Individual ID: {currentevent.Contact_id} ({GetiName(currentevent.Contact_id)}  - Telephone: {GetiTel(currentevent.Contact_id)})\n\n");
            }
            return contactResults;
        }

        //Method for returning list of formatted lines from query response - location
        public List<string> QueryLocation(DateTime s_date_time, DateTime e_date_time, int location_id)
        {
            List<Location_Event> events = _dataStorage.QueryLocation(s_date_time, e_date_time, location_id);
            List<string> locationResults = new List<string>();
            foreach (Location_Event currentevent in events)
            {
                int individual_id = GetiID(currentevent.Event_id);
                locationResults.Add($"Location Contact Event Found!\nEvent ID: {currentevent.Event_id}\nDate/Time: {currentevent.Date_time}\nContact with Individual ID: {individual_id} ({GetiName(individual_id)} - Telephone: {GetiTel(individual_id)})\n\n");
            }
            return locationResults;
        }

        //Method for returning Name of individual from individual id
        public string GetiName(int individual_id)
        {
            //string contactname;
            foreach (Individual ind in _dataStorage.Individuals)
            {
                if (ind.Individual_id == individual_id)
                {
                    string contactname = ind.Name;
                    return contactname;
                }
            }
            return null;
        }

        //Method for returning telephone number of individual from individual id
        public string GetiTel(int individual_id)
        {
            //string contactname;
            foreach (Individual ind in _dataStorage.Individuals)
            {
                if (ind.Individual_id == individual_id)
                {
                    string tel = ind.Telephone;
                    return tel;
                }
            }
            return null;
        }

        //Method for returning individual's id from associated event id
        public int GetiID(int event_id)
        {
            //string contactname;
            foreach (Individual ind in _dataStorage.Individuals)
            {
                foreach (Location_Event currentevent in ind.locationEvents)
                {
                    if (currentevent.Event_id == event_id)
                    {
                        int contactid = ind.Individual_id;
                        return contactid;
                    }
                }
            }
            return -1;
        }

        //Method for returning individuals list from data storage class
        public List<Individual> GetInds()
        {
            return _dataStorage.Individuals;
        }

        //Method for returning location list from datastorage class
        public List<Location> GetLocs()
        {
            return _dataStorage.Locations;
        }

        //Method for getting location name from location id
        public string GetlName(int location_id)
        {
            //string contactname;
            foreach (Location loc in _dataStorage.Locations)
            {
                if (loc.Location_id == location_id)
                {
                    string locationname = loc.Name;
                    return locationname;
                }
            }
            return null;
        }
    }
}
