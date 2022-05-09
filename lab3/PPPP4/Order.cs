using System.Runtime.CompilerServices;
using PPPP4.Data;
using PPPP4.Routes;

namespace PPPP4;

public class Order
{
    private double _cost;
    private int _volume;
    private string _startPoint;
    private string _endPoint;
    private Type _type;
    private Track _track;

    public Order()
    {
        _cost = 0;
        _volume = 0;
        _startPoint = "";
        _endPoint = "";
        _track = new Track();
        _type = Type.Economy;
    }

    public Order(Type type, string startPoint, string endPoint, int volume)
    {
        _type = type;
        _startPoint = startPoint;
        _endPoint = endPoint;
        _volume = volume;
        _track = Best();
        _cost = _track.Cost;

    }

    private static int Decr(string s)
    {
        return DataHolder.GetInstance("main").Towns.ContainsKey(s) ?
            DataHolder.GetInstance("main").Towns[s] : -1;
    }

    private static List<List<int>> MatrixUpd(Type type)
    {
        var dataHolder = DataHolder.GetInstance("main");
        var points = dataHolder.Points;

        var matrix = dataHolder.MatrixDist;
        var SIZE = dataHolder.Size;
        switch (type)
        {
            case Type.Economy:
                for (int i = 0; i < SIZE; i++)
                {
                    if (points[i].Contains("TS"))
                        for (int j = 0; j < SIZE; j++)
                            matrix[i][j] = 99999;
                }

                break;
            case Type.Standard:
                for (int i = 0; i < SIZE; i++)
                {
                    if (points[i].Contains("AP"))
                        for (int j = 0; j < SIZE; j++)
                            matrix[i][j] = 99999;
                }

                break;
            case Type.Turbo:
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                        if (matrix[i][j] == 0)
                            matrix[i][j] = 99999;
                }

                break;
        }
        return matrix;
    }

    private List<int> Optim(List<List<int>> arr, int beginPoint, int endPoint)
    {
        var SIZE = DataHolder.GetInstance("main").Size;
        var d = new int[16];
        var v = new int[16];

        int temp, minindex, min;
        int begin_index = beginPoint;

        for (int i = 0; i < SIZE; i++)
        {
            d[i] = 99999;
            v[i] = 1;
        }
        d[begin_index] = 0;

        do
        {
            minindex = 99999;
            min = 99999;
            for (int i = 0; i < SIZE; i++)
            {
                if ((v[i] == 1) && (d[i] < min))
                {
                    min = d[i];
                    minindex = i;
                }
            }

            if (minindex != 99999)
            {
                for (int i = 0; i < SIZE; i++)
                {
                    if (arr[minindex][i] > 0)
                    {
                        temp = min + arr[minindex][i];
                        if (temp < d[i])
                        {
                            d[i] = temp;
                        }
                    }
                }
                v[minindex] = 0;
            }
        } while (minindex < 99999);

        var ver = new int[SIZE];
        int end = endPoint;
        ver[0] = end;
        int k = 1;
        int weight = d[end];

        while (end != begin_index)
        {
            for (int i = 0; i < SIZE; i++)
                if (arr[end][i] != 0)
                {
                    temp = weight - arr[end][i];
                    if (temp == d[i])
                    {
                        weight = temp;
                        end = i;
                        ver[k] = i + 1;
                        k++;
                    }
                }
        }

        for (int i = 0; i < k / 2; i++)
            (ver[i], ver[k - 1 - i]) = (ver[k - 1 - i], ver[i]);

        return ver.ToList();
    }

    public Track Best()
    {
        DataHolder dataHolder = DataHolder.GetInstance("main");
        var SIZE = dataHolder.Size;

        int start = Decr(_startPoint);
        int finish = Decr(_endPoint);

        var mat = MatrixUpd(_type);

        var path = Enumerable.Repeat(-1, SIZE).ToList();
        path = Optim(mat, start, finish);
        int count = 0;
        for (int i = 0; i < SIZE; i++)
        {
            if (path[i] != -1)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        var routes = new List<AbstractRoute>();

        switch (count)
        {
            case 1:
                {
                    routes.AddRange(CreateCars(path, 1));
                    break;
                }

            case 3:
                {
                    routes.AddRange(CreateCars(path, 2));
                    AbstractRoute route = new Plane();
                    foreach (var (k, v) in dataHolder.Table)
                    {
                        if (k.Contains(dataHolder.Points[path[1]]) && dataHolder.Points[path[1]].Contains("TS"))
                        {
                            route = new Train(dataHolder.TableCost[v * 3 + 1], dataHolder.MatrixDist[path[1]][path[2]]);
                        }

                        if (k.Contains(dataHolder.Points[path[1]]) && dataHolder.Points[path[1]].Contains("AP"))
                        {
                            route = new Plane(dataHolder.TableCost[v * 3], dataHolder.MatrixDist[path[1]][path[2]]);
                        }
                    }
                    routes.Add(route);
                    break;
                    }

            case 5:
                {
                    routes.AddRange(CreateCars(path, 3));
                    var train = new Train();
                    var plane = new Plane();
                    foreach (var (k, v) in dataHolder.Table)
                    {
                        if (k.Contains(dataHolder.Points[path[1]]) && dataHolder.Points[path[1]].Contains("TS"))
                        {
                            train = new Train(dataHolder.TableCost[v * 3 + 1], dataHolder.MatrixDist[path[1]][path[2]]);
                        }

                        if (k.Contains(dataHolder.Points[path[1]]) && dataHolder.Points[path[1]].Contains("AP"))
                        {
                            plane = new Plane(dataHolder.TableCost[v * 3], dataHolder.MatrixDist[path[1]][path[2]]);
                        }
                    }

                    foreach (var (k, v) in dataHolder.Table)
                    {
                        if (k.Contains(dataHolder.Points[path[3]]) && dataHolder.Points[path[1]].Contains("TS"))
                        {
                            train = new Train(dataHolder.TableCost[v * 3 + 1], dataHolder.MatrixDist[path[3]][path[4]]);
                        }

                        if (k.Contains(dataHolder.Points[path[3]]) && dataHolder.Points[path[1]].Contains("AP"))
                        {
                            plane = new Plane(dataHolder.TableCost[v * 3], dataHolder.MatrixDist[path[3]][path[4]]);
                        }
                    }

                    routes.Add(train);
                    routes.Add(plane);  

                    break;
                }

            case 7:
                {
                    routes.AddRange(CreateCars(path, 4));
                    Train train1 = new();
                    Train train2 = new();
                    Plane plane = new();

                    foreach (var (k, v) in dataHolder.Table)
                    {
                        if (k.Contains(dataHolder.Points[path[1]]))
                        {
                            train1 = new Train(dataHolder.TableCost[v * 3 + 1],
                                dataHolder.MatrixDist[path[1]][path[2]]);
                        }

                        if (k.Contains(dataHolder.Points[path[3]]))
                        {
                            plane = new Plane(dataHolder.TableCost[v * 3],
                                dataHolder.MatrixDist[path[3]][path[4]]);
                        }

                        if (k.Contains(dataHolder.Points[path[5]]))
                        {
                            train2 = new Train(dataHolder.TableCost[v * 3 + 1],
                                dataHolder.MatrixDist[path[5]][path[6]]);
                        }
                    }


                    routes.Add(train1);
                    routes.Add(train2);
                    routes.Add(plane);
                    break;
                }
        }

        return new Track(routes, _volume);
    }

    private IEnumerable<Car> CreateCars(List<int> path, int count)
    {
        var cars = new List<Car>();
        var dataHolder = DataHolder.GetInstance("main");
        for (int i = 0, ind = 0; i < count; i++, ind+=2)
        {
            var car = new Car();
            foreach (var (k, v) in dataHolder.Table)
            {
                if (k.Contains(dataHolder.Points[path[ind]]))
                {
                    car = new Car(dataHolder.TableCost[v * 3 + 2], dataHolder.MatrixDist[path[ind]][path[ind + 1]]);
                }
            }
            cars.Add(car);
        }

        return cars;
    }
}