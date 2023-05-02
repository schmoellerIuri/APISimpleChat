using APISimples.Models;
using APISimples.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISimples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        public IMensagem _MensagemFactory;

        public MensagemController(IMensagem mensagemFactory) 
        {
            _MensagemFactory = mensagemFactory;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<MensagemModel>>> GetMessages(int id)
        {
            try
            {
                List<MensagemModel> mensagens = await _MensagemFactory.GetMensagens(id);
                return Ok(mensagens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }   
        }

        [HttpPost]
        public async Task<ActionResult<MensagemModel>> SendMessage([FromBody] MensagemModel mensagem) 
        {
            try
            {
                var message = await _MensagemFactory.EnviarMensagem(mensagem);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
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
