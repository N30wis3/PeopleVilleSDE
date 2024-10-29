using PeopleVilleEngine;
using PeopleVilleTickManager;
Console.WriteLine("PeopleVille");

//Create village
var village = new Village();
Console.WriteLine(village.ToString());


//Print locations with villagers to screen
foreach (var location in village.Locations)
{
    var locationStatus = location.Name;
    foreach(var villager in location.Villagers().OrderByDescending(v => v.Age))
    {
        locationStatus += $" {villager}";
    }
    Console.WriteLine(locationStatus);
}

// Initialize and start the tick manager
PeopleVilleTickManager.TickManager peopleVilleTickManager = new(village);
peopleVilleTickManager.StartTicking(); // Start the ticking process

Console.WriteLine("Press Enter to stop the application...");
// Keep the application running by waiting for an Enter key press
Console.ReadLine();