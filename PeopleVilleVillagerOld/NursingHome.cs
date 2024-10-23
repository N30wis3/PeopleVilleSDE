using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

namespace PeopleVilleVillagerOld;
public class NursingHome : IHouse
{
    public NursingHome()
    {
        var random = RNG.GetInstance();
        MaxPopulation = random.Next(3, 7);
    }
    private readonly List<BaseVillager> _villagers = new();
    public string Name => $"Nursing Home, with a population of {Population}.";

    public List<BaseVillager> Villagers()
    {
        return _villagers;
    }

    public int Population => _villagers.Count;
    public int MaxPopulation { get; set; }
}