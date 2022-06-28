using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursachAPI.Model;
using CursachAPI.Clients;
using Swashbuckle.AspNetCore.Annotations;

namespace CursachAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CursachController : ControllerBase
    {
        [HttpGet]
        [Route("/Hero/Id/{Id}")]
        public heroModel FindHeroById(int Id)
        {
            HeroClient hc = new HeroClient();

            return hc.GetHeroByIdAsync(Id).Result;
        }

        [HttpGet]
        [Route("/Hero/Name/{name}")]
        public heroModel FindHeroByName(string name)
        {
            HeroClient hc = new HeroClient();

            return hc.GetHeroByNameAsync(name).Result;
        }

        [HttpGet]
        [Route("/Hero/Attr/{mainAttr}")]
        public List<string> GetAllHeroesByAttr(string mainAttr)
        {
            HeroClient hc = new HeroClient();

            return hc.GetHeroesByAttributes(mainAttr).Result;
        }
        


        [HttpGet]
        [Route("/Player/Id/{Id}")]
        public playerModel GetPlayerById(double Id)
        {
            PlayerClient hc = new PlayerClient();

            return hc.GetPlayerByIdAsync(Id).Result;
        }

        [HttpGet]
        [Route("/Player/Name/{name}")]
        public playerModel GetPlayerByName(string name)
        {
            PlayerClient hc = new PlayerClient();

            return hc.GetPlayerByNameAsync(name).Result;
        }

        [HttpGet]
        [Route("/Team/Id/{Id}")]
        public teamModel GetTeamByName(double Id)
        {
            TeamClient hc = new TeamClient();

            return hc.GetTeamByIdAsync(Id).Result;
        }

        [HttpGet]
        [Route("/Team/Name/{name}")]
        public teamModel GetTeamByName(string name)
        {
            TeamClient hc = new TeamClient();

            return hc.GetTeamByNameAsync(name).Result;
        }

        [HttpGet]
        [Route("/Team/{teamName}/ActivePlayers")]
        public List<playerModel> GetActiveTeamMembers(string teamName)
        {
            TeamClient tc = new TeamClient();

            return tc.GetActiveTeamMembers(teamName).Result;
        }

        [HttpGet]
        [Route("/Team/All")]
        public List<string> GetAllTeams()
        {
            TeamClient tc = new TeamClient();

            return tc.GetAllTeamsAsync().Result;
        }

        [HttpPost]
        [Route("/favorites/{teamName}")]
        public string PostFavoriteTeam(string name)
        {
            FavClient fc = new FavClient();
            fc.PostFavouriteTeamAsync(name);

            return "Operation complete";
        }
        
        [HttpDelete]
        [Route("/favorites/{teamName}")]
        public string DeleteFavoriteTeam(string name)
        {
            FavClient fc = new FavClient();

            fc.DeleteFavoriteTeamAsync(name);
            return "Operation complete";
        }

        [HttpGet]
        [Route("/favorites/{teamName}")]
        public List<teamModel> FindFavoriteTeam()
        {
            FavClient fc = new FavClient();

            return fc.FindFavoriteTeamAsync().Result;
        }
    }
}
