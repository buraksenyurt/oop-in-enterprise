using Storage.Business;
using Storage.Infra.Contracts;
using Storage.Infra.Services;
using Serilog;

namespace Storage.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Serilog için bazı konfigurasyon ayarları yapıldı (appSettings üzerinden de gelebilir)
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console( // Konsol ekranına log atacak
                theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Sixteen, // bir renk teması seçtik
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}" // nasıl bir şablon ile yazılacak
                                                                                                     // Örneğin U3 seviyeyi büyük harflerle ve 3 karakter olarak göstermek için kullanılır. (INF, WRN, ERR gibi)
            )
            .CreateBootstrapLogger();

        builder.Services.AddControllers();

        builder.Services.AddOpenApi();
        // Controller bileşenlerinin ihtiyaç duyacağı tüm DI bileşenlerini kayıt ediyoruz (DI Register)
        builder.Services.AddScoped<IInvoiceCreateService, InvoiceCreateService>();
        builder.Services.AddScoped<IInvoiceReadService, InvoiceReadService>();
        builder.Services.AddSingleton<IStorageService, AwsS3StorageService>();

        builder.Host.UseSerilog(); // Serilog loglayıcı middleware'e adapte edildi

        Log.Information("Starting web host");

        var app = builder.Build();
        app.UseSerilogRequestLogging(); // Tüm http isteklerini loglar

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
