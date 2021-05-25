using System.Threading;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;

namespace JaegerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using var tracer = new Tracer.Builder("test")
                .WithSampler(new ConstSampler(true))
                .WithReporter(new RemoteReporter.Builder()
                    .WithSender(new UdpSender("localhost", 0, 0))
                    .Build())
                .Build();

            using (tracer.BuildSpan("root").StartActive(true))
            {
                Thread.Sleep(500);
                using (tracer.BuildSpan("child-one").StartActive(true))
                {
                    Thread.Sleep(200);
                }
                Thread.Sleep(400);
                using (tracer.BuildSpan("child-two").StartActive(true))
                {
                    Thread.Sleep(200);
                }
                Thread.Sleep(300);
            }
        }
    }
}
