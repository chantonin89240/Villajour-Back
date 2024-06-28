using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class ApiBenchmark
{
    private readonly HttpClient _client = new HttpClient();

    [Benchmark]
    public async Task GetEventFavoriteByUser()
    {
        var response = await _client.GetAsync("https://localhost:5000/Api/Event/GetEventFavoriteByUser/123e4567-e89b-12d3-a456-426614174000");
        response.EnsureSuccessStatusCode();
    }
}

public class Program
{
    public static void Maini(string[] args)
    {
        var summary = BenchmarkRunner.Run<ApiBenchmark>();
    }
}