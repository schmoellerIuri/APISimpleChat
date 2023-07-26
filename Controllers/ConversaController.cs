using APISimples.Models;
using APISimples.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISimples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversaController : ControllerBase
    {
        public IConversa _ConversaFactory;

        public ConversaController(IConversa ConversaFactory) 
        {
            _ConversaFactory = ConversaFactory;        
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<ConversaModel>>> GetConversasByUser(int id) 
        {
            try 
            {
                List<ConversaModel> conversas = await _ConversaFactory.GetConversas(id);

                return Ok(conversas);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ConversaModel>> DeletarConversa(int id) 
        {
            try
            {
                var conversa = await _ConversaFactory.DeletarConversa(id);
                return Ok(conversa);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);   
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ConversaModel>> AddConversa([FromBody] ConversaModel conversaModel) 
        {
            try
            {
                var conversa = await _ConversaFactory.CriarConversa(conversaModel);
                return Ok(conversa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
