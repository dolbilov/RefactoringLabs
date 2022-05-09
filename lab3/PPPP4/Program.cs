namespace PPPP4;

public static class Program
{
    private static void Main()
    {
        TransportManager tm = new();
        tm.GenerateOrder(Type.Economy, "Moscow", "Volgograd", 100);
    }
}