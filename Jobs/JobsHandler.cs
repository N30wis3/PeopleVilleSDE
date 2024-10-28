using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jobs
{
    public class JobsHandler
    {
        public List<Job> jobs = new List<Job>();
        private readonly RNG random = RNG.GetInstance(); // Assuming RNG is your random utility

        // Constructor
        public JobsHandler(PeopleVilleEngine.Village village)
        {
            foreach (var location in village.Locations)
            {
                foreach (var villager in location.Villagers())
                {
                    if (villager.Age >= 18 && string.IsNullOrEmpty(villager.WorkPlace))
                    {
                        // Find a job with capacity, or create a new job if none exist
                        Job assignedJob = FindJobWithCapacity() ?? CreateNewJob();
                        villager.WorkPlace = assignedJob.Name;
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
                newJob = new Job();
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
    }
}
