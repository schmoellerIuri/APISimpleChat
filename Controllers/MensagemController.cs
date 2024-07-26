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
    public class MensagemController : ControllerBase
    {
        public IMensagem _MensagemFactory;
        public IConversa _ConversaFactory;

        public MensagemController(IMensagem mensagemFactory, IConversa conversaFactory)
        {
            _MensagemFactory = mensagemFactory;
            _ConversaFactory = conversaFactory;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<MensagemModel>>> GetMessages(int id)
        {
            try
            {
                var conversa = await _ConversaFactory.GetConversa(id);
                var userIdFromJWt = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);

                if(conversa.idUser1 != userIdFromJWt && conversa.idUser2 != userIdFromJWt)
                    return Unauthorized();

                List<MensagemModel> mensagens = await _MensagemFactory.GetMensagens(id);
                

                return Ok(mensagens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }   
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MensagemModel>> SendMessage([FromBody] MensagemModel mensagem) 
        {
            try
            {
                var conversa = await _ConversaFactory.GetConversa(mensagem.conversaId);
                var userIdFromJWt = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);

                if(conversa.idUser1 != userIdFromJWt && conversa.idUser2 != userIdFromJWt)
                    return Unauthorized();

                var message = await _MensagemFactory.EnviarMensagem(mensagem);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<MensagemModel>> DeleteMessage(int id) 
        {
            try
            {
                var message = await _MensagemFactory.DeletarMensagem(id);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
