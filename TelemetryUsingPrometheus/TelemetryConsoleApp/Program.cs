using System.Reactive.Linq;

using HttpClient client = new()
{
    BaseAddress = new Uri("http://localhost:5174")
};

var random = new Random();

var requestGenerator = Observable.Interval(TimeSpan.FromMilliseconds(2000)).Subscribe(_ =>
{
    var number = random.Next(1, 10);

    var contentData = new Dictionary<string, string>
    {
        { "inc", number.ToString() }
    };

    var content = new FormUrlEncodedContent(contentData);

    client.PostAsync("api/Telemetry/IncrementCounter", content).GetAwaiter().GetResult();
    client.PostAsync("api/Telemetry/IncrementGauge", content).GetAwaiter().GetResult();
    client.PostAsync("api/Telemetry/IncrementHistogram", content).GetAwaiter().GetResult();
    client.PostAsync("api/Telemetry/IncrementSummary", content).GetAwaiter().GetResult();

});

Console.ReadKey();
requestGenerator.Dispose();
