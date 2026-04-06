/*
    C# programlama dili case-sensitive (Büyük/küçük harf duyarlı) bir dildir. 
    Main metodu ile main metodu farklı anlamda.
    Compiler söz konusu. Build ettiğiniz bir console uygulaması executable olur.
*/

using System;
using System.Collections.Generic;
using System.Text;

// Namepsace içerisindeyiz. Genellikle aynı amaca hizmet eden yapıların bir başlık altında toplanması olarak özetleyebiliriz.
// Sytem namespace en güzel örneklerden birisi. 
// Namespace'ler alt namespace'ler içerebilir.
// Örneğin; System.IO.Pipes. ifadesi System isim alanı altındaki, IO isim alanı altındaki Pipes isim alanı altındaki tiplere
// ulaşmamızı sağlar.
namespace Fundamentals;

public class PrepGuide
{
    public record Country(string Name, string Code, string Continent, string Capital);
    public class DataReporter
    {
        private readonly List<Country> countries = [
            new Country("Türkiye", "TR", "Avrupa", "Ankara"),
            new Country("Almanya", "DE", "Avrupa", "Berlin"),
            new Country("Fransa", "FR", "Avrupa", "Paris"),
            new Country("İtalya", "IT", "Avrupa", "Roma"),
            new Country("Brezilya", "BR", "Güney Amerika", "Brasilia"),
            new Country("Arjantin", "AR", "Güney Amerika", "Buenos Aires"),
            new Country("Meksika", "MX", "Kuzey Amerika", "Mexico City"),
        ];

        public List<Country> Countries()
        {
            return countries;
        }
    }

    // PrepGuide isimli sınıfımızda yer alan Run metodu statik tanımlandığı için
    // çağıran taraf PrepGuide.Run() şeklinde kullanabilir.
    public static void Run()
    {
        /*
            Console bir class. 
            Aşağıdaki kod parçasında Console sınıfının WriteLine metodunu çağırıp ona bir parametre gönderiyoruz.
            Bir sınıf adı üstünden doğrudan metodunu çağırabildik çünkü WriteLine static metot. (Yani o sınıfın new operatörü
            ile nesnesi oluşturmaya gerek kalmadan kullanabiliriz)

            Bir başka statik üye örneği olarak pi sayısı veren Math sınıfının PI isimli constant'ını gösterebiliriz.
        */
        Console.WriteLine("Hello world");
        System.Console.WriteLine($"Matematikteki Pi sayısı {Math.PI}");

        // CatalogProduct sınıfından bir nesne örneği (object instance) oluşturmak için aşağıdaki yollar izlenebilir
        var monitor = new CatalogProduct
        {
            Id = 100123,
            Title = "ElCi 58 inç UHD Super Uber Lux Monitor",
            ListPrice = 999.99M,
            Quantity = 1
        };

        /*
            Product sınıfımızın iki constructor metodu var (Method overloading)
            3 parametre alan versiyon this keyword sayesinde yine CatalogProduct sınıfının 4 parametre alan sürümünü çağırıyor.
        */
        var cpu = new CatalogProduct(1001, "Gogıl Quanta çip", 5000.00M);
        var gpu = new CatalogProduct(1002, "EnVidiya GTX5400", 1499.50M, 10);


        /*
            readonly ile const kullanımları bazen karıştırılabilir.
            İlk ayrım şu: readonly tanımlı değişkenler runtime'da hesaplanır.
            const değişkenler ise derleme zamanında(compile time) hesaplanır. 

            Initialization farklı olabilir. readonly ise declartion tarafında veya yapıcı metodda(constructor)
            const ise sadece declaration tarafında başlatılabiliyor.
        */


        // try
        // {
        //     var blue = new Rectangle(10, -4);
        // }
        // catch (ShapeException exception)
        // {
        //     Console.WriteLine(exception.Message);
        // }

        var azonCorp = new Company("Azon Mefrüşat Komandit Şirketi");
        // azonCorp.Title = "Limited oldu"; // Burada da Title bilgisini değiştiremeyiz çünkü readonly tanımlanmıştır.
        var newAzonCorp = new Company("Azon Limited Şirketi"); // Yeni bir nesne örneği oluşturup ona farklı bir değer atayabiliriz.

        var point1 = new Point(10.5f, 20.3f);
        // point1.X = 13.0f; // Point record türü olduğu için değerlerini değiştiremeyiz. Immutable bir türdür.
        Console.WriteLine($"Point1: X={point1.X}, Y={point1.Y}");
        var point2 = new Point(10.5f, 20.3f);
        if (point1 == point2)
        {
            Console.WriteLine("Point1 ve Point2 eşittir");
        }
        else
        {
            Console.WriteLine("Point1 ve Point2 eşit değildir");
        }

        var origin = new Origin { X = 10.5f, Y = 20.3f };
        var newOrigin = new Origin { X = 10.5f, Y = 20.3f };
        if (origin == newOrigin)
        {
            Console.WriteLine("Ayn origin");
        }
        else
        {
            Console.WriteLine("Farklı origin");
        }

        var burak = new Person("Burak", 30);
        // burak.Name = "Burak Selim";
        var biBurakDaha = new Person("Burak", 30);
        if (burak == biBurakDaha)
        {
            Console.WriteLine("Burak ve biBurakDaha eşittir");
        }
        else
        {
            Console.WriteLine("Burak ve biBurakDaha eşit değildir");
        }
        var p1 = new Point2D(10.0f, 20.0f);
        p1.X = 15.0f;


        // Kuzey Amerika kıtasındaki ülkelerin başkentlerini
        var reporter = new DataReporter();
        reporter
            .Countries()
            .Where(c => c.Continent == "Kuzey Amerika") // Where metoduna parametre bir kod bloğu veriyoruz (=> lambda operatörü olarak geçiyor)
            .ToList() // Kıtası Kuzey Amerika olan ülkeleri bir listeye dönüştürüyoruz
            .ForEach(c => Console.WriteLine($"{c.Name} - {c.Capital}")); // Burada yine ForEach metoduna bir kod bloğunu parametre olarak geçiyoruz.
                                                                         // ve her bir ülke için o ülkenin adını ve başkentini yazdırıyoruz.
    }
    public class Origin
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
    public record Point(float X, float Y);
    // record türü immutable (değiştirilemez) bir türdür.
    // Yani Point türünden oluşturulan bir nesnenin X ve Y değerleri oluşturulduktan sonra değiştirilemez.
    // Eşitliklerde de veri karşılaştırılması yapılır. Yani iki Point nesnesi X ve Y değerleri aynı ise birbirlerine eşit kabul edilirler.

    /*
     record class, record struct, readonly record struct kullanımlarına da bir bakalım.
     */
    public record class Person(string Name, int Age); // Referans türlü olan record türü. Varsayılan olarak immutable ve heap'de duruyor.
    public record struct Point2D(float X, float Y); // Value type olarak geçer ve stack'te durur. Varsayılan olarak mutable.
    public readonly record struct ImmutablePoint(float X, float Y); // Value type'dır. Immutable ve property'ler readonly'dir.
    public class Company
    {
        public readonly int Year = 2025;
        public readonly string Title;
        private const string CountryCode = "TR";
        // const olarak tanımlanmış CountryCode değişkeni sadece bu sınıf içerisinde kullanılabilir ve değeri değiştirilemez.
        public Company(string title)
        {
            Title = title;
        }
        public void Update()
        {
            /*
             Title readonly bir değişken olduğu ya tanımlandığı yerde ilk değerini alabilir ya da
             constructor içerisinden değer ataması yapılabilir.
              Buradaki gibi farklı bir metot içerisinden değer ataması yapamayız. Compile time error verir.
             */
            // Title = "New Title";
        }
    }

    //public class Invoice
    //{
    //    public string Id { get; set; }
    //    // public string InvoiceNumber{get;set;}
    //    public InvoiceNumber InvoiceNumber { get; set; }
    //}
    //public class InvoiceNumber
    //{
    //    public readonly string Value;
    //    public InvoiceNumber(string value)
    //    {
    //        Value = value.Trim();
    //        if (string.IsNullOrEmpty(value))
    //        {
    //            throw new ArgumentException("Invoice number cannot be empty or null");
    //        }
    //        // readonly bir alan olduğu için sadece constructor içerisinde değer ataması yapabiliriz.
    //        Value = value;
    //    }
    //}

    /*
        C# dilinin zengin bir tip sistemi var(Common Type System). Genelde belli başlı enstrümanlar üzerine kurgulanıyorlar.
        class, struct, interface, enum, delegate, record ...
        int, float, byte gibi türler struct ve value type olarak geçer
        string bir class türüdür. referans türü olarak geçer.
        struct ve class'lar bellekte farklı şekilde tutulduğu için önemlidir (stack ve heap konusuna bakılabilir)

        Bir iş alanı ile ilgili kavramı tanımlarken sınıflardan çokça yararlanırız.

        Sınıflar genellikle veri taşımak için özellikler (property) veya alanlar (field) taşır.
        İş yapan metotları olabilir.
        Event'ler içerebilir vs
    */
    public class CatalogProduct // Sistemdeki ürünleri tariflemek istiyorum
    {
        public CatalogProduct() // Default constructor. Yani parametresiz çalışan nesne örnekleme metodu.
        {
        }
        // Constructor metotlar ile de nesneler örneklenebilir.
        public CatalogProduct(int id, string title, decimal listPrice, int Quantity)
        {
            Id = id;
            Title = title;
            ListPrice = listPrice;
            this.Quantity = Quantity;
            // this kullanmak zorunda değiliz ama buradaki anlamı şu; Product sınıfındaki Quantity alanını ifade ediyorum
        }

        public CatalogProduct(int id, string title, decimal listPrice)
            : this(id, title, listPrice, 0) // Burada ise; CatalogProduct sınıfındaki yapıcı metodun 4 parametre alan versiyonunu çağır
        {
        }
        // public CatalogProduct(int id, string title)
        //     : this(id, title, 1.0M, 1)
        // {
        // }
        public int Id { get; set; } // Sayısal bir id değeri olur (benzersiz olarak düşünülür, onun kimliğini ifade eder vs)
        public string Title { get; set; } // Ürünü tanımlayan bir isimlendirme
        public decimal ListPrice { get; set; } // Ürünün fiyatı
        public int Quantity { get; set; } // Ürün miktarı ağırlık cinsinden de olabilir, adet cinsinden de olabilir vs
    }
    public struct Rectangle
    {
        public float Width { get; private set; }
        public float Height { get; private set; }
        public Rectangle(float width, float height)
        {
            if (width <= 0.0) throw new InvalidSizeException(width);
            if (height <= 0.0) throw new InvalidSizeException(height);
            Width = width;
            Height = height;
        }
    }
    /*
        ShapeException , Exception (.Net ile gelen) sınıfından türediği için(inherit)
        throw ile çalışma zamanına fırlatılabilir ve catch bloğu ile de yakalanabilir.

        Ayrıca, InvalidSizeException'da ShapeException sınıfından türediği için dolaylı olarak 
        bir Exception nesnesidir. Yani onu da throw ile ortama fırlatabilir ya da catch ile yakalayabiliriz.
    */
    public class ShapeException : Exception
    {
        public ShapeException(string message)
            : base(message) // base ile ShapeException'a gelen message değeri aslında Exception sınıfının yapıcı metoduna gidiyor.
        {
        }
    }
    public class InvalidSizeException : ShapeException
    {
        public float SourceValue { get; }

        public InvalidSizeException(float value)
            : base($"Value cannot be negative. The source value is `{value}`") // base keyword ile aslında bir üst sınıfın yapıcı metodunu çağırıyoruz
                                                                               // ki bu senaryoda ShapeException oluyor.
        {
        }
    }
}
