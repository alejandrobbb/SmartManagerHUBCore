using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HUBCore.Hubs
{
    public sealed class TestHub : Hub
    {
        // Método para enviar mensajes (único de CTM kiosk)
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Método para actualizar el gráfico en todos los clientes (único de chart view turnos test)
        public async Task SendChartUpdate(int incrementValue)
        {
            // Propaga el incremento a todos los clientes
            await Clients.All.SendAsync("ReceiveChartUpdate", incrementValue);
        }
        // Metodo unico para Face recognition
        // Método para enviar una imagen (Base64)
        public async Task SendImage(string base64Image, string description)
        {
            // Propaga la imagen a todos los clientes conectados
            await Clients.All.SendAsync("ReceiveImage", base64Image, description);
        }
        public async Task SendMessagepy(string message)
        {
            await Clients.All.SendAsync("ReceiveMessagepy", message);
        }
        public async Task SendImagePersons(string base64Image, string description)
        {
            // Propaga la imagen a todos los clientes conectados
            await Clients.All.SendAsync("ReceiveImagePersons", base64Image, description);
        }
        public async Task PersonsInRoom(string base64Image, string NameRoom, int NUMpersons)
        {
            // Propaga la imagen a todos los clientes conectados
            await Clients.All.SendAsync("ReceiveImagePersons", base64Image, NameRoom, NUMpersons);
        }
        //hub para enviar una lista de la cantidad de persoans encontradas en una sala (imagenes)
        public async Task ListRoomPersons(List<string> base64Images, string NameRoom, int numPerson)
        {
            await Clients.All.SendAsync("ReceiveImagePersons", base64Images, NameRoom, numPerson);
        }
    }
}
