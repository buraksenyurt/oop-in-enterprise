using Storage.Domain;

namespace Storage.Infra.Contracts;

/*
Senaryomuzda yer alan farklı depolama stratejilerinde ana fonksiyonların 
tanımlandığı sözleşme(contract) türü.

Task: İlgili metotların asenkron olarak çağırılabilmesini sağlar.
*/
public interface IStorageService
{
    Task SaveAsync(Asset asset);
    Task<byte[]> LoadAsync(string key);
    // void AddValidator(IAssetValidator validator);
}
