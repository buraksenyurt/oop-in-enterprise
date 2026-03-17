# OOP Test Soruları 00

Bu testin amacı OOP *(Object-Oriented Programming)* kavramlarını örnek senaryolar üzerinden değerlendirmektir. Aşağıdaki sorulara verilen seçeneklerden en uygun olanını seçiniz.

*Not: Sorular .Net 10 sürümü üzerinden hazırlanmıştır.*

---

## Soru 1

Terminalden çalışan bir uygulama yazmak istiyorsunuz. C# programlama dili ile çalışmayı tercih ettiniz. Terminal uygulamaları **executable** olarak derlenirler ve örneğin **Windows** sistemlerinde **.exe** uzantılı dosyalar oluşur. Bu tür uygulamalar genellikle **Console Application** olarak adlandırılır. Aşağıda örnek bir terminal kod parçasına yer verilmiştir.

```csharp
using System;

namespace HelloWorld;

public class Application
{
    public static void Main()
    {
        Console.WriteLine("Merhaba Dünya!");
    }
}
```

Bu kod parçası ile ilgili aşağıdaki düşüncelere yer verilmiştir.

- **I.** C türevli birçok programlama dilinde olduğu gibi, C# dilinde yazılan bir executable'ın giriş noktası **Main** metodudur.
- **II.** C# case-sensitive bir dildir, bu nedenle **Main** ve **main** farklı tanımlardır.
- **III.** Terminal uygulamaları genellikle grafik arayüzü içermezler, bu nedenle kullanıcı etkileşimi genellikle metin tabanlıdır.
- **IV.** .Net standart kütüphanesinde **WriteLine** isimli bir metot yoktur.

Sizce hangi ifade/ifadeler doğrudur?

- A) Yalnız I
- B) I ve II
- C) I, II ve III
- D) Hepsi

## Soru 2

80li yılların sevilen oyunlarına ait bazı bilgilerin yer aldığı bir verisetine denk geldiğinizi düşünün. Söz konusu veriseti **dat** uzantılı bir dosyada **Comma-Separated Values (CSV)** formatında saklanmakta. Aşağıda örnek bir iki satıra yer verilmektedir. Bu dosyada oyunların hangi yıl piyasaya sürüldüğü, hangi şirket tarafından geliştirildiği ve kullanıcı puanları gibi bilgiler yer almaktadır.

```text
10000|Pac-Man|1980|Namco|6.79|
10001|Donkey Kong|1981|Nintendo|9.50|
10002|Space Invaders|1978|Taito|8.00|
10003|Prince of Persia|1989|Broderbund|8.50|
10004|Tetris|1984|Alexey Pajitnov|9.00|
```

Yaklaşık 1000 satırdan oluşan bu verisetini programatik ortamda modellemek istiyorsunuz. Herbir satırı bir sınıf örneği *(Object Instance)* olarak ifade etmeye karar verdiniz. Aşağıdaki sınıf tasarımlarından hangisi doğru bir modelleme sağlar?

A)

```csharp
public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ReleaseYear { get; set; }
    public string Developer { get; set; }
    public double UserRating { get; set; }
}
```

B)

```csharp
public class Game
{
    private Id;
    private Name;
    private ReleaseYear;
}
```

C)

```csharp
public class Game
{
    public string Id;
    public string Name;
    public string ReleaseYear;
    public string Developer;
    public string UserRating;
}
```

D)

```csharp
public class Game
{
    public string Id { get; }
    public string Name { get; set; }
    public string ReleaseYear { get; set; }
    public string Developer { get; set;}
    public string UserRating { get; set;}
}
```

## Soru 3

Bir fabrikadaki ürünler aşağıdaki sınıf ile modellemeye çalışılmıştır:

```csharp
public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
}
```

Dikkat edileceği üzere **Id** özelliği sadece okunabilir bir özellik *(Readonly Property)* olarak tanımlanmıştır. Sizce *Id* özelliğini bu şekilde kapsüllemenin *(Encapsulation)* amacı nedir?

- A) Ürünlerin benzersiz bir kimliğe sahip olmasını sağlamak ve bu kimliğin dışarıdan değiştirilmesini engellemek.
- B) Ürünlerin fiyatlarının ve stok bilgilerinin güvenliğini sağlamak.
- C) Ürünlerin adlarının değiştirilmesini engellemek.
- D) Ürünlerin kimlik bilgilerini gizlemek ve sadece ürün adı, fiyat ve stok bilgilerini erişilebilir kılmak.

## Soru 4

**C#** programlama dili genel bir tip sistemini *(Common Type System)* benimser. Bu tip sistemi sayesinde farklı türlerdeki verilerle çalışmak mümkün olur. *int, float, double, char, string* gibi ilkel türler *(primitive types)* ve *class, struct, interface, enum, delegate* gibi karmaşık türler *(complex types)* bu tip sisteminin bir parçasıdır. Böyle bir tip sistemi gerçek dünya nesnelerinin modellenmesini de kolaylaştırır. Örneğin **enum** türünü kullanarak bazı sayısal değerleri anlamlı isimlerle ifade edebiliriz. Aşağıdaki kod parçasında temel renklerden sekizini temsil eden bir enum tanımı yer almaktadır.

```csharp
public enum BasicColor
{
    Red,
    Green,
    Blue,
    Yellow,
    Cyan,
    Magenta,
    White,
    Black
}
```

Bu enum tanımından da yararlanarak geliştirmekte olduğunuz oyuna katılacak karakterlerin favori renklerini ifade etmek istiyorsunuz. Öncelikle karakterlerinizi temsil edecek ve ilk kez tanımlandıktan sonra içeriği değiştirilemez *(immutable)* değiştirilmek istenirse de yeni bir örneği oluşturmayı zorunlu kılan bir enstrüman kullanmak istediğimizi varsayalım. Elimizde aşağıdaki seçenekler mevcut.

I.

```csharp
public class Character
{
    public string Name { get; }
    public BasicColor FavoriteColor { get; }

    public Character(string name, BasicColor favoriteColor)
    {
        Name = name;
        FavoriteColor = favoriteColor;
    }
}
```

II.

```csharp
public record Character(string Name, BasicColor FavoriteColor);
```

III.

```csharp
public readonly struct Character
{
    public string Name { get; }
    public BasicColor FavoriteColor { get; }

    public Character(string name, BasicColor favoriteColor)
    {
        Name = name;
        FavoriteColor = favoriteColor;
    }
}
```

Bu yaklaşımlardan hangisini veya hangilerini tercih edersiniz.*(**Immutable** olma halini dikkate alınız)*

- A) I ve II
- B) I, II ve III
- C) Sadece I
- D) Sadece II

## Soru 5

Kurumsal çaptaki büyük projelerde verilerin tutarlılığını sağlamak önemlidir ve bunun için farklı seviyelerde çeşitli kurallar işletilebilir. Verinin doğru şekilde girilmesini sağlamak bunu yaparken bazı doğrulama kurallarını işletmek kullanılan yöntemler arasındadır. Bazı doğrulama kuralları domain yapısını korumak için en alt çekirdek nesnelerde değerlendirilir. Örneğin aşağıdaki basit kod parçasını göz önüne alalım.

```csharp
public class Product
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public decimal UnitPrice { get; private set; }
    
    private Product(Guid productId, string title) {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Ürün başlığı boş olamaz.");

        Id = productId;
        Title = title;
    }

    public Product(Guid productId, string title, decimal unitPrice)
        : this(productId, title)
    {
        if (unitPrice <= 0.0M) throw new ArgumentException("Birim fiyatı negatif olamaz.");

        UnitPrice = unitPrice;
    }
}
```

Sizce aşağıdaki şıklardan hangisi bu kod parçasındaki amaçlardan birisi **değildir**.

- A) Bir ürün nesnesi oluşturulurken negatif bir fiyat belirlenmesini engellemek.
- B) Ürün nesnesi oluşturulurken başlık bilgisinin boş veya sadece boşluklardan oluşmasını engellemek.
- C) Oluşturulan her bir ürün nesnesinin benzersiz bir kimliğinin olmasını sağlamak.
- D) Bir ürün oluşturulduktan sonra başlık ve fiyat bilgilerinin değiştirilebilmesini sağlamak.

## Soru 6

Bazı durumlarda bir sınıfın ilgili alanları sadece belirli metotlar üzerinden değişime açık olabilir. Örneğin bir oyuncunun genel skor bilgisini belirli oranda azaltmak veya artırmak için iki ayrı metot kullanmak seçeneklerden birisidir. Aşağıdaki kod parçasında bu durum örneklenmiştir.

```csharp
public class Player
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public Player(string name, int initialScore)
    {
        Name = name;
        Score = initialScore;
    }

    public void IncreaseScore(int amount)
    {
        if (amount < 0) throw new ArgumentException("Puan artırma miktarı negatif olamaz.");
        Score += amount;
    }

    public void DecreaseScore(int amount)
    {
        if (amount < 0) throw new ArgumentException("Puan azaltma miktarı negatif olamaz.");
        if( amount > Score) throw new ArgumentException("Puan azaltma miktarı mevcut puandan fazla olamaz.");
        Score -= amount;
    }
}
```

Bu kod parçası ile ilgili şu fikirler ortaya atılmıştır:

- **I.** Oyuncunun skor bilgisini doğrudan değiştirmek mümkün değildir, sadece *IncreaseScore* ve *DecreaseScore* metotları üzerinden değişiklik yapılabilir.
- **II.** Hatalı skor bilgisi girilmesi durumunda ortama istisna *(Exception)* fırlatılır ve bu istisnaların nesne kullanıcısı *(object user)* tarafında yönetilmesi beklenir.
- **III.** Bu sınıf tasarımı oyuncunun skor bilgisinin tutarlılığını sağlamak için kapsülleme *(Encapsulation)* ilkesine uygun bir şekilde tasarlanmıştır.
- **IV.** Bu kod parçası derlenmez çünkü *Score* özelliği sadece okunabilir *(readonly)* olarak tanımlanmıştır ve *IncreaseScore* ve *DecreaseScore* metotları bu özelliği değiştirmeye çalışmaktadır.

Sizce hangi ifade/ifadeler doğrudur?

- A) Hepsi
- B) I, II ve III
- C) Sadece I
- D) III, IV

## Soru 7

İstisna yönetimi C# ve benzeri birçok programlama dilinde ele alınan önemli kavramlardan birisidir. İstisnalar programın çalışması sırasında ortaya beklenmedik durumları ifade eder ve bu durumların programın normal akışını bozmadan yönetilmesi gerekir. Aşağıdaki kod parçasında basit bir istisna yönetimi örneği yer almaktadır.

```csharp
public class Calculator
{
    public int Divide(int numerator, int denominator)
    {
        if (denominator == 0) throw new DivideByZeroException("Bölen sıfır olamaz.");
        return numerator / denominator;
    }
}

public class Program
{
    public static void Main()
    {
        var calculator = new Calculator();
        try
        {
            int result = calculator.Divide(10, 0);
            Console.WriteLine($"Sonuç: {result}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
        }
        Console.WriteLine("Program sonlandı.");
    }
}
```

Bu kod parçası ile ilgili aşağıdaki ifadeler ortaya atılmıştır:

- **I.** *Divide* metodu bölen sıfır olduğunda bir istisna fırlatır ve bu istisna *Main* metodunda yakalanır.
- **II.** İstisna yönetimi sayesinde programın beklenmedik durumlarda çökmesi engellenir ve kullanıcıya anlamlı bir hata mesajı sunulur.
- **III.** Bu kod parçası derlenmez çünkü *Divide* metodunda *DivideByZeroException* türünde bir istisna fırlatılmaktadır.
- **IV.** *try-catch* bloğu içinde yer alan kod parçası sadece *Divide* metodunu çağırmakla kalmaz aynı zamanda bölme sonucunu **Infinite** olarak ekrana yazdırır.

Sizce hangi ifade/ifadeler doğrudur?

- A) I, II ve IV
- B) Hepsi
- C) I ve II
- D) Sadece III

## Soru 8

Aşağıdaki kod parçasını dikkatlice inceleyelim.

```csharp
using System;

public class Counter
{
    private int _count;

    public void Increment()
    {
        if (_count == int.MaxValue) throw new OverflowException("Sayaç değeri maksimuma ulaştı.");
        _count++;
    }

    public void Decrement()
    {
        if (_count == int.MinValue) throw new OverflowException("Sayaç değeri minimuma ulaştı.");
        _count--;
    }

    public int GetCount()
    {
        return _count;
    }
}

public class Program
{
    public static void Main()
    {
        var counter = new Counter();
        for (int i = int.MaxValue-1; i <= int.MaxValue; i++)
        {
            counter.Increment();
        }
        Console.WriteLine($"Sayaç değeri: {counter.GetCount()}");
    }
}
```

Sizce bu kod parçası çalışma zamanında nasıl bir davranış sergiler?

- A) Kod derlenmez çünkü Counter sınıfında tanımlı _count alanı int türünün sınırlarını aşabilir ve bu durum derleyici tarafından tespit edilir.
- B) Sayaç değeri sadece bir kez artar ve ardından bir istisna fırlatılır çünkü sayaç değeri int.MaxValue değerine ulaştığında Increment metodu bir **OverflowException** fırlatacak şekilde tasarlanmıştır.
- C) Sayaç değeri sürekli artar, MaxValue aşıldığında - değerinden devam eder ve program normal şekilde çalışmaya devam eder.
- D) Sayaç değeri sürekli artar, MaxValue değerine ulaşıldığında sıfırlanır ve doğal olarak ekrana "Sayaç değeri: 0" yazdırılır.

## Soru 9

Elimizde oyun bilgilerinin tutulduğu generic bir koleksiyon olduğunu düşünelim. Tüm oyun bilgileri **Game** türünden bir nesne ile ifade edilmekte ve **Inventory** sınıfında `List<Game>` türünden bir özellikle tutulmakta olsun. Bu ilişkinin Inventory sınıfında aşağıdaki gibi tanımlandığını varsayalım.

```csharp
public class Inventory
{
    public List<Game> Games { get; private set; }

    // Diğer kodlar
}
```

C#, fonksiyonel dil özelliklerini de desteklediğinden koleksiyon gibi veri yapıları üzerinde çeşitli fonksiyonel işlemler yapmaya olanak tanır. Birçok iterasyonu destekler. Örneğin `Where`, `Select`, `ForEach` gibi metotlar yardımıyla bu veri yapıları üzerinde filtreleme, dönüştürme ve yineleme işlemleri icra edilebilir. Aşağıdaki kod parçasında bu tür bir kullanım örneği yer almaktadır.

```csharp
var inventory = new Inventory();
// inventory.Games koleksiyonuna oyunlar ekleniyor...
inventory.Games.Where(game => game.UserRating >= 8.0).ToList().ForEach(game => Console.WriteLine(game.Name));
```

Sizce bu kod parçası ne yapar?

- A) Kod derlenmez çünkü `Where` metodu `List<Game>` türünde bir koleksiyon üzerinde doğrudan çağrılamaz.
- B) `inventory.Games` koleksiyonundaki kullanıcı puanı 8.0'den yüksek olan oyunları filtreler ve isimlerini ekrana yazdırır.
- C) `inventory.Games` koleksiyonundaki kullanıcı puanı 8.0 veya daha yüksek olan oyunları filtreler ve isimlerini ekrana yazdırır.
- D) `inventory.Games` koleksiyonundaki tüm oyunların isimlerini ekrana yazdırır.

## Soru 10

Bir insan kaynakları yönetim platformunda kullanıcıların mezuniyet notunu tutmak istediğimizi varsayalım. Ancak farklı kullanıcılar farklı not sistemlerini kullanabileceğini biliyorsunuz. Örneğin bazıları 100 tabanlı bir not sistemini ele alırken kimisi harf tabanlı *(A,B,C,...)* ya da 4.0 tabanlı bir not sistemini kullanıyor olabilir. Bunu karşılayacak türden bir tasarım yapmak istediğimizde aşağıdaki seçeneklerden hangisini tercih edersiniz?

A)

```csharp
public class Graduate
{
    public float Score { get; set; }
}
```

B)

```csharp
public class Graduate
{
    public object Score { get; set; }
}
```

C)

```csharp
public class Graduate
{
    public string Score { get; set; }
}
```

D)

```csharp
public record Score(object Value, Notation Notation);
public enum Notation
{
    Numeric100,
    Letter,
    Numeric4
}
public class Graduate
{
    public Score Score { get; set; }
}
```

## Soru 11

Yazılım çözümlerinde kodun kalitesini yüksek tutmak için çeşitli prensipler benimsenir ve metodolojiler uygulanır. Özellikle yazılan kodun daha deneyimli birisi tarafından denetlenmesi üzerine odaklanan *Code Review* süreçleri bu metodolojiler arasında önemli bir yer tutar. *Code Review* süreçleri sayesinde kodun kalitesi artırılabilir, hatalar erken aşamada tespit edilebilir ve takım içinde bilgi paylaşımı sağlanabilir. Siz deneyimli bir yazılımcı olduğunuzu düşünün ve takımınızda yeni başlayan bir geliştiricinin aşağıdaki kod parçasını yazdığını varsayalım.

```csharp
using System;
using Application.Infrastructure.Database;

namespace HrDomain.Entities;

public class Candidate
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }

    public Candidate(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("İsim alanı boş geçilemez.");
            
        Id = Guid.NewGuid();
        FullName = fullName;
    }

    public void SaveToDatabase()
    {
        var dbContext = new HrDbContext();
        dbContext.Candidates.Add(this);
        dbContext.SaveChanges();
    }
}
```

Bu kodu kurumsal mimari standartları ve *SOLID* ilkeleri açısından değerlendirdiğinizde yapacağınız en kritik mimari eleştiri aşağıdakilerden hangisi olurdu?

- A) **Id** özelliği **Guid.NewGuid()** ile sınıf içinde oluşturulmamalı, bu sorumluluk tamamen veritabanına bırakılmalıdır.
- B) **FullName** özelliği dışarıdan rahatça değiştirilebilmesi için **private set** yerine **public set** olarak tanımlanmalıdır; aksi halde nesne esnekliğini kaybeder.
- C) **Candidate** domain kurgusunun temel nesnelerindendir. Veritabanı işlemleri gibi altyapı *(Infrastructure)* detaylarını kendi içinde barındırması ve **HrDbContext**'e doğrudan bağımlı olması Single Responsibility *(Tek Sorumluluk)* ve Dependency Inversion *(Bağımlılıkların Tersine Çevrilmesi)* prensiplerini ihlal eder.
- D) Sınıf içerisinde parametresiz bir kurucu metot *(parameterless constructor)* bulunmadığı için ORM araçları bu sınıfı eşleştiremez *(map edemez)*, bu yüzden kod derlense bile çalışma zamanında hata verir.
