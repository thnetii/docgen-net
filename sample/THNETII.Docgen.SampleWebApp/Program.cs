using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace THNETII.Docgen.SampleWebApp
{
    public static class Program
    {
        public static ICommandHandler Handler { get; } = CommandHandler.Create(
        (IHost host, CancellationToken cancelToken) =>
            host.WaitForShutdownAsync(cancelToken)
        );

        public static Task<int> Main(string[] args)
        {
            var root = new RootCommand { Handler = Handler };
            var parser = new CommandLineBuilder(root)
                .UseDefaults()
                .UseHost(CreateHostBuilder)
                .Build();

            return parser.InvokeAsync(args ?? Array.Empty<string>());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
