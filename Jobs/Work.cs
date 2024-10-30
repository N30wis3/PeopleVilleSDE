using PeopleVilleEngine.Locations;
using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Items;

namespace PeopleVilleSupermarket
{
    public class Work : IWork
    {
        private readonly List<BaseVillager> _villagers = new();

        public string Name { get; set; }

        public List<BaseVillager> Villagers()
        {
            return _villagers;
        }
        public int Population => _villagers.Count;
    }
}