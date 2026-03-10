using Application.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class WebSocketManager : IWebSocketBroadcaster
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private static readonly ConcurrentDictionary<string, WebSocket> _clients = new();
        public WebSocketManager(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task HandleConnection(HttpContext context, WebSocket socket)
        {
            if (!context.User.Identity?.IsAuthenticated ?? false)
            {
                context.Response.StatusCode = 401;
                return ;
            }

            var clientId = Guid.NewGuid().ToString();
            _clients.TryAdd(clientId, socket);

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IFieldWorkRepository>();
                var fieldwork = await repository.GetCurrentOpenFieldwork();
                string message;

                if (fieldwork == null)
                {
                    message = JsonSerializer.Serialize(new
                    {
                        isFieldworkTime = false,
                        startTime = (DateTime?)null,
                        fieldworkId = (int?)null
                    });
                }
                else
                {
                    message = JsonSerializer.Serialize(new
                    {
                        isFieldworkTime = true,
                        startTime = fieldwork.StartDate,
                        fieldworkId = fieldwork.FieldWorkId
                    });
                }
                // Send initial message to the client
                await socket.SendAsync(
                    new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );

                Console.WriteLine($"Sent initial message to WebSocket: {message}");
                var buffer = new byte[1024];
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
                        break;
                    }
                }
            }
            catch (WebSocketException)
            {
                
            }
            catch (ObjectDisposedException)
            {
                
            }
            finally
            {
                RemoveClient(clientId);
            }
        }

        public async Task BroadcastStatusAsync(bool _isFieldworkTime, DateTime? _startTime, int? _fieldworkId)
        {
            var message = JsonSerializer.Serialize(new
            {
                isFieldworkTime = _isFieldworkTime,
                startTime = _startTime,
                fieldworkId = _fieldworkId
            });

            var bytes = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(bytes);

            foreach (var kvp in _clients.ToList())
            {
                var clientId = kvp.Key;
                var client = kvp.Value;
                if (client == null || client.State != WebSocketState.Open)
                {
                    RemoveClient(clientId);
                    continue;
                }

                try
                {
                    await client.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch (WebSocketException)
                {
                    RemoveClient(clientId);
                }
                catch (ObjectDisposedException)
                {
                    RemoveClient(clientId);
                }
            }

        }

        private void RemoveClient(string clientId)
        {
            if (_clients.TryRemove(clientId, out var client))
            {
                try
                {
                    if (client.State == WebSocketState.Open || client.State == WebSocketState.CloseReceived)
                    {
                        client.CloseAsync(
                            WebSocketCloseStatus.NormalClosure,
                            "Connection removed",
                            CancellationToken.None).GetAwaiter().GetResult();
                    }
                }
                catch
                {
                }

                try
                {
                    client.Dispose();
                }
                catch
                {
                }
            }
        }
    }
}
