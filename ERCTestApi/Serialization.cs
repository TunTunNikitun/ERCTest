using Newtonsoft.Json;
using System;
using System.IO;
namespace ERCTestApi
{
    public class Serialization
    {
        public static string Getjson<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T JsonDeserializing<T>(string data)
        {
            try
            {
                T result = JsonConvert.DeserializeObject<T>(data);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong input format. Check input data!");
            }
        }
    }
}
