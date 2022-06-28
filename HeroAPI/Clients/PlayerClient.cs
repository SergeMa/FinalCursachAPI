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
    public class PlayerClient
    {
        private HttpClient _client = new HttpClient();
        private static string _address;
        public PlayerClient()
        {
            _address = Constants.Address + "/proPlayers";
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }

        public async Task<playerModel> GetPlayerByIdAsync(double Id)
        {
            var responce = await _client.GetAsync(_address);
            var playerList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<playerModel>>(playerList);

            foreach (playerModel item in result)
            {
                if (item.account_id == Id)
                {
                    return item;
                }
            }
            return new playerModel() { name = "Player not found" };
        }

        public async Task<playerModel> GetPlayerByNameAsync(string name)
        {
            var responce = await _client.GetAsync(_address);
            var heroList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<playerModel>>(heroList);

            foreach (playerModel item in result)
            {
                if (item.personaname == name || item.name == name || item.personaname.ToLower() == name || item.name.ToLower() == name)
                {
                    return item;
                }
            }
            return new playerModel() { name = "Player not found" };
        }
    }
}

