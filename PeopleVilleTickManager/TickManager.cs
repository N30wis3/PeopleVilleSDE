using Jobs;
using PeopleVilleEngine;
using PeopleVilleEngine.Items;
using PeopleVilleEngine.Locations;
using PeopleVilleMovement;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Timers;

namespace PeopleVilleTickManager
{
    public class TickManager
    {
        private TickSystem tickSystem;
        private PeopleVilleEngine.Village village;
        private PeopleVilleMovement.VillagerMover villagerMover;

        public TickManager(PeopleVilleEngine.Village village)
        {
            villagerMover = new();
            this.village = village;
            JobsHandler jobHandler = new(village);
            tickSystem = new(500); // Ticks every second
            tickSystem.OnTick += HandleTick;
        }

        public void StartTicking()
        {
            tickSystem.Start();
        }

        private int hour = 0;
        private int day = 0;

        private void HandleTick(int tickCount)
        {
            Console.WriteLine($"\nTick {tickCount}: Game state updated. Day {day}, hour {hour}");

            if (village.CountPopulation() == 0)
            {
                Console.WriteLine($"All villagers have died, amount of days passed: {day}");
                tickSystem.OnTick -= HandleTick;
            }

            if (hour >= 24) // Reset hour and increment day at the end of a full day
            {
                hour = 0;
                foreach (var location in village.Locations)
                {
                    foreach (var villager in location.Villagers())
                    {
                        villager.Money = villager.
                    }
                }
                day++;
            }

            foreach (var location in village.Locations)
            {
                foreach (var villager in location.Villagers())
                {
                    ProcessVillagerActions(location, villager);
                }
            }
            hour++;
        }

        private void ProcessVillagerActions(ILocation location, BaseVillager villager)
        {
            // Function: y = (0.1 * x)^2 + 1000.
            int deathChance = Convert.ToInt32(Math.Round(Math.Pow(Math.Round(0.1f * villager.Age), 2) + 1000)); // A 40 year old has around a 0,15% chance of dying randomly. 

            RNG ran = RNG.GetInstance();

            if (villager.Health == 0)
            {
                villager.Die("starvation");
                return;
            }
            else if (ran.Next(0, deathChance) == 0)
            {
                villager.Die("unknown causes");
                return;
            }

            if (hour >= 9 && hour <= 17)
            {
                if (villager.Location != village.Locations.Find(location => location.Name == "Work"))
                {
                    Console.WriteLine($"{villager.FullName()} has gone to work.");
                    villagerMover.MoveVillager(villager, village.Locations.Find(location => location.Name == "Work"));
                }
            }
            else
            {
                if (villager.Location == village.Locations.Find(location => location.Name == "Work"))
                {
                    villagerMover.MoveVillagerHome(villager);
                }
            }

            if (ran.Next(0, 100) == 0) villager.Trade();

            if (ran.Next(0, 2) == 0)
            {
                if (!villager.IsWorking()) villager.Food = Math.Clamp(villager.Food - ran.Next(4, 7), 0, 100);
            }
            else
            {
                villager.Food = Math.Clamp(villager.Food - ran.Next(4, 7), 0, 100);
            }

            if (!villager.IsWorking())
            {
                // If villager has less than 5 food items, buy more. This should prevent villagers from starving to death too easily.
                if (villager.GetAmountOfInventoryItems(ItemCategory.Food) <= 5) villager.BuyFood();
            }

            // Drain health if villager is starving.
            if (villager.Food == 0) villager.LoseHealth(ran.Next(1,4));

            // Eat if villager owns food.
            if (villager.Food <= 25 && villager.GetAmountOfInventoryItems(ItemCategory.Food) > 0) villager.Eat();
        }
    }

    public delegate void TickHandler(int tickCount);

    public class TickSystem
    {
        public event TickHandler OnTick;
        private System.Timers.Timer timer;
        private int tickCount = 0;

        public TickSystem(double intervalMs)
        {
            timer = new System.Timers.Timer(intervalMs);
            timer.Elapsed += TimerElapsed;
        }

        public void Start()
        {
            timer.Start();
            Console.WriteLine("Tick system started.");
        }

        public void Stop()
        {
            timer.Stop();
            Console.WriteLine("Tick system stopped.");
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            tickCount++;
            OnTick?.Invoke(tickCount);
        }
    }
}
