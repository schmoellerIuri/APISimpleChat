using APISimples.DataBase;
using APISimples.Models;
using Microsoft.EntityFrameworkCore;

namespace APISimples.Repositorio
{
    public class MensagemFactory : IMensagem
    {
        DataBaseContext _dbContext;

        public MensagemFactory(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
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
