using Data.Repository;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FASistemas.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServices _categoriaServices;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaServices categoriaServices, ICategoriaRepository categoriaRepository)
        {
            _categoriaServices = categoriaServices;
            _categoriaRepository = categoriaRepository;
        }

        [HttpPost]
        public async Task<object> Cadastrar(Categoria categoria)
        {
            await _categoriaRepository.Add(categoria);
            return categoria;
        }

        [HttpGet("{id}")]
        public async Task<object> GetById(int id)
        {
            return await _categoriaRepository.GetEntityById(id);
        }

        [HttpGet]
        public async Task<object> GetAll()
        {
            return await _categoriaRepository.List();
        }

        [HttpPut]
        public async Task<object> Atualizar(Categoria categoria)
        {
            await _categoriaRepository.Update(categoria);

            return Task.FromResult(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var produto = await _categoriaRepository.GetEntityById(id);

            if (produto is null)
            {
                return NotFound("Categoria não localizado...");
            }

            await _categoriaRepository.Delete(produto);

            return Ok(produto);
        }
    }
}