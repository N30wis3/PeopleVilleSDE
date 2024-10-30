using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

namespace PeopleVilleMovement
{
    public class VillagerMover
    {
        public void MoveVillager(BaseVillager villager, ILocation newLocation)
        {
            villager.Location = newLocation;
            Console.WriteLine($"{villager.FullName()} moved to {newLocation.Name}.");
        }

        public void MoveVillagerHome(BaseVillager villager)
        {
            if (villager.Home != null)
            {
                villager.Location = villager.Home;
                Console.WriteLine($"{villager.FullName()} went home.");
            }
        }
    }
}