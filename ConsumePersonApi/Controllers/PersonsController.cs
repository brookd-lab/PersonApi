using Microsoft.AspNetCore.Mvc;
using PersonApiDAL.Models;
using PersonApiDAL.Services.ConsumeApiPersonService;

namespace ConsumePersonApi.Controllers
{
    public class PersonsController : Controller
    {
        HttpClient _client;
        private readonly PersonService _personService;

        public PersonsController(
            IConfiguration config,
            PersonService personService)
        {
            var handler = new HttpClientHandler();
            _client = new HttpClient(handler);
            _personService = personService;
            
            _personService._url = config["BaseUrl"]!;
        }

        public async Task<IActionResult> Index()
        {
            var persons = await _personService.GetAllPeople();
            return View(persons);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var person = await _personService.GetPerson(id);
            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                await _personService.CreatePerson(person);

                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var person = await _personService.GetPerson(id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                await _personService.UpdatePerson(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var person = await _personService.GetPerson(id);
            return View(person);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personService.DeletePerson(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
