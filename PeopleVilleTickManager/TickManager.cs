using PeopleVilleEngine;
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

        async void Ticker(PeopleVilleEngine.Village village)
        {
            if (hour == 25) { hour = 0; day++; }
            foreach (var location in village.Locations)
            {
                foreach (var villager in location.Villagers() )
                {
                    float deathChance = villager.Age / 25;
                    //if (ran.Next(0, Math.Round(deathChance)) == 0) { location.Villagers().Remove(villager); }
                    
                }
            }
            Console.WriteLine("running...");
            hour++;
            await Task.Delay(50);
            Ticker(village);
        }
    }
}
