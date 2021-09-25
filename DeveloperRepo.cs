using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsuranceApp
{
    public class DeveloperRepo
    {
        private List<Developer> _listOfDevs = new List<Developer>();
        private int _nextDevID = 0;

        // CRUD

        // Create
        public bool CreateDeveloper(Developer dev)
        {
            if(dev is null)
            {
                return false;
            }

            // Assign unique ID
            dev.ID = _nextDevID;
            _nextDevID++;
            
            int before = _listOfDevs.Count;
            _listOfDevs.Add(dev);

            int after = _listOfDevs.Count;
            if(after > before)
            {
                return true;
            }

            return false;
        }


        // Read
        public List<Developer> GetAllDevelopers()
        {
            return _listOfDevs;
        }

        public Developer GetDeveloperForID(int id)
        {
            foreach(Developer dev in _listOfDevs)
            {
                if(dev.ID == id)
                {
                    return dev;
                }
            }

            return null;
        }

        public List<Developer> GetAllDevelopersWithPluralSightAccess(bool access)
        {
            List<Developer> devs = new List<Developer>();

            foreach (Developer dev in _listOfDevs)
            {
                if (dev.AccessToPluralsight == access)
                {
                    devs.Add(dev);
                }
            }

            return devs;
        }


        // Update
        public bool UpdateDeveloperForID(int id, Developer newDev)
        {
            if(newDev is null)
            {
                return false;
            }

            Developer oldDev = GetDeveloperForID(id);
            if(oldDev is null)
            {
                return false;
            }

            oldDev.ID = newDev.ID;
            oldDev.Name = newDev.Name;
            oldDev.AccessToPluralsight = newDev.AccessToPluralsight;

            return true;
        }


        // Delete
        public bool DeleteDeveloper(Developer dev)
        {
            if(dev is null)
            {
                return false;
            }

            int before = _listOfDevs.Count;
            _listOfDevs.Remove(dev);

            int after = _listOfDevs.Count;
            if(after < before)
            {
                return true;
            }

            return false;
        }


        // Helper methods (if any)
    }
}
