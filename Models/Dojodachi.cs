using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace dojodachi.Models
{
    public class Dojodachi
    {
        public int Fullness { get; set; }
        public int Happiness { get; set; }
        public int Meals { get; set; }
        public int Energy { get; set; }

        public Dojodachi()
        {
            Fullness = 20;
            Happiness = 20;
            Meals = 3;
            Energy = 50;
        }

        public string Work(int energy)
        {
            Energy -=5;
            Random rand = new Random();
            int mealsAdd = rand.Next(1,3);
            Meals+=mealsAdd;
            return $"You put your dojodachi to werkkk. +{mealsAdd} Meals, Energy is now {Energy}";
        }
        public string Sleeping (int energy)
        {
            Energy+=15;
            Fullness-=5;
            Happiness-=5;
            return $"You put your dojodachi to sleep. Woke up with {Energy} energy, {Fullness} fullness, {Happiness} happiness";
        }
    }
}