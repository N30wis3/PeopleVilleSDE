﻿using PeopleVilleEngine;
using PeopleVilleEngine.Items;
using PeopleVilleEngine.Locations;
using PeopleVilleMovement;
using System.Timers;

namespace PeopleVilleTickManager
{
    public class TickManager
    {
        private TickSystem tickSystem;

        Random ran = new Random();
        int hour;
        int day;
        public void StartTicking()
        {

        }

        public TickManager(PeopleVilleEngine.Village village)
        {
            tickSystem = new TickSystem(1000);
            tickSystem.onTick += HandleTick;
            Ticker(village);
        }
        private void HandleTick(int tickCount)
        {
            Console.WriteLine($"Tick {tickCount}: Game state updated.");
            // You can add more logic here that will execute on each tick
        }

        private int pricePerFood = 1; // Price per single point of food

        async void Ticker(PeopleVilleEngine.Village village)
        {
            if (hour == 25) { hour = 0; day++; }
            foreach (var location in village.Locations)
            {
                foreach (var villager in location.Villagers() )
                {
                    float deathChance = villager.Age / 25; // TODO: Balance this
                    if (ran.Next(0, Convert.ToInt32(Math.Round(deathChance))) == 0) // TODO: Seperate these two conditions into seperate statements which run the same method with different parameters
                    {
                        Die(location, villager, "unknown causes");
                    }
                    else if (villager.Food == 0)
                    {
                        Die(location, villager, "starvations");
                    }

                    if (villager.IsWorking) { villager.Food -= 2; }
                    else { villager.Food -= 1; }

                    if (GetFoodItems(villager).Count <= villager.Location.Villagers().Count * 2) // TODO: Seperate into own method
                    {
                        
                        if (villager.Money >= (100 - villager.Food) * pricePerFood)
                        {
                            villager.Money -= (100 - villager.Food) * pricePerFood;
                        }
                    }

                    if (villager.Food <= 20)
                    {
                        if (GetFoodItems(villager).Count != 0)
                        {
                            Eat(villager);
                        }
                        else
                        {
                            BuyFood(villager);
                        }
                    }

                    // TODO:
                    // Add a chance for a villager to trade with another villager by choosing a wanted item,
                    // running a loop to find a villager who is in possesion of that item and buy it for a price which is in range of the item value
                }
            }
            Console.WriteLine("running...");
            hour++;
            await Task.Delay(50);
            Ticker(village);
        }

        void BuyFood(BaseVillager villager)
        {
            //villager.Location =
            Console.WriteLine($"{villager.FirstName} {villager.LastName} has gone to the supermarket to buy food");
        }

        void Eat(BaseVillager villager)
        {
            villager.Items.Remove(GetFoodItems(villager).First());
            villager.Food += 10;
        }

        void Die(ILocation location, BaseVillager villager, string cause)
        {
            Console.WriteLine($"{villager.FirstName} {villager.LastName} has died at the age of {villager.Age} due to {cause}.");
            location.Villagers().Remove(villager);

            if (location.Villagers().Count == 0)
            {
                Console.WriteLine($"{location.Name} has been abandoned due to lack of inhabitants, {villager.FirstName} {villager.LastName} was the last inhabitant");
            }
        }

        List<Item> GetFoodItems(BaseVillager villager)
        {
            List<Item> items = new List<Item>();
            foreach (var item in villager.Items)
            {
                if (item.Category == ItemCategory.Food)
                {
                    items.Add(item);
                }
            }

            return items;
        }
    }
    public delegate void TickHandler(int tickCount);
    public class TickSystem
    {
        public event TickHandler onTick;

        private int hour;
        private int day;
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
            onTick?.Invoke(tickCount);
        }
    }
}
