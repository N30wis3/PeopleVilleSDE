using PeopleVilleEngine.Locations;
using PeopleVilleEngine;
using PeopleVilleEngine.Villagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Villagers.Creators;
using PeopleVilleVillagerOld;

namespace PeopleVilleVillagerHomeless.Creator;
public class OldVillageCreator : IVillagerCreator
{
    public bool CreateVillager(Village village)
    {
        var random = RNG.GetInstance();
        var person = new OldVillager(village, random.Next(65, 100));
        //Find house
        var home = FindHome(village);

        if (home.Villagers().Count(v => v.GetType() == typeof(OldVillager)) >= 1)
        {
            var first = home.Villagers().First(v => v.GetType() == typeof(OldVillager));
            person.LastName = first.LastName;
            person.IsMale = !first.IsMale;
            person.FirstName = village.VillagerNameLibrary.GetRandomFirstName(person.IsMale);
        }

        home.Villagers().Add(person);
        person.Home = home;

        //Add to village
        village.Villagers.Add(person);
        return true;
    }

    private IHouse FindHome(Village village)
    {
        var random = RNG.GetInstance();

        var potentialHomes = village.Locations.OfType<NursingHome>()
                            .Where(p => p.Population < p.MaxPopulation).ToList();

        if (potentialHomes.Count > 0 && random.Next(1, 5) != 1) //Return current house
            return (IHouse)potentialHomes[random.Next(0, potentialHomes.Count)];

        //create a new house
        IHouse house = new NursingHome();
        village.Locations.Add(house);
        return house;

    }
}
