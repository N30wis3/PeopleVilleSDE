using PeopleVilleEngine;
using PeopleVilleEngine.Villagers;
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

        private static readonly string jsonFilePath = @"lib\\JobNames.json";
        private string[] jobNames = { "" };

        public Job()
        {
            LoadNames();

            Job job = new Job();
            int randomNumber = random.Next(jobNames.Length);

            //select random job name
            job.Name = jobNames[randomNumber];
            //job.ID = 0; //idk man i just got here
            job.Salary = random.Next(10, 101);

            return;
        }

        private void LoadNames()
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException(jsonFilePath);

            string jsonData = File.ReadAllText(jsonFilePath);
            // MAYBE ERROR, (im just a boy thihi :3)
            string[] namesData = JsonSerializer.Deserialize<string[]>(jsonData).ToArray();
            jobNames = namesData;
        }

        public override string ToString()
        {
            return $"Company: {Name}, Salary: {Salary}, Capacity: {MaxWorkers}";
        }
    }
}
