using Api.Cliente.Interfaces;
using Api.Cliente.Models;
using System.Data;
using System.Data.SqlClient;

namespace Api.Cliente.Services
{
    public class ClienteService : IDisposable, IClienteService
    {
        private string _stringConexao = "Server=localhost;Database=BitPagg; User Id=sa; Password=Senha@123456;";
        private SqlConnection? _conexao
        {
            get
            {
                try
                {
                    var conexao = new SqlConnection(_stringConexao);

                    if (conexao.State == ConnectionState.Closed)
                    {
                        conexao.Open();
                    }

                    return conexao;
                }
                catch
                {
                    return null;
                }
            }
        }
        public void Dispose()
        {
            if (_conexao?.State == ConnectionState.Open)
            {
                _conexao.Dispose();
            }
        }
        public bool Cadastrar(ClienteModel cliente)
        {
            try
            {
                string stringSql = "INSERT INTO cliente (cpf, nome, sobrenome, datadenascimento) " +
                                   "VALUES (@cpf, @nome, @sobrenome, @datadenascimento);";

                SqlCommand sqlCommand = new SqlCommand(stringSql);
                sqlCommand.Parameters.AddWithValue("@cpf", cliente.Cpf);
                sqlCommand.Parameters.AddWithValue("@nome", cliente.Nome);
                sqlCommand.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
                sqlCommand.Parameters.AddWithValue("@datadenascimento", cliente.DataDeNascimento);

                sqlCommand.Connection = _conexao;
                if (sqlCommand.ExecuteNonQuery() > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                _conexao?.Dispose();
            }
        }

        public bool Excluir(string cpf)
        {
            try
            {
                string stringSql = "DELETE FROM cliente WHERE cpf = @cpf;";

                SqlCommand sqlCommand = new SqlCommand(stringSql);
                sqlCommand.Parameters.AddWithValue("@cpf", cpf);

                sqlCommand.Connection = _conexao;
                if (sqlCommand.ExecuteNonQuery() > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                _conexao?.Dispose();
            }
        }

        public List<ClienteModel>? BuscarTodos()
        {
            try
            {
                string stringSql = "SELECT * FROM cliente;";

                SqlCommand sqlCommand = new SqlCommand(stringSql);

                sqlCommand.Connection = _conexao;
                SqlDataReader sdr = sqlCommand.ExecuteReader();

                if (sdr.HasRows == false)
                    return null;
                else
                {
                    var retorno = new List<ClienteModel>();
                    while (sdr.Read())
                    {
                        ClienteModel clienteModel = new ClienteModel()
                        {
                            Cpf = sdr["cpf"].ToString(),
                            Nome = sdr["nome"].ToString(),
                            Sobrenome = sdr["sobrenome"].ToString(),
                            DataDeNascimento = DateTime.Parse(sdr["datadenascimento"]?.ToString()!)
                        };
                        retorno.Add(clienteModel);
                    }
                    return retorno;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                _conexao?.Dispose();
            }
        }

        public ClienteModel? Buscar(string cpf)
        {
            try
            {
                string stringSql = "SELECT * FROM cliente WHERE cpf = @cpf;";

                SqlCommand sqlCommand = new SqlCommand(stringSql);
                sqlCommand.Parameters.AddWithValue("@cpf", cpf);

                sqlCommand.Connection = _conexao;
                SqlDataReader sdr = sqlCommand.ExecuteReader();

                if (sdr.HasRows == false)
                    return null;
                else
                {
                    ClienteModel? clienteModel = null;
                    while (sdr.Read())
                    {
                        clienteModel = new ClienteModel()
                        {
                            Cpf = sdr["cpf"].ToString(),
                            Nome = sdr["nome"].ToString(),
                            Sobrenome = sdr["sobrenome"].ToString(),
                            DataDeNascimento = DateTime.Parse(sdr["datadenascimento"]?.ToString()!)
                        };
                    }
                    return clienteModel;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                _conexao?.Dispose();
            }
        }

        public bool Alterar(string cpf, ClienteModel cliente)
        {
            try
            {
                string stringSql = "UPDATE cliente SET nome = @nome, sobrenome = @sobrenome, datadenascimento = @datadenascimento WHERE cpf = @cpf;";

                SqlCommand sqlCommand = new SqlCommand(stringSql);
                sqlCommand.Parameters.AddWithValue("@nome", cliente.Nome);
                sqlCommand.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
                sqlCommand.Parameters.AddWithValue("@datadenascimento", cliente.DataDeNascimento);
                sqlCommand.Parameters.AddWithValue("@cpf", cpf);

                sqlCommand.Connection = _conexao;
                if (sqlCommand.ExecuteNonQuery() > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                _conexao?.Dispose();
            }
        }
    }
}