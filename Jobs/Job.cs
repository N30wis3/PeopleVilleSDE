using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Villagers;
using PeopleVilleSupermarket;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Jobs
{
    public class Job
    {
        private readonly RNG random = RNG.GetInstance();

        public string Name { get; private set; }
        //public int ID { private get; set; }
        public int Salary { get; private set; }
        public List<BaseVillager> Workers = new List<BaseVillager>();
        public static int MaxWorkers = 10;



        public Job(List<string> jobNames)
        {
            int randomNumber = random.Next(jobNames.Count);

            //select random job name
            string name = jobNames[randomNumber];
            Name = name;

            //job.ID = 0; //idk man i just got here
            Salary = random.Next(10, 101);

            return;
        }

        public override string ToString()
        {
            return $"Company: {Name}, Salary: {Salary}, Capacity: {MaxWorkers}";
        }
    }
}
