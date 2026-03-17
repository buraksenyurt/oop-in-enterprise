# OOP Test Soruları 01

Bu testin amacı yüksek kalite kodlama standartlarını sağlamak için temel yazılım prensiplerinden olan SOLID ilkelerindeki bilgilerinizi ölçmektir. Sorular, nesne yönelimli programlama (OOP) kavramları, C# dil özellikleri ve yazılım tasarım prensipleri üzerine odaklanmıştır.

*Not: Sorular .Net 10 sürümü üzerinden hazırlanmıştır.*

---

## Soru 1

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

## Soru 2

Bir elektronik ticaret sitesinin ödeme platformunda müşterilerin üyelik tiplerine göre indirim oranları uygulayan bir çözüm üzerinde çalıştığımızı düşünelim. Önceki versiyonda bu yapı aşağıdaki kod parçasına göre kurgulanmış olsun.

```csharp
public class DiscountCalculator
{
    public decimal CalculateFinalPrice(decimal amount, string customerType)
    {
        if (customerType == "Standard")
        {
            return amount;
        }
        else if (customerType == "Premium")
        {
            return amount * 0.90m; // %10 indirim
        }
        else if (customerType == "VIP")
        {
            return amount * 0.80m; // %20 indirim
        }
        
        throw new ArgumentException("Geçersiz müşteri tipi");
    }
}
```

Ancak zamanla sisteme farklı müşteri tiplerinin eklenmesi ihtiyacı ortaya çıkmıştır. Örneğin "Öğrenci" ve "Emekli" gibi yeni müşteri tiplerinin eklenmesi söz konusudur. Bunu if bloklarını genişleterek sağlamak mümkündür ancak bu yaklaşım kodun okunabilirliğini ve sürdürülebilirliğini olumsuz etkiler. Bu yaklaşım SOLID ilkelerinden hangisini ihlal eder ve çözüm olarak nasıl bir tasarım önerirsiniz?

- A) Bu yaklaşım Open/Closed Principle (Açık/Kapalı Prensibi) ihlal eder. Yeni müşteri tipleri eklemek için mevcut kodu değiştirmek gerekir. Çözüm olarak, müşteri tiplerini temsil eden bir arayüz veya soyut sınıf oluşturup, her müşteri tipi için bu arayüzü uygulayan ayrı sınıflar oluşturulabilir.
- B) Tek Sorumluluk Prensibi (SRP) ihlal edilmiştir. Çözüm olarak her müşteri tipi için ayrı bir hesaplayıcı sınıfı *(Örneğin: VipCalculator, PremiumCalculator)* yazılmalı ve bunlar CalculateFinalPrice metodu içinde switch-case ile çağrılmalıdır.
- C) Liskov Substitution Principle (Liskov Yerine Geçme Prensibi) ihlal edilmiştir çünkü müşteri tipleri birbirinin yerine geçemez. Çözüm olarak, müşteri tiplerini temsil eden bir enum oluşturulmalı ve bu enum üzerinden indirim oranları belirlenmelidir.
- D) Kod içinde kullanılan string değerler (örneğin "Standard", "Premium", "VIP") hard-coded olarak tanımlanmıştır, bu da Magic String kullanımı olarak adlandırılır ve kodun bakımını zorlaştırır. Çözüm olarak, müşteri tiplerini temsil eden bir enum oluşturulmalı ve bu enum üzerinden indirim oranları belirlenmelidir.

## Soru 3

Oldukça eski bir ERP sisteminin modernizasyonu sürecinde raporlama ile ilgili bir modülün yeniden yazılması ile ilgileniyorsunuz. Sistemdeki rapor türleri zaman içerisinde aşağıdaki gibi derin bir kalıtım *(hierarchical inheritance)* yapısına sahip olmuş.

```text
1. Report (Temel Sınıf)
2. ExcelReport : Report
3. MailedExcelReport : ExcelReport
4. EncryptedMailedExcelReport : MailedExcelReport
```

Yeni gelen bir isteğe göre şifrelenmiş PDF formatında raporlar da eklenmesi gerekmektedir. Bu durumda mevcut kalıtım yapısına göre yeni bir sınıf eklemek aşağıdaki gibi olacaktır:

```text
5. EncryptedMailedPdfReport : MailedExcelReport
```

Sınıf patlaması *(class explosion)* olarak adlandırılan bu durum, kalıtım yapısının karmaşıklaşmasına ve bakım zorluklarına yol açar. Sizce bu durumun üstesinden gelmek için aşağıdaki seçeneklerden hangisini kullanarak kod kalitesini artırırsınız?

- A) Tüm rapor tiplerini tek bir sınıf içinde toplar ve türlerini bool tipinden özellikler ile kontrol ederim. Örneğin, IsExcel, IsMailed, IsEncrypted gibi özellikler ekleyerek rapor türlerini belirlerim.
- B) Mecut kalıtım hiyerarşisini derinleştirip Report sınıfına Encrypt(), Email() gibi metodlar ekler ve bunları alt sınıflarda ezilebilir olması için virtual tanımlarım.
- C) Report sınıfını bir struct haline getiririm ve bellekteki referans yükünü azaltmayı hedeflerim. Sınıflar arası bağlantıları koparmamaya çalışırım.
- D) Kalıtım yerine kompozisyon *(composition over inheritance)* kullanıp rapor özelliklerini soyutlarım. Örneğin, IEncryptable, IMailable gibi arayüzler oluşturur ve bu arayüzleri uygulayan somut sınıflar ile raporları oluştururum. Böylece yeni rapor türleri eklemek için mevcut kodu değiştirmek zorunda kalmam ve sınıf patlamasını önlemiş olurum.

## Soru 4

Kurumsal bir doküman yönetim sistemi tasarladığımız düşünelim. Bu sistemde belgelerin farklı davranışları için aşağıdaki gibi bir arayüz tanımlanmış olsun.

```csharp
public interface IDocumentOperations
{
    void Read();
    void Write(string content);
    void Print();
    void Fax();
}
```

*ReadOnlyPdfDocument* isimli sınıf da sisteme yeni eklenmiş ve bu arayüzü uygulamış olsun. Sadece okunabilir formattaki bir PDF dokümanını temsil eden bu sınıfa **Fax** ve **Write** gibi davranışları da uygulamak zorunda kalacağız. Sorunu çözmek içinsde bu metotların implementasyonlarını boş bırakmak veya **NotImplementedException** fırlatmak gibi yaklaşımlar kullanacağız. Sizce bu vakada hangi **SOLID** prensibi ihlal edilmiştir.

- A) **DRY *(Don't Repeat Yourself)*** prensibi ihlal edilmiştir çünkü *ReadOnlyPdfDocument* sınıfında Fax ve Write gibi metotların implementasyonları boş bırakılmakta veya NotImplementedException fırlatılmaktadır, bu da kodun tekrarına ve gereksiz karmaşıklığa yol açar.
- B) **KISS *(Keep It Simple, Stupid)*** prensibi ihlal edilmiştir çünkü IDocumentOperations arayüzü, *ReadOnlyPdfDocument* gibi sınıflar için gereksiz metotlar içermektedir, bu da kodun basitliğini ve anlaşılabilirliğini azaltır.
- C) **YAGNI *(You Aren't Gonna Need It)*** prensibi ihlal edilmiştir çünkü IDocumentOperations arayüzü, *ReadOnlyPdfDocument* gibi sınıflar için gereksiz metotlar içermektedir, bu da kodun gereksiz yere karmaşıklaşmasına ve bakım zorluklarına yol açar.
- D) **Interface Segregation Principle (ISP)** ihlal edilmiştir çünkü *ReadOnlyPdfDocument* sınıfı, ihtiyaç duymadığı metotları uygulamak zorunda kalmaktadır. Bu durum, arayüzlerin daha küçük ve spesifik parçalara ayrılması gerektiğini gösterir.

DEVAM EDECEK...
