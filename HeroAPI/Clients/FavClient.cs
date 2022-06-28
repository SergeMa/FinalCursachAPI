using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using CursachAPI.Model;
using CursachAPI.Constant;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CursachAPI.Clients
{
    public class FavClient
    {

        public string personName = "SergiyMak";

        public async Task PostFavouriteTeamAsync(string name)
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://SergiyMak:Mak2506serg@cluster0.dvqbr9x.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(personName);
            string collectionName = "favTeams";

            TeamClient tc = new TeamClient();

            var collection = database.GetCollection<teamModel>(collectionName);

            teamModel favTeam = tc.GetTeamByNameAsync(name).Result;
            var filter = Builders<teamModel>.Filter.Eq("team_id", favTeam.team_id);
            bool exists = await collection.Find(_ => _.team_id == favTeam.team_id).AnyAsync(); ;
            await collection.InsertOneAsync(favTeam);
            if (!exists && favTeam.team_id != 0)
            {
                await collection.InsertOneAsync(favTeam);
            }
        }

        public async Task DeleteFavoriteTeamAsync(string name)
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://SergiyMak:Mak2506serg@cluster0.dvqbr9x.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(personName);
            string collectionName = "favTeams";

            TeamClient tc = new TeamClient();
            teamModel favTeam = tc.GetTeamByNameAsync(name).Result;
            var collection = database.GetCollection<favTeamModel>(collectionName);
            var filter = Builders<favTeamModel>.Filter.Eq("team_id", favTeam.team_id);
            await collection.DeleteOneAsync(filter);
        }

        public async Task<List<teamModel>> FindFavoriteTeamAsync()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://SergiyMak:Mak2506serg@cluster0.dvqbr9x.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(personName);
            string collectionName = "favTeams";
            TeamClient tc = new TeamClient();
            var collection = database.GetCollection<favTeamModel>(collectionName);
            var result = await collection.FindAsync(_ => true);

            List<teamModel> res = new List<teamModel>();

            foreach (var item in result.ToList())
            {
                res.Add(tc.GetTeamByNameAsync(item.tag).Result);
            }
            return res;
        }
    }
}
