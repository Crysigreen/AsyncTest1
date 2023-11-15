using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace AsyncTest1.Pages
{
    public class DiningPhilosophersModel : PageModel
    {
        private readonly IHubContext<PhilosophersHub> hubContext;

        public DiningPhilosophersModel(IHubContext<PhilosophersHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public List<Philosopher> Philosophers { get; set; }

        public void OnGet()
        {
            var forks = new SemaphoreSlim[5];

            for (int i = 0; i < 5; i++)
            {
                forks[i] = new SemaphoreSlim(1);
            }

            Philosophers = new List<Philosopher>(5);

            for (int i = 0; i < 5; i++)
            {
                Philosophers.Add( new Philosopher(i, forks[i], forks[(i + 1) % 5], hubContext));
            }

        
            foreach (var philosopher in Philosophers)
            {
                Task.Run(() => philosopher.EatAsync());
            }

        }
    }

    public class Philosopher
    {
        private readonly int id;
        private readonly SemaphoreSlim leftFork;
        private readonly SemaphoreSlim rightFork;
        private readonly IHubContext<PhilosophersHub> hubContext;
        private string status;

        public Philosopher(int id, SemaphoreSlim leftFork, SemaphoreSlim rightFork, IHubContext<PhilosophersHub> hubContext)
        {
            this.id = id;
            this.leftFork = leftFork;
            this.rightFork = rightFork;
            this.hubContext = hubContext;
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int Id
        {
            get { return id; }
        }

        public async Task EatAsync()
        {
            while (true)
            {
                Console.WriteLine($"Философ {id} думает");
                await hubContext.Clients.All.SendAsync("UpdatePhilosopherStatus", id, "Thinking", "black");
                await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(5000, 10000)));

  
                if (await leftFork.WaitAsync(0))
                {
                    Console.WriteLine($"Философ {id} взял левую вилку");

                    if(await rightFork.WaitAsync(0))
                    {
                        Console.WriteLine($"Философ {id} взял правую вилку");
                        await hubContext.Clients.All.SendAsync("UpdatePhilosopherStatus", id, "Eating", "red");
                        Console.WriteLine($"Философ {id} начал есть");
                        await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(5000, 5000)));
                        leftFork.Release();
                        rightFork.Release();
                        Console.WriteLine($"Философ {id} положил вилки");
                    }
                    else
                    {
                        Console.WriteLine($"Философ {id} положил левую вилку так как занята правая");
                        leftFork.Release();
                    }

                }
                //else
                //{
                //    await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(1000, 10000)));
                //}


                //Console.WriteLine($"Philosopher {id} picked up the left fork.");
                //Console.WriteLine($"Philosopher {id} is attempting to pick up the right fork.");
                //await rightFork.WaitAsync();
                //if (rightFork.Wait(0))
                //{
                //    await hubContext.Clients.All.SendAsync("UpdatePhilosopherStatus", id, "Eating");
                //    Console.WriteLine($"Philosopher {id} is Eating.");
                //    await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(1000, 10000)));
                //    rightFork.Release();
                //    leftFork.Release();
                //    await hubContext.Clients.All.SendAsync("UpdatePhilosopherStatus", id, "Thinking");
                //}
                //else
                //{
                //    leftFork.Release();
                //    await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(1000, 10000)));
                //}

                //Console.WriteLine($"Philosopher {id} picked up the right fork and is eating.");
                //Console.WriteLine($"Philosopher {id} is Eating.");

                // Обновляем состояние философа через SignalR
                //await hubContext.Clients.All.SendAsync("UpdatePhilosopherStatus", id, "Eating");

                //await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(1000, 10000)));

                //rightFork.Release();
                //leftFork.Release();

                //Console.WriteLine($"Philosopher {id} released both forks.");
                //Console.WriteLine($"Philosopher {id} is Thinking.");

                // Обновляем состояние философа через SignalR
                //await hubContext.Clients.All.SendAsync("UpdatePhilosopherStatus", id, "Thinking");
            }
        }
    }
}
