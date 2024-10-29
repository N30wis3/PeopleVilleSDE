using PeopleVilleEngine.Locations;
using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Items;

namespace PeopleVilleSupermarket
{
    public class Supermarket : IShop
    {
        private readonly List<BaseVillager> _villagers = new();
        public List<Item> Items { get; set; } = ItemHandler.GetInstance().GetItemsByCategory(ItemCategory.Food);

        public string Name => $"Supermarket, with {Population} villagers. Items: " + string.Join(", ", Items);

        public List<BaseVillager> Villagers()
        {
            return _villagers;
        }
        public int Population => _villagers.Count;

    }

}
