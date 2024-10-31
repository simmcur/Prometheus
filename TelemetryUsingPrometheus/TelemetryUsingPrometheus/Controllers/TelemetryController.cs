using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace TelemetryUsingPrometheus.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    [Consumes("application/x-www-form-urlencoded")]
    public class TelemetryController : ControllerBase
    {
        Counter _counter;
        Histogram _histogram;
        Gauge _gauge;
        Summary _summary;

        public TelemetryController()
        {
            _counter = Metrics.CreateCounter("MetricCounter", "will be incremented and published as metrics");
            _histogram = Metrics.CreateHistogram("MetricHistogram", "Will observe a value and publish it as Histogram");
            _gauge = Metrics.CreateGauge("MetricGauge", "Will observe a value and publish it as Gauge");
            _summary = Metrics.CreateSummary("MetricSummary", "Will observe a value and publish it as Summary");
        }

        [HttpPost("IncrementCounter")]        
        public void PostCounter([FromForm] int inc)
        {
            _counter.Inc(inc);
            _counter.Publish();        
        }

        [HttpPost("IncrementGauge")]
        public void IncrementGauge([FromForm] int inc)
        {
            _gauge.Inc(inc);
            _gauge.Publish();
        }

        [HttpPost("IncrementHistogram")]
        public void IncrementHistogram([FromForm] int inc)
        {
            _histogram.Observe(inc);
            _histogram.Publish();
        }

        [HttpPost("IncrementSummary")]
        public void PostSummary([FromForm] int inc)
        {
            _summary.Observe(inc);
            _summary.Publish();
        }

        //[HttpGet("Histogram")]
        //public int GetHistogram()
        //{
        //    var randomNummber = new Random().Next(1, 5);

        //    using (_histogram.NewTimer())
        //    {
        //        Thread.Sleep(randomNummber * 1000);
        //    }

        //    _histogram.Publish();

        //    return randomNummber;
        //}
    }
}
