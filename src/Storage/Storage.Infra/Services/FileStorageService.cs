using Storage.Domain;
using Storage.Infra.Contracts;
using Storage.Infra.Validators;

namespace Storage.Infra.Services;

/*
    Bu bileşendeki Save metodu birden fazla doğrulama işlemi içerebilir.
    Örneğiden gelen içeriğin boyutunun kontrolü, geçerli türlerden biri olup olmadığını kontrolü.
    Bunu yönetmek için _validators isimli generic list koleksiyonunu kullanabiliriz. Yeni bir validasyon stratejisi gerekirse de,
    bunu da AddValidator ile ekleyebiliriz. SaveAsync içerisindeki foreach döngüsü tüm validator bileşenlerini işletir
    herhangibirisinde ihlal varsa exception fıralatarak süreci keser. 
*/

public class FileStorageService
    : // BaseStorage,
     IStorageService
{
    private const string StorageFolder = "Storage"; // Dışarıdan bir konfigurasyonda alınır
    //private readonly List<IAssetValidator> _validators = [
    //    new SizeValidator(),
    //    new TypeValidator()
    //];
    //public FileStorageService(string name = "FileStorage") : base(name)    {
        
    //}
    //public void AddValidator(IAssetValidator validator)
    //{
    //    _validators.Add(validator);
    //}
    public Task<byte[]> LoadAsync(string key)
    {
        throw new NotImplementedException();
    }
    public Task SaveAsync(Asset asset)
    {
        //if (asset.Content.Length > int.MaxValue)
        //{
        //    throw new InvalidOperationException($"Content size exceeds the maximum allowed limit for {asset.Key}");
        //}

        //// Size validator
        //// Type validator (kabul edilebilen türden bir içerik mi?)
        //foreach (var validator in _validators)
        //{
        //    if (!validator.Validate(asset))
        //    {
        //        throw new InvalidOperationException($"Validation failed for {asset.Key} with {validator.GetType().Name}");
        //    }
        //}

        // base.ApplyValidators(asset);
        Console.WriteLine($"Saving asset with key: {asset.Key} to file system in folder {StorageFolder}");
        return Task.CompletedTask;
    }
}
