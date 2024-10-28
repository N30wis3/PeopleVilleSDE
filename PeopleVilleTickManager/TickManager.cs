using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using PeopleVilleMovement;

namespace PeopleVilleTickManager
{
    public class TickManager
    {
        private int day;
        private int hour;
        Random ran = new Random();
        public TickManager(PeopleVilleEngine.Village village)
        {
            Ticker(village);
        }

        private int pricePerFood = 1; // Price per single point of food

        async void Ticker(PeopleVilleEngine.Village village)
        {
            if (hour == 25) { hour = 0; day++; }
            foreach (var location in village.Locations)
            {
                foreach (var villager in location.Villagers() )
                {
                    float deathChance = villager.Age / 25; // TODO: Balance this
                    if (ran.Next(0, Convert.ToInt32(Math.Round(deathChance))) == 0) // TODO: Seperate these two conditions into seperate statements which run the same method with different parameters
                    {
                        Die(location, villager, "complications");
                    }
                    else if (villager.Food == 0)
                    {
                        Die(location, villager, "starvations");
                    }

                    if (villager.IsWorking) { villager.Food -= 2; }
                    else { villager.Food -= 1; }

                    if (villager.Food <= 20) // TODO: Seperate into own method
                    {
                        
                        if (villager.Money >= (100 - villager.Food) * pricePerFood)
                        {
                            villager.Money -= (100 - villager.Food) * pricePerFood;
                        }
                    }

                    // TODO:
                    // Add a chance for a villager to trade with another villager by choosing a wanted item,
                    // running a loop to find a villager who is in possesion of that item and buy it for a price which is in range of the item value
                }
            }
            Console.WriteLine("running...");
            hour++;
            await Task.Delay(50);
            Ticker(village);
        }

        void Die(ILocation location, BaseVillager villager, string cause)
        {
            Console.WriteLine($"{villager.FirstName} {villager.LastName} has died at the age of {villager.Age} due to {cause}.");
            location.Villagers().Remove(villager);

            if (location.Villagers().Count == 0)
            {
                Console.WriteLine($"{location.Name} has been abandoned due to lack of inhabitants, {villager.FirstName} {villager.LastName} was the last inhabitant");
            }
        }
    }
}
