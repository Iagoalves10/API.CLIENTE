using Api.Cliente.Models;

namespace Api.Cliente.Interfaces
{
    public interface IClienteService
    {
        bool Cadastrar(ClienteModel cliente);

        bool Excluir(string cpf);

        bool Alterar(string cpf, ClienteModel cliente);

        List<ClienteModel>? BuscarTodos();

        ClienteModel? Buscar(string cpf);
    }
}
