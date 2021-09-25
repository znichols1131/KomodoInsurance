using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsuranceApp
{
    public class DevTeamRepo
    {
        private List<DevTeam> _listOfDevTeams = new List<DevTeam>();
        private int _nextTeamID = 0;

        // CRUD

        // Create
        public bool CreateDevTeam(DevTeam team)
        {
            if(team is null)
            {
                return false;
            }

            // Assign unique ID
            team.TeamID = _nextTeamID;
            _nextTeamID++;

            int before = _listOfDevTeams.Count;
            _listOfDevTeams.Add(team);

            int after = _listOfDevTeams.Count;
            if(after > before)
            {
                return true;
            }

            return false;
        }


        // Read
        public List<DevTeam> GetAllDevTeams()
        {
            return _listOfDevTeams;
        }

        public DevTeam GetDevTeamForID(int id)
        {
            foreach(DevTeam team in _listOfDevTeams)
            {
                if(team.TeamID == id)
                {
                    return team;
                }
            }

            return null;
        }


        // Update
        public bool UpdateDevTeamForID(int id, DevTeam newTeam)
        {
            if(newTeam is null)
            {
                return false;
            }

            DevTeam oldTeam = GetDevTeamForID(id);
            if(oldTeam is null)
            {
                return false;
            }

            oldTeam.TeamID = newTeam.TeamID;
            oldTeam.TeamName = newTeam.TeamName;
            oldTeam.TeamMembers = newTeam.TeamMembers;

            return true;
        }

        public void UpdateDevTeamsContainingDeletedDeveloper(Developer developer)
        {
            // Use this method to remove any instances of developer from Dev Teams
            foreach(DevTeam team in _listOfDevTeams)
            {
                team.RemoveTeamMember(developer);
            }
        }

        // Delete
        public bool DeleteDevTeam(DevTeam team)
        {
            if(team is null)
            {
                return false;
            }

            int before = _listOfDevTeams.Count;
            _listOfDevTeams.Remove(team);

            int after = _listOfDevTeams.Count;
            if(after < before)
            {
                return true;
            }

            return false;
        }


        // Helper methods (if any)
    }
}
