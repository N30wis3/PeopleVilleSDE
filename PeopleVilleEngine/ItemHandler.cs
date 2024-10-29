using System.Text.Json;
using System;
using PeopleVilleEngine.Items;

namespace PeopleVilleEngine
{
    public class ItemHandler
    {
        //List to keep track of all items
        private List<Item> allItems = new List<Item>();
        RNG _random;
        private static ItemHandler? _instance = null;

        private ItemHandler()
        {
            _random = RNG.GetInstance();
            LoadNamesFromJsonFile();
        }

        public static ItemHandler GetInstance()
        {
            if (_instance == null)
                _instance = new ItemHandler();
            return _instance;
        }

        private void LoadNamesFromJsonFile()
        {
            string jsonFile = "lib\\items.json";
            if (!File.Exists(jsonFile))
                throw new FileNotFoundException(jsonFile);

            string jsonData = File.ReadAllText(jsonFile);
            var itemData = JsonSerializer.Deserialize<List<JsonItem>>(jsonData);
            foreach (var jsonItem in itemData)
            {
                Item item = new Item
                {
                    Name = jsonItem.Name,
                    Value = jsonItem.Value,
                    Category = Enum.Parse<ItemCategory>(jsonItem.Category),
                    Description = jsonItem.Description,
                    Id = jsonItem.Id
                };
                allItems.Add(item);
            }
        }

        public Item GetItemById(int id)
        {
            return allItems.FirstOrDefault(i => i.Id == id);
        }
        public List<Item> GetItemsByCategory(ItemCategory category)
        {
            return allItems.Where(i => i.Category == category).ToList();
        }
        public Item GetRandomItem()
        {
            return allItems[_random.Next(0, allItems.Count() - 1)];
        }
    }
    public class JsonItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
