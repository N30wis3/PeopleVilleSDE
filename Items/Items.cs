using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Items
{
    public class Item
    {
        private readonly RNG random = RNG.GetInstance();

        public string Name { get; private set; }
        public ItemCategory Category { get; private set; }
        public string Description { get; private set; }
        public int Value { get; private set; }

        //Maybe add more itemsss?
        private static readonly string jsonFilePath = @"lib\\itemNames.json";
        private string[] foodNames = { "" };
        private string[] electronicNames = { "" };
        private string[] toolNames = { "" };
        //private static readonly string[] descriptions =
        //{
        //    "Something to eat",
        //    "An electronic device",
        //    "A useful item",
        //};

        public Item()
        {
            //Load names into string[]'s
            LoadNames();

            //Selects either food, electric or tool 
            int randomNumber = random.Next(1, 4);

            //Get random name and category
            switch (randomNumber)
            {
                case 1:
                    int foodIndex = random.Next(0, foodNames.Length);
                    Name = foodNames[foodIndex];
                    Category = ItemCategory.Food;
                    break;

                case 2:
                    int electronicIndex = random.Next(0, electronicNames.Length);
                    Name = electronicNames[electronicIndex];
                    Category = ItemCategory.Electronics;
                    break;

                case 3:
                    int toolIndex = random.Next(0, toolNames.Length);
                    Name = toolNames[toolIndex];
                    Category = ItemCategory.Tool;
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"Invalid random number: {randomNumber}");
            }

            switch (Category)
            {
                case ItemCategory.Food:
                    Description = "Something to eat";
                    break;
                case ItemCategory.Electronics:
                    Description = "An electronic device";
                    break;
                case ItemCategory.Tool:
                    Description = "A useful item";
                    break;
            }

            //Set value
            Value = random.Next(1, 101);
            return;
        }

        private void LoadNames()
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException(jsonFilePath);

            string jsonData = File.ReadAllText(jsonFilePath);
            var namesData = JsonSerializer.Deserialize<ItemNamesData>(jsonData);
            foodNames = namesData.FoodNames; 
            electronicNames = namesData.ElectronicNames;
            toolNames = namesData.ToolNames;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Category: {Category}, Value: {Value}, Description: {Description}";
        }
    }

    public enum ItemCategory
    {
        Food, Electronics, Tool,
    }

    public class ItemNamesData
    {
        public string[]? FoodNames;
        public string[]? ElectronicNames;
        public string[]? ToolNames;
    }
}
