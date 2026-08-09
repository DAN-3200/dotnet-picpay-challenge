using Npgsql;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace PicPay.Infrastructure.Telemetry;

public static class OpenTelemetryExtensions
{
    private const string DefaultServiceName = "PicPay.Api";
    private const string DefaultCollectorEndpoint = "http://localhost:4317";

    /// <summary>
    /// Configura logs, traces e métricas para exportação OTLP/gRPC.
    /// </summary>
    public static WebApplicationBuilder AddMyOtel(this WebApplicationBuilder builder)
    {
        var serviceName = builder.Configuration["OpenTelemetry:ServiceName"] ?? DefaultServiceName;
        var collectorEndpoint = builder.Configuration["OpenTelemetry:Endpoint"] ?? DefaultCollectorEndpoint;
        var endpoint = new Uri(collectorEndpoint);
        var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName);

        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.SetResourceBuilder(resourceBuilder);
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
            logging.ParseStateValues = true;
            logging.AddOtlpExporter(options =>
            {
                options.Endpoint = endpoint;
                options.Protocol = OtlpExportProtocol.Grpc;
            });
        });

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation(options =>
                {
                    options.EnrichWithIDbCommand = (activity, command) =>
                    {
                        activity.SetTag("db.name", command.Connection?.Database);
                        activity.SetTag("db.system", command.Connection?.GetType().Name);
                        activity.SetTag("db.statement", command.CommandText);
                    };
                })
                .AddNpgsql()
                .AddSource("Oracle.ManagedDataAccess.*")
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = endpoint;
                    options.Protocol = OtlpExportProtocol.Grpc;
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = endpoint;
                    options.Protocol = OtlpExportProtocol.Grpc;
                }));

        return builder;
    }
}