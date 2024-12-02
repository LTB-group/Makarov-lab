using System;
using System.Collections.Generic;
using System.Threading;

public interface Subject
{
    string Request(string request);
}

public class RealSubject : Subject
{
    public string Request(string request)
    {
        Thread.Sleep(1000);
        return $"RealSubject: Обработан запрос '{request}'";
    }
}

public class Proxy : Subject
{
    private RealSubject _realSubject;
    private readonly Dictionary<string, (string Response, DateTime Timestamp)> _cache = new Dictionary<string, (string, DateTime)>();
    private readonly TimeSpan _cacheLifetime = TimeSpan.FromSeconds(30);

    public string Request(string request)
    {
        if (!HasAccess())
            return "Proxy: Доступ запрещен.";

        if (_cache.ContainsKey(request))
        {
            var (cachedResponse, timestamp) = _cache[request];
            if (DateTime.Now - timestamp < _cacheLifetime)
                return $"Proxy: Ответ из кэша: {cachedResponse}";

            _cache.Remove(request);
        }

        _realSubject ??= new RealSubject();
        string response = _realSubject.Request(request);

        _cache[request] = (response, DateTime.Now);
        return response;
    }

    private bool HasAccess()
    {
        return true;
    }
}

class Program
{
    static void Main()
    {
        Subject proxy = new Proxy();

        Console.WriteLine(proxy.Request("Data1"));
        Console.WriteLine(proxy.Request("Data1"));
        Console.WriteLine(proxy.Request("Data2"));

        Console.WriteLine("Ожидание истечения срока действия кэша...");
        Thread.Sleep(31000);

        Console.WriteLine(proxy.Request("Data1"));
    }
}
