/*
    Dependecy'leri yönetmek için .Net'in kendi iç DIC mekanizmasını kullanabiliriz.
    Ama bunun için Microsoft.Extensions.DependencyInjection paketini projemize eklememiz gerekir.
    
    dotnet add package Microsoft.Extensions.DependencyInjection
*/

using Microsoft.Extensions.DependencyInjection;
using Storage.ConsoleApp;
using Storage.Domain;
using Storage.Infra.Contracts;
using Storage.Infra.Services;
using Storage.Infra;

// ServiceCollection ile çalışma zamanında hangi arayüzlerin hangi bileşenlere denk geldiğini
// DI Container sistemine tanımlayabiliyoruz.

/*
    Buradaki önemli konulardan birisi de herhangi bir bileşenin ihtiyaç duyduğu
    diğer bileşenlerin DI Container üzerinde tanımlanmadığı(unutulduğu) durumlardır.
    Böyle bir senaryoda kod derleme zamanında hata vermez ancak çalışma zamanında null reference
    hatasıa düştüğümüz zaman anlayabiliriz.
    Bu durumu deneyimlemek için .AddSingleton çağrısını yorum satırı haline getirip deneyebiliriz.
*/
var serviceProvider = new ServiceCollection()
    // .AddSingleton<IStorageService, AwsS3StorageService>() // IStorageService talep edildiğinde AwsS3StorageService örneği verilir.
    .AddInfrastructure() // AddInfrastructure, IServiceCollection için yazılmış bir genişletme metodu.
                         // Görevi Storage.Infra kütüphanesinde tanımlı olan ve DI Container'a eklenecek bileşenleri tek noktadan yönetmek
    .BuildServiceProvider();

// Diyelimli çalışma zamanında IStorageService'in gerçek implementasyonuna ihtiyacımız oldu.
// Aşağıdaki gibi DIC üzerinden onun somut örneğini alabilir ve kullanabiliriz
/*
Sürpriz Yumurta:

AddInfrastructure kurgusunda ele aldığımız Storage Service implementasyonları constructor üzerinden
Abstract base class bir name parametresi gönderiyorlar. Bu parametre verilmezse ya da optional olarak
atanmazsa çalışma zamanında aşağıdaki çağrımda exception alırız, çünkü DI Container AwsS3StorageService sınıfının
constructor'ındaki string yerine ne gönderileceğini de bilmek ister. Hiç beklenmedik bir bağımlılık yaratmış oluruz.
 */
var storageService = serviceProvider.GetRequiredService<IStorageService>();

// InvoiceController'ın ihtiyacı olan asıl servisi buradaki gibi Constructor üzerinden gönderebiliriz.
var invoiceController = new InvoiceController(storageService);
await invoiceController.SaveInvoice(new Invoice
{
    ID = Guid.NewGuid(),
    EInvoice = new Asset("invoice-123.pdf", [0x25, 0x50, 0x44, 0x46])

});