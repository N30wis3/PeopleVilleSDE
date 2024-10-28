using PeopleVilleEngine.Items;

namespace PeopleVilleEngine.Locations;
public interface ILocation
{
    string Name { get; }
    List<BaseVillager> Villagers();
}

public interface IHouse : ILocation
{
    public int Population { get; }
    public int MaxPopulation { get; set; }
}
public interface IShop : ILocation
{
    public List<Item> Items { get; set; }

}