using System.Diagnostics.CodeAnalysis;

namespace PPPP4.Data
{
    public class DataHolder
    {
        private static readonly Dictionary<string, DataHolder> _instances = new();

        public static DataHolder GetInstance([NotNull]string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key can't be empty or null");

            return _instances.ContainsKey(key) ? _instances[key] : new DataHolder(key);
        }

        private DataHolder(string key)
        {
            // we can change way to fill this arrays if we need
            
            Points =  new List<string>(){
                "msk_AP","msk_TS","msk_WH","mzh_TS",
                "mzh_WH","zvn_WH","NN_AP","NN_TS", "NN_WH","dzr_TS","dzr_WH",
                "vlg_AP","vlg_TS","vlg_WH","kam_TS","kam_WH" };

            Towns = new Dictionary<string, int>() {
                {"Moscow", 2},
                {"Mozhaisk", 4},
                {"Zvenigorod", 5},
                {"Nizhniy Novgorod", 8},
                {"Dzerzhinsk", 10},
                {"Volgograd", 13},
                {"Kamishin", 15} };

            Table = new Dictionary<string, int>() {
                {"msk", 0},
                {"mzh", 1},
                {"zvn", 2},
                {"NN", 3},
                {"dzr", 4},
                {"vlg", 5},
                {"kam", 6} };

            TableCost = new List<(int, int, double)>() {
                (500, 300, 100),
                (200, 100, 500),
                (100, 60, 50),
                (0, 0, 0),
                (200, 100, 500),
                (100, 60, 50),
                (0, 0, 0),
                (0, 0, 0),
                (100, 60, 50),
                (500, 300, 100),
                (200, 100, 500),
                (100, 60, 50),
                (0, 0, 0),
                (200, 100, 500),
                (100, 60, 50),
                (500, 300, 100),
                (200, 100, 500),
                (100, 60, 50),
                (0, 0, 0),
                (200, 100, 500),
                (100, 60, 50)
            };

            MatrixDist = new List<List<int>>() {
                new(){0, 20, 30, 108, 110, 65, 398, 0, 0, 0, 0, 926, 0, 0, 0, 0},
                new(){20, 0, 10, 98, 100, 55, 0, 415, 0, 483, 0, 0, 937, 0, 1204, 0},
                new(){30, 10, 0, 108, 110, 65, 0, 0, 430, 0, 390, 0, 0, 652, 0, 1234},
                new(){108, 98, 108, 0, 5, 0, 0, 0, 0, 581, 0, 0, 1035, 0, 1302, 0},
                new(){110, 100, 110, 5, 0, 78, 0, 0, 540, 0, 500, 0, 0 ,1062, 0, 1344},
                new(){65, 55, 65, 0, 78, 0, 0, 0, 495, 0, 455, 0, 0, 1017, 0, 1299},
                new(){398, 0, 0, 0, 0, 0, 0, 19, 23, 0, 26, 828, 0, 0, 0, 0},
                new(){0, 415, 0, 0, 0, 0 ,19, 0, 4, 32, 0, 0, 840, 0, 919, 0},
                new(){0, 0, 430, 0, 540, 495, 23, 4, 0, 0, 40, 0, 0, 849, 0, 1010},
                new(){0, 483, 0, 581, 0, 0, 0, 32, 0, 0, 4, 0, 872, 0, 951, 0},
                new(){0, 0, 390, 0, 500, 455, 26, 0, 40, 4, 0, 0, 0, 889, 0, 1050},
                new(){926, 0, 0, 0, 0 ,0 ,828, 0, 0 ,0 ,0, 0, 15, 16, 0, 285},
                new(){0, 937, 0, 1035, 0, 0, 0, 840, 0, 872, 0, 15, 0, 2, 257, 281},
                new(){0, 0, 952, 0, 1062, 1017, 0, 0, 849, 0, 889, 16, 2, 0, 0 ,282},
                new(){0, 1204, 0, 1302, 0, 0, 0, 919, 0, 951, 0, 0, 257, 0, 0, 2},
                new(){0, 0, 1234, 0, 1344, 1299, 0, 0, 1010, 0, 1050, 285, 281, 282, 2, 0} };

            Size = 16;

            _instances.Add(key, this);
        }

        public List<string>? Points { get;}

        public Dictionary<string, int>? Towns { get; }

        public Dictionary<string, int>? Table { get; }

        public List<(int, int, double)>? TableCost { get; }

        public List<List<int>>? MatrixDist { get; }

        public int Size { get; }
    }
}
