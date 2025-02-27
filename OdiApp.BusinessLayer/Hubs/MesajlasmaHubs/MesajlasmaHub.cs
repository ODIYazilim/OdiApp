using Microsoft.AspNetCore.SignalR;
using OdiApp.BusinessLayer.Core.AuthAttribute;

namespace OdiApp.BusinessLayer.Hubs.MesajlasmaHubs
{

    [AllAuthorize]
    public class MesajlasmaHub : Hub<IMesajlasmaHub>
    {
        public static List<Tuple<string, string>> KullaniciList = new List<Tuple<string, string>>();

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            var connectionId = Context.ConnectionId;

            if (!KullaniciList.Any(t => t.Item1 == userId && t.Item2 == connectionId))
            {
                KullaniciList.Add(new Tuple<string, string>(userId, connectionId));
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            var connectionId = Context.ConnectionId;

            var connectionToRemove = KullaniciList.FirstOrDefault(t => t.Item1 == userId && t.Item2 == connectionId);

            if (connectionToRemove != null)
            {
                KullaniciList.Remove(connectionToRemove);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
