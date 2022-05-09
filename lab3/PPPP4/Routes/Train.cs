namespace PPPP4.Routes;

public class Train : AbstractRoute
{
    public Train() : this((200, 100, 500), 0) {}
    public Train((int, int, double) tuple, int dist) : base(tuple, dist) {}
}