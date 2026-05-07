using Microsoft.Extensions.DependencyInjection;
using Storage.Infra.Contracts;
using Storage.Infra.Services;
using Storage.Infra.Validators;

namespace Storage.Infra;

/*
DependencyInjection static bir sınıf. Dolayısıyla nesne örneğine ihtiyaç duymadan,
üyeleri (AddInfrastructure metodu gibi) çağırılabilir (DependencyInjection.AddInfrastructure(services)).

this keyword ile IServiceCollection türünü genişletebiliriz(extension method).
Yani, herhangi bir runtime'ın (Program.cs olabilir, web api program sınıfı olabili) IServiceCollection ile çalışan
DI Container'ını bu metodu çağırarak genişletebilir ve burada tanımlanan tüm servisleri ekleyebiliriz.

Extension Method dediğimiz kavram this keyword ile ilk parametre gelen bir nesnenin genişletilmesi olarak ifade edilebilir.
     
*/
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<AwsS3StorageService>();
        services.AddScoped<IAssetValidator, S3KeyValidator>();
        services.AddScoped<IAssetValidator, SizeValidator>();
        services.AddScoped<IAssetValidator, TypeValidator>();

        services.AddScoped<IStorageService>(sp =>
        {
            var inner = sp.GetRequiredService<AwsS3StorageService>();
            var validators = sp.GetServices<IAssetValidator>().ToList();
            return new ValidatingStorageService(inner, validators);
        });

        // services.AddSingleton<IRedisCache, RedisCacheService>();
        return services;
    }
}
