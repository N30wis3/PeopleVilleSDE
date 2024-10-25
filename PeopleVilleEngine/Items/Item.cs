using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace PeopleVilleEngine.Items
{
    public class Item
    {
        private readonly RNG random = RNG.GetInstance();

        public int Id { get; set; }
        public string Name { get; set; }
        public ItemCategory Category { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Category: {Category}, Value: {Value}, Description: {Description}";
        }
    }

    public enum ItemCategory
    {
        Food, Electronics, Tool,
    }
}
