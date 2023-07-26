using APISimples.Models;

namespace APISimples.Repositorio
{
    public interface IUsuario
    {
        Task<List<UsuarioModel>> GetAllUsers();
        Task<UsuarioModel> GetById(int id);
        Task<UsuarioModel> Delete(int id);
        Task<UsuarioModel> Add(UsuarioModel usuario);
        Task<UsuarioModel> Update(UsuarioModel usuario, int id);
        Task<dynamic> DoLogin(UsuarioModel usuario);
    }
}
