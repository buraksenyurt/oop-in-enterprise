using Storage.Domain;
using Storage.Infra.Contracts;
using Storage.Infra.Services;
using Storage.Infra.Validators;

namespace Storage.ConsoleApp;

/*
    InvoiceController sınıfının bir faturalama sürecini yönettiğini düşünelim.
    Simülasyon amacıyla gerçek hayatta uygulanan bir çok detayı dışarıda bıraktık.
    Bir faturanın elektronik ortamdaki dokümanıda bu sürecin bir parçası.
    
    Buradaki gereksinimlerden ikisinin de faturanın elektronik dokümanının bir ortama kayıt edilmesi
    veya oradan çekilmesi olduğunu düşünelim.

    O zaman soru şu: InvoiceController bileşeninin bu işlemler için hangi bileşenlere ihtiyacı var.
    İhtiyaç duyulan tüm bileşenler Dependency Injection tekniği ile buraya alınabilirler.
*/
public class InvoiceController(IStorageService storageService)
{
    private readonly IStorageService _storageService = storageService;

    // SaveInvoice metodu generic Result türünden değer döner.
    public async Task<Result<Guid>> SaveInvoice(Invoice invoice)
    {
        /*
            Diyelim ki, AWS Storage kullanılıyor. Aşağıdaki gibi InvoiceController sınıfı içinde,
            doğrudan AwsS3StorageService sınıfından bir örnek oluşturup onun üzerinden işlemler yapabiliriz.
            Ancak bu controller başka bir çalışma zamanında(runtime) FileStorageService ile çalışacaksa
            buraya gelip kodu değiştirmek gerekir, ya da burada if/switch vs kullanıp
            neye göre hangi storage service'inin kullanılacağına karar vermek gerekir. 
            Bu da kodun bakımını zorlaştırır.

            Bu kodda çalışır ama sadece AWS3StorageService ile çalışır. Bunun yerine Constructor Injection ile
            bağımlı olduğumuz bileşenin sözleşmesini(interface) dışarıdan alıp kullanabiliriz.
            Kim olacağına InvoiceController sınıfını kullanan yer karar verir.

        */
        //var s3Storage = new AwsS3StorageService("East-1-InvoiceBucket");
        //s3Storage.AddValidator(new S3KeyValidator());
        //await _storageService.SaveAsync(invoice.EInvoice);

        // AddValidator problemini çözmemiz lazım. IStorageService değil, BaseStorage'a ait çünkü (Done, ValidatingStorageService kullanarak)
        // Buradan şunu da ortaya koyabiliriz. Validator(Audit) dediğimiz işlevler, StorageService bileşenlerine mi ait olmalı
        // yoksa bu sorumluluk tamamen farklı bir bileşende mi yönetilmeli?
        // _storageService.AddValidator(new S3KeyValidator());

        try
        {
            await _storageService.SaveAsync(invoice.EInvoice);

            // Herhangi bir exception yoksa geriye işlemin başarılı olduğuna dair bilgi döneriz.
            // Bu bilgi içerisinde işlemin örneğin hangi fatura ID'si ile alakalı olduğunu da söyleyebiliriz.
            return new Result<Guid>
            {
                IsSuccess = true,
                Value = invoice.ID
            };
        }
        catch (Exception ex)
        {
            // Geriye işlemin hatalı olduğunu döndürüyoruz. Hata mesajını da ekleyebiliriz.
            return new Result<Guid>
            {
                IsSuccess = false,
                Value = invoice.ID,
                ErrorMessage = ex.Message
            };
        }


    }

    public async Task<Invoice> GetInvoice(Guid id)
    {
        var eInvoiceContent = await _storageService.LoadAsync(id.ToString());
        // Faturanın bulunması, belgesini çekilip ona eklenmesi
        throw new NotImplementedException();
    }
}

public class Invoice
{
    public Guid ID { get; set; }
    public DateTime AcceptDate { get; set; }
    public decimal TotalAmount { get; set; }
    public Asset EInvoice { get; set; }
}
