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

// ServiceCollection ile çalışma zamanında hangi arayüzlerin hangi bileşenlere denk geldiğini
// DI Container sistemine tanımlayabiliyoruz.

var serviceProvider = new ServiceCollection()
    .AddSingleton<IStorageService, AwsS3StorageService>() // IStorageService talep edildiğinde AwsS3StorageService örneği verilir.
    .BuildServiceProvider();

// Diyelimli çalışma zamanında IStorageService'in gerçek implementasyonuna ihtiyacımız oldu.
// Aşağıdaki gibi DIC üzerinden onun somut örneğini alabilir ve kullanabiliriz
var storageService = serviceProvider.GetService<IStorageService>();

// InvoiceController'ın ihtiyacı olan asıl servisi buradaki gibi Constructor üzerinden gönderebiliriz.
var invoiceController = new InvoiceController(storageService);
await invoiceController.SaveInvoice(new Invoice
{
    ID = Guid.NewGuid(),
    EInvoice = new Asset("invoice-123.pdf", [0x25, 0x50, 0x44, 0x46])

});