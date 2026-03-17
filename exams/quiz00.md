# OOP Test Soruları 00

Bu testin amacı OOP (Object-Oriented Programming) kavramlarını örnek senaryolar üzerinden değerlendirmektir. Aşağıdaki sorulara verilen seçeneklerden en uygun olanını seçiniz.

## Soru 1

Terminalden çalışan bir uygulama yazmak istiyorsunuz. C# programlama dili ile çalışmayı tercih ettiniz. Terminal uygulamaları **executable** olarak derlenirler ve örneğin **Windows** sistemlerinde **.exe** uzantılı dosyalar oluşur. Bu tür uygulamalar genellikle **Console Application** olarak adlandırılır. Aşağıda örnek bir terminal kod parçasına yer verilmiştir.

```csharp
namespace HelloWorld;

public class Application
{
    public static void Main()
    {
        Console.WriteLine("Merhaba Dünya!");
    }
}
```

Bu kod parçasını göz önüne aldığınızda aşağıdaki ifadelerden hangisi veya hangileri doğrudur?

I. C türevli birçok programlama dilinde olduğu gibi, C# dilinde yazılan bir executable'ın giriş noktası **Main** metodudur.
II. C# case-sensitive bir dildir, bu nedenle **Main** ve **main** farklı tanımlardır.
III. Terminal uygulamaları genellikle grafik arayüzü içermezler, bu nedenle kullanıcı etkileşimi genellikle metin tabanlıdır.
IV. .Net standart kütüphanesinde **WriteLine** isimli bir metot yoktur.

A) Yalnız I
B) I ve II
C) I, II ve III
D) Hepsi

---

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
public class Game{
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

---

**Soru 3:**

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

A) Ürünlerin benzersiz bir kimliğe sahip olmasını sağlamak ve bu kimliğin dışarıdan değiştirilmesini engellemek.
B) Ürünlerin fiyatlarının ve stok bilgilerinin güvenliğini sağlamak.
C) Ürünlerin adlarının değiştirilmesini engellemek.
D) Ürünlerin kimlik bilgilerini gizlemek ve sadece ürün adı, fiyat ve stok bilgilerini erişilebilir kılmak.

---

**Soru 4:**

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

A) I ve II
B) I, II ve III
C) Sadece I
D) Sadece II

---

**Soru 5:**

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

A) Bir ürün nesnesi oluşturulurken negatif bir fiyat belirlenmesini engellemek.
B) Ürün nesnesi oluşturulurken başlık bilgisinin boş veya sadece boşluklardan oluşmasını engellemek.
C) Oluşturulan her bir ürün nesnesinin benzersiz bir kimliğinin olmasını sağlamak.
D) Bir ürün oluşturulduktan sonra başlık ve fiyat bilgilerinin değiştirilebilmesini sağlamak.
