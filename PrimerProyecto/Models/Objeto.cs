using Newtonsoft.Json;

namespace PrimerProyecto.Models;

public static class Objeto
{
    public static string ObjectToString<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T StringToObject<T>(string str)
    {
        return JsonConvert.DeserializeObject<T>(str);
    }
}