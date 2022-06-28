using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using CursachAPI.Model;
using CursachAPI.Constant;
namespace CursachAPI.Clients
{
    public class TeamClient
    {
        private HttpClient _client = new HttpClient();
        private static string _address;
        public TeamClient()
        {
            _address = Constants.Address + "/teams";
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }

        public async Task<teamModel> GetTeamByIdAsync(double Id)
        {
            var responce = await _client.GetAsync(_address);
            var playerList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<teamModel>>(playerList);

            foreach (teamModel item in result)
            {
                if (item.team_id == Id)
                {
                    return item;
                }
            }
            return new teamModel() { name = "Team not found" };
        }

        public async Task<teamModel> GetTeamByNameAsync(string name)
        {
            var responce = await _client.GetAsync(_address);
            var heroList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<teamModel>>(heroList);

            foreach (teamModel item in result)
            {
                if (item.name == name || item.tag == name || item.name.ToLower() == name || item.tag.ToLower() == name)
                {
                    return item;
                }
            }
            return new teamModel() { name = "Team not found" };
        }

        public async Task<List<playerModel>> GetActiveTeamMembers(string name)
        {
            List<playerModel> activePlayersList = new List<playerModel>();
            teamModel currentTeam = GetTeamByNameAsync(name).Result;

            var responce = await _client.GetAsync($"{_address}/{currentTeam.team_id}/players");
            var playerList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<IsActivePlayer>>(playerList);

            foreach (IsActivePlayer item in result)
            {
                if (item.is_current_team_member == "true")
                {
                    PlayerClient pc = new PlayerClient();
                    activePlayersList.Add(pc.GetPlayerByIdAsync(item.account_id).Result);
                }
            }
            return activePlayersList;
        }

        public async Task<List<string>> GetAllTeamsAsync()
        {
            var responce = await _client.GetAsync(_address);
            var heroList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<teamModel>>(heroList);
            List<string> answer = new List<string>();
            foreach (var item in result)
            {
                answer.Add(item.name);
            }
            return answer;
        }

        class IsActivePlayer
        {
            public string is_current_team_member { get; set; }
            public double account_id { get; set; }
        }
    }
}
