using APISimples.DataBase;
using APISimples.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using APISimples.Services;

namespace APISimples.Repositorio
{
    public class UsuarioFactory : IUsuario
    {
        DataBaseContext _dbContext;
        TokenService _tokenService;
        WSocketHandler _wsHandler;

        static string ComputeMD5(string s)
        {
            using (MD5 md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(s)))
                            .Replace("-", "");
            }
        }

        public bool UsuarioInvalido(UsuarioModel usuario)
        {
            return usuario == null || String.IsNullOrEmpty(usuario.password) || String.IsNullOrEmpty(usuario.username) || _dbContext.Usuarios.FirstOrDefault(x => x.username== usuario.username) != null;
        }

        public UsuarioFactory(DataBaseContext dbContext, TokenService tokenService, WSocketHandler wsHandler)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _wsHandler = wsHandler;
        }

        public async Task<UsuarioModel> Add(UsuarioModel usuario)
        {
            if (UsuarioInvalido(usuario)) throw new Exception("Credenciais invalidas");
            
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

        public async Task<dynamic> DoLogin(UsuarioModel user)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.username == user.username);
            if (usuario == null) throw new Exception("Usuario não encontrado");
            if (_wsHandler.UserHasSocket(usuario.id)) throw new Exception("Usuario já está logado");
            var id = usuario.id;
            var token = _tokenService.GenerateToken(usuario);
            if (ComputeMD5(user.password) == usuario.password) return new { id = id, token = token };
            else throw new Exception("Senha Incorreta");
        }

        public async Task<UsuarioModel> Update(UsuarioModel usuario, int id)
        {
            var newUsuario = await GetById(id);
            newUsuario.username = usuario.username;
            newUsuario.password = ComputeMD5(usuario.password);
            _dbContext.Usuarios.Update(newUsuario);
            await _dbContext.SaveChangesAsync();

            return newUsuario ?? throw new Exception("Usuario não encontrado");
        }
    }
}
