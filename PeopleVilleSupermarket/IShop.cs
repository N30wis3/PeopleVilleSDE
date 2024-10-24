using PeopleVilleEngine.Locations;

namespace PeopleVilleSupermarket
{
    public interface IShop : ILocation
    {
        public List<Item> Items { get; set; }

    }

    public class Item
    {

    }
}
