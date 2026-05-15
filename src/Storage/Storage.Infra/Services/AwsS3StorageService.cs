using Storage.Domain;
using Storage.Infra.Contracts;
using Storage.Infra.Validators;

namespace Storage.Infra.Services;

/*
    Servis bileşenimiz hem BaseStorage'dan hem de IStorageService'ten türüyor.
    C# dilinde normalde bir sınıfın birden fazla sınıftan türetilmesi yasak.
    Fakat bir sınıf ve n sayıda interface ile çoklu türetme desteklenebilir.
*/
public class AwsS3StorageService
    : //BaseStorage,
      IStorageService
{
    private readonly string _region = "us-east-1"; // Dışarıdan bir konfigurasyonda alınır
    //private readonly List<IAssetValidator> _validators = [
    //    new SizeValidator(),
    //    new TypeValidator()
    //];

    //public AwsS3StorageService(string name = "AwsS3Storage") : base(name)
    //{        
    //}

    //public void AddValidator(IAssetValidator validator)
    //{
    //    // _validators base sınıfta (BaseStorage) tanımlı ve protected erişim belirleyicisi ile işaretlendi.
    //    // Dolayısıyla türeyen sınıflardan erişebiliriz.
    //    _validators.Add(validator);
    //}
    public Task<byte[]> LoadAsync(string key)
    {
        return Task.FromResult(new byte[] { 0x25, 0x50, 0x44, 0x46 });
    }

    public Task SaveAsync(Asset asset)
    {
        // base.ApplyValidators(asset);
        //foreach (var validator in _validators)
        //{
        //    if (!validator.Validate(asset))
        //    {
        //        throw new InvalidOperationException($"Validation failed for {asset.Key} with {validator.GetType().Name}");
        //    }
        //}
        Console.WriteLine($"Saving asset with key: {asset.Key} to AWS S3 in region {_region}");
        return Task.CompletedTask;
    }
}
