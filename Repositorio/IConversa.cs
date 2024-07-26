using APISimples.Models;

namespace APISimples.Repositorio
{
    public interface IConversa
    {
        Task<List<ConversaModel>> GetConversas(int idUser1);
        Task<ConversaModel> CriarConversa(ConversaModel conversa);
        Task<ConversaModel> DeletarConversa(int idConversa);
        Task <ConversaModel> GetConversa(int idConversa);
    }
}
