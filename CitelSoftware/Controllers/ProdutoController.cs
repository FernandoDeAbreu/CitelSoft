using CitelSoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CitelSoftware.Controllers
{
    public class ProdutoController : Controller
    {
        private Uri baseAddress = new Uri("https://localhost:7263/api");
        private readonly HttpClient _client;

        public ProdutoController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            List<ProdutoViewModel> produtoList = new List<ProdutoViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/produto/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                produtoList = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(data);
            }

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                produtoList = produtoList.Where(s => s.Descricao.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "descricao_desc":
                    produtoList = produtoList.OrderByDescending(s => s.Descricao).ToList();
                    break;

                default:
                    produtoList = produtoList.OrderBy(s => s.Id).ToList();
                    break;
            }

            return View(produtoList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProdutoViewModel produto)
        {
            try
            {
                string data = JsonConvert.SerializeObject(produto);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/produto/Cadastrar", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Produto criado com sucesso";
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
                ProdutoViewModel produto = new ProdutoViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/produto/GetById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(data);
                }
                return View(produto);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(ProdutoViewModel produto)
        {
            try
            {
                string data = JsonConvert.SerializeObject(produto);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/produto/Atualizar", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Produto atualizado com sucesso";
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
                ProdutoViewModel produto = new ProdutoViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/produto/GetById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(data);
                }
                return View(produto);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/produto/Deletar/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Produto atualizado com sucesso";
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