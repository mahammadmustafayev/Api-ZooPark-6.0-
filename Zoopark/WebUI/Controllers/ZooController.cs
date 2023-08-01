using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebUI.Models;

namespace WebUI.Controllers;

public class ZooController : Controller
{


    Uri baseUrl = new("https://localhost:7182/api/ZooPark");
    private readonly HttpClient _httpClient;

    public ZooController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseUrl;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<AnimalViewModel> animals = new();
        HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/All").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            animals = JsonConvert.DeserializeObject<List<AnimalViewModel>>(data);
        }

        return View(animals);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(AnimalViewModel animal)
    {
        try
        {
            string data = JsonConvert.SerializeObject(animal);
            StringContent content = new(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        try
        {
            AnimalViewModel animal = new();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Details/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                animal = JsonConvert.DeserializeObject<AnimalViewModel>(data);
            }
            return View(animal);
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }



    [HttpPost]
    public IActionResult Edit(AnimalViewModel animal)
    {
        try
        {
            string data = JsonConvert.SerializeObject(animal);
            StringContent content = new(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Update", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception)
        {

            return BadRequest();
        }
        return View(animal);

    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        try
        {
            AnimalViewModel animal = new();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Details/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                animal = JsonConvert.DeserializeObject<AnimalViewModel>(data);
            }
            return View(animal);
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
            AnimalViewModel animal = new();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Details/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                animal = JsonConvert.DeserializeObject<AnimalViewModel>(data);
            }
            return View(animal);
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }
    [HttpPost, ActionName(nameof(Delete))]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception)
        {

            return BadRequest();
        }
        return View();
    }

}
