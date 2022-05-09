using PPPP4.Data;

namespace PPPP4;

public class TransportManager
{
    public void GenerateOrder(Type type, string start, string end, int volume)
    {
        Order ord = new(type, start, end, volume);
        var track = ord.Best();
        Console.WriteLine(track);
    }

}