using System.Net.Http;
System.Console.WriteLine("sad manual requests");

HttpClient client = new();

while (true)
{
    Console.WriteLine("enter full url:");
    string url = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrWhiteSpace(url)) return;

    Console.WriteLine("enter amount:");

    string amount = Console.ReadLine() ?? "";
    if (!int.TryParse(amount, out int reqs))
    {
        System.Console.WriteLine("enter a number");
        continue;
    }
    for (int i = 1; i < reqs + 1; i++)
    {
        var response = await client.GetAsync(url);
        System.Console.WriteLine($"sending #{i}");
        System.Console.WriteLine($"{(int)response.StatusCode}");
    }
}
