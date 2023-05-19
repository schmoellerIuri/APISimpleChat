using APISimples.DataBase;
using APISimples.Models;
using Microsoft.EntityFrameworkCore;

namespace APISimples.Repositorio
{
    public class MensagemFactory : IMensagem
    {
        DataBaseContext _dbContext;
        private readonly WSocketHandler _webSocketHandler;

        public MensagemFactory(DataBaseContext dbContext, WSocketHandler webSocketHandler)
        {
            _dbContext = dbContext;
            _webSocketHandler = webSocketHandler;
        }

        public async Task<MensagemModel> DeletarMensagem(int idMensagem)
        {
            var mensagem = await _dbContext.Mensagens.FirstOrDefaultAsync(m => m.id == idMensagem);
            if (mensagem == null)
                throw new Exception("Mensagem não encontrada");

            _dbContext.Mensagens.Remove(mensagem);
            await _dbContext.SaveChangesAsync();

            return mensagem;
        }

        public async Task<MensagemModel> EnviarMensagem(MensagemModel mensagem)
        {
            await _dbContext.Mensagens.AddAsync(mensagem);
            _dbContext.SaveChanges();

            var conversa = _dbContext.Conversas.FirstOrDefault(x => x.id == mensagem.conversaId);

            if(_webSocketHandler.UserHasSocket(conversa.idUser1))
                await _webSocketHandler.SendMessage(mensagem, conversa.idUser1);

            if (_webSocketHandler.UserHasSocket(conversa.idUser2))
                await _webSocketHandler.SendMessage(mensagem, conversa.idUser2);

            return mensagem;
        }

        public async Task<List<MensagemModel>> GetMensagens(int idConversa)
        {
            List<MensagemModel> mensagens = await _dbContext.Mensagens.Where(c => c.conversaId == idConversa).ToListAsync();
            if (mensagens == null) throw new Exception("Ainda não existem mensagens nessa conversa");

            return mensagens;
        }
    }
}
