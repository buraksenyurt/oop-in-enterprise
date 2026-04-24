using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Infra.Services;

/*
    Tüm StorageService türevlerinde varsayılan validator bileşenleri kullanılacaksa,
    kod tekrarını önlemek için nasıl bir yol izleyebiliriz?

    Abstract class oluşturup validasyonları buradan yönetebiliriz,
    Validasyon nesne koleksiyonunu storage servislerine constructor üzerinden enjekte edebiliriz
    gibi...
*/
public class AwsS3StorageService
    : IStorageService
{
    private readonly string _region = "us-east-1"; // Dışarıdan bir konfigurasyonda alınır
    private readonly List<IAssetValidator> _validators = [
        new SizeValidator(),
        new TypeValidator()
    ];
    public void AddValidator(IAssetValidator validator)
    {
        _validators.Add(validator);
    }
    public Task<byte[]> LoadAsync(string key)
    {
        throw new NotImplementedException();
    }
    public Task SaveAsync(Asset asset)
    {
        foreach (var validator in _validators)
        {
            if (!validator.Validate(asset))
            {
                throw new InvalidOperationException($"Validation failed for {asset.Key} with {validator.GetType().Name}");
            }
        }

        throw new NotImplementedException();
    }
}
