using DiningHall.DiningHall;

namespace DiningHall.BackgroundTasks;

public class BackgroundTask : BackgroundService
{
    private readonly ILogger<BackgroundTask> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDiningHall _diningHall;
    private Timer timer;
    private int number;


    public BackgroundTask(ILogger<BackgroundTask> logger, IDiningHall diningHall,
        IServiceScopeFactory serviceScopeFactory, Timer timer)
    {
        _logger = logger;
        _diningHall = diningHall;
        _serviceScopeFactory = serviceScopeFactory;
        this.timer = timer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public override async Task StartAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scoped = scope.ServiceProvider.GetRequiredService<IDiningHall>();
            scoped.RunRestaurant();
            //Do your stuff
        }

        _diningHall.RunRestaurant();
        await Task.CompletedTask;
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}