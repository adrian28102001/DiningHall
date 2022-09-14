namespace DiningHall.DiningHall;

public interface IDiningHall
{
    void InitializeDiningHall();
    void MaintainRestaurant(CancellationToken stoppingToken);
}