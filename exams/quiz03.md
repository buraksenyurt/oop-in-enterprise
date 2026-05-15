# OOP Test Soruları 03

Bu testin amacı **Dependency Injection** tasarım kalıbının kurumsal yazılım projelerinde nasıl uygulandığını, servis ömürlerini ve DI Container mekanizmalarını ölçmektir.

*Not: Sorular .Net 10 sürümü ve Storage projesi üzerinden hazırlanmıştır.*

---

## Soru 1

Bir web api projesinde controller ve business servis katmanlarında kullanılan çeşitli bağımlılıklar DI Container'a aşağıdaki kod parçasında görüldüğü gibi kayıt edilmiştir.

```csharp
builder.Services.AddScoped<IInvoiceCreateService, InvoiceCreateService>();
builder.Services.AddScoped<IInvoiceReadService, InvoiceReadService>();
builder.Services.AddSingleton<IStorageService, AwsS3StorageService>();
```

Bu yapıda **IInvoiceCreateService** için **AddScoped**, **IStorageService** içinse **AddSingleton** metotları tercih edilmiştir. Bu iki yaşam döngüsü ile ilgili olarak şıklardaki ifadelerden hangisi doğrudur?

- A) **AddScoped** ve **AddSingleton** arasında işlevsel hiçbir fark yoktur. İkisi de aynı nesne örneğini döndürür. Sadece isimlendirme farklılığı söz konusudur.
- B) **AddSingleton** ile kayıt edilen bir servis uygulama boyunca yalnızca bir kez örneklenir ve tüm **HTTP** istekleri bu tek örneği paylaşır. **AddScoped** ise her **HTTP** isteği için ayrı bir örnek üretir. **AwsS3StorageService**'in bağlantı bilgilerini ve konfigürasyonunu uygulama genelinde paylaşması gerektiğinden Singleton tercih edilmesi mantıklıdır; **InvoiceCreateService** ise istek başına izole bir örnek gerektirdiğinden Scoped olarak tanımlanmıştır.
- C) **AddScoped** yalnızca **Console** uygulamalarında kullanılabilir. **Web API** projelerinde AddScoped çağrısı derleme hatası üretir.
- D) **AddSingleton** ile kayıt edilen servis her metot çağrısında yeniden örneklenir ve bu yüzden **AwsS3StorageService** her istekte taze bir bağlantı alır. **AddScoped** ise uygulama genelinde tek bir örnek kullanır.

## Soru 2

Bir Console uygulamasında InvoiceController isimli bir bileşen kullanılmaktadır. Bu bileşenin ihtiyaç duyduğu **IStorageService** implementasyonunu kullanmak için manuel olarak nesne üretmek yerine **DI Container** tercih edilmiştir. Aşağıdaki örnek kod parçasında bu durum ele alınmaktadır.

```csharp
// new ile nesne örneklemeli yaklaşım
// var s3Storage = new AwsS3StorageService("East-1-InvoiceBucket");
// var invoiceController = new InvoiceController(s3Storage);

// DI Container yaklaşımı
var serviceProvider = new ServiceCollection()
    .AddInfrastructure()
    .BuildServiceProvider();

var storageService = serviceProvider.GetRequiredService<IStorageService>();
var invoiceController = new InvoiceController(storageService);
```

**InvoiceController** sınıfının kullandığı **IStorageService** implementasyonunu **new** anahtar kelimesiyle doğrudan **InvoiceController** içinde oluşturmak yerine **DI Container** üzerinden almanın temel avantajı nedir?

- A) `GetRequiredService<T>` çağrısı, **new** anahtar kelimesine göre bellek kullanımı açısından her zaman daha verimlidir. Bu nedenle büyük ölçekli projelerde tercih edilir.
- B) **new** anahtar kelimesiyle nesne oluşturmak C#'ta desteklenmez; bileşenler yalnızca **DI Container** üzerinden elde edilebilir.
- C) **new** ile doğrudan somut sınıfa bağımlı olan **InvoiceController**, depolama stratejisi değiştiğinde kaynak kodun elle güncellenmesini zorunlu kılar. **DI Container** kullanıldığında ise hangi implementasyonun kullanılacağına tek bir noktadan *(Program sınıfı veya bir IServiceCollection genişletme metodu gibi)* karar verilir ve **InvoiceController**'ın kodu değişmeden kalır. Bu da test edilebilirliği ve bakım kolaylığını *(maintainability)* artırır.
- D) **DI Container**, **new** kullanımına kıyasla çalışma zamanı performansını doğrudan artırmaz, yalnızca nesne oluşturma işini biraz geciktirir. Bu nedenle küçük projelerde **new** kullanımı tercih edilmelidir.

## Soru 3

DI Container içinde kullanılan servis kayıtları **DependencyInjection** adlı statik bir sınıfta **IServiceCollection** için yazılmış bir **extension method** içerisinde toplanmıştır. Aşağıdaki kod parçasında bu durum gösterilmektedir.

```csharp
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

        return services;
    }
}
```

Bu tasarım kararının en önemli amacı nedir?

- A) Altyapı katmanına ait tüm DI kayıtlarını tek bir noktada toplamak, **Program** sınıfı gibi uygulama giriş noktalarını sade tutmayı ve hangi servisin hangi implementasyonla eşleştirildiğini tek noktadan yönetmeyi sağlar. Böylece basit bir terminal uygulamasından web api servislerine kadar birçok çalışma ortamında **.AddInfrastructure()** çağrısı yaparak aynı DI kayıtlarına ulaşılabilir. Kayıt mantığını kopyalamak gerekmez.
- B) **static** sınıflar daha hızlı derlenir. Bu nedenle DI kayıtlarını statik bir sınıfa taşımak derleme süresini kısaltır.
- C) **IServiceCollection** arayüzüne **AddInfrastructure** adında yeni bir metot eklemek için .Net Framework'ün kaynak kodunu değiştirmek gerekir.
- D) **DependencyInjection** sınıfının static olması, uygulama boyunca DI Container'ın yalnızca bir kez başlatılmasını garantiler ve bu olmadan DI Container çalışmaz.

## Soru 4

**IServiceCollection** arayüzünün genişletildiği metot içerisinde **IStorageService** arayüzü için *factory delegate* yaklaşımı aşağıdaki kod parçasında görüldüğü gibi ele alınmıştır.

```csharp
services.AddScoped<IStorageService>(sp =>
{
    var inner = sp.GetRequiredService<AwsS3StorageService>();
    var validators = sp.GetServices<IAssetValidator>().ToList();
    return new ValidatingStorageService(inner, validators);
});
```

Bu kod parçasına göre **ValidatingStorageService** sınıfının bağımlı olduğu iki bileşen vardır; bir **IStorageService** implementasyonu ve birde `List<IAssetValidator>` listesi. Factory Delegate tekniğini kullanmak yerine aşağıdaki gibi doğrudan kayıt yapmak neden yeterli olmaz?

```csharp
services.AddScoped<IStorageService, ValidatingStorageService>();
```

- A) Doğrudan kayıt tamamen geçerlidir ve fabrika delegesiyle birebir aynı sonucu üretir. Fabrika delegesinin kullanılmasının teknik bir nedeni yoktur.
- B) DI Container, **ValidatingStorageService** sınıfı yapıcı metodundaki `List<IAssetValidator>` parametresini otomatik çözümleyemez; çünkü bu tür `IEnumerable<IAssetValidator>` değil `List<T>` olarak tanımlanmıştır. Fabrika delegesi sayesinde `GetServices<IAssetValidator>()` ile tüm kayıtlı **IAssetValidator** implementasyonları toplanıp `List<T>`'e dönüştürülür ve **ValidatingStorageService**'e manuel olarak verilir.
- C) Doğrudan kayıt yalnızca **AddSingleton** ile çalışır. **AddScoped** ile generic parametre verilemez ve bu nedenle fabrika delegesi kullanmak zorunludur.
- D) **ValidatingStorageService**, **IStorageService** arayüzünü implement etmediğinden doğrudan `AddScoped<IStorageService, ValidatingStorageService>()` çağrısı derleme hatası verir. Bu nedenle fabrika delegesi kullanılmak zorunda kalınmıştır.

## Soru 5

Bazı hallerde DI servislerine aynı arayüzün birden fazla implementasyonu kayıt edilebilir. Örneğin **IAssetValidator** arayüzü için üç farklı implementasyon aşağıdaki gibi kayıt edilmiştir.

```csharp
services.AddScoped<IAssetValidator, S3KeyValidator>();
services.AddScoped<IAssetValidator, SizeValidator>();
services.AddScoped<IAssetValidator, TypeValidator>();
```

Daha sonra bu implementasyonlar fabrika delegesi içinde **GetServices** metot çağrısı ile aşağıdaki kod parçasındaki gibi yakalanabilir.

```csharp
var validators = sp.GetServices<IAssetValidator>().ToList();
```

Bu kurguyla ilgili aşağıdaki ifadelerden hangisi **doğrudur?**

- A) Aynı arayüz için birden fazla **AddScoped** çağrısı yapılırsa önceki kayıtların üzerine yazılır. Yani yalnızca en son kayıt edilen **TypeValidator** aktif olur, diğerleri yok sayılır.
- B) `GetRequiredService<IAssetValidator>()` çağrısı tüm kayıtlı implementasyonları bir `IEnumerable<T>` olarak döndürür.
- C) `GetServices<IAssetValidator>()` çağrısı, **IAssetValidator** arayüzünü implement eden tüm kayıtlı sınıfların birer örneğini içeren bir koleksiyon döndürür.
- D) Aynı arayüz için birden fazla implementasyon kayıt edilemez. Bu durumda DI Container başlatma anında InvalidOperationException fırlatır ve uygulama çalışmaz.

| Soru | Doğru Cevap |
|------|-------------|
| 1    | B           |
| 2    | C           |
| 3    | A           |
| 4    | B           |
| 5    | C           |
