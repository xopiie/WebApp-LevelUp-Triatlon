using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTriathlon.Models;

namespace WebAppTriathlon.Controllers
{
    public class CompetitionsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Competitions> tekmovanja = new List<Competitions>();
            tekmovanja = await CompetitionHandler.GetAll();

            List<LikedCompetitions> likes = new List<LikedCompetitions>();
            likes = await CompetitionHandler.GetAllLikes();

            if(User.Identity.IsAuthenticated)
            {
                foreach (var like in likes)
                {
                    foreach (var competition in tekmovanja)
                    {
                        if (like.FK_ID_Competitiion == competition.ID_Competition)
                        {
                            if (like.Email == User.Identity.Name) competition.Liked = true;
                        }
                    }
                }
            }
            
            return View(tekmovanja);
        }

        public IActionResult ResultsByCompetition(int id)
        {
            return RedirectToAction("AllResults", "Results", new { competitionID = id });
        }

        public async Task<IActionResult> Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString)) return RedirectToAction("Index");
            List<Competitions> tekmovanja = new List<Competitions>();
            tekmovanja = await CompetitionHandler.Search(searchString);

            List<LikedCompetitions> likes = new List<LikedCompetitions>();
            likes = await CompetitionHandler.GetAllLikes();
            if (User.Identity.IsAuthenticated)
            {
                foreach (var like in likes)
                {
                    foreach (var competition in tekmovanja)
                    {
                        if (like.FK_ID_Competitiion == competition.ID_Competition)
                        {
                            if (like.Email == User.Identity.Name) competition.Liked = true;
                        }
                    }
                }
            }

            return View("Index", tekmovanja);
        }

        public async Task<IActionResult> LikeCompetition(int id)
        {
            if(User.Identity.IsAuthenticated) await CompetitionHandler.LikeCompetition(id, User.Identity.Name);

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> MyLikes()
        {
            List<Competitions> tekmovanja = new List<Competitions>();
            if (User.Identity.IsAuthenticated)
            {
                List<Competitions> allCompetitions = new List<Competitions>();
                allCompetitions = await CompetitionHandler.GetAll();

                List<LikedCompetitions> likes = new List<LikedCompetitions>();
                likes = await CompetitionHandler.GetAllLikes();

                if (User.Identity.IsAuthenticated)
                {
                    foreach (var like in likes)
                    {
                        foreach (var competition in allCompetitions)
                        {
                            if (like.FK_ID_Competitiion == competition.ID_Competition)
                            {
                                if (like.Email == User.Identity.Name)
                                {
                                    competition.Liked = true;
                                    tekmovanja.Add(competition);
                                }
                            }
                        }
                    }
                }
            }

            tekmovanja.Reverse();
            return View("Index", tekmovanja);
        }
    }
}
