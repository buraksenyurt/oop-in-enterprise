# OOP Test Soruları 00

Bu testin amacı OOP *(Object-Oriented Programming)* kavramlarını örnek senaryolar üzerinden değerlendirmektir.

*Not: Sorular .Net 10 sürümü üzerinden hazırlanmıştır.*

---

## Soru 1

Terminalden çalışan bir uygulama yazmak istiyorsunuz. **C#** programlama dili ile çalışmayı tercih ettiniz. Terminal uygulamaları **executable** olarak derlenirler. Derleme sonrası örneğin **Windows** tabanlı sistemlerde **.exe** uzantılı dosyalar oluşur. Bu tür uygulamalar genellikle **Console Application** olarak da bilinir. Aşağıda örnek bir konsol uygulamasına ait kodlara yer verilmiştir.

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

Bu kod parçası ile ilgili olarak sizce aşağıdaki ifadelerden hangisi veya hangileri doğrudur?

- **I.** C türevli birçok programlama dilinde olduğu gibi, C# dilinde yazılan bir executable'ın giriş noktası **Main** metodudur.
- **II.** C# case-sensitive bir dildir, bu nedenle **Main** ve **main** farklı tanımlardır.
- **III.** Terminal uygulamaları genellikle grafik arayüzü içermezler, bu nedenle kullanıcı etkileşimi genellikle metin tabanlıdır.
- **IV.** .Net standart kütüphanesinde **WriteLine** isimli bir metot yoktur.

- A) Yalnız I
- B) I ve II
- C) I, II ve III
- D) Hepsi

## Soru 2

80li yılların sevilen oyunlarına ait bazı bilgilerin yer aldığı bir veri setine sahip olduğunuzu düşünün. Söz konusu veriler **dat** uzantılı bir dosyada **Comma-Separated Values *(CSV)*** formatında saklanmakta. Aşağıda bu satırlardan oluşan küçük bir örnek set yer alıyor. Söz konusu dosyada oyunların hangi yıl piyasaya sürüldüğü, hangi şirket tarafından geliştirildiği ve kullanıcı puanları gibi bilgiler yer alıyor.

```text
10000|Pac-Man|1980|Namco|6.79|
10001|Donkey Kong|1981|Nintendo|9.50|
10002|Space Invaders|1978|Taito|8.00|
10003|Prince of Persia|1989|Broderbund|8.50|
10004|Tetris|1984|Alexey Pajitnov|9.00|
```

Yaklaşık bin *(1000)* satırdan oluşan bu verisetini programatik ortamda modellemeniz gerekiyor. Bu sayede programın çalışma zamanında her bir satırı bir nesne örneği *(Object Instance)* olarak kullanabileceğiz. Sizce aşağıdaki sınıf tasarımlarından hangisi doğru bir modelleme şeklini ifade eder?

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

Bir fabrikadaki ürünler aşağıdaki kod parçasında görüldüğü gibi **Product** sınıfı ile modellenmiştir.

```csharp
public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public Product(string name, double price, int stock)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Stock = stock;
    }
}
```

Dikkat edileceği üzere **Id** özelliği sadece okunabilir bir özellik *(Readonly Property)* olarak tanımlanmıştır. Sizce *Id* özelliğini bu şekilde kapsüllemenin *(Encapsulation)* amacı nedir?

- A) Ürünlerin benzersiz bir kimliğe sahip olmasını sağlamak ve bu kimliğin dışarıdan değiştirilmesini engellemek.
- B) Ürünlerin fiyatlarının ve stok bilgilerinin güvenliğini sağlamak.
- C) Ürünlerin adlarının değiştirilmesini engellemek.
- D) Ürünlerin kimlik bilgilerini gizlemek ve sadece ürün adı, fiyat ve stok bilgilerini erişilebilir kılmak.

## Soru 4

**C#** programlama dili genel bir tip sistemini *(Common Type System)* benimser. Bu tip sistemi sayesinde farklı türden verilerle çalışmak mümkün olur. *int, float, double, char, string* gibi ilkel türler *(primitive types)* ve *class, struct, interface, enum, delegate* gibi karmaşık türler *(complex types)* bu tip sisteminin bir parçasıdır. Böyle bir tip sistemi gerçek dünya nesnelerinin modellenmesini de kolaylaştırır. Örneğin **enum** türünü kullanarak bazı sayısal değerleri anlamlı isimlerle ifade edebiliriz. Aşağıdaki kod parçasında temel renklerden sekizini temsil eden bir **enum** tanımı yer almaktadır.

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

Geliştirilmekte olan oyunda bu **enum** tanımından da yararlanarak karakterlerin favori renklerinin de tutulması beklenmektedir. Oyuncu karakterlerini temsil edecek nesne örnekleri, ilk kez oluşturulduktan sonra içeriği değiştirilemez *(immutable)* olarak kullanılmalıdır. Değiştirilmek istenirse de yeni bir örneği oluşturmayı zorunlu kılmanız gerekmektedir. Elimizde aşağıdaki seçeneklerin mevcut olduğunu düşünelim.

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

Bu seçeneklerden hangisini veya hangilerini tercih edersiniz.*(**Immutable** olma halini dikkate alınız)*

- A) I ve II
- B) Hepsi
- C) Sadece I
- D) Sadece II

## Soru 5

Kurumsal çaptaki büyük projelerde verilerin tutarlılığını sağlamak önemlidir ve bunun için farklı seviyelerde çeşitli kurallar işletilebilir. Verinin doğru şekilde girilmesini sağlamak ve bunu yaparken bazı doğrulama kurallarını *(validation rules)* işletmek kullanılan yöntemler arasındadır. Bazı doğrulama kuralları çözümün **domain** yapısını korumak için en alt çekirdek nesnelerde değerlendirilir. Bu düşünceyle yazılmış aşağıdaki **Product** sınıfını ele alalım.

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

Bazı durumlarda bir sınıfın *(class)* ilgili alanları sadece belirli metotlar üzerinden değişime açık olabilir. Örneğin bir oyuncunun genel skor bilgisini belirli oranda azaltmak veya artırmak için iki ayrı metot kullanmak ya da property'lerin set bloklarında bu işlemi yapmak seçeneklerden arasındadır. Aşağıdaki kod parçasında örnek bir senaryo verilmiştir.

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
- **II.** Hatalı skor bilgisi girilmesi durumunda ortama istisna *(Exception)* fırlatılır ve bu istisnaların nesne kullanıcısı *(object user)* tarafından yönetilmesi beklenir.
- **III.** Bu sınıf tasarımı oyuncunun skor bilgisinin tutarlılığını sağlamak için kapsülleme *(Encapsulation)* ilkesine uygun bir şekilde tasarlanmıştır.
- **IV.** Bu kod parçası derlenmez çünkü *Score* özelliği sadece okunabilir *(readonly)* olarak tanımlanmıştır ve *IncreaseScore* ve *DecreaseScore* metotları bu özelliği değiştirmeye çalışmaktadır.

Sizce hangi ifade/ifadeler doğrudur?

- A) Hepsi
- B) I, II ve III
- C) Sadece I
- D) III, IV

## Soru 7

İstisna yönetimi *(Exception Handling)*, **C#** ve benzeri birçok programlama dilinde ele alınan önemli kavramlardan birisidir. İstisnalar programın çalışması sırasında ortaya çıkan beklenmedik durumları ifade eder ve bu durumların programın normal akışını bozmadan yönetilmesi gerekir. Aşağıdaki kod parçasında basit bir istisna yönetimi örneği yer almaktadır.

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

- **I.** *Divide* metodu bölen sıfır olduğunda bir istisna fırlatır *(throw exception)* ve bu istisna *Main* metodunda yakalanır.
- **II.** Buradaki istisna yönetimi *(exception handling)* sayesinde programın beklenmedik durumlarda çökmesi engellenir ve kullanıcıya anlamlı bir hata mesajı sunulur.
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

- A) Kod derlenmez çünkü **Counter** sınıfında tanımlı **_count** alanı **int** türünün sınırlarını aşabilir ve bu durum derleyici tarafından tespit edilir.
- B) Sayaç değeri sadece bir kez artar ve ardından bir istisna fırlatılır çünkü sayaç değeri **int.MaxValue** değerine ulaştığında **Increment** metodu bir **OverflowException** fırlatacak şekilde tasarlanmıştır.
- C) Sayaç değeri sürekli artar, **MaxValue** aşıldığında **-*(negatif)*** değerinden devam eder ve program normal akışını sürdürür.
- D) Sayaç değeri sürekli artar, **MaxValue** değerine ulaşıldığında sıfırlanır ve doğal olarak ekrana **"Sayaç değeri: 0"** yazdırılır.

## Soru 9

Elimizde oyun bilgilerinin tutulduğu generic bir koleksiyon olduğunu düşünelim. Tüm oyun bilgileri **Game** türünden bir nesne ile ifade edilmekte ve **Inventory** sınıfında `List<Game>` türünden bir özellik *(property)* olarak tutulmaktadır. Bu ilişkinin **Inventory** sınıfında aşağıdaki gibi tanımlandığını düşünelim.

```csharp
public class Inventory
{
    public List<Game> Games { get; private set; }

    // Diğer kodlar
}
```

**C#**, birçok fonksiyonel dil özelliğini de desteklediğinden koleksiyon gibi veri yapıları üzerinde çeşitli fonksiyonel işlemler yapmaya da olanak tanır. Bu anlamda birçok iterasyonu destekler. Örneğin `Where`, `Select`, `ForEach` gibi metotlar yardımıyla bu veri yapıları üzerinde filtreleme, dönüştürme ve yineleme işlemleri icra edilebilir. Aşağıdaki kod parçasında bu tür bir kullanım örneği yer almaktadır.

```csharp
var inventory = new Inventory();
// inventory.Games koleksiyonuna oyunlar ekleniyor...
inventory.Games.Where(game => game.UserRating >= 8.0).ToList().ForEach(game => Console.WriteLine(game.Name));
```

Sizce bu kod parçası ne yapmaktadır?

- A) Kod derlenmez çünkü `Where` metodu `List<Game>` türünde bir koleksiyon üzerinde doğrudan **çağrılamaz**.
- B) **inventory.Games** koleksiyonundaki kullanıcı puanı 8.0'den yüksek olan oyunları filtreler ve isimlerini ekrana yazdırır.
- C) **inventory.Games** koleksiyonundaki kullanıcı puanı 8.0 veya daha yüksek olan oyunları filtreler ve isimlerini ekrana yazdırır.
- D) **inventory.Games** koleksiyonundaki tüm oyunların isimlerini ekrana yazdırır.

## Soru 10

Bir insan kaynakları yönetim platformunda kullanıcıların mezuniyet notunu tutmak istediğimizi varsayalım. Ancak farklı kullanıcıların farklı not sistemlerini kullanabileceği gibi bir durum da söz konusu. Örneğin bazıları **100 tabanlı** bir not sistemini ele alırken kimisi **harf tabanlı *(A,B,C,...)*** ya da **4.0 tabanlı** bir not sistemini kullanıyor olabilir. Bunu karşılayacak türden bir tasarım yapmak istediğimizde aşağıdaki seçenekler arasından hangisini tercih edersiniz?

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

Çalışma zamanında oluşabilecek istisnalar *(Exception)*, try...catch...finally bloklarınca yönetilebilir. Bu bloklar sayesinde programın beklenmedik durumlarda çökmesi engellenir ve kullanıcıya anlamlı geri bildirimler sağlanır. Aşağıdaki kod parçasında bir animasyon programındaki sahne *(stage)* nesnesi oluşturulurken ortaya çıkabilecek hataların yönetilmesine istinaden exception handling kullanımı gösterilmektedir.

```csharp
public class Application
{
    public static void Main()
    {
        try
        {
            var stage = new Stage("", -5);
        }
        catch (...................... ex)
        {
            Console.WriteLine($"Error creating stage: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}

public class InvalidStageException(string message) 
    : Exception(message)
{
}

public class Stage
{
    public string Name { get; private set; }
    public short PlayerCount { get; private set; }

    public Stage(string name, short playerCount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ......................("Stage name cannot be empty.");

        if (playerCount < 0)
            throw new ......................("Player count cannot be negative.");

        Name = name;
        PlayerCount = playerCount;
    }
}
```

Sizce ...... şeklinde boş bırakılan yerlere şıklardan hangisi gelmelidir?

- A) ArgumentException
- B) ArgumentNullException
- C) InvalidStageException
- D) ArgumentOutOfRangeException

## Soru 12

Nesen yönelimli programlama dillerinin benimsediği temel ilkeler encapsulation, inheritance, polymorphism ve abstraction'dır. Bu ilkeler sayesinde gerçek dünya nesnelerinin programatik ortamda modellenmesi kolaylaşır. Modern dillerin kullandığı standart kütüphaneler de bu ilkeler doğrultusunda tasarlanır ve böylece genişletilebilir, yeniden kullanılabilir kod yazmak mümkün hale gelir.

Örneğin var olan kütüphane hiyerarşisindeki birçok enstrüman kalıtım *(inheritance)* yoluyla genişletilebilir ve bu sayede yeni özellikler eklenebilir. İstisna yönetimi *(Exception Handling)* de bu ilkeler doğrultusunda ele alınır. Örneğin kendi **Exception** türlerimizi tasarlayarak **try...catch* bloklarında kullanabiliriz. Domain Driven Design *(DDD)* yaklaşımında çekirdek nesne kurallarını korumak için özel istisna türleri tasarlamak yaygın bir yaklaşımdır. Aşağıdaki entity sınıfını göz önüne alalım.

```csharp
public class Stage
{
    public string Name { get; private set; }
    public short PlayerCount { get; private set; }

    public Stage(string name, short playerCount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Stage name cannot be empty.");

        if (playerCount < 0)
            throw new Exception("Player count cannot be negative.");

        Name = name;
        PlayerCount = playerCount;
    }
}
```

**Exception** sınıfı yerine kendi istisna türümüzü kullanmaya karar verdik. Sizce şıklardaki yazılış biçimlerinden hangisi doğrudur?

- A)

```csharp
public class InvalidStageException(string message) 
    : Exception(message)
{
}
```

- B)

```csharp
public class InvalidStageException(string message) 
    : base(message)
{
}
```

- C)

```csharp
public struct InvalidStageException{
    public string Message { get; } = "Invalid stage data.";
}
```

- D)

```csharp
public class InvalidStageException
{
    private string _message;
    public InvalidStageException(string message)
    {
        _message = message;
    }
    public string GetMessage() => _message;
}
```

## Cevap Anahtarı

| Soru No | Doğru Cevap |
| ------- | ----------- |
| 1 | C |
| 2 | A |
| 3 | A |
| 4 | B |
| 5 | D |
| 6 | B |
| 7 | C |
| 8 | B |
| 9 | C |
| 10 | D |
| 11 | C |
| 12 | A |
