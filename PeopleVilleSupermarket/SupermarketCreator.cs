using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Villagers.Creators;
using PeopleVilleEngine.Villagers;
using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Locations.Creators;

namespace PeopleVilleSupermarket
{
    public class SupermarketCreator : IShopCreator
    {
        public void CreateShop(Village village)
        {
            var supermarket = new Supermarket();
            village.Locations.Add(supermarket);
        }
    }
}
