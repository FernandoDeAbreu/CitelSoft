using CitelSoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CitelSoftware.Controllers
{
    public class CategoriaController : Controller
    {
        private Uri baseAddress = new Uri("https://localhost:7263/api");
        private readonly HttpClient _client;

        public CategoriaController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            List<CategoriaViewModel> categoriaList = new List<CategoriaViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/categoria/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                categoriaList = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(data);
            }

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                categoriaList = categoriaList.Where(s => s.Descricao.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "descricao_desc":
                    categoriaList = categoriaList.OrderByDescending(s => s.Descricao).ToList();
                    break;

                default:
                    categoriaList = categoriaList.OrderBy(s => s.Id).ToList();
                    break;
            }

            return View(categoriaList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoriaViewModel categoria)
        {
            try
            {
                string data = JsonConvert.SerializeObject(categoria);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/categoria/Cadastrar", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Categoria criado com sucesso";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;

                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                CategoriaViewModel categoria = new CategoriaViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/categoria/GetById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    categoria = JsonConvert.DeserializeObject<CategoriaViewModel>(data);
                }
                return View(categoria);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(CategoriaViewModel categoria)
        {
            try
            {
                string data = JsonConvert.SerializeObject(categoria);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/categoria/Atualizar", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Categoria atualizado com sucesso";
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                CategoriaViewModel categoria = new CategoriaViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/categoria/GetById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    categoria = JsonConvert.DeserializeObject<CategoriaViewModel>(data);
                }
                return View(categoria);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCofirmar(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/categoria/Deletar/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Categoria atualizado com sucesso";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}