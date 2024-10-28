using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleGui
{
    public class Villager
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public List<Villager> Villagers { get; set; }
    }

    public class Village
    {
        public List<Location> Locations { get; set; }
    }


}
