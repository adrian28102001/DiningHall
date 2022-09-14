using DiningHall.DiningHall;

namespace DiningHall.BackgroundTask;

public class BackgroundTask : BackgroundService
{
    private readonly ILogger<BackgroundTask> logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer timer;
    private int number;

    public BackgroundTask(ILogger<BackgroundTask> logger, IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000, stoppingToken);
        using var scope = _serviceScopeFactory.CreateScope();
        var diningHall = scope.ServiceProvider.GetRequiredService<IDiningHall>();
        diningHall.InitializeDiningHall();
        diningHall.MaintainRestaurant(stoppingToken);
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}