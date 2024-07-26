using APISimples.Models;
using APISimples.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                var userIdFromJWt = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);

                if (userIdFromJWt != id)
                    return Unauthorized();
                

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
                var conversa = await _ConversaFactory.GetConversa(id);
                var userIdFromJWt = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);

                if(conversa.idUser1 != userIdFromJWt && conversa.idUser2 != userIdFromJWt)
                    return Unauthorized();

                _ = await _ConversaFactory.DeletarConversa(id);
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
                var userIdFromJWt = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);
                if(userIdFromJWt != conversaModel.idUser1 && userIdFromJWt != conversaModel.idUser2)
                    return Unauthorized();

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
