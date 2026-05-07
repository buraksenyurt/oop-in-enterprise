using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Infra.Services;

/*
    Amacımız, deploama(storage) ve doğrulama(validations) işlemlerini ele alan ama yine bir IStorageService implementasyonu olan
    bir sınıf tasarlamak.

    IStorageService türevi olduğu için DI Container'lar da kullanabilir.
    Kendi içinde IStorageService implementasyonunu kullanabildiği için bir storage service n sayıda validasyonu uygulatabilirim.
    Bir başka deyişle, StorageService'in validasyon işlemlerini de ele alan bir versiyonunu oluşturduğumuz ifade edebiliriz.

    Burada inner ile belirtilen IStorageService örneğin gerçek Save ve Load operasyonlarını icra eden asıl sınıflardır(concrete classes)
    inner ile belirtilen nesnenin validasyon ihtiyacı ise validators isimli liste ile karşılanır.

    Her IStorageService implementasyonu validasyon kuralları içerip işletmek zorunda olmayabilir.
    Önceki stratejide gittiğimizde her storage service implementasyonunun varsayılan olarak çalıştırdığı
    validasyonlar vardı. Yani disable edemediğimiz bir özellik.
    Bu nedenle validasyon ihtiyaçlarını StorageService içerisinde tutmak, StorageService örneklerini
    validasyon türleri ile sıkı bağlar içerisine almak mantıklı değil.
    Bunun yerine validasyon içeren IStorageService implementasyonu veya içermeyen IStorageService implementasyonu gibi farklı seçenekler sunabiliriz. 
    ValidatingStorageService, validasyon işlemlerini ele alan bir IStorageService implementasyonu olarak düşünülebilir.
*/
public class ValidatingStorageService : IStorageService
{
    private readonly IStorageService inner;
    private readonly List<IAssetValidator> validators;

    public ValidatingStorageService(IStorageService inner, List<IAssetValidator> validators)
    {
        this.inner = inner ?? throw new ArgumentNullException(nameof(inner), "Inner storage service cannot be null.");

        if(validators is null || validators.Count == 0)
        {
            throw new ArgumentException("Validators list cannot be null or empty.", nameof(validators));
        }
        this.validators = validators;
    }

    public Task SaveAsync(Asset asset)
    {
        // Concrete class'ta olmayan bir takım validasyon işlemlerinin yapıldığı yer
        foreach (var validator in validators)
        {
            if (!validator.Validate(asset))
            {
                throw new InvalidOperationException($"Asset with key {asset.Key} failed validation.");
            }
        }
        return inner.SaveAsync(asset); // Concrete object neyse onun Save metodunu çağırıyoruz
    }
    public Task<byte[]> LoadAsync(string key)
    {
        return inner.LoadAsync(key); // Burada da concrete object kimse yine onun Load metodunu çağırıyoruz
    }
}
