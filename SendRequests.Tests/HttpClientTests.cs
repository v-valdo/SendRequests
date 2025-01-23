using Moq;
using SendRequests.console;
using System.Net;
using Moq.Protected;

namespace SendRequests.Tests;
public class RequesterTests
{
    [Fact]
    public async Task Test_Requester_Start()
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("OK") });

        var client = new HttpClient(mockHandler.Object);
        var requester = new Requester();

        var input = new StringReader("http://example.com\n2\n");
        var oldConsoleIn = Console.In;
        Console.SetIn(input);

        var output = new StringWriter();
        var oldConsoleOut = Console.Out;
        Console.SetOut(output);

        await requester.Start(client);

        string consoleOutput = output.ToString();
        Assert.Contains("sending #1", consoleOutput);
        Assert.Contains("sending #2", consoleOutput);

        Console.SetIn(oldConsoleIn);
        Console.SetOut(oldConsoleOut);
    }
}