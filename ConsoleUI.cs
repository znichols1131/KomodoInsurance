using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsuranceApp
{
    public class ConsoleUI
    {
        // Repositories

        private DeveloperRepo _devRepo = new DeveloperRepo();
        private DevTeamRepo _devTeamRepo = new DevTeamRepo();

        private string _dashedLine = "--------------------";


        // Start up

        public void Run()
        {
            // Call this method to generate dev teams and developers for testing
            Populate();

            Run_MainMenu();
        }

        public void Populate()
        {
            Developer dev1 = new Developer("Han Solo", true);
            Developer dev2 = new Developer("Chewbacca", true);
            Developer dev3 = new Developer("Luke Skywalker", false);
            Developer dev4 = new Developer("Darth Vader", true);
            Developer dev5 = new Developer("Stormtrooper", false);
            Developer dev6 = new Developer("R2D2", true);
            Developer dev7 = new Developer("C3PO", true);
            Developer dev8 = new Developer("Obi Wan Kenobi", false);
            Developer dev9 = new Developer("Leia Organa", false);

            _devRepo.CreateDeveloper(dev1);
            _devRepo.CreateDeveloper(dev2);
            _devRepo.CreateDeveloper(dev3);
            _devRepo.CreateDeveloper(dev4);
            _devRepo.CreateDeveloper(dev5);
            _devRepo.CreateDeveloper(dev6);
            _devRepo.CreateDeveloper(dev7);
            _devRepo.CreateDeveloper(dev8);
            _devRepo.CreateDeveloper(dev9);

            DevTeam rebels = new DevTeam("Rebels");
            rebels.AddTeamMember(dev1);
            rebels.AddTeamMember(dev2);
            rebels.AddTeamMember(dev3);
            rebels.AddTeamMember(dev6);
            rebels.AddTeamMember(dev7);
            rebels.AddTeamMember(dev9);
            _devTeamRepo.CreateDevTeam(rebels);

            DevTeam empire = new DevTeam("Empire");
            empire.AddTeamMember(dev4);
            empire.AddTeamMember(dev5);
            _devTeamRepo.CreateDevTeam(empire);

            DevTeam falcon = new DevTeam("Millenium Falcon");
            falcon.AddTeamMember(dev1);
            falcon.AddTeamMember(dev2);
            _devTeamRepo.CreateDevTeam(falcon);

            DevTeam forceUsers = new DevTeam("Force Users");
            forceUsers.AddTeamMember(dev3);
            forceUsers.AddTeamMember(dev4);
            forceUsers.AddTeamMember(dev8);
            _devTeamRepo.CreateDevTeam(forceUsers);

        }

        public void Run_MainMenu()
        {
            bool keepRunning = true;
            while(keepRunning)
            {
                Console.Clear();

                PrintTitle();

                Console.WriteLine("\n\nWelcome to the Komodo Insurance Developer Team Management program. " +
                    "What would you like to do?\n\n" +
                    "1. Manage developer teams.\n" +
                    "2. Manage developers.\n" +
                    "3. Manage access to Pluralsight.\n" +
                    "4. Quit.\n");
                string response = Console.ReadLine();

                switch(response)
                {
                    case "1":
                        // Manage developer teams
                        Run_DevTeamMenu();
                        break;
                    case "2":
                        // Manage developers
                        Run_DeveloperMenu();
                        break;
                    case "3":
                        // Manage access to Pluralsight
                        Run_PluralsightMenu();
                        break;
                    case "4":
                        // Exit program
                        Environment.Exit(0);
                        break;
                    case "":
                        break;
                    default:
                        // Unrecognized input
                        InputNotRecognized(response);
                        break;
                }
            }
        }



        // Dev Team Menu

        public void Run_DevTeamMenu()
        {
            bool keepLooping = true;
            while(keepLooping)
            {
                Console.Clear();
                Console.WriteLine("Current Developer Teams:\n\n" +
                    $"{_dashedLine}\n");

                PrintAllDevTeams();

                Console.WriteLine($"\n{_dashedLine}\n\n" +
                    "What would you like to do?\n\n" +
                    "1. Create a new developer team.\n" +
                    "2. Manage an existing developer team.\n" +
                    "3. Remove a developer team.\n" +
                    "4. Return to main menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Create a DevTeam
                        CreateDevTeam();
                        break;
                    case "2":
                        // Manage a DevTeam
                        ManageDevTeam_Selection();
                        break;
                    case "3":
                        // Remove a DevTeam
                        RemoveDevTeam();
                        break;
                    case "4":
                        // Return to main menu
                        return;
                    case "":
                        break;
                    default:
                        InputNotRecognized(response);
                        break;
                }
            }
        }

        public void CreateDevTeam()
        {
            // Get team name
            Console.Clear();
            Console.WriteLine("Developer Team Creation Tool:\n\n" +
                "Enter a team name: ");
            string teamName = Console.ReadLine();
            if(teamName is null || teamName == "")
            {
                Console.WriteLine("Team name is not valid. Press any key to continue.");
                Console.ReadLine();
                return;
            }

            // Assign developers
            List<Developer> devs = SelectDevelopersWithPrompt($"Which developers would you like to add to {teamName}?", null);

            // Create team
            DevTeam newTeam = new DevTeam(teamName, devs);

            // Add team to repo
            bool success = _devTeamRepo.CreateDevTeam(newTeam);

            if(!success)
            {
                Console.WriteLine($"The '{newTeam.TeamName}' was not able to be created. " +
                    $"Please press any key to return to the previous menu.");
            }
        }

        public void ManageDevTeam_Selection()
        {
            bool keepLooping = true;
            while(keepLooping)
            {
                Console.Clear();
                Console.WriteLine("Developer Team Management Tool:\n\n" +
                    $"{_dashedLine}\n");

                PrintAllDevTeams();

                Console.WriteLine($"\n{_dashedLine}\n\n" +
                    "Which team would you like to manage? " +
                    "Press enter if you would like to return to the previous menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "":
                        // Return to previous menu
                        return;
                    default:
                        // Find that team, manage it
                        response.Trim();

                        try
                        {
                            int id = int.Parse(response);
                            DevTeam team = _devTeamRepo.GetDevTeamForID(id);

                            if(team is null)
                            {
                                Console.WriteLine($"\nID '{response}' could not be found. Please press any key to continue.");
                                Console.ReadLine();
                                return;
                            }

                            // Manage that specific dev team
                            ManageDevTeam_Specific(team);

                        }
                        catch
                        {
                            Console.WriteLine($"\nID '{response}' could not be found. Please press any key to continue.");
                            Console.ReadLine();
                            return;
                        }
                        break;
                }
            }
        }

        public void ManageDevTeam_Specific(DevTeam team)
        {
            string genericError = "Development team could not be updated. Press any key to continue.";

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                Console.WriteLine("Developer Team Management Tool:\n\n" +
                    $"Team ID: {team.TeamID}\n" +
                    $"Team Name: {team.TeamName}" +
                    "Developers:\n\n" +
                    $"{_dashedLine}\n");

                PrintDevelopersForDevTeam(team);

                Console.WriteLine($"\n{_dashedLine}\n\n" +
                    "What would you like to do?\n" +
                    "1. Change team name.\n" +
                    "2. Add developers.\n" +
                    "3. Remove developers.\n" +
                    "4. Return to previous menu.\n");
                string response = Console.ReadLine();

                DevTeam newTeam = new DevTeam();

                switch (response)
                {
                    case "1":
                        // Change team name
                        Console.WriteLine("\nEnter new team name: ");
                        string newName = Console.ReadLine();

                        if (newName is null || newName == "")
                        {
                            Console.WriteLine("\nTeam name is not valid. Press any key to continue.");
                            Console.ReadLine();
                            return;
                        }
                        else
                        {
                            newTeam.TeamName = newName;
                            newTeam.TeamID = team.TeamID;
                            newTeam.TeamMembers = team.TeamMembers;
                        }

                        break;
                    case "2":
                        // Add developers
                        List<Developer> devsToAdd = SelectDevelopersWithPrompt($"Which developers would you like to add to {team.TeamName}?", null);
                        if (devsToAdd is null || devsToAdd.Count == 0)
                        {
                            Console.WriteLine("\n"+ genericError);
                            Console.ReadLine();
                            return;
                        }
                        else
                        {
                            newTeam.TeamName = team.TeamName;
                            newTeam.TeamID = team.TeamID;
                            newTeam.TeamMembers = team.TeamMembers;
                            foreach(Developer newDev in devsToAdd)
                            {
                                bool successfulAdd = newTeam.AddTeamMember(newDev);
                                if (!successfulAdd)
                                {
                                    Console.WriteLine($"\nWe're sorry, {newDev.Name} could not be added. Press any key to continue.");
                                    Console.ReadLine();
                                }
                            }
                        }
                        break;
                    case "3":
                        // Remove developers
                        List<Developer> devsToDelete = SelectDevelopersWithPrompt($"Which developers would you like to remove from {team.TeamName}?", team);
                        if (devsToDelete is null || devsToDelete.Count == 0)
                        {
                            Console.WriteLine("\n" + genericError);
                            Console.ReadLine();
                            return;
                        }
                        else
                        {
                            newTeam.TeamName = team.TeamName;
                            newTeam.TeamID = team.TeamID;
                            newTeam.TeamMembers = team.TeamMembers;
                            foreach (Developer deletedDev in devsToDelete)
                            {
                                bool successfulDelete = newTeam.RemoveTeamMember(deletedDev);
                                if(!successfulDelete)
                                {
                                    Console.WriteLine($"\nWe're sorry, {deletedDev.Name} could not be removed. Press any key to continue.");
                                    Console.ReadLine();
                                }
                            }
                        }
                        break;
                    case "4":
                        // Return to previous menu
                        return;
                    case "":
                        break;
                    default:
                        InputNotRecognized(response);
                        return;
                }



                // Update team
                bool success = _devTeamRepo.UpdateDevTeamForID(team.TeamID, newTeam);

                if(!success)
                {
                    Console.WriteLine("\nWe're sorry, this Developer Team could not be updated. " +
                        "Press any key to continue.");
                    Console.ReadLine();
                }

            }
        }

        public void RemoveDevTeam()
        {
            // Get team name
            Console.Clear();
            Console.WriteLine("Developer Team Deletion Tool:\n\n" +
                "Which teams would you like to delete?\n\n" +
                $"{_dashedLine}\n");

            PrintAllDevTeams();

            Console.WriteLine("\n\nEnter their IDs separated by commas. " +
                "If you do not wish to remove any teams at this time, press enter.");
            string response = Console.ReadLine();

            if(response is null || response == "")
            {
                return;
            }

            List<DevTeam> teamsToDelete = ParseStringForDevTeams(response);

            if (teamsToDelete is null || teamsToDelete.Count == 0)
            {
                Console.WriteLine("\nWe're sorry, these teams could not be deleted. Press any key to continue.\n");
                Console.ReadLine();
                return;
            }
            else
            {
                foreach(DevTeam team in teamsToDelete)
                {
                    bool successfulDelete = _devTeamRepo.DeleteDevTeam(team);
                    if (!successfulDelete)
                    {
                        Console.WriteLine($"\nWe're sorry, {team.TeamName} could not be removed. Press any key to continue.");
                        Console.ReadLine();
                    }
                }
            }
        }





        // Developer menu

        public void Run_DeveloperMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                Console.WriteLine("Current Developers:\n\n" +
                    $"{_dashedLine}\n");

                PrintAllDevelopers();

                Console.WriteLine($"\n{_dashedLine}\n\n" +
                    "What would you like to do?\n\n" +
                    "1. Add a new developer.\n" +
                    "2. Update an existing developer.\n" +
                    "3. Remove a developer.\n" +
                    "4. Return to main menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Add a dev
                        CreateDev();
                        break;
                    case "2":
                        // Update a dev
                        UpdateDev_Select();
                        break;
                    case "3":
                        // Remove a dev
                        RemoveDev();
                        break;
                    case "4":
                        // Return to main menu
                        return;
                    case "":
                        break;
                    default:
                        InputNotRecognized(response);
                        break;
                }
            }
        }

        public void CreateDev()
        {
            // Get team name
            Console.Clear();
            Console.WriteLine("Developer Addition Tool:\n\n" +
                "Enter the developer's name: ");
            string name = Console.ReadLine();
            if (name is null || name == "")
            {
                Console.WriteLine("Developer name is not valid. Press any key to continue.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nDoes this developer have access to Pluralsight? (Y/N)");
            string accessResponse = Console.ReadLine();
            bool pluralsightAccess = false; // default
            if(accessResponse.ToLower().Trim() == "y" || accessResponse.ToLower().Trim() == "yes")
            {
                pluralsightAccess = true;
            }

            // Create developer
            Developer newDev = new Developer(name, pluralsightAccess);

            // Add team to repo
            bool success = _devRepo.CreateDeveloper(newDev);

            if (!success)
            {
                Console.WriteLine($"The '{newDev.Name}' was not able to be created. " +
                    $"Please press any key to return to the previous menu.");
            }
        }

        public void UpdateDev_Select()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                Console.WriteLine("Developer Update Tool:\n\n" +
                    $"{_dashedLine}\n");

                PrintAllDevelopers();

                Console.WriteLine($"\n{_dashedLine}\n\n" +
                    "Which developer would you like to update? " +
                    "Press enter if you would like to return to the previous menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "":
                        // Return to previous menu
                        return;
                    default:
                        // Find that dev, update it
                        response.Trim();

                        try
                        {
                            int id = int.Parse(response);
                            Developer dev = _devRepo.GetDeveloperForID(id);

                            if (dev is null)
                            {
                                Console.WriteLine($"\nID '{response}' could not be found. Please press any key to continue.");
                                Console.ReadLine();
                                return;
                            }

                            // Update that specific dev
                            UpdateDev_Specific(dev);

                        }
                        catch
                        {
                            Console.WriteLine($"\nID '{response}' could not be found. Please press any key to continue.");
                            Console.ReadLine();
                            return;
                        }
                        break;
                }
            }
        }

        public void UpdateDev_Specific(Developer dev)
        {
            string genericError = "Developer could not be updated. Press any key to continue.";

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                string accessStr = "NO";
                if(dev.AccessToPluralsight)
                {
                    accessStr = "YES";
                }

                Console.Write("Developer Update Tool:\n\n" +
                    $"Developer ID: {dev.ID}\n" +
                    $"Developer Name: {dev.Name}\n" +
                    $"Has Access to Pluralsight: ");
                Console.ForegroundColor = ConsoleColorForBool(dev.AccessToPluralsight);
                Console.WriteLine($"{accessStr}\n\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"What would you like to do?\n" +
                    "1. Change developer name.\n" +
                    "2. Toggle Pluralsight access.\n" +
                    "3. Return to previous menu.\n");
                string response = Console.ReadLine();

                Developer newDev = new Developer();

                switch (response)
                {
                    case "1":
                        // Change name
                        Console.WriteLine("\nEnter new name: ");
                        string newName = Console.ReadLine();

                        if (newName is null || newName == "")
                        {
                            Console.WriteLine("\nDeveloper name is not valid. Press any key to continue.");
                            Console.ReadLine();
                            return;
                        }
                        else
                        {
                            newDev.Name = newName;
                            newDev.ID = dev.ID;
                            newDev.AccessToPluralsight = dev.AccessToPluralsight;
                        }

                        break;
                    case "2":
                        // Update Pluralsight access
                        newDev.Name = dev.Name;
                        newDev.ID = dev.ID;
                        newDev.AccessToPluralsight = !dev.AccessToPluralsight; // Toggle

                        break;                    
                    case "3":
                        // Return to previous menu
                        return;
                    case "":
                        break;
                    default:
                        InputNotRecognized(response);
                        return;
                }



                // Update team
                bool success = _devRepo.UpdateDeveloperForID(dev.ID, newDev);

                if (!success)
                {
                    Console.WriteLine("\nWe're sorry, this Developer could not be updated. " +
                        "Press any key to continue.");
                    Console.ReadLine();
                }

            }
        }

        public void RemoveDev()
        {
            Console.Clear();
            Console.WriteLine("Developer Deletion Tool:\n\n" +
                "Which developers would you like to delete?\n\n" +
                $"{_dashedLine}\n");

            PrintAllDevelopers();

            Console.WriteLine("\n\nEnter their IDs separated by commas. " +
                "If you do not wish to remove developers at this time, press enter.");
            string response = Console.ReadLine();

            if (response is null || response == "")
            {
                return;
            }

            List<Developer> devsToDelete = ParseStringForDevelopers(response);

            if (devsToDelete is null || devsToDelete.Count == 0)
            {
                Console.WriteLine("\nWe're sorry, these developers could not be deleted. Press any key to continue.\n");
                Console.ReadLine();
                return;
            }
            else
            {
                foreach (Developer dev in devsToDelete)
                {
                    _devTeamRepo.UpdateDevTeamsContainingDeletedDeveloper(dev);
                    bool successfulDelete = _devRepo.DeleteDeveloper(dev);
                    if (!successfulDelete)
                    {
                        Console.WriteLine($"\nWe're sorry, {dev.Name} could not be removed. Press any key to continue.");
                        Console.ReadLine();
                    }
                }
            }
        }
        


        // Pluralsight program

        public void Run_PluralsightMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                Console.WriteLine("The following developers still need access to Pluralsight:\n\n" +
                    $"{_dashedLine}\n");

                List<Developer> devs = _devRepo.GetAllDevelopersWithPluralSightAccess(false);
                PrintDevelopersInList(devs);

                Console.WriteLine($"\n{_dashedLine}\n\n" +
                    "What would you like to do?\n\n" +
                    "1. Grant all listed developers access to Pluralsight.\n" +
                    "2. Return to main menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Grant Pluralsight access
                        if (devs == null || devs.Count == 0)
                        {
                            Console.WriteLine("\nAll developers already have access to Pluralsight access. " +
                                "Press any key to continue.");
                            Console.ReadLine();
                        }
                        else
                        {
                            AssignPluralsightAccessToDevelopers(true, devs);
                        }

                        break;
                    case "2":
                        // Return to main menu
                        return;
                    case "":
                        break;
                    default:
                        InputNotRecognized(response);
                        break;
                }
            }
        }

        public void AssignPluralsightAccessToDevelopers(bool access, List<Developer> devs)
        {
            foreach (Developer dev in devs)
            {
                Developer newDev = new Developer(dev.Name, true);
                newDev.ID = dev.ID;
                _devRepo.UpdateDeveloperForID(dev.ID, newDev);
            }
        }


        // Helper methods
        public void InputNotRecognized(string input)
        {
            Console.WriteLine($"\nInput '{input}' not recognized. Please try again. Press any key to continue.");
            Console.ReadLine();
        }

        public void PrintAllDevTeams()
        {
            List<DevTeam> teams = _devTeamRepo.GetAllDevTeams();
            if (teams is null || teams.Count == 0)
            {
                Console.WriteLine("There are no existing developer teams at this time.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-30}{2,-10}",
                    "Team ID",
                    "Team Name",
                    "Team Members");

                foreach (DevTeam team in teams)
                {
                    int teamMemberCount = 0;
                    if (team.TeamMembers != null)
                    {
                        teamMemberCount = team.TeamMembers.Count;
                    }

                    Console.WriteLine("{0,-10}{1,-30}{2,-10}",
                    $"{team.TeamID}",
                    $"{team.TeamName}",
                    $"{teamMemberCount} members");
                }
            }
        }

        public void PrintAllDevelopers()
        {
            List<Developer> devs = _devRepo.GetAllDevelopers();
            if (devs is null || devs.Count == 0)
            {
                Console.WriteLine("There are no existing developers at this time.");
            }
            else
            {
                PrintDevelopersInList(devs);
            }
        }

        public void PrintDevelopersInList(List<Developer>devs)
        {
            if (devs is null || devs.Count == 0)
            {
                Console.WriteLine("There are no existing developers at this time.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-30}{2,-10}",
                    "Dev. ID",
                    "Developer Name",
                    "Pluralsight Access");

                foreach (Developer dev in devs)
                {
                    string pluralsightAccessStr = "NO";
                    if (dev.AccessToPluralsight)
                    {
                        pluralsightAccessStr = "YES";
                    }

                    Console.Write("{0,-10}{1,-30}",
                        $"{dev.ID}",
                        $"{dev.Name}");
                    Console.ForegroundColor = ConsoleColorForBool(dev.AccessToPluralsight);
                    Console.WriteLine("{0,-10}", pluralsightAccessStr);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void PrintDevelopersForDevTeam(DevTeam team)
        {
            if (team.TeamMembers is null || team.TeamMembers.Count == 0)
            {
                Console.WriteLine("There are no developers assigned to this team.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-30}",
                    "Dev. ID",
                    "Developer Name");

                foreach (Developer dev in team.TeamMembers)
                {
                    Console.WriteLine("{0,-10}{1,-30}",
                        $"{dev.ID}",
                        $"{dev.Name}");
                }
            }
        }

        public List<Developer> ParseStringForDevelopers(string input)
        {
            List<Developer> developers = new List<Developer>();
            
            // Split at every comma
            string[] separatedInputs = input.Split(',');
            foreach(string idString in separatedInputs)
            {
                // Remove any spaces
                idString.Trim();

                try
                {
                    int idNum = int.Parse(idString);
                    Developer dev = _devRepo.GetDeveloperForID(idNum);
                    if(dev != null)
                    {
                        developers.Add(dev);
                    }
                }
                catch
                {

                }
            }

            return developers;
        }

        public List<DevTeam> ParseStringForDevTeams(string input)
        {
            List<DevTeam> teams = new List<DevTeam>();

            // Split at every comma
            string[] separatedInputs = input.Split(',');
            foreach (string idString in separatedInputs)
            {
                // Remove any spaces
                idString.Trim();

                try
                {
                    int idNum = int.Parse(idString);
                    DevTeam team = _devTeamRepo.GetDevTeamForID(idNum);
                    if (team != null)
                    {
                        teams.Add(team);
                    }
                }
                catch
                {

                }
            }

            return teams;
        }

        public List<Developer> SelectDevelopersWithPrompt(string prompt, DevTeam team)
        {
            if (prompt is null || prompt == "")
            {
                return null;
            }

            // Assign developers
            Console.Clear();
            Console.WriteLine(prompt +
                $"\n\n{_dashedLine}\n");

            // Show list of developers
            if(team is null)
            {
                PrintAllDevelopers();
            }
            else
            {
                PrintDevelopersForDevTeam(team);
            }

            Console.WriteLine($"\n{_dashedLine}\n\n" +
                "Enter their IDs separated by commas. " +
                "If you do not wish to add developers at this time, press enter.");
            string devString = Console.ReadLine();

            // Create new dev team
            if (devString is null || devString == "")
            {
                return null;
            }
            else
            {
                // Get all developers by splitting on commas and removing whitespace              
                return ParseStringForDevelopers(devString);
            }
        }

        public ConsoleColor ConsoleColorForBool(bool input)
        {
            if(input)
            {
                return ConsoleColor.Green;
            }

            return ConsoleColor.Red;
        }

        public void PrintTitle()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" __  ___   ______    .___  ___.   ______    _______   ______   ");
            Console.WriteLine("|  |/  /  /  __  \\   |   \\/   |  /  __  \\  |       \\ /  __  \\  ");
            Console.WriteLine("|  '  /  |  |  |  |  |  \\  /  | |  |  |  | |  .--.  |  |  |  | ");
            Console.WriteLine("|     <  |  |  |  |  |  |\\/|  | |  |  |  | |  |  |  |  |  |  |");
            Console.WriteLine("|  .   \\ |  `--'  |  |  |  |  | |  `--'  | |  '--'  |  `--'  | ");
            Console.WriteLine("|__|\\ __\\ \\______/   |__|  |__|  \\______/  |_______/ \\______/");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
