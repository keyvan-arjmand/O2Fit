namespace Chat.Api.Hubs;

public class TestHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("test", $"{Context.ConnectionId} testst ts ett teis  j io");
    }
}