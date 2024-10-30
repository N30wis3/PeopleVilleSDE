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
    public int Food { get; set; } = 100;
    public int Money { get; set; } = 1000;
    public int Health { get; set; } = 100;
    public ILocation WorkPlace { get; set; }


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

    public string FullName()
    {
        return $"{FirstName} {LastName}";
    }

    public void Die(string cause)
    {
        Console.WriteLine($"{FullName()} has died at the age of {Age} due to {cause}.");

        if (Home == null) Location.Villagers().Remove(this);
        else Home.Villagers().Remove(this);

        if (Home?.Villagers().Count == 0)
        {
            Console.WriteLine($"{Home.Name} has been abandoned due to lack of inhabitants; {FullName()} was the last inhabitant.");
        }
    }

    public void Eat()
    {
        RNG ran = RNG.GetInstance();
        Item foodItem = GetInventoryItems(ItemCategory.Food).OrderByDescending(f => f.Value).First();
        if (foodItem != null)
        {
            Items.Remove(foodItem);
            Food = Math.Clamp(Food + foodItem.Value * ran.Next(3,5), 0, 100);
            RegenHealth(foodItem.Value);
            Console.WriteLine($"{FullName()} ate {foodItem.Name}, current hunger level: {Food}");
        }
        else // This should never happen, but I made it just in case.
        {
            Console.WriteLine($"{FullName()} has no more food left");
        }
    }

    public void LoseHealth(int amount)
    {
        Health = Math.Clamp(Health - amount, 0, 100);
    }

    public void RegenHealth(int amount)
    {
        Health = Math.Clamp(Health + amount, 0, 100);
    }

    public void BuyFood()
    {
        if (Location == _village.Locations.Find(location => location.Name == "Supermarket"))
        {
            Console.WriteLine($"{FullName()} has gone to the supermarket to buy food.");


            int totalItems = 0;

            ItemHandler itemHandler = ItemHandler.GetInstance();

            // Buys 10 at once (Or as close as it can get)
            while (totalItems <= 10 && Money >= itemHandler.GetItemsByCategory(ItemCategory.Food).OrderBy(f => f.Value).First().Value)
            {
                foreach (Item item in itemHandler.GetItemsByCategory(ItemCategory.Food).OrderByDescending(f => f.Value))
                {
                    if (Money >= item.Value)
                    {
                        Items.Add(item);
                        Money -= item.Value;
                        totalItems++;
                        Console.WriteLine($"{FullName()} has bought {item.Name} at the supermarket for {item.Value}, remaining balance: {Money}$");
                        break;
                    }
                }
            }

            Location = Home;
            Console.WriteLine($"{FullName()} has gone home from the supermarket.");
        }
    }

    public void Trade()
    {
        ItemHandler itemHandler = ItemHandler.GetInstance();

        Item wantedItem = itemHandler.GetRandomItem();

        foreach (var location in _village.Locations)
        {
            foreach (var villager in location.Villagers())
            {
                if (villager != this && villager.Items.Contains(wantedItem) && Money >= wantedItem.Value + 50) // Adds 50 to ensure that villagers don't bankrupt themselves and starve.
                {
                    villager.Items.Remove(wantedItem);
                    villager.Money += wantedItem.Value;
                    Items.Add(wantedItem);
                    Money -= wantedItem.Value;
                    Console.WriteLine($"{FullName()} bought {wantedItem.Name.ToLower()} off of {villager.FullName()} for {wantedItem.Value}$");
                }
            }
        }
    }

    public bool IsWorking() // TODO: Check if villager is at work location
    {
        return false;
    }

    public List<Item> GetInventoryItems(ItemCategory category)
    {
        return Items.FindAll(item => item.Category == category);
    }

    public int GetAmountOfInventoryItems(ItemCategory category)
    {
        return GetInventoryItems(category).Count;
    }
}