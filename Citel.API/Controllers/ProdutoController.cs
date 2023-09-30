using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FASistemas.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoServices _produtoServices;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoServices produtoServices, IProdutoRepository produtoRepository)
        {
            _produtoServices = produtoServices;
            _produtoRepository = produtoRepository;
        }

        [HttpPost]
        public async Task<object> Cadastrar(Produto produto)
        {
            await _produtoRepository.Add(produto);
            return produto;
        }

        [HttpGet("{id}")]
        public async Task<object> GetById(int id)
        {
            return await _produtoRepository.GetEntityById(id);
        }

        [HttpGet]
        public async Task<object> GetAll()
        {
            return await _produtoRepository.List();
        }

        [HttpPut]
        public async Task<object> Atualizar(Produto produto)
        {
            await _produtoRepository.Update(produto);

            return Task.FromResult(produto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var produto = await _produtoRepository.GetEntityById(id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }

            await _produtoRepository.Delete(produto);

            return Ok(produto);
        }
    }
}