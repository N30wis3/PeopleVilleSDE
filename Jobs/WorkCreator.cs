using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Villagers.Creators;
using PeopleVilleEngine.Villagers;
using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Locations.Creators;
using Jobs;

namespace PeopleVilleSupermarket
{
    public class WorkCreator : IWorkCreator
    {
        public void CreateWork(Village village)
        {
            var work = new Work();
            work.Name = "Work";
            village.Locations.Add(work);
        }
    }
}
