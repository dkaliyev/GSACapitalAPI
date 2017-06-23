using System;
using System.IO;

namespace Utilities
{
    public class DataLoader: IDataLoader
    {

        public string[] Load(string path)
        {
            try
            {
                var lines = File.ReadAllLines(path);
                return lines;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
