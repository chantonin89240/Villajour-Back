using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class ApiBenchmark
{
    private readonly HttpClient _client = new HttpClient();

    [Benchmark]
    public async Task GetMairie()
    {
        var response = await _client.GetAsync("https://localhost:44357/Api/Mairie");
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