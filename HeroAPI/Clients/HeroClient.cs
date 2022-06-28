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
    public class HeroClient
    {
        private HttpClient _client = new HttpClient();
        private static string _address;
        public HeroClient()
        {
            _address = Constants.Address + "/heroStats";
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }

        public async Task<heroModel> GetHeroByIdAsync(int Id)
        {
            var responce = await _client.GetAsync(_address);
            var heroList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<heroModel>>(heroList);

            foreach (heroModel item in result)
            {
                if (item.id == Id)
                {
                    return item;
                }
            }
            return new heroModel() { localized_name = "Hero not found" };
        }

        public async Task<heroModel> GetHeroByNameAsync(string name)
        {
            var responce = await _client.GetAsync(_address);
            var heroList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<heroModel>>(heroList);

            foreach (heroModel item in result)
            {
                if (item.localized_name == name || item.localized_name.ToLower() == name)
                {
                    return item;
                }
            }
            return new heroModel() { localized_name = "Hero not found" };
        }

        public async Task<List<string>> GetHeroesByAttributes(string MainAttr)
        {
            var responce = await _client.GetAsync(_address);
            var heroList = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<heroModel>>(heroList);

            List<string> strHeroes = new List<string>();

            foreach (heroModel item in result)
            {
                if (item.primary_attr == MainAttr)
                {
                    strHeroes.Add(item.localized_name);
                }
            }
            return strHeroes; 
        }
    }
}
