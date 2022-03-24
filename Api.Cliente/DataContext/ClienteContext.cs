
using Api.Cliente.Models;
using System.Data.SqlClient;

namespace Api.Cliente.DataContext
{
    public class Conexao
    {
        SqlConnection sql = new SqlConnection();
        public Conexao()
        {
            sql.ConnectionString = ("Server=localhost;Database=BitPagg; User Id=sa; Password=Senha@123456;");
        }

        public Task<IEnumerable<ClienteModel>> ClienteModel { get; internal set; }

        public SqlConnection Conectar()
        {
            if (sql.State == System.Data.ConnectionState.Closed)
            {
                sql.Open();
            }
            return sql;
        }

        public void desconectar()
        {
            if (sql.State ==System.Data.ConnectionState.Open)
            {
                sql.Close();
            }
        }
             
    }
}