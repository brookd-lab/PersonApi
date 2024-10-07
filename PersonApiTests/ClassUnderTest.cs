using Azure;
using Newtonsoft.Json;
using PersonApiDAL.Models;
using PersonApiDAL.Services.ConsumeApiPersonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PersonApiTests
{
    public class ClassUnderTest
    {
        private readonly HttpClient _httpClient;
        private const string Url = "https://myurl";
        private readonly PersonService _personService;

        public ClassUnderTest(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _personService = new PersonService() { _url = Url };
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{Url}?id={id}");
            return await response.Content.ReadFromJsonAsync<Person>();

            //var person = await _personService.GetPerson(id);
            //return person;
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            var url = $@"{Url}/GetAllPeople";
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<List<Person>>(); ;
        }

     
        public async Task CreatePersonAsync(Person person)
        {
            var url = $@"{Url}/CreatePerson";
            var response = await _httpClient.PostAsJsonAsync(url, person);
        }

        public async Task UpdatePersonAsync(Person person)
        {
            var url = $@"{Url}/UpdatePerson";
            await _httpClient.PutAsJsonAsync(url, person);
        }

        public async Task DeletePersonAsync(int id)
        {
            var url = $@"{Url}/DeletePerson/{id}";
            await _httpClient.DeleteAsync(url);
        }
    }
}
