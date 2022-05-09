namespace PPPP4.Routes;

public class Car : AbstractRoute
{
    public Car() : this((100, 60, 50), 0) {}
    public Car((int, int, double) tuple, int dist) : base(tuple, dist) {}
}