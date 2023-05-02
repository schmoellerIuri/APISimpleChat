using APISimples.Models;

namespace APISimples.Repositorio
{
    public interface IMensagem
    {
        public Task<List<MensagemModel>> GetMensagens(int idConversa);
        public Task<MensagemModel> EnviarMensagem(MensagemModel mensagem);
        public Task<MensagemModel> DeletarMensagem(int idMensagem);
    }
}
