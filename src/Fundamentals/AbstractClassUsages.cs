/*
    abstract sınıflar da arayüzler (interface) gibi çok biçimliliği destekler.
    Yani kendisinden türeyen nesneleri taşıyabilir, onlara bürünüp onların fonksiyonlarını çalıştırabilir.

    Kendisinden türeyen sınıfıların mutlaka ezmesi gereken davranışları, özellikleri tanımlar (Interface gibi)
    ancak iş yapan metotlar, base metotlar veya özellikler gibi ortak kullanılabilecek veya sadece kendisinin kullanabileceği
    davranışları da tanımlayabilir (Abstract olmayan metotlar gibi)

    Abstract sınıfların new operatörü ile nesne örnekleri (object instance) oluşturulamaz.
*/

namespace Fundamentals;

public class AbstractClassUsages
{
    public static void Run()
    {
        // StorageFactory storageFactory = new StorageFactory(); // new ile örneklenemezler
        StorageFactory storage = new FileStorage("MyFileStorage"); // Bu mümkündür zira kendisinden türeyen nesne örneklerini taşıyabilir.
        storage.Load("123456"); // StorageFactory'nin Load metodunu çağırıyorum ama aslında FileStorage'ın ezdiği Load metodu çalışır. (Polymorphism)

        storage = new NetworkStorage("MyNetworkStorage"); // Şu anda storage değişkeni NetworkStorage nesnesine dönüştü.
        storage.Load("123456"); // StorageFactory'nin Load metodunu çağırıyorum ama aslında NetworkStorage'ın ezdiği Load metodu çalışır. (Polymorphism)

        // Virtual metot kavramına bir bakalım
        var eInvoice = new Asset
        {
            Key = "123456",
            Name = "Fatura",
            Kind = Asset.AssetType.Document
        };
        Console.WriteLine(eInvoice.ToString());
        // Aslında Asset sınıfına bir ToString() metodu yazmadık ama herkes bir Object olduğundan orada tanımlı virtual ToString metoduna erişebilirim.
        // Eğer overide etmezsek varsayılan hali çalışır. (Fundamentals.Asset döner)
        // Edersek kendi yazdığımız ToString() versiyonu çalışır.

        LoadData([new FileStorage("MyFileStorage"), new NetworkStorage("MyNetworkStorage")]);
    }

    public static void LoadData(List<StorageFactory> storages)
    {
        foreach (var stroage in storages)
        {
            stroage.Load("123456");
        }
    }
}

public class Asset
{
    public enum AssetType
    {
        Document,
        Audio,
        Video
    }
    public string Name { get; set; }
    public AssetType Kind { get; set; }
    public string Key { get; set; }

    // İstersek Object.ToString() ezebiliriz(virtual metot olduğu için istersek diyoruz)
    public override string ToString()
    {
        return $"Asset Name: {Name}, Asset Type: {Kind}, Asset Key: {Key}";
    }
}

public abstract class StorageFactory
{
    // Tüm storage servisilerinin bir ismi vardır.
    // Protected olarak tanımlandığı için sadece StorageFactory'den türeyen sınıflardan erişilebilir
    protected string StorageName { get; set; }

    // Abstract sınıflar yapıcı metotlar içerebilir.
    protected StorageFactory(string storageName)
    {
        StorageName = storageName;
    }

    // RunValidations türeyen sınıflar tarafından kullanılan ortak bir fonksiyonellik sağlar.
    // Bunu virtual tanımladığımız için türeyen sınıflar isterse ezebilir eğer ezmezlerse de burada kullanacağımız
    // varsayılan doğrulama davranışları işletilebilir.
    protected virtual bool RunValidations()
    {
        //  validasyon işlemlerinin yapıldığı metot
        return true;
    }
    public abstract void Save(string data); // Bu bir abstract metottur. Türeyen sınıf mutlaka bu davranışı yazmalıdır(override)
    public abstract string Load(string key);
}

/*
    FileStorage sınıfı StorageFactory sınıfından türemiştir. StorageFactory'nin tüm özelliklerini ve davranışlarını miras alır.
    StorageFactory tanımlanan Save ve Load metotlarını mutlaka ezmek zorundadır. (Interface implementasyonundan farklı olarak
    burada override keyword geçtiğine dikkat edelim)

    Abstract sınıflarda abstract olarak tanımlanan üyeler mutlaka türeyenlerde ezilmek (override) zorunda.
    Ancak birde üst sınıflar virtual olarak tanımlanan üyeler olabilir. Object sınıfındaki ToString(), Equals() buna verilebilecek
    en güzel örneklerdendir. Aradaki fark şudur; virtual metotları isterseniz ezebilirsiniz ve doğal davranışını değiştirebilirsiniz,
    ezmezseniz de varsayılan bir davranışı vardır ve onu kullanabiliriz(istersek).
*/
public class FileStorage : StorageFactory
{
    /*
     Burada base keyword kullanımını görüyoruz.
     Buna göre FileStorage sınıfının yapıcı metodu StorageFactory'nin yapıcı metodunu çağırır ve 
     ona storageName parametresini iletir.
    */
    public FileStorage(string storageName) : base(storageName)
    {
    }
    public override string Load(string key)
    {
        Console.WriteLine($"Loading {key} from {StorageName}");
        return $"Data from {StorageName}";
    }

    public override void Save(string data)
    {
        Console.WriteLine($"Saving {data} to {StorageName}");
    }
}

public class NetworkStorage : StorageFactory
{
    // ProtocolType NetworkStorage bileşenine ait bir özellik. FileStorage için geçerli değil.
    // Dolasıyıla Storage kavramının ortak bir özelliği değil.
    public string ProtocolType { get; set; } // tcp, http, gRpc

    // CheckConnection metodu da aslında NetworkStorage'a haiz bir fonksiyonellik. StorageFactory'nin ortak bir davranışı değil.
    public void CheckConnection()
    {
        // Bağlantı kontrolü yapan bir metot
    }
    public NetworkStorage(string storageName) : base(storageName)
    {

    }
    public override string Load(string key)
    {
        Console.WriteLine($"Loading {key} from {StorageName}");
        return $"Data from {StorageName}";
    }

    public override void Save(string data)
    {
        Console.WriteLine($"Saving {data} to {StorageName}");
    }
}