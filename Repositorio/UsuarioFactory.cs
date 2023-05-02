using APISimples.DataBase;
using APISimples.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace APISimples.Repositorio
{
    public class UsuarioFactory : IUsuario
    {
        DataBaseContext _dbContext;

        static string ComputeMD5(string s)
        {
            using (MD5 md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(s)))
                            .Replace("-", "");
            }
        }

        public UsuarioFactory(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UsuarioModel> Add(UsuarioModel usuario)
        {
            usuario.password = ComputeMD5(usuario.password);
            await _dbContext.Usuarios.AddAsync(usuario);
            _dbContext.SaveChanges();

            return usuario;
        }

        public async Task<UsuarioModel> Delete(int id)
        {
            var usuario = await GetById(id);
            if (usuario == null) throw new Exception("Usuario não encontrado");
            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
            
        }

        public async Task<List<UsuarioModel>> GetAllUsers()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> GetById(int id)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.id == id);
            if (usuario == null) throw new Exception("Usuario não encontrado");
            return usuario;
        }

        public async Task<UsuarioModel> DoLogin(UsuarioModel user)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.username == user.username);
            if (usuario == null) throw new Exception("Usuario não encontrado");
            if (ComputeMD5(user.password) == usuario.password) return usuario;
            else throw new Exception("Senha Incorreta");
        }

        public async Task<UsuarioModel> Update(UsuarioModel usuario, int id)
        {
            var newUsuario = await GetById(id);
            if (newUsuario == null) throw new Exception("Usuario não encontrado");
            newUsuario.username = usuario.username;
            newUsuario.password = ComputeMD5(usuario.password);
            _dbContext.Usuarios.Update(newUsuario);
            await _dbContext.SaveChangesAsync();

            return newUsuario;
        }
    }
}
