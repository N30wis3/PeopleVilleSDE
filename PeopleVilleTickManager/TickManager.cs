using PeopleVilleEngine;
using PeopleVilleEngine.Items;
using PeopleVilleEngine.Locations;
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

        public TickManager(PeopleVilleEngine.Village village)
        {
            this.village = village;
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

            Random ran = new();

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

            if (ran.Next(0, 100) == 0) villager.Trade();

            if (!villager.IsWorking())
            {
                // Meant to simulate that the villager eats food supplied by their work, hunger drain during work is turned off to not trigger the Eat method.
                if (!villager.IsWorking()) villager.Food = Math.Clamp(villager.Food - 5, 0, 100);

                //Math.Clamp(villager.Food -= villager.IsWorking() ? 5 : 2, 0, 100);

                // If villager has less than 5 food items, buy more. This should prevent villagers from starving to death too easily.
                if (villager.GetAmountOfInventoryItems(ItemCategory.Food) <= 5) villager.BuyFood();
            }

            // Drain health if villager is starving.
            if (villager.Food == 0) villager.LoseHealth(2);

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
