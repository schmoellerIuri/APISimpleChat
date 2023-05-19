using APISimples.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace APISimples.Repositorio
{
    public class WSocketHandler
    {
        private static Dictionary<int, WebSocket> connectedSockets = new Dictionary<int, WebSocket>();


        public void AdicionarSocket(int id, WebSocket ws)
        {
            connectedSockets.Add(id, ws);
        }

        public void RemoverSocket(int id)
        {
            connectedSockets.Remove(id);
        }

        public bool UserHasSocket(int id)
        {
            return connectedSockets.ContainsKey(id);
        }


        public async Task SendMessage(MensagemModel msg ,int userId) 
        {
            WebSocket ws = connectedSockets[userId];

            try
            {
                var data = Encoding.UTF8.GetBytes($"{msg.conversaId}");
                await ws.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
