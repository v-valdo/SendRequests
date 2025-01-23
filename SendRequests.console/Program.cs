using SendRequests.console;
HttpClient client = new();

Requester req = new();

await req.Start(client);