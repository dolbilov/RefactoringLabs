namespace PPPP4.Routes;

public abstract class AbstractRoute
{
    private readonly int _speed;
    private readonly int _volume;
    private readonly double _price;
    private readonly int _distance;

    protected AbstractRoute((int, int, double) tuple, int dist)
    {
        _price = tuple.Item1;
        _speed = tuple.Item2;
        _volume = Convert.ToInt32(tuple.Item3);
        _distance = dist;
    }

    public double sumCost(int mass)
    {
        return ((double)mass / _volume) * SumTime * _price;
    }

    public double SumTime => (double)_distance / _speed;

    public int Distance => _distance;
}