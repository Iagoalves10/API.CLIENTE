using Microsoft.AspNetCore.Mvc;
using Api.Cliente.Models;
using System.Data.SqlClient;
using Api.Cliente.Services;
using Microsoft.EntityFrameworkCore;
using Api.Cliente.Interfaces;

namespace Api.Cliente.Controllers
{
    [ApiController]
    [Route("")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("/api/v1/cliente")]
        public ActionResult<ClienteModel> BuscarTodosClientes()
        {
            try
            {
                var clientes = _clienteService.BuscarTodos();
                if (clientes != null && clientes.Count > 0)
                    return Ok(clientes);

                else
                    return BadRequest("Nenhum cliente encontrado!");
            }
            catch
            {
                return BadRequest("Nenhum cliente encontrado!");
            }
        }

        [HttpGet]
        [Route("/api/v1/cliente/{cpf}")]
        public ActionResult<ClienteModel> BuscarCliente(string cpf)
        {
            try
            {
                var cliente = _clienteService.Buscar(cpf);
                if (cliente != null)
                    return Ok(cliente);

                else
                    return BadRequest("Cliente não encontrado!");
            }
            catch
            {
                return BadRequest("Cliente não encontrado!");
            }
        }

        [HttpPost]
        [Route("/api/v1/cliente")]
        public ActionResult<ClienteModel> CadastrarCliente([FromBody] ClienteModel cliente)
        {
            try
            {
                if (_clienteService.Cadastrar(cliente) == true)
                    return CreatedAtAction(null,"Cliente Cadastrado com suceeso!");

                else
                    return BadRequest("Erro ao Cadastrar cliente!");
            }
            catch
            {
                return BadRequest("Erro ao Cadastrar cliente!");
            }

        }

        [HttpDelete]
        [Route("/api/v1/cliente/{cpf}")]
        public ActionResult<ClienteModel> ExcluirCliente(string cpf)
        {
            try
            {
                if (_clienteService.Excluir(cpf) == true)
                    return Ok("Cliente Excluído com suceeso!");

                else
                    return BadRequest("Erro ao Excluir cliente!");
            }
            catch
            {
                return BadRequest("Erro ao Excluir cliente!");
            }
        }

        [HttpPut]
        [Route("/api/v1/cliente/{cpf}")]
        public ActionResult<ClienteModel> AlterarCliente(string cpf, [FromBody] ClienteModel cliente)
        {
            try
            {
                if (_clienteService.Alterar(cpf, cliente) == true)
                    return Ok("Cliente Alterado com suceeso!");

                else
                    return BadRequest("Erro ao Alterar cliente!");
            }
            catch
            {
                return BadRequest("Erro ao Alterar cliente!");
            }
        }

    }
}

