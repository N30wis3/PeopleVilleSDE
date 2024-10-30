using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using PeopleVilleSupermarket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jobs
{
    public class JobsHandler
    {
        public List<Job> jobs = new List<Job>();
        private readonly RNG random = RNG.GetInstance(); // Assuming RNG is your random utility

        private static readonly string jsonFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GitHub\\PeopleVilleSDE\\Jobs\\lib\\JobNames.json";
        private List<string> jobNames = new List<string>();

        // Constructor
        public JobsHandler(PeopleVilleEngine.Village village)
        {
            LoadNames();
            WorkCreator workCreator = new WorkCreator();
            workCreator.CreateWork(village);

            foreach (var location in village.Locations)
            {
                foreach (var villager in location.Villagers())
                {
                    if (villager.Age >= 18 && villager.WorkPlace == null)
                    {
                        // Find a job with capacity, or create a new job if none exist
                        Job assignedJob = FindJobWithCapacity() ?? CreateNewJob();
                        assignedJob.Workers.Add(villager);
                    }
                }
            }
        }

        // Method to create a new job with a unique name
        public Job CreateNewJob()
        {
            Job newJob;
            do
            {
                newJob = new Job(jobNames);
            } while (jobs.Any(j => j.Name == newJob.Name)); // Ensure the job name is unique

            jobs.Add(newJob);
            return newJob;
        }

        // Method to find a random job with capacity for more workers
        public Job FindJobWithCapacity()
        {
            var jobsWithCapacity = jobs.Where(j => j.Workers.Count < Job.MaxWorkers).ToList();
            if (jobsWithCapacity.Count == 0) return null;

            // Select a random job from the list of jobs with capacity
            return jobsWithCapacity[random.Next(jobsWithCapacity.Count)];
        }
        private void LoadNames()
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException(jsonFilePath);

            string jsonData = File.ReadAllText(jsonFilePath);
            // MAYBE ERROR, (im just a boy thihi :3)
            var namesData = JsonSerializer.Deserialize<JobNamesData>(jsonData);
            jobNames = namesData.Names;
        }
    }
    public class JobNamesData
    {
        public List<string> Names { get; set; }
    }
}
