using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dojodachi.Models;

namespace randomPasscodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("dojodachi")]
        public IActionResult Index()
        {
            Dojodachi newDojoDachi = new Dojodachi();
            HttpContext.Session.SetInt32("Fullness", newDojoDachi.Fullness);
            HttpContext.Session.SetInt32("Happiness", newDojoDachi.Happiness);
            HttpContext.Session.SetInt32("Meals", newDojoDachi.Meals);
            HttpContext.Session.SetInt32("Energy", newDojoDachi.Energy);
            return View("Index", newDojoDachi);
        }
        
        [HttpPost("feed")]
        public IActionResult Feed
        {
            if(_Meals > 0)
            {
                Random likeit = new Random();
                int _likeit = likeit.Next(0, 100);
                if (_likeit < 26)
                {
                    if (_Meals < 2)
                    {
                        _Meals -=1;
                        TempData["returnString"] = $"Your dojodachi was grossed out by the scum you fed it. No fullness gained. You're also running low on meals. -1 Meal";
                    }
                    else
                    {
                        _Meals -= 1;
                        TempData["returnString"] = $"Your dojodachi was grossed out by the scum you fed it. No fullness gain. Maybe try feeding it again? -1 Meal";
                    }
                }
                else
                {
                    if (_Meals < 2)
                    {
                        if (_Fullness != null)
                        {
                            Random rand = new Random();
                            int fullnessAdd = rand.Next(5, 10);
                            _Fullness += fullnessAdd;
                            HttpContext.Session.SetInt32("Fullness", (int)_Fullness);
                            TempData["returnString"] = $"Your dojodachi liked the meal! You're running out of meals! +{fullnessAdd}";
                            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("Fullness", (int)_Fullness);
                        }
                    }
                    else
                    {
                        _Meals -= 1;
                        Random rand = new Random();
                        int fullnessAdd = rand.Next(5, 10);
                        _Fullness += fullnessAdd;
                        TempData["returnString"] = $"You fed your Dojodachi! You have {_Meals} left. Fullness +{_Fullness}";
                        HttpContext.Session.SetInt32("Fullness", (int)_Fullness);
                        ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
                    }
                }
            }
            else
            {
                TempData["returnString"] = "You don't have enough meals to feed your Dojodachi! Get more";
            }
        }
    }
        [HttpPost("play")]
        public IActionResult Play()
        {
            int? _Energy = HttpContext.Session.GetInt32("Energy");
            int? _Happiness = HttpContext.Session.GetInt32("Happiness");
            if (_Energy != null)
            {
                Random likeit = new Random();
                int _likeit = likeit.Next(0, 100);
                if (_Energy < 10)
                {
                    if (_likeit < 26)
                    {
                        _Energy -= 5;
                        TempData["returnString"] = $"Your dojodachi's energy is running low! It doesn't like the way you play with it. -5 Energy";
                        HttpContext.Session.SetInt32("Energy", (int)_Energy);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Random rand = new Random();
                        int playEnergy = rand.Next(5, 10);
                        _Happiness += playEnergy;
                        _Energy -= 5;
                        TempData["returnString"] = $"Your dojodachi is having a blast! But it is tired! Be warry of Energy. +{playEnergy} Happiness, -5 Energy";
                        HttpContext.Session.SetInt32("Energy", (int)_Energy);
                        HttpContext.Session.SetInt32("_Happiness", (int)_Happiness);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    if (_likeit < 26)
                    {
                        _Energy -= 5;
                        TempData["returnString"] = $"Your dojodachi doesn't like the way you play with it. -5 Energy";
                        HttpContext.Session.SetInt32("Energy", (int)_Energy);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Random rand = new Random();
                        int lowEnergyplayEnergy = rand.Next(5, 10);
                        _Happiness += lowEnergyplayEnergy;
                        _Energy -= 5;
                        TempData["returnString"] = $"Your dojodachi is having a blast!. -5 Energy, +{lowEnergyplayEnergy} Happiness";
                        HttpContext.Session.SetInt32("Energy", (int)_Energy);
                        HttpContext.Session.SetInt32("Happiness", (int)_Happiness);
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                HttpContext.Session.SetInt32("Energy", (int)_Energy);
            }
        }
        [HttpPost("work")]
        public IActionResult Work()
        {
            int? _Energy = HttpContext.Session.GetInt32("Energy");
            int? _Meals = HttpContext.Session.GetInt32("Meals");
            if (_Energy != null)
            {
                _Energy -= 5;
                Random rand = new Random();
                int mealsAdd = rand.Next(1, 3);
                _Meals += mealsAdd;
                TempData["returnString"] = $"You put your dojodachi to werkkk. +{mealsAdd} Meals, Energy is now {_Energy}";
            }
            else
            {
                HttpContext.Session.SetInt32("Energy", (int)_Happiness);
            }

            return RedirectToAction("Index");
        }
        [HttpPost("sleep")]
        public IActionResult Sleep()
        {
            return RedirectToAction("Index");
        }
    }
}