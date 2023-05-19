using APISimples.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace APISimples.Controllers
{
    public class WebSocketController : ControllerBase
    {

        private readonly WSocketHandler _webSocketHandler;

        public WebSocketController(WSocketHandler ws) 
        {
            _webSocketHandler = ws;
        }

        [Route("/ws")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest){
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                int userId = int.Parse(HttpContext.Request.Query["userId"]);
                _webSocketHandler.AdicionarSocket(userId, webSocket);
                await Echo(webSocket, userId);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task Echo(WebSocket webSocket, int id)
        {
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
               var data = Encoding.ASCII.GetBytes("ping");

                await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);

                await Task.Delay(30000);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            _webSocketHandler.RemoverSocket(id);

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
