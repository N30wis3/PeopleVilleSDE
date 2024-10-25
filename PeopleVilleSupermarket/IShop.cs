using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Items;
using PeopleVilleEngine;

namespace PeopleVilleSupermarket
{
    public interface IShop : ILocation
    {
        public List<Item> Items { get; set; }

    }
}
