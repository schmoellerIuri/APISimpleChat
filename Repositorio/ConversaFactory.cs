using APISimples.DataBase;
using APISimples.Models;
using Microsoft.EntityFrameworkCore;

namespace APISimples.Repositorio
{
    public class ConversaFactory : IConversa
    {
        DataBaseContext _dbContext;

        public ConversaFactory(DataBaseContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<ConversaModel> CriarConversa(ConversaModel conversa)
        {
            if (!_dbContext.Usuarios.Any(u => u.id == conversa.idUser1) || !_dbContext.Usuarios.Any(u => u.id == conversa.idUser2))
                throw new Exception("Usuario inexistente");

            if (_dbContext.Conversas.Any(c => (c.idUser1 == conversa.idUser1 && c.idUser2 == conversa.idUser2) || (c.idUser1 == conversa.idUser2 && c.idUser2 == conversa.idUser1)))
                throw new Exception("Conversa já existente");
            await _dbContext.AddAsync(conversa);
            _dbContext.SaveChanges();

            return conversa;
        }

        public async Task<ConversaModel> DeletarConversa(int idConversa)
        {
            var conversa = await _dbContext.Conversas.FirstOrDefaultAsync(c => c.id == idConversa);
            if (conversa == null) throw new Exception("Conversa não identificada");
            foreach (var mensagem in _dbContext.Mensagens.Where(m => m.conversaId == conversa.id)) 
            {
                _dbContext.Mensagens.Remove(mensagem);
                await _dbContext.SaveChangesAsync();
            }

            _dbContext.Conversas.Remove(conversa);
            await _dbContext.SaveChangesAsync();

            return conversa;
        }

        public async Task<List<ConversaModel>> GetConversas(int idUser)
        {
            List<ConversaModel> conversas = await _dbContext.Conversas.Where(c => c.idUser1 == idUser || c.idUser2 == idUser).ToListAsync();

            return conversas ?? throw new Exception("Não exitem conversas ativas no momento");
        }

        public async Task<ConversaModel> GetConversa(int id){
            var conversa  = await _dbContext.Conversas.FirstOrDefaultAsync(c => c.id == id);

            return conversa ?? throw new Exception("Conversa inexistente");
        }
    }
}
