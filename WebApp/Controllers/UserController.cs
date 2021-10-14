using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<User> listOfUsers = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44392/api/User"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listOfUsers = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }

            return View("Index",listOfUsers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            User user = new User();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44392/api/User/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Error!");
                return View("Edit", user);
            }

            StringContent userContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            User updatedUser = new User();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:44392/api/User/" + user.IdUser, userContent))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        updatedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                        ViewBag.StatusCode = response.StatusCode; 
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return View("Edit", updatedUser); 
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44392/api/User/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        ViewBag.Result = "Delete successful!";
                    }
                    else
                    {
                        ViewBag.Result = "Delete unsuccessful!";
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {

            StringContent userContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            User newUser = new User();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:44392/api/User", userContent))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        newUser = JsonConvert.DeserializeObject<User>(apiResponse);
                        ViewBag.StatusCode = response.StatusCode;
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return View("Create", newUser);
        }
    }
}
