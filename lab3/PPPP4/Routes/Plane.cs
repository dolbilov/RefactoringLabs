namespace PPPP4.Routes;

public class Plane : AbstractRoute
{
    public Plane() : this((500, 300, 100), 0) {}
    public Plane((int, int, double) tuple, int dist) : base(tuple, dist) {}
}