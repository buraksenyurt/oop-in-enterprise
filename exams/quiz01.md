# OOP Test Soruları 01

Bu testin amacı yüksek kalite kodlama standartlarını sağlamak için temel yazılım prensiplerinden olan **SOLID** ilkelerindeki bilgilerinizi ölçmektir. Sorular, nesne yönelimli programlama *(OOP)* kavramları, C# dil özellikleri ve yazılım tasarım prensipleri üzerine odaklanmıştır.

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

## Soru 5

Teknik borç *(Technical Debt)*, yazılım geliştirme sürecinde alınan kısa vadeli kararların uzun vadede ortaya çıkan olumsuz etkilerini ifade eder. Bu borç, kodun kalitesini düşürebilir, bakım maliyetlerini artırabilir ve yeni özelliklerin eklenmesini zorlaştırabilir. Teknik borç genellikle zaman baskısı, yetersiz kaynaklar veya deneyimsiz ekip üyeleri nedeniyle ortaya çıkar. Teknik borçların tespitinde **Sonarqube** gibi statik kod tarama araçları sıklıkla kullanılır. Bu araçlar kod kalitesini ölçümlerken bazı metrikler kullanır. Aşağıdaki kod parçasını göz önüne alalım.

```csharp
public class CustomerService
{
    public void RegisterCustomer(
        string firstName, 
        string lastName, 
        DateTime birthDate, 
        string email, 
        string phoneNumber, 
        string addressLine1, 
        string addressLine2, 
        string city, 
        string country, 
        string zipCode,
        bool isPremium)
    {
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate,
            Email = email,
            PhoneNumber = phoneNumber,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            City = city,
            Country = country,
            ZipCode = zipCode,
            IsPremium = isPremium
        };
        var dbContext = new CustomerDbContext();
        dbContext.Customers.Add(customer);
        dbContext.SaveChanges();        
    }
}
```

Sizce bu kod parçası için şıklarda belirtilen hangi ihlal söz konusudur.

- A) RegisterCustomer metodunun parametre sayısı çok fazladır. Bu `Long Parameter List` olarak adlandırılan bir ihlaldir ve **Code Smell** kategorisinde yer alır.
- B) İsimlendirme ihlali söz konusudur. Argüman adları **CamelCase** formatında değil, **snake_case** formatında yazılmalıdır.
- C) **Cyclomatic Complexity (Döngüsel Karmaşıklık)** ihlali bulunmaktadır. Metot içerisindeki karar ağaçlarının *(if-else, switch blokları)* ve döngülerin çok fazla olması sebebiyle karmaşıklık limiti aşılmıştır.
- D) Kod genelinde **Duplicated Blocks *(Tekrarlanan Kod Blokları)*** tespit edilmiştir. **SonarQube**, benzer nesne oluşturma ve değer atama işlemlerinin proje içerisinde kopya kod olarak yer almasından dolayı DRY prensibine aykırı bulup bu uyarıyı üretir.

## Soru 6

Müşteri paneline giriş işlemini *(Login)* yapan mevcut eski bir metodu inceliyorsunuz. Kod aşağıdaki gibi bir SQL sorgusu çalıştırarak kullanıcının veritabanında olup olmadığını kontrol etmektedir.

```csharp
public class AuthService
{
    public bool AuthenticateUser(string username, string password)
    {
        string query = $"SELECT COUNT(*) FROM Users WHERE Username = '{username}' AND Password = '{password}'";
        
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            int result = (int)cmd.ExecuteScalar();
            return result > 0;
        }
    }
}
```

Bu metod kurumsal standartlar bakımından incelendiğinde statik kod analizi araçları tarafından büyük bir güvenlik zafiyeti *(Security Vulnerability)* tespit edilecektir. Bu ihlalin temel nedeni ve en doğru çözüm yöntemi hangisidir?

- A) **Cross-Site Scripting (XSS)** zafiyeti bulunmaktadır. Kullanıcıdan alınan `username` ve `password` parametreleri HTML Encode işleminden geçirilmeden kullanılmıştır.
- B) Sorguda doğrudan bir **SQL Injection** zafiyeti *(CWE-89)* bulunmaktadır. Girdi parametreleri birleştirilerek *(string interpolation / concatenation)* çalıştırıldığı için, zararlı betikler veritabanında komut olarak işletilebilir. Çözüm olarak parametreli sorgular *(Parameterized Queries)* veya ORM *(Object-Relational Mapping)* kütüphaneleri kullanmak ya da **Stored Procedure** tercih edilmelidir.
- C) Veritabanı bağlantısı `using` bloğunda kullanılmıştır. Bu durum bağlantının gereğinden fazla açık kalmasına sebep olarak **Denial of Service (DoS)** açıklarına neden olur.
- D) Şifreler düz metin olarak kontrol edildiğinden **Broken Authentication** zafiyeti vardır. Bu durum koddaki SQL string kullanımından bağımsızdır, yalnızca **JWT *(JSON Web Token)*** kullanılarak çözülebilir.

## Soru 7

Yeni mikroservis altyapısına taşınan bir projeyi gözden geçirirken takım arkadaşınızın yazdığı bir veri erişim sınıfında *(Repository)* aşağıdaki gibi bir kod kullanımına denk geldiğinizi düşünelim.

```csharp
public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=prod-db.corp.local;Database=CustomerDb;User ID=admin_sa;Password=supersecretPassword123!;TrustServerCertificate=True;");
        }
    }
    
    public DbSet<Customer> Customers { get; set; }
}
```

Bu kod parçası github reposuna gönderildiğinde modern kod analizi ve güvenlik araçları *(SonarQube, GitHub Advanced Security vb.)* bunu **Kritik / Blocker** seviyesinde bir ihlal olarak işaretlemiştir. Bu uyarının sebebi nedir ve bu durum nasıl çözülmelidir?

- A) Sınıf içerisinde yer alan *DbSet* gibi *Entity Framework* özelliklerinin *public* erişim belirleyicisi ile dışarı açılması, veritabanı tablolarının doğrudan manipüle edilmesine *(Data Exposure)* yol açar. DbSet'ler kapsüllenmelidir *(Encapsulation)*.
- B) Veritabanının isminin *CustomerDb* olarak verilmesi güvenlik ihlalidir. Veritabanı isimleri hiçbir zaman kod veya yapılandırma dosyalarında gösterilmemelidir.
- C) **Hardcoded Credentials *(Sabitlenmiş Kimlik Bilgileri)*** güvenlik ihlalidir *(CWE-798)*. Şifre ve hassas bağlantı bilgileri kod içerisinde açık metin olarak *(plaintext)* yer almaktadır. Bunun yerine veriler *appsettings.json*, Ortam Değişkenleri *(Environment Variables)* ya da *Azure Key Vault / AWS Key Management Service* gibi gizli dizi *(secret)* yönetim araçlarından okunmalıdır.
- D) *TrustServerCertificate=True* kullanımı **Man-in-the-Middle *(MitM)*** saldırılarına kapı araladığı için bir ihlal söz konusudur. Kodda yalnızca *Password* alanı silinerek bu sorun çözülebilir.

## Soru 8

Şirketin maaş ve prim hesaplamalarını yürüten planlı bir arka plan *(Background Job/Worker)* işine ait bir sınıfta aşağıdaki kod parçasında yer alan metotla karşılaştığınızı düşünelim.

```csharp
public decimal CalculateBonus(Employee employee, int yearsOfService, decimal baseSalary)
{
    decimal bonus = 0;
    
    switch (employee.Department)
    {
        case Department.Sales:
            if (yearsOfService > 5)
                bonus = baseSalary * 0.20m;
            else if (yearsOfService > 2)
                bonus = baseSalary * 0.10m;
            else
                bonus = baseSalary * 0.05m;
            break;

        case Department.Engineering:
            if (employee.Level == Level.Senior)
                bonus = baseSalary * 0.15m;
            else if (employee.Level == Level.Mid)
                bonus = baseSalary * 0.10m;
            else
                bonus = baseSalary * 0.05m;
            break;

        case Department.HR:
            if (yearsOfService > 10)
                bonus = baseSalary * 0.12m;
            else
                bonus = baseSalary * 0.06m;
            break;

        case Department.Marketing:
            if (employee.HasCampaignSuccess)
                bonus = baseSalary * 0.18m;
            else
                bonus = baseSalary * 0.04m;
            break;
            
        case Department.Management:
            bonus = baseSalary * 0.50m;
            break;

        default:
            bonus = 0;
            break;
    }
    
    return bonus;
}
```

Statik kod tarama araçları *(Sonarqube)* bu metot için **Cognitive Complexity *(Bilişsel Karmaşıklık)*** ihlali yapıldığını belirtecektir. İhlalin temel sebebi nedir ve kurumsal mimari standartlarına uygun olarak en ideal çözüm yaklaşımı aşağıdakilerden hangisidir?

- A) İhlal, metot içerisindeki değişken atamalarının çokluğundan kaynaklanmaktadır. *decimal bonus = 0;* değişkeni gereksiz yer kaplar. Kod sadece *return baseSalary * 0.20m* gibi doğrudan dönüşler içermelidir.
- B) İhlalin sebebi uzun bir kod bloğu yazılmış olmasıdır. Çözüm olarak, her bir *case* içerisindeki işlemler asenkron *async/await* olarak yeniden kurgulanmalı ve performans arttırılmalıdır.
- C) İhlal, *switch* ve iç içe geçmiş *if-else* bloklarının kodu okumayı, takip etmeyi ve test etmeyi aşırı zorlaştırmasından kaynaklanmaktadır. Bu problemi çözmek için OOP mantığına uygun bir biçimde **Strateji Tasarım Deseni (Strategy Pattern)** veya **Polimorfizm** kullanılarak her departman için ayrı bir yetki hesaplama sınıfı oluşturulmalıdır.
- D) Sorun *employee.Department* değerinin bir *Enum* sabiti olmasından kaynaklanmaktadır. *Enum* değerler *switch-case* bloklarında kullanıldığında derleyiciler çalışma zamanı optimizasyonunu sağlayamaz. Bu veriler *string* değerler olarak tutulmalıdır.

## Soru 9

Bir e-ticaret platformunda ürünlerin fiyatlandırılmasıyla ilgili iş kuralları içeren bir modül kodunu inceliyorsunuz. Aşağıdaki kod parçasında olduğu gibi. Lakin bu kodun bakımının zor olacağından ve yeni kurallar eklenmesi gerektiğinde sürekli değiştirilmek zorunda kalacağından endişe etmektesiniz.

```csharp
public class PricingService
{
    public decimal CalculatePrice(Product product, Customer customer)
    {
        decimal price = product.BasePrice;

        if (customer.IsPremium)
        {
            price *= 0.9m; // %10 indirim
        }

        if (product.Category == "Electronics")
        {
            price *= 1.2m; // %20 zam
        }

        if (customer.HasLoyaltyCard)
        {
            price *= 0.95m; // %5 indirim
        }

        if (product.IsOnSale)
        {
            price *= 0.8m; // %20 indirim
        }

        return price;
    }
}
```

Kodun değiştirilmeden genişletilebilir olmasını sağlamak için farklı bir yol arıyorsunuz. Aşağıdaki kod örneklerinden hangisini tercih edersiniz.

- A)

```csharp
public class PricingService
{
    public decimal CalculatePrice(Product product, Customer customer, string discountType)
    {
        return discountType switch
        {
            "Premium" => product.BasePrice * 0.9m,
            "Electronics" => product.BasePrice * 1.2m,
            "Loyalty" => product.BasePrice * 0.95m,
            "OnSale" => product.BasePrice * 0.8m,
            _ => product.BasePrice
        };
    }
}
```

- B)

```csharp
public interface IPricingRule
{
    decimal ApplyRule(decimal currentPrice, Product product, Customer customer);
}

public class PricingService
{
    private readonly IEnumerable<IPricingRule> _rules;

    public PricingService(IEnumerable<IPricingRule> rules)
    {
        _rules = rules;
    }

    public decimal CalculatePrice(Product product, Customer customer)
    {
        decimal price = product.BasePrice;
        foreach (var rule in _rules)
        {
            price = rule.ApplyRule(price, product, customer);
        }
        return price;
    }
}
```

- C)

```csharp
public partial class PricingService
{
    // C# 'partial class' özelliği ile her kurumsal fiyat kuralı ayrı bir dosyada 
    // ele alınır. Örneğin PricingService.Premium.cs, PricingService.Elektronics.cs gibi.
    // Bu sayede PricingService sınıfı tek bir dosyada büyümemiş olur.
}
```

- D)

```csharp
public class PricingService
{
    public decimal CalculatePrice(Product product, Customer customer)
    {
        return ApplyPremiumRule(
                  ApplyElectronicsRule(
                      ApplyLoyaltyRule(
                          ApplySaleRule(product.BasePrice, product), 
                      customer), 
                  product), 
               customer);
    }
    // Private metodlar burada tanımlanır...
}
```

## Soru 10

Büyük ölçekli bir lojistik firmasının sipariş yönetim süreçlerinde görev alan bir yazılım geliştiricisi olduğunuzu düşünün. Kod inceleme *(Code Review)* toplantısında, sipariş başarıyla tamamlandığında müşteriye otomatik olarak e-posta ve SMS gönderen aşağıdaki *OrderProcessorService* sınıfının kodunu gözden geçiriyorsunuz.

```csharp
public class OrderProcessorService
{
    public void CompleteOrder(Order order)
    {
        // 1. Siparişin durumunu güncelleyecek iş kuralları...
        order.Status = OrderStatus.Completed;
        order.CompletedAt = DateTime.UtcNow;

        // 2. Bildirimlerin gönderilmesi
        var emailSender = new SmtpEmailService();
        emailSender.SendEmail(order.CustomerEmail, "Siparişiniz tamamlandı ve yola çıktı.");

        var smsSender = new TwilioSmsService();
        smsSender.SendSms(order.CustomerPhone, "Kargonuz yola çıkmıştır. Takip no: ...");
    }
}
```

Bu örnekte nesne yönelimli tasarım ilkelerinden hangisinin ihlal edildiği net bir şekilde görülmektedir ve kurumsal mimaride en doğru refactoring yaklaşımı hangisi olmalıdır?

- A) Metot içerisinde farklı türden bildirimlerin arka arkaya gönderilmesi **DRY *(Don't Repeat Yourself)*** prensibine aykırıdır. Bildirimler tek bir *SendAllNotifications()* metodu altında birleştirilmeli ve sadece o çağrılmalıdır.
- B) İşlem sırasında *new* anahtar kelimesi kullanılarak *SmtpEmailService* ve *TwilioSmsService* gibi somut *(concrete)* sınıflara doğrudan bağımlılık *(tight coupling)* oluşturulmuştur. Bu durum **Dependency Inversion Principle *(DIP)*** ihlalidir. Sınıf somut servislere değil, *INotificationService* gibi soyutlamalara *(abstractions)* bağımlı olmalı bu bağımlılıklar koda *constructor-yapıcı metot* aracılığıyla enjekte edilmelidir *(Dependency Injection)*.
- C) Yerel *(local)* değişken olarak yaratılan bu servisler bellekte sızıntıya *(Memory Leak)* yol açabilir. Performans kaybı yaşamamak için değişkenler metot içinde değil, sınıf seviyesinde *static* değişkenler olarak tanımlanmalıdır.
- D) Siparişin statüsünü değiştirmek ve arkasından bildirim göndermek aynı metotta yapıldığı için **Liskov Substitution Principle *(LSP)*** ihlal edilmiştir. Alt sınıflar bu metodu ezdiğinde *(override)* sorun yaşama ihtimali oldukça yüksektir.
