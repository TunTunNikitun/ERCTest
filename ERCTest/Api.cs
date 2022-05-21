using System.Diagnostics;
namespace ERCTest
{
    public static class Api
    {
        public static void Statrt()
        {
            string path = Environment.CurrentDirectory;
            path = path.Replace("ERCTest\\ERCTest", "ERCTest\\ERCTestApi\\bin\\Debug\\net6.0\\ERCTestApi.exe");
            Process.Start(path);
        }

    }
}
