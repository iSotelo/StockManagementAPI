using System.IO;

namespace StockManagementAPI
{
    public class Settings
    {
        public string ProjectPath => Directory.GetCurrentDirectory();
        public string PathJsonData => Directory.GetCurrentDirectory() + @"\Seed\cars.json";
    }
}
