using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

namespace PeopleVilleMovement
{
    public class VillagerMover
    {
        public void MoveVillager(BaseVillager villager, IHouse newLocation)
        {
            villager.Location = newLocation;
        }

        public void MoveVillagerHome(BaseVillager villager)
        {
            villager.Location = villager.Home;
        }
    }
}