using DiningHall.DiningHall;

namespace DiningHall.BackgroundTask;

public class BackgroundTask : BackgroundService
{
    private readonly ILogger<BackgroundTask> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer timer;
    private int number;


    public BackgroundTask(ILogger<BackgroundTask> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scoped = scope.ServiceProvider.GetRequiredService<IDiningHall>();
            scoped.RunRestaurant();
            //Do your stuff
        }

        await Task.CompletedTask;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        await Task.CompletedTask;
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
}