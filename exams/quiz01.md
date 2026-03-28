# OOP Test Soruları 01

Bu testin amacı yüksek kalite kodlama standartlarını sağlamak için temel yazılım prensipleri bilgimizi ölçmektedir. Genel olarak **SOLID *(Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion)*** ilkeleri ele alınır ama kod güvenliği ve temel nesne yönelimli dil prensipleri ile bazı mimari konulara da yer verilmektedir.

*Not: Sorular .Net 10 sürümü üzerinden hazırlanmıştır.*

---

## Soru 1

Yazılım çözümlerinde kodun kalitesini yüksek tutmak için çeşitli prensipler benimsenir ve metodolojiler uygulanır. Özellikle yazılan kodun daha deneyimli birisi tarafından denetlenmesi üzerine odaklanan *Code Review* süreçleri bu metodolojiler arasında önemli bir yer tutar. *Code Review* süreçleri sayesinde kodun kalitesi artar, hatalar erken aşamada tespit edilir ve takım içinde bilgi paylaşımı sağlanır. Ayrıca tüm yazılım ekibinin belli bir standardın üzerinde kodlama yapması da sağlanır.

Deneyimli bir yazılımcı olduğunuzu düşünün ve takımınızda yeni başlayan bir geliştiricinin aşağıdaki kod parçasını gözden geçirdiğinizi varsayın.

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

Bu kodu kurumsal mimari standartları ve *SOLID* ilkeleri açısından değerlendirdiğinizde yapacağınız en kritik mimari eleştiri aşağıdakilerden hangisi olur?

- A) **Id** özelliği **Guid.NewGuid()** ile sınıf içinde *oluşturulmamalı*, bu sorumluluk tamamen veritabanına bırakılmalıdır.
- B) **FullName** özelliği dışarıdan rahatça değiştirilebilmesi için **private set** yerine **public set** olarak tanımlanmalıdır; aksi halde nesne esnekliğini kaybeder.
- C) **Candidate** domain kurgusunun temel nesnelerindendir. Veritabanı işlemleri gibi altyapı *(Infrastructure)* detaylarını kendi içinde barındırması ve **HrDbContext**'e doğrudan bağımlı olması **Single Responsibility *(Tek Sorumluluk)*** ve **Dependency Inversion *(Bağımlılıkların Tersine Çevrilmesi)*** prensiplerini ihlal eder.
- D) Sınıf içerisinde parametresiz bir kurucu metot *(parameterless constructor)* bulunmadığı için **ORM *(Object-Relational Mapping)*** araçları bu sınıfı eşleştiremez *(map edemez)*, bu yüzden kod derlense bile çalışma zamanında hata verir.

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

Ancak zamanla sisteme farklı müşteri tiplerinin eklenmesi ihtiyacı ortaya çıkmıştır. Örneğin **"Öğrenci"** ve **"Emekli"** gibi yeni müşteri tipleri gibi. Bunu **if** bloklarını genişleterek sağlamak mümkündür ancak bu yaklaşım kodun okunabilirliğini ve sürdürülebilirliğini olumsuz etkiler. Bu yaklaşım **SOLID** ilkelerinden hangisini ihlal eder ve çözüm olarak nasıl bir tasarım önerirsiniz?

- A) Bu yaklaşım **Open/Closed Principle *(Açık/Kapalı Prensibi)*** ihlalidir. Yeni müşteri tipleri eklemek için mevcut kodu değiştirmek gerekir. Çözüm olarak, müşteri tiplerini temsil eden bir arayüz *(interface)* veya soyut sınıf *(abstract class)* oluşturup, her müşteri tipi için bu arayüzü uygulayan ayrı sınıflar oluşturulabilir.
- B) **Tek Sorumluluk Prensibi *(Single Responsibility Principle)*** ihlal edilmiştir. Çözüm olarak her müşteri tipi için ayrı bir hesaplayıcı sınıfı *(Örneğin: VipCalculator, PremiumCalculator)* yazılmalı ve bunlar **CalculateFinalPrice** metodu içinde **switch-case** ile çağrılmalıdır.
- C) **Liskov Substitution Principle *(Liskov Yerine Geçme Prensibi)*** ihlal edilmiştir çünkü müşteri tipleri birbirinin yerine geçemez. Çözüm olarak, müşteri tiplerini temsil eden bir **enum** oluşturulmalı ve bu **enum** üzerinden indirim oranları belirlenmelidir.
- D) Kod içinde kullanılan **string** değerler *(örneğin "Standard", "Premium", "VIP")* hard-coded olarak tanımlanmıştır, bu da **Magic String** kullanımı olarak adlandırılır ve kodun bakımını zorlaştırır. Çözüm olarak, müşteri tiplerini temsil eden bir **enum** oluşturulmalı ve bu **enum** üzerinden indirim oranları belirlenmelidir.

## Soru 3

Oldukça eski bir **ERP *(Enterprise Resource Planning)*** sisteminin modernizasyonu sürecinde raporlama ile ilgili bir modülün yeniden yazılması gerekmektedir. Sistemdeki rapor türleri zaman içerisinde aşağıdaki gibi derin bir kalıtım *(hierarchical inheritance)* yapısına sahip olmuştur.

```text
1. Report (Temel Sınıf)
2. ExcelReport : Report (Excel formatında raporlar için Report sınıfından türetilmiş bir sınıf)
3. MailedExcelReport : ExcelReport (Excel raporlarını e-posta ile göndermek için ExcelReport sınıfından türetilmiş bir sınıf)
4. EncryptedMailedExcelReport : MailedExcelReport (Şifrelenmiş ve e-posta ile gönderilen Excel raporları için MailedExcelReport sınıfından türetilmiş bir sınıf)
```

Yeni gelen bir isteğe göre sisteme *şifrelenmiş PDF formatında* raporların da eklenmesi gerekmektedir. Bu durumda mevcut kalıtım yapısına göre yeni sınıf aşağıdaki gibi tanımlanacaktır.

```text
5. EncryptedMailedPdfReport : MailedExcelReport (Şifrelenmiş ve e-posta ile gönderilen PDF raporları için MailedExcelReport sınıfından türetilmiş bir sınıf)
```

Sınıf patlaması *(class explosion)* olarak adlandırılan bu durum, kalıtım *(inheritance)* yapısının karmaşıklaşmasına ve bakım zorluklarına yol açar. Bu durumun üstesinden gelmek için aşağıdaki seçeneklerden hangisini seçersiniz?

- A) Tüm rapor tiplerini tek bir sınıf içinde toplar ve türlerini **bool** tipinden özellikler ile kontrol ederiz. Örneğin, *IsExcel, IsMailed, IsEncrypted* gibi özellikler ekleyerek rapor türlerini belirler ve yönetiriz.
- B) Mecut kalıtım hiyerarşisini derinleştirip **Report** sınıfına *Encrypt(), Email()* gibi metodlar ekler ve bunları alt sınıflarda ezilebilir olması için **virtual** nitelikli tanımlarız.
- C) **Report** sınıfını bir **struct** haline getirir ve bellekteki referans yükünü azaltmayı hedefleriz. Sınıflar arasındaki sıkı bağlantıları koparmamaya çalışırız.
- D) Kalıtım yerine kompozisyon *(composition over inheritance)* ilkesini kullanıp rapor özelliklerini soyutlarız. Örneğin, *IEncryptable, IMailable* gibi arayüzler oluşturur ve bu arayüzleri uygulayan somut sınıflar *(concrete classes)* ile rapor nesnelerinin örneklenmesini sağlarız. Böylece yeni rapor türleri eklemek için mevcut kodu değiştirmek zorunda kalmaz ve sınıf patlamasını önlemiş oluruz.

## Soru 4

Kurumsal bir doküman yönetim sistemi *(Document Management System)* tasarladığımızı düşünelim. Bu sistemde belgelerin farklı davranışları için aşağıdaki gibi bir sözleşme *(interface)* tanımlanmış olsun. Bu arayüz kendini uygulayan türlere belgelerin okunması, yazılması, yazdırılması ve faks gönderilmesi gibi işlemleri gerçekleştirme yeteneği kazandırır ve bu davranışları uygulamaya zorunlu kılar.

```csharp
public interface IDocumentOperations
{
    void Read();
    void Write(string content);
    void Print();
    void Fax();
}
```

Sisteme yeni eklenen **ReadOnlyPdfDocument** isimli başka bir sınıf da bu arayüzü kullanıyor olsun. Sadece okunabilir *(read-only)* formattaki bir PDF dokümanını temsil eden bu sınıfa **Fax** ve **Write** gibi davranışları da uygulamak zorunda kalacağız *(Interface uygulama zorunluluğu nedeniyle)*. Sorunu çözmek için bu metotların implementasyonlarını boş bırakmak veya **NotImplementedException** fırlatmak gibi yaklaşımlar kullanmak zorundayız. Burada açık bir şekilde bir SOLID ilkesi ihlali söz konusudur. Sizce bu vakada hangi **SOLID** prensibi ihlal edilmektedir.

- A) **DRY *(Don't Repeat Yourself)*** prensibi ihlal edilir çünkü *ReadOnlyPdfDocument* sınıfında *Fax* ve *Write* gibi metotların implementasyonları boş bırakılmakta veya *NotImplementedException* fırlatılmaktadır. Bu yaklaşım kod tekrarına ve gereksiz karmaşıklığa yol açar.
- B) **KISS *(Keep It Simple, Stupid)*** prensibi ihlal edilir çünkü *IDocumentOperations* arayüzü, *ReadOnlyPdfDocument* gibi sınıflar için gereksiz metotlar içermektedir, bu da kodun basitliğini ve anlaşılabilirliğini azaltır.
- C) **YAGNI *(You Aren't Gonna Need It)*** prensibi ihlal edilir çünkü *IDocumentOperations* arayüzü, *ReadOnlyPdfDocument* gibi sınıflar için gereksiz metotlar içermektedir, bu da kodun gereksiz yere karmaşıklaşmasına ve bakım zorluklarına yol açar.
- D) **Interface Segregation Principle *(ISP)*** ihlali söz konusudur çünkü *ReadOnlyPdfDocument* sınıfı, ihtiyaç duymadığı metotları uygulamak zorunda kalmaktadır. Bu durum, arayüzlerin daha küçük ve spesifik parçalara ayrılması gerektiğini gösterir.

## Soru 5

Teknik borç *(Technical Debt)*, yazılım geliştirme sürecinde alınan kısa vadeli kararların uzun vadede ortaya çıkan olumsuz etkilerini ifade eder. Bu borç, kodun kalitesini düşürebilir, bakım maliyetlerini artırabilir ve yeni özelliklerin eklenmesini zorlaştırabilir. Daha da önemlisi üretim ortamlarına önceden fark edilmeyen görev kritik hataların sızmasına yol açabilir.

Teknik borç genellikle zaman baskısı, yetersiz kaynaklar veya deneyim eksikliği gibi nedenlerle ortaya çıkar. Teknik borçların tespitinde **Sonarqube**, **Veracode**, **SonarLint** gibi statik kod tarama araçları sıklıkla kullanılır. Bu araçlar kod kalitesini ölçümlerken bazı metrikler kullanır.

Şimdi aşağıdaki kod parçasını göz önüne alalım.

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

- A) **RegisterCustomer** metodunun parametre sayısı çok fazladır. Bu `Long Parameter List` olarak adlandırılan bir ihlaldir ve **Code Smell** kategorisinde yer alır.
- B) İsimlendirme ihlali söz konusudur. Argüman adları **CamelCase** formatında değil, **snake_case** formatında yazılmalıdır.
- C) **Cyclomatic Complexity *(Döngüsel Karmaşıklık)*** ihlali bulunmaktadır. Metot içerisindeki karar ağaçlarının *(if-else, switch blokları)* ve döngülerin çok fazla olması sebebiyle karmaşıklık limiti aşılmıştır.
- D) Kod genelinde **Duplicated Blocks *(Tekrarlanan Kod Blokları)*** tespit edilmiştir. **SonarQube**, benzer nesne oluşturma ve değer atama işlemlerinin proje içerisinde kopya kod olarak yer almasından dolayı **DRY *(Don't Repeat Yourself)*** prensibine aykırı bir durum olduğuna karar verip bu uyarıyı verir.

## Soru 6

Müşteri paneline giriş işlemini *(Login)* icra eden eski bir metodu inceliyorsunuz. Kod aşağıdaki gibi bir **SQL *(Structured Query Language)*** sorgusu çalıştırarak kullanıcının veritabanında olup olmadığını kontrol etmektedir.

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

Kullandığınız derinlemesine kod güvenliği tarama aracı söz konusu kod parçasında kritik bir güvenlik zafiyeti *(Security Vulnerability)* tespit etmiştir. Bu ihlalin temel nedeni ve en doğru çözüm yöntemi hangisidir?

- A) **Cross-Site Scripting (XSS)** zafiyeti bulunmaktadır. Kullanıcıdan alınan *username* ve *password* parametreleri **HTML Encode** işleminden geçirilmeden kullanılmıştır.
- B) Sorguda doğrudan bir **SQL Injection** zafiyeti *(CWE-89)* bulunmaktadır. Girdi parametreleri birleştirilerek *(string interpolation / concatenation)* çalıştırıldığı için, zararlı betikler veritabanında komut olarak işletilebilir. Çözüm olarak parametreli sorgular *(Parameterized Queries)* veya ORM *(Object-Relational Mapping)* kütüphaneleri kullanmak tercih edilmelidir.
- C) Veritabanı bağlantısı **using** bloğunda kullanılmıştır. Bu durum bağlantının gereğinden fazla açık kalmasına sebep olarak **Denial of Service *(DoS)*** saldırılarına karşı sistemi korumasız bırakır.
- D) Şifreler düz metin olarak kontrol edildiğinden **Broken Authentication** zafiyeti vardır. Bu durum koddaki **SQL string** kullanımından bağımsızdır, yalnızca **JWT *(JSON Web Token)*** kullanılarak çözülebilir.

## Soru 7

Henüz mikroservis altyapısına taşınan bir projeyi gözden geçirirken takım arkadaşınızın yazdığı bir veri erişim sınıfında aşağıdaki gibi bir kod kullanımına denk geldiğimizi düşünelim.

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

Bu kod parçası **github** reposuna gönderildiğinde modern kod analizi ve güvenlik araçları *(SonarQube, GitHub Advanced Security, Fortify vb.)* bunu **Kritik / Blocker** seviyesinde bir ihlal olarak işaretlemiştir. Bu uyarının sebebi nedir ve problem nasıl çözülmelidir?

- A) Sınıf içerisinde yer alan *DbSet* gibi *Entity Framework* özelliklerinin *public* erişim belirleyicisi ile dışarı açılması, veritabanı tablolarının doğrudan manipüle edilmesine *(Data Exposure)* yol açar. DbSet'ler kapsüllenmelidir *(Encapsulation)*.
- B) Veritabanının isminin *CustomerDb* olarak verilmesi güvenlik ihlalidir. Veritabanı isimleri hiçbir zaman kod veya yapılandırma dosyalarında gösterilmemelidir.
- C) **Hardcoded Credentials *(Sabitlenmiş Kimlik Bilgileri)*** güvenlik ihlalidir *(CWE-798)*. Şifre ve hassas bağlantı bilgileri kod içerisinde açık metin olarak *(plaintext)* yer almaktadır. Bunun yerine veriler *appsettings.json*, Ortam Değişkenleri *(Environment Variables)* ya da *Azure Key Vault / AWS Key Management Service* gibi gizli dizi *(secret)* yönetim araçlarından okunmalıdır.
- D) *TrustServerCertificate=True* kullanımı **Man-in-the-Middle *(MitM)*** saldırılarına kapı araladığı için bir ihlal söz konusudur. Kodda yalnızca *Password* alanı silinerek bu sorun çözülebilir.

## Soru 8

Şirketin maaş ve prim hesaplamalarını yürüten planlı bir arka plan *(Background Job/Worker)* işine ait bir sınıfta aşağıdaki kod parçasında yer alan metotla karşılaştığımızı düşünelim.

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

Statik kod tarama araçları *(Sonarqube)* bu metot için **Cognitive Complexity *(Bilişsel Karmaşıklık)*** ihlali yapıldığını belirtmekte. İhlalin temel sebebi nedir ve kurumsal mimari standartlarına uygun olarak en ideal çözüm yaklaşımı aşağıdakilerden hangisidir?

- A) İhlal, metot içerisindeki değişken atamalarının çokluğundan kaynaklanmaktadır. *decimal bonus = 0;* değişkeni gereksiz yer kaplar. Kod sadece *return baseSalary * 0.20m* gibi doğrudan dönüşler içermelidir.
- B) İhlalin sebebi uzun bir kod bloğu yazılmış olmasıdır. Çözüm olarak, her bir *case* içerisindeki işlemler asenkron *async/await* olarak yeniden kurgulanmalı ve performans arttırılmalıdır.
- C) İhlal, *switch* ve iç içe geçmiş *if-else* bloklarının kodu okumayı, takip etmeyi ve test etmeyi aşırı zorlaştırmasından kaynaklanmaktadır. Bu problemi çözmek için OOP mantığına uygun bir biçimde **Strateji Tasarım Deseni *(Strategy Pattern)*** veya **Polimorfizm** kullanılarak her departman için ayrı bir yetki hesaplama sınıfı oluşturulmalıdır.
- D) Sorun *employee.Department* değerinin bir *Enum* sabiti olmasından kaynaklanmaktadır. *Enum* değerler *switch-case* bloklarında kullanıldığında derleyiciler çalışma zamanı optimizasyonunu sağlayamaz. Bu veriler *string* değerler olarak tutulmalıdır.

## Soru 9

Bir e-ticaret platformunda ürünlerin fiyatlandırılmasıyla ilgili iş kuralları içeren aşağıdaki modül kodunu inceliyorsunuz. Kodun bakımının zor olduğundan ve yeni kurallar eklenmesi gerektiğinde sürekli değiştirilmek zorunda kalacağından endişe etmektesiniz.

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

Kodun değiştirilmeden genişletilebilir olmasını sağlamak için farklı bir yol arıyorsunuz. Aşağıdaki kod örneklerinden hangisini tercih edersiniz?

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

Büyük ölçekli bir lojistik firmasının sipariş yönetim süreçlerinde görev alan bir yazılım geliştiricisi olduğunuzu düşünün. Kod inceleme *(Code Review)* toplantısında, sipariş başarıyla tamamlandığında müşteriye otomatik olarak e-posta ve SMS gönderme görevini üstlenecek aşağıdaki *OrderProcessorService* sınıfının kodunu gözden geçiriyorsunuz.

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

Bu örnekte nesne yönelimli tasarım ilkelerinden hangisinin ihlal edildiği net bir şekilde görülmektedir ve en doğru çözüm yaklaşımı hangisi olmalıdır? Doğru tespit ve çözüm ikilisini bulun.

- A) Metot içerisinde farklı türden bildirimlerin arka arkaya gönderilmesi **DRY *(Don't Repeat Yourself)*** prensibine aykırıdır. Bildirimler tek bir *SendAllNotifications()* metodu altında birleştirilmeli ve sadece o çağrılmalıdır.
- B) İşlem sırasında *new* anahtar kelimesi kullanılarak *SmtpEmailService* ve *TwilioSmsService* gibi somut *(concrete)* sınıflara doğrudan bağımlılık *(tight coupling)* oluşturulmuştur. Bu durum **Dependency Inversion Principle *(DIP)*** ihlalidir. Sınıf somut servislere değil, *INotificationService* gibi soyutlamalara *(abstractions)* bağımlı olmalı bu bağımlılıklar koda *constructor-yapıcı metot* aracılığıyla enjekte edilmelidir *(Dependency Injection)*.
- C) Yerel *(local)* değişken olarak yaratılan bu servisler bellekte sızıntıya *(Memory Leak)* yol açabilir. Performans kaybı yaşamamak için değişkenler metot içinde değil, sınıf seviyesinde *static* değişkenler olarak tanımlanmalıdır.
- D) Siparişin statüsünü değiştirmek ve arkasından bildirim göndermek aynı metotta yapıldığı için **Liskov Substitution Principle *(LSP)*** ihlal edilmiştir. Alt sınıflar bu metodu ezdiğinde *(override)* sorun yaşama ihtimali oldukça yüksektir.

## Soru 11

Bir çağrı merkezi sisteminde aday kayıt sürecini yöneten modülde iş kurallarına aykırı veriler için özel hatalar üretmek istiyorsunuz. Örneğin, adayın e-posta adresi geçersiz ise sistemin teknik bir hata yerine anlamlı bir domain hatası döndürmesini amaçlıyorsunuz. Bunun için takım arkadaşınız aşağıdaki yaklaşımları öneriyor. Sizce en doğru yaklaşım hangisidir?

- A) Tüm hataları tek tip yönetmek için her yerde yalnızca **Exception** fırlatılmalı, hata türleri mesaj metninden anlaşılmalıdır.
- B) Geçersiz iş durumlarını temsil eden, **Exception** sınıfından türeyen anlamlı bir özel hata türü *(örneğin InvalidCandidateEmailException)* tanımlanmalı; ilgili iş kuralı ihlalinde bu tür fırlatılmalıdır.
- C) Hata yönetimini sadeleştirmek için iş kuralı ihlallerinde **null** döndürülmeli ve üst katmanlar bunu sessizce yok saymalıdır.
- D) Domain katmanında hata fırlatmak yerine tüm doğrulamaları yalnızca veritabanı kısıtlarına bırakmak en doğru yöntemdir.

## Soru 12

Kurumsal bir insan kaynakları uygulamasında, başlangıçta tek sunucuda çalışan katmanlı *(Layered)* bir sistem kullanıldığını düşünelim. Zamanla popüler halen gelen uygulama gün geçtikçe daha çok abone almaya ve buna bağlı olarak da trafik artmaya başlıyor. Bunun üzerine yazılım ekibi uygulamanın bazı parçalarını farklı sunuculara ayırmayı ve böylece sistemi kabul edilebilir şekilde ölçeklemeyi *(scaling)* istiyor. Bu noktada çözümün hem mantıksal katman *(Logical Layer)* ayrımını koruması hem de fiziksel dağıtıma *(Deployment)* imkan vermesi bekleniyor. Şıklarda belirtilen mimari yaklaşımlardan hangisini tercih edersiniz?

- A) N-Tier yaklaşımını kullanarak sunum *(presentation)*, iş *(business)* ve veri katmanlarını *(data layer)* farklı fiziksel ortamlara dağıtmak.
- B) Tüm bileşenleri tek bir çalıştırılabilir dosyada toplayıp katmanları sadece klasör seviyesinde ayırmak.
- C) **Onion** mimarisini tamamen bırakıp doğrudan veritabanı odaklı **stored procedure** kullanımına geçmek ve veritabanı seviyesinde hız optimizasyonu yapmak.
- D) Katmanlar arası iletişimi azaltmak için bütün modülleri tek bir **Domain Service** sınıfında birleştirmek.

## Soru 13

Operasyon ekibi mevcut monolitik yapıda bakım maliyetlerinin arttığını ve dağıtım *(deployment)* hızının düşük kaldığını raporluyor. Buna karşılık ekipte bazı geliştiriciler doğrudan mikroservislere geçmeyi, bazıları ise önce geçiş sürecini daha kontrollü yönetmeyi öneriyor. Kararsızlık ve belirsizlik sürecin uzamasına neden oluyor. Ancak önünüzde aşağıdaki seçenekler var. Sizce böyle bir durumda en sağlıklı yaklaşım hangisi olur?

- A) Tüm modülleri tek seferde mikroservislere bölmek ve geçişler sırasında vakit kaybına neden olan tüm testleri geçici olarak devre dışı bırakmak.
- B) Yalnızca veritabanını bölmek, uygulama kodunu ve dağıtımı ise tamamen aynı bırakmak.
- C) Önce **modüler monolit** veya **vertical slice** gibi yaklaşımlarla sınırları netleştirip, olgunlaşan modülleri ihtiyaç oldukça bağımsız servislere taşımak.
- D) Dağıtık mimariye geçişin karmaşıklığını azaltmak için tüm domain kurallarını istemci tarafında çalıştırmak.

## Soru 14

Bir yazılım projesinde katmanlar arası bağımlılık yönünü yanlış kurgulanmış ve domain katmanında doğrudan *ORM-Object Relational Mapping* ve mesaj kuyruğu *(message broker)* kütüphaneleri kullanılmaya başlanmıştır. Oysa ki bu tür bağımlılıklar domain katmanını etkilememelidir. Bu hata birim testlerini zorlaştırmış ve iş kurallarının altyapı değişikliklerinden etkilenmesine neden olmuştur. Bu problemi çözmek için seçeneklerden hangisini önerirsiniz?

- A) Domain katmanında altyapı bağımlılıklarını koruyup testlerde bu kütüphaneleri gerçek ortamda çalıştırmak.
- B) Domain katmanını kaldırıp tüm iş kurallarını **Application** katmanında statik yardımcı sınıflara taşımak.
- C) Bağımlılık problemini azaltmak için tüm katmanları aynı namespace altında birleştirmek.
- D) Altyapı detaylarını domain'e taşımak yerine domain içinde yalnızca sözleşmeleri *(interface/contract)* tanımlayıp somut implementasyonları **Infrastructure** katmanında tutmak.

---

## Cevap Anahtarı

| Soru | Doğru Cevap |
|------|-------------|
| 1    | C           |
| 2    | A           |
| 3    | D           |
| 4    | D           |
| 5    | A           |
| 6    | B           |
| 7    | C           |
| 8    | C           |
| 9    | B           |
| 10   | B           |
| 11   | B           |
| 12   | A           |
| 13   | C           |
| 14   | D           |
