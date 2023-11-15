using Microsoft.AspNetCore.SignalR;

namespace AsyncTest1
{
    public class PhilosophersHub : Hub
    {
        public async Task UpdatePhilosopherStatus(int philosopherId, string status)
        {
            await Clients.All.SendAsync("UpdatePhilosopherStatus", philosopherId, status);
        }
    }
}
