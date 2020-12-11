namespace FastestApi.IntegrationTest
{
    using System;
    using System.Net.Http;
    using FastestApi.Options;
    using FastestApi.Repositories;
    using FastestApi.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Moq;
    using Serilog;
    using Serilog.Events;
    using Xunit.Abstractions;

    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        public CustomWebApplicationFactory(ITestOutputHelper testOutputHelper)
        {
            this.ClientOptions.AllowAutoRedirect = false;
            this.ClientOptions.BaseAddress = new Uri("https://localhost");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.TestOutput(testOutputHelper, LogEventLevel.Verbose)
                .CreateLogger();
        }

        public ApplicationOptions ApplicationOptions { get; private set; }

        public Mock<ICarRepository> CarRepositoryMock { get; } = new Mock<ICarRepository>(MockBehavior.Strict);

        public Mock<IClockService> ClockServiceMock { get; } = new Mock<IClockService>(MockBehavior.Strict);

        public void VerifyAllMocks() => Mock.VerifyAll(this.CarRepositoryMock, this.ClockServiceMock);

        protected override void ConfigureClient(HttpClient client)
        {
            using (var serviceScope = this.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                this.ApplicationOptions = serviceProvider.GetRequiredService<IOptions<ApplicationOptions>>().Value;
            }

            base.ConfigureClient(client);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder
                .UseEnvironment("Test")
                .ConfigureServices(this.ConfigureServices);

        protected virtual void ConfigureServices(IServiceCollection services) =>
            services
                .AddSingleton(this.CarRepositoryMock.Object)
                .AddSingleton(this.ClockServiceMock.Object);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.VerifyAllMocks();
            }

            base.Dispose(disposing);
        }
    }
}
