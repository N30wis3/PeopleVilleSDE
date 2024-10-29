namespace PeopleVilleEngine;
using PeopleVilleEngine.Villagers.Creators;
using PeopleVilleEngine.Locations;
using System.Reflection;
using System.Linq;
using PeopleVilleEngine.Locations.Creators;

public class Village
{
    private readonly RNG _random = RNG.GetInstance();
    public List<BaseVillager> Villagers { get; } = new();
    public List<ILocation> Locations { get; } = new();
    public VillagerNames VillagerNameLibrary { get; } = VillagerNames.GetInstance();

    public Village()
    {
        Console.WriteLine("Creating villager");
        CreateVillage();
    }


    private void CreateVillage()
    {
        var villagers = _random.Next(10, 24);
        Console.ForegroundColor = ConsoleColor.Red;

        var libraryFiles = Directory.EnumerateFiles("lib").Where(f => Path.GetExtension(f) == ".dll");
        foreach (var libraryFile in libraryFiles)
        {
            Assembly.LoadFrom(libraryFile);
        }

        var ShopCreators = LoadFactories<IShopCreator>();
        var villageCreators = LoadFactories<IVillagerCreator>();
        
        Console.ResetColor();
        Console.WriteLine();

        int villageCreatorindex = 0;

        for (int i = 0; i < villagers; i++)
        {
            var created = false;
            do
            {
                created = villageCreators[villageCreatorindex].CreateVillager(this);
                villageCreatorindex = villageCreatorindex + 1 < villageCreators.Count ? villageCreatorindex + 1 : 0;
            } while (!created);
        }

        foreach (var creator in ShopCreators) 
        {
            creator.CreateShop(this);
        }

        Console.ResetColor();
    }

    private List<T> LoadFactories<T>() where T : class
    {
        var factoryList = new List<T>();
        var interfaceType = typeof(T);

        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes());
        LoadFactoriesFromType(types, factoryList, interfaceType);

        return factoryList;
    }


    private void LoadFactoriesFromType<T>(IEnumerable<Type> inputTypes, List<T> outputFactories, Type interfaceType) where T : class
    {
        var factoryTypes = inputTypes
            .Where(p => interfaceType.IsAssignableFrom(p) && !p.IsInterface)
            .ToList();

        foreach (var type in factoryTypes)
        {
            Console.WriteLine($"Factory loaded: {type}");
            outputFactories.Add((T)Activator.CreateInstance(type));
        }
    }
    public override string ToString()
    {
        return $"Village have {Villagers.Count} villagers, where {Villagers.Count(v => v.HasHome() == false)} are homeless.";
    }

    public int CountPopulation()
    {
        int villagers = 0;
        foreach (var location in Locations)
        {
            foreach (var villager in location.Villagers())
            {
                villagers++;
            }
        }

        return villagers;
    }
}