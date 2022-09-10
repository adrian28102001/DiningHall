using DiningHall.BackgroundTasks;
using DiningHall.DiningHall;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;

namespace DiningHall;

public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IWaiterRepository, WaiterRepository>();
        services.AddSingleton<BackgroundTask>();
        services.AddHostedService<BackgroundTask>();
        services.AddSingleton<IHostedService, BackgroundTask>();
        

    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}