using Storage.Business;
using Storage.Infra.Contracts;
using Storage.Infra.Services;

namespace Storage.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        // Controller bileşenlerinin ihtiyaç duyacağı tüm DI bileşenlerini kayıt ediyoruz (DI Register)
        builder.Services.AddScoped<IInvoiceCreateService, InvoiceCreateService>();
        builder.Services.AddScoped<IInvoiceReadService, InvoiceReadService>();
        builder.Services.AddSingleton<IStorageService, AwsS3StorageService>();

        var app = builder.Build();

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
