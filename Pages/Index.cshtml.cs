using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace AsyncTest1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        //private readonly IHubContext<PhilosopherHub> _hubContext;

        public IndexModel(ILogger<IndexModel> logger /*IHubContext<PhilosopherHub> hubContext*/)
        {
            _logger = logger;
            //_hubContext = hubContext;
        }

        public void OnGet()
        {
            //int numberOfPhilosophers = 5;
            //var forks = new SemaphoreSlim[numberOfPhilosophers];
            //var philosophers = new Philosopher[numberOfPhilosophers];

            //for (int i = 0; i < numberOfPhilosophers; i++)
            //{
            //    forks[i] = new SemaphoreSlim(1);
            //}

            //var tasks = new Task[numberOfPhilosophers];

            //for (int i = 0; i < numberOfPhilosophers; i++)
            //{
            //    philosophers[i] = new Philosopher(i, forks[i], forks[(i + 1) % numberOfPhilosophers], _hubContext);
            //    tasks[i] = Task.Run(() => philosophers[i].EatAsync());
            //}

            //Task.WaitAll(tasks); // Ждем завершения всех задач
        }
    }

    //public class Philosopher
    //{
    //    private readonly int id;
    //    private readonly SemaphoreSlim leftFork;
    //    private readonly SemaphoreSlim rightFork;
    //    private readonly IHubContext<PhilosopherHub> hubContext;

    //    public Philosopher(int id, SemaphoreSlim leftFork, SemaphoreSlim rightFork, IHubContext<PhilosopherHub> hubContext)
    //    {
    //        this.id = id;
    //        this.leftFork = leftFork;
    //        this.rightFork = rightFork;
    //        this.hubContext = hubContext;
    //    }

    //    public async Task EatAsync()
    //    {
    //        while (true)
    //        {
    //            Console.WriteLine($"Philosopher {id} is thinking.");
    //            await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(100, 1000)));

    //            Console.WriteLine($"Philosopher {id} is attempting to pick up the left fork.");
    //            await leftFork.WaitAsync();

    //            Console.WriteLine($"Philosopher {id} picked up the left fork.");
    //            Console.WriteLine($"Philosopher {id} is attempting to pick up the right fork.");
    //            await rightFork.WaitAsync();

    //            Console.WriteLine($"Philosopher {id} picked up the right fork and is eating.");

    //            // Обновляем состояние философа через SignalR
    //            await hubContext.Clients.All.SendAsync("UpdatePhilosopherStatus", id, "Eating");

    //            await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(100, 1000)));

    //            rightFork.Release();
    //            leftFork.Release();

    //            Console.WriteLine($"Philosopher {id} released both forks.");

    //            // Обновляем состояние философа через SignalR
    //            await hubContext.Clients.All.SendAsync("UpdatePhilosopherStatus", id, "Thinking");
    //        }
    //    }
    //}
}
