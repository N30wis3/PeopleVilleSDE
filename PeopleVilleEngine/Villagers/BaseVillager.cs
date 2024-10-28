using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Items;

public abstract class BaseVillager
{
    public int Age { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsMale { get; set; }
    private Village _village;
    public ILocation? Home { get; set; } = null;
    public ILocation? Location { get; set; } = null;
    public bool HasHome() => Home != null;
    public List<Item> Items { get; set; } = new List<Item>();

    // custom test variables, not made for final build

    public int Food;
    public bool IsWorking;
    public int Money;

    protected BaseVillager(Village village)
    {
        _village = village;
        IsMale = RNG.GetInstance().Next(0, 2) == 0;
        (FirstName, LastName) = village.VillagerNameLibrary.GetRandomNames(IsMale);
        ItemHandler ITH = ItemHandler.GetInstance();
        for (int i = 0; i < RNG.GetInstance().Next(0, 6); i++)
        {
            Items.Add(ITH.GetRandomItem());
        }
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} ({Age} years) | Items: {string.Join(", ", Items.Select(item => item.Name))}";
    }
}