using PPPP4.Routes;

namespace PPPP4;

public class Track
{
    private double _cost;
    private double _time;
    private int _volume;

    public double Cost => _cost;

    public Track()
    {
        _cost = 0;
        _time = 0;
        _volume = 0;
    }

    public Track(IEnumerable<AbstractRoute> transports, int volume)
    {
        //TODO: finish it
        _cost = transports.Sum(s => s.sumCost(volume));
        _time = transports.Sum(s => s.SumTime);
    }

    public override string ToString()
    {
        return $"Track generated.\nSummary cost: {_cost}.\nSummary time: {_time}.";
    }
}