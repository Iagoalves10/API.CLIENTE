using Microsoft.AspNetCore.Mvc;
using Api.Cliente.Models;
using System.Data.SqlClient;
using Api.Cliente.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Api.Cliente.Controllers
{
    [ApiController]
    [Route("")]
    public class ClienteController : ControllerBase
    {
        Conexao conexao = new Conexao();
        SqlCommand sqlCommand = new SqlCommand();
        public string mensagem = "";
         

        [HttpPost]
        [Route("/api/v1/clientes")]
        public ActionResult<ClienteModel> AdicionarCliente([FromBody] ClienteModel cliente)
        {
            sqlCommand.CommandText = "INSERT INTO cliente (cpf, nome, sobrenome, datadenascimento)" +
                 "VALUES (@cpf, @nome, @sobrenome, @datadenascimento )";

            sqlCommand.Parameters.AddWithValue("@cpf", cliente.Cpf);
            sqlCommand.Parameters.AddWithValue("@nome", cliente.Nome);
            sqlCommand.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
            sqlCommand.Parameters.AddWithValue("@datadenascimento", cliente.DataDeNascimento);

            try
            {
                sqlCommand.Connection = conexao.Conectar();
                sqlCommand.ExecuteNonQuery();
                conexao.desconectar();
                this.mensagem = "Cliente Cadastrado!!";
            }
            catch (SqlException ex)
            {
                this.mensagem = "Erro ao cadastrar cliente";
            }

            return cliente;
        }

        [HttpDelete]
        [Route("/api/v1/clientes/{cpf}")]
        public ActionResult<ClienteModel> ExcluirCliente(string cpf)
        {
            object parametro = cpf;
            sqlCommand.CommandText = "DELETE FROM cliente WHERE cpf = @cpf ;";
            object parametrosSql = new { @cpf = cpf };

            try
            {
                sqlCommand.Connection = conexao.Conectar();
                sqlCommand.ExecuteNonQuery();
                conexao.desconectar();
                this.mensagem = "Cliente Cadastrado!!";
            }
            catch (SqlException ex)
            {
                this.mensagem = "Erro ao cadastrar cliente";
            }

            return Ok(cpf);
         }
    }
}


