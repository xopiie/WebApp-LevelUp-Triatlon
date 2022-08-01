using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTriathlon.Models;
using static WebAppTriathlon.Models.Results;

namespace WebAppTriathlon.Controllers
{
    public class ResultsController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AllResults(int competitionID)
        {
            ViewData["SearchByAthlete"] = "false";
            List<Results> rezultati = new List<Results>();
            rezultati = await ResultsHandler.GetAllFromCompetition(competitionID);
            ViewData["ID_Competition"] = competitionID.ToString();

            if (rezultati.Count > 0)
            {
                TimeSpan BestBike = new TimeSpan(99, 99, 99);
                TimeSpan BestRun = new TimeSpan(99, 99, 99);
                TimeSpan BestSwim = new TimeSpan(99, 99, 99);

            
                foreach (var r in rezultati)
                {
                    try
                    {
                        if (TimeSpan.Parse(r.SwimTime) != new TimeSpan())
                        {
                            if (TimeSpan.Parse(r.SwimTime) < BestSwim) BestSwim = TimeSpan.Parse(r.SwimTime);
                        }

                        if (TimeSpan.Parse(r.BikeTime) != new TimeSpan())
                        {
                            if (TimeSpan.Parse(r.BikeTime) < BestBike) BestBike = TimeSpan.Parse(r.BikeTime);
                        }

                        if (TimeSpan.Parse(r.RunTime) != new TimeSpan())
                        {
                            if (TimeSpan.Parse(r.RunTime) < BestRun) BestRun = TimeSpan.Parse(r.RunTime);
                        }
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                }

                if (BestBike == new TimeSpan(99, 99, 99))
                {
                    BestBike = new TimeSpan(00, 00, 00);
                }
                if (BestRun == new TimeSpan(99, 99, 99))
                {
                    BestRun = new TimeSpan(00, 00, 00);
                }
                if (BestSwim == new TimeSpan(99, 99, 99))
                {
                    BestSwim = new TimeSpan(00, 00, 00);
                }


                ViewData["ResultsNumber"] = rezultati.Count().ToString();
                ViewData["WorstTimeSwim"] = rezultati.Max(x => x.SwimTime).ToString();
                ViewData["WorstTimeBike"] = rezultati.Max(x => x.BikeTime).ToString();
                ViewData["WorstTimeRun"] = rezultati.Max(x => x.RunTime).ToString();
                ViewData["BestTimeSwim"] = BestSwim.ToString();
                ViewData["BestTimeBike"] = BestBike.ToString();
                ViewData["BestTimeRun"] = BestRun.ToString();
            }
            else
            {
                ViewData["ResultsNumber"] = "0";
                ViewData["WorstTimeSwim"] = "?";
                ViewData["WorstTimeBike"] = "?";
                ViewData["WorstTimeRun"] = "?";
                ViewData["BestTimeSwim"] = "?";
                ViewData["BestTimeBike"] = "?";
                ViewData["BestTimeRun"] = "?";
            }

            return View("Index", rezultati);
        }

        public async Task<IActionResult> Search(string searchString, int competitionID)
        {
            ViewData["SearchByAthlete"] = "false";
            List<Results> rezultati = new List<Results>();
            if (String.IsNullOrEmpty(searchString))
            {
                rezultati = await ResultsHandler.GetAllFromCompetition(competitionID);
                ViewData["ID_Competition"] = competitionID.ToString();

                if (rezultati.Count > 0)
                {
                    TimeSpan BestBike = new TimeSpan(99, 99, 99);
                    TimeSpan BestRun = new TimeSpan(99, 99, 99);
                    TimeSpan BestSwim = new TimeSpan(99, 99, 99);

                    foreach (var r in rezultati)
                    {
                        try
                        {
                            if (TimeSpan.Parse(r.SwimTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.SwimTime) < BestSwim) BestSwim = TimeSpan.Parse(r.SwimTime);
                            }

                            if (TimeSpan.Parse(r.BikeTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.BikeTime) < BestBike) BestBike = TimeSpan.Parse(r.BikeTime);
                            }

                            if (TimeSpan.Parse(r.RunTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.RunTime) < BestRun) BestRun = TimeSpan.Parse(r.RunTime);
                            }
                        }
                        catch (Exception)
                        {


                        }
                    }

                    if (BestBike == new TimeSpan(99, 99, 99))
                    {
                        BestBike = new TimeSpan(00, 00, 00);
                    }
                    if (BestRun == new TimeSpan(99, 99, 99))
                    {
                        BestRun = new TimeSpan(00, 00, 00);
                    }
                    if (BestSwim == new TimeSpan(99, 99, 99))
                    {
                        BestSwim = new TimeSpan(00, 00, 00);
                    }


                    ViewData["ResultsNumber"] = rezultati.Count().ToString();
                    ViewData["WorstTimeSwim"] = rezultati.Max(x => x.SwimTime).ToString();
                    ViewData["WorstTimeBike"] = rezultati.Max(x => x.BikeTime).ToString();
                    ViewData["WorstTimeRun"] = rezultati.Max(x => x.RunTime).ToString();
                    ViewData["BestTimeSwim"] = BestSwim.ToString();
                    ViewData["BestTimeBike"] = BestBike.ToString();
                    ViewData["BestTimeRun"] = BestRun.ToString();
                }
                else
                {
                    ViewData["ResultsNumber"] = "0";
                    ViewData["WorstTimeSwim"] = "?";
                    ViewData["WorstTimeBike"] = "?";
                    ViewData["WorstTimeRun"] = "?";
                    ViewData["BestTimeSwim"] = "?";
                    ViewData["BestTimeBike"] = "?";
                    ViewData["BestTimeRun"] = "?";
                }

                return View("Index", rezultati);
            }
            else
            {
                rezultati = await ResultsHandler.Search(searchString, competitionID);
                ViewData["ID_Competition"] = competitionID.ToString();

                if (rezultati.Count > 0)
                {
                    TimeSpan BestBike = new TimeSpan(99, 99, 99);
                    TimeSpan BestRun = new TimeSpan(99, 99, 99);
                    TimeSpan BestSwim = new TimeSpan(99, 99, 99);

                    foreach (var r in rezultati)
                    {
                        try
                        {
                            if (TimeSpan.Parse(r.SwimTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.SwimTime) < BestSwim) BestSwim = TimeSpan.Parse(r.SwimTime);
                            }

                            if (TimeSpan.Parse(r.BikeTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.BikeTime) < BestBike) BestBike = TimeSpan.Parse(r.BikeTime);
                            }

                            if (TimeSpan.Parse(r.RunTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.RunTime) < BestRun) BestRun = TimeSpan.Parse(r.RunTime);
                            }
                        }
                        catch (Exception)
                        {


                        }
                    }

                    if (BestBike == new TimeSpan(99, 99, 99))
                    {
                        BestBike = new TimeSpan(00, 00, 00);
                    }
                    if (BestRun == new TimeSpan(99, 99, 99))
                    {
                        BestRun = new TimeSpan(00, 00, 00);
                    }
                    if (BestSwim == new TimeSpan(99, 99, 99))
                    {
                        BestSwim = new TimeSpan(00, 00, 00);
                    }


                    ViewData["ResultsNumber"] = rezultati.Count().ToString();
                    ViewData["WorstTimeSwim"] = rezultati.Max(x => x.SwimTime).ToString();
                    ViewData["WorstTimeBike"] = rezultati.Max(x => x.BikeTime).ToString();
                    ViewData["WorstTimeRun"] = rezultati.Max(x => x.RunTime).ToString();
                    ViewData["BestTimeSwim"] = BestSwim.ToString();
                    ViewData["BestTimeBike"] = BestBike.ToString();
                    ViewData["BestTimeRun"] = BestRun.ToString();
                }
                else
                {
                    ViewData["ResultsNumber"] = "0";
                    ViewData["WorstTimeSwim"] = "?";
                    ViewData["WorstTimeBike"] = "?";
                    ViewData["WorstTimeRun"] = "?";
                    ViewData["BestTimeSwim"] = "?";
                    ViewData["BestTimeBike"] = "?";
                    ViewData["BestTimeRun"] = "?";
                }

                return View("Index", rezultati);
            }
        }


        public IActionResult SearchByAthlete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchByAthlete(SearchAthlete ia)
        {
            List<Results> rezultati = new List<Results>();
            if (ModelState.IsValid)
            {
                rezultati = await ResultsHandler.SearchByAthlete(ia.searchTerm);
                ViewData["SearchByAthlete"] = "true";

                if (rezultati.Count > 0)
                {
                    TimeSpan BestBike = new TimeSpan(99, 99, 99);
                    TimeSpan BestRun = new TimeSpan(99, 99, 99);
                    TimeSpan BestSwim = new TimeSpan(99, 99, 99);

                    foreach (var r in rezultati)
                    {
                        try
                        {
                            if (TimeSpan.Parse(r.SwimTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.SwimTime) < BestSwim) BestSwim = TimeSpan.Parse(r.SwimTime);
                            }

                            if (TimeSpan.Parse(r.BikeTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.BikeTime) < BestBike) BestBike = TimeSpan.Parse(r.BikeTime);
                            }

                            if (TimeSpan.Parse(r.RunTime) != new TimeSpan())
                            {
                                if (TimeSpan.Parse(r.RunTime) < BestRun) BestRun = TimeSpan.Parse(r.RunTime);
                            }
                        }
                        catch (Exception)
                        {


                        }
                    }

                    if (BestBike == new TimeSpan(99, 99, 99))
                    {
                        BestBike = new TimeSpan(00, 00, 00);
                    }
                    if (BestRun == new TimeSpan(99, 99, 99))
                    {
                        BestRun = new TimeSpan(00, 00, 00);
                    }
                    if (BestSwim == new TimeSpan(99, 99, 99))
                    {
                        BestSwim = new TimeSpan(00, 00, 00);
                    }


                    ViewData["ResultsNumber"] = rezultati.Count().ToString();
                    ViewData["WorstTimeSwim"] = rezultati.Max(x => x.SwimTime).ToString();
                    ViewData["WorstTimeBike"] = rezultati.Max(x => x.BikeTime).ToString();
                    ViewData["WorstTimeRun"] = rezultati.Max(x => x.RunTime).ToString();
                    ViewData["BestTimeSwim"] = BestSwim.ToString();
                    ViewData["BestTimeBike"] = BestBike.ToString();
                    ViewData["BestTimeRun"] = BestRun.ToString();
                }
                else
                {
                    ViewData["ResultsNumber"] = "0";
                    ViewData["WorstTimeSwim"] = "?";
                    ViewData["WorstTimeBike"] = "?";
                    ViewData["WorstTimeRun"] = "?";
                    ViewData["BestTimeSwim"] = "?";
                    ViewData["BestTimeBike"] = "?";
                    ViewData["BestTimeRun"] = "?";
                }


                return View("Index", rezultati);
            }
            return View(ia);
        }
    }
}
