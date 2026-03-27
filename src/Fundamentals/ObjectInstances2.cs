namespace Fundamentals;

/*
    Senaryomuz: Depodaki ürünlere ait temel bilgileri tutan bir sınıf inşa edelim. Bu sınıf stok bilgisi de tutsun,
    bir üründen stokta ne kadar var öğrenebilelim. Stok artışı yapma veya stoktan düşürme gibi birkaç fonksiyonellikde ekleyelim. 
    
    Gitmek istediğimiz nokta; Rich Entity sınıfı tasarımı yapmak.
    Sadece veri tutan değil, bu veriler üzerinde iş yapan fonksiyonellikler barındıran bir sınıf tasarlamak.
    Rich Entity birçok yazılım mimari yaklaşımında karşımıza çıkabilir, örneğin Domain Driven Design(DDD).
*/

// public record Category(Guid CategoryId, string Name);

public class Product
{
    // Guid, Global Unique Identifier, dünya üzerinde benzersiz bir şekilde tanımlanabilen bir veri türüdür.
    // ProductId isimli property, dışarıdan okunabilir (public get) ancak dışarıdan değiştirilemez( private set)
    // Yani sadece sınıf içerisinde değiştirebilirim veya bir metod üzerinden değerini içeriye alabilirim.
    public Guid ProductId { get; private set; }
    public string Title { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int StockLevel { get; private set; }
    // public Category Category { get; private set; }

    /*
        Sınıf adıyla aynı isimli olan bu metod diğer metotlardan farklıdır.
        Geriye void dahi bir şey döndürmez. Bunlar yapıcı metod(constructor) olarak ifade edilirler.
        Bir nesneyi örneklemek için kullanılırlar.
        Bir sınıfın birden fazla yapıcı metodu olabilir (constructor overloading).
        Method overloading : aynı isimli metodun farklı parametre imzalarına/argüman sayılarına sahip farklı versiyonları yazılabilir.
     */
    private Product(Guid productId, string title)
    {
        /*
            C# gibi diller managed ortamlarda çalışır. Bu ortamlarda çalışma zamanı (runtime) hataları
            istisna yönetimi (exception handling) mekanizmalarıyla ele alınır. 
            İstisnalar(Exceptions) çalışma zamanında ele alınmadıkları durumda programın istem dışı sonlanmasına neden olurlar.
            Çalışma zamanına bilinçli olarak bir istisna(Exception) fırlatmak istediğimizde throw keyword kullanılabilir.

            Exception türleri genel isimlendirme standardı olarak Exception kelimesi ile biter.
            ArgumentException, InvalidOperationException, DivideByZeroException, NullReferenceException, FileNotFoundException vb
            İşin güzel yanı kendi Exception türlerimizi de yazabiliriz.
         */

        // Bir iş kuralı ekledik. Domain içerisindeki çekirdek(core) entity'lerden birisi olan
        // Product için Title bilgisinin boş veya null olmaması gerektiği kuralını ekledik.
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Ürün bilgisi girilmelidir.");
        // Bir iş kuralı daha. Buna göre title bilgisi 50 karakterden uzun olmamalıdır.
        if (title.Length < 10 && title.Length > 50) throw new ArgumentException("Ürün bilgisi yeterli uzunlukta olmalıdır.");

        ProductId = productId;
        Title = title;
    }
    // Bu yapıcı metod, this ile productId ve title bilgisini diğer yapıcı metoda gönderir.
    // this ve base keyword'leri bazen karıştırılabilir.
    // Bu senaryoda this keyword aynı sınıf içerisindeki diğer uygun olan yapıcı metodu işaret eder.
    // base ile genelde türün türediği üst sınıfın elemanlarına erişiriz.
    public Product(Guid productId, string title, decimal unitPrice)
        : this(productId, title)
    {
        // Burada da birim fiyat için bir kural eklemiş olduk.
        if (unitPrice <= 0.0M) throw new ArgumentException("Birim fiyatı negatif olamaz.");
        UnitPrice = unitPrice;
    }
    // Stok miktarını artırmak veya azaltmak için yine çekirdek iş kuralları barındıran metodlar kullanabiliriz.
    public void IncreaseStock(int quantity)
    {
        // Stok artış değeri 0dan küçük veya eşitse bir exception fırlatılır.
        if (quantity <= 0) throw new ArgumentException("Stok artışı için miktar pozitif olmalıdır.");

        StockLevel += quantity;
    }
    public void DecreaseStock(int quantity)
    {
        // Stok azalış değeri 0dan küçük veya eşitse bir exception fırlatılır.
        //if (quantity <= 0) throw new ArgumentException("Stok azalışı için miktar pozitif olmalıdır.");
        if (quantity <= 0) throw new InvalidStockAmountException();

        // Stok azalış miktarı mevcut stok seviyesinden fazla ise bir exception fırlatılır.
        //if (quantity > StockLevel) throw new InvalidOperationException("Stok seviyesinden fazla ürün çıkarılamaz.");
        // Kendi yazdığımı exception türünü de fırlatabiliriz(Domain'e özgü bir exception türü yazmak gibi).
        if (quantity > StockLevel) throw new InvalidStockAmountException();

        StockLevel -= quantity;
    }
}

/*
    InvalidStockAmountException, Exception sınıfından türetildiği için (inheritance)
    throw keyword ile birlikte kullanılabilir ve dolayısıyla bir catch bloğunda yakalanabilir. 
*/
public class InvalidStockAmountException
    : Exception
{
    public InvalidStockAmountException()
        : base("Invalid stock amount") // base keyword'ü ile üst sınıfın constructor'ına(Exception sınıfının yapıcı metoduna) mesajı gönderiyoruz.
    {
    }
}

public class ObjectInstances2
{
    /*
     DİKKATE ALALIM:
     Kodu breakpoint koyarak debug edelim ki, exception handling ve kod akışını doğru şekilde izleyebilelim.

     Start Debugging ile başlattık.
     Step Into (F11) ile adım adım kodu izleyelim.
     Step Over (F10) ile adım adım kodu izleyelim ancak metodların içine girmeyelim.
     Continue (F5) ile bir sonraki breakpoint'e kadar olan kodu çalıştırarak ilereyelim.
    */
    public static void Run()
    {
        // var laptop = new Product();
        // The property or indexer 'Product.ProductId' cannot be used in this context because the set accessor is inaccessible
        // laptop.ProductId = Guid.NewGuid(); // private set olduğu için ProductId bilgisi bu satırda olduğu gibi dışarıdan belirlenemez.

        Console.WriteLine("Product instances:");

        try
        {
            var laptop = new Product(Guid.NewGuid(), "AyBiEm i7 14.1 inch, 1Tb laptop", 1499.99M);
            laptop.IncreaseStock(10); // stok seviyesini 10 artırdık.
            Console.WriteLine($"{laptop.Title} stok seviyesi: {laptop.StockLevel}");
            laptop.DecreaseStock(1);
            laptop.DecreaseStock(-1); // stok seviyesini 1 azalttık. Stok azalış miktarı negatif olduğu için InvalidStockAmountException fırlatılmasına neden olur.
            Console.WriteLine($"{laptop.Title} stok seviyesi: {laptop.StockLevel}");

            // var mouse = new Product(Guid.NewGuid(), "", 29.99M);
            // var csharpBook = new Product(Guid.NewGuid(), "C# Programming Book"); // 2 parametreli constructor private olduğu için erişilemez.
            var csharpBook = new Product(Guid.NewGuid(), "C# Programming Book", 0.0M); // 0 değeri ele alan iş kuralı devreye girip exception fırlatılmasına neden olur.

            Console.WriteLine($"Laptop: {laptop.Title}, Price: {laptop.UnitPrice}, Stock Level: {laptop.StockLevel}");
            Console.WriteLine($"Book: {csharpBook.Title}, Price: {csharpBook.UnitPrice}, Stock Level: {csharpBook.StockLevel}");
        }
        // Birden fazla catch bloğu yazabiliriz ama sıralaması önemlidir. En alt kümedeki Exception türevinden en genele doğru bir sıralama yapılır.
        catch (InvalidStockAmountException exp) // InvalidStockAmountException türünde bir exception oluştuğunda bu blok çalışacak.
        {
            Console.WriteLine(exp.Message);
        }
        catch (Exception exp) // try bloğu içerisinde bir Exception oluştuğunda bu blok çalışacak.
        {
            // .Net içinde tanımlı birçok Exception türü vardır.
            // Tüm exception türleri System.Exception sınıfından türemiştir.
            // Dolayısıyla bu türler için tek bir catch bloğu yazmak mümkündür.
            // Ama belirli bir exception türünü de catch bloğunda ele alabiliriz.(Sadece ArgumentException türü için catch bloğu yazmak gibi)
            // Kendi exception türlerimizi de yazabiliriz. Tek yapmamız gereken Exception sınıfından türetmektir(Örneğin InvalidStockLevelException)
            Console.WriteLine(exp.Message);
            //todo@buraksenyurt Farklı Exception türlerinin ele alınması ve kendi exception türlerimizi yazılması konularını da ele alalım
        }
        finally // try bloğunda exception olsa da olmasa da mutlaka çalışır
        {
            // Çoğunlukla kaynakları(resources) iade etmek için kullanılır.
            // Okumak üzere açılan dosyaların kapatılması, ara belleğe alınmış verilerin işlenmesi,
            // Veritabanı bağlantılarının kapatılması, açık network bağlantılarının kapatılması gibi işlemler için kullanılır.
            Console.WriteLine("Finally bloğu.");
        }

        Console.WriteLine("End of the program");
    }
}
