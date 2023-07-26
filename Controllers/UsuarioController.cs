using APISimples.Models;
using APISimples.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISimples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public IUsuario _UsuarioFactory;

        public UsuarioController(IUsuario factory)
        {
            _UsuarioFactory = factory;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UsuarioModel>>> GetUsers()
        {
            List<UsuarioModel> usuarios = await _UsuarioFactory.GetAllUsers();
            usuarios.ForEach(u => u.password = "");
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioModel>> GetUserById(int id)
        {
            var usuario = await _UsuarioFactory.GetById(id);
            return Ok(usuario);
        }

        [HttpDelete("/{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioModel>> DeleteUser(int id)
        {
            var usuario = await _UsuarioFactory.Delete(id);
            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UsuarioModel user)
        {
            try
            {
                var response = await _UsuarioFactory.DoLogin(user);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> AddUser([FromBody] UsuarioModel user)
        {
            try
            {
                var usuario = await _UsuarioFactory.Add(user);
                return Ok(usuario.id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UsuarioModel>> Update([FromBody] UsuarioModel user)
        {
            try
            {
                var usuario = await _UsuarioFactory.Update(user, user.id);
                return Ok(usuario.id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
