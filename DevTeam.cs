using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsuranceApp
{
    public class DevTeam
    {
        public string TeamName { get; set; }
        public int TeamID { get; set; }
        public List<Developer> TeamMembers { get; set; }

        public DevTeam() { }
        public DevTeam(string teamName)
        {
            TeamName = teamName;
            TeamMembers = new List<Developer>();
        }
        public DevTeam(string teamName, List<Developer> teamMembers)
        {
            TeamName = teamName;

            if(teamMembers is null)
            {
                TeamMembers = new List<Developer>();
            }
            else
            {
                TeamMembers = teamMembers;
            }
        }

        public bool AddTeamMember(Developer newDev)
        {
            if(newDev is null)
            {
                return false;
            }

            // Prevent duplicates
            foreach(Developer dev in TeamMembers)
            {
                if(dev.ID == newDev.ID)
                {
                    return false;
                }
            }

            TeamMembers.Add(newDev);
            return true;
        }

        public bool RemoveTeamMember(Developer removedDev)
        {
            if (removedDev is null)
            {
                return false;
            }

            // Prevent duplicates
            foreach (Developer dev in TeamMembers)
            {
                if (dev.ID == removedDev.ID)
                {
                    TeamMembers.Remove(removedDev);
                    return true;
                }
            }

            return false;
        }
    }
}
