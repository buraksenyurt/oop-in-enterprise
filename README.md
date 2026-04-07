# Kurumsal Yazılım Çözümlerinde OOP ile Çalışmak

Sektör kampüste projesi kapsamında açtığımız **"Kurumsal Yazılım Çözümlerinden OOP ile Çalışmak"** dersine ait notların, örnek kodların yer aldığı repodur.

- [Kurumsal Yazılım Çözümlerinden OOP ile Çalışmak](#kurumsal-yazılım-çözümlerinden-oop-ile-çalışmak)
  - [Önsöz](#önsöz)
  - [Referanslar](#referanslar)
  - [Gereksinimler](#gereksinimler)
  - [Gün Bazlı Özetler](#gün-bazlı-özetler)
    - [Gün 00](#gün-00)
    - [Gün 01](#gün-01)
    - [Gün 02](#gün-02)
    - [Gün 03](#gün-03)
    - [Gün 04](#gün-04)
      - [Mimariler Hakkında Genel Kavramlar](#mimariler-hakkında-genel-kavramlar)
    - [Gün 05](#gün-05)

## Önsöz

Bu derste amacımız kurumsal yazılım çözümlerinde OOP'nin nasıl kullanıldığına dair temel bir anlayış kazanmaktır. OOP'nin temel prensiplerini ve bu prensiplerin kurumsal yazılım geliştirme süreçlerinde nasıl uygulandığını öğreneceğiz. Ayrıca, OOP'nin avantajlarını ve dezavantajlarını tartışarak, gerçek dünya senaryolarında nasıl etkili bir şekilde kullanılabileceğini göreceğiz. Uçtan uca bir proje üzerinden ilerlerken, dağıtık sistemlere, domain driven design (DDD) ve microservices mimarisi gibi kavramlara da değineceğiz.

**Vize ve final sınavlarına hazırlanmak için aşağıdaki quizleri çözebilirsiniz.**

- [Quiz 00](./exams/quiz00.md)
- [Quiz 01](./exams/quiz01.md)
- [Quiz 02(Draft)](./exams/quiz02.md)

## Referanslar

- C# Programlama dili ile ilgili temel kavramlar için [ProgrammingWithCSharp](https://github.com/buraksenyurt/ProgrammingWithCSharp) reposunu inceleyebilirsiniz.

## Gereksinimler

Müfredat boyunca bize gereken araçlar ve ortamlar;

- DotNet 8 veya üstü SDK [Download için](https://dotnet.microsoft.com/en-us/download)
- *Visual Studio Code* veya *Visual Studio* ya da kendinizi rahat hissettiğiniz herhangi bir IDE
- [Git](https://git-scm.com/install/) ve GitHub hesabı

## Gün Bazlı Özetler

Derslerde ele aldığımız konulara ait ana başlıklar ve yardımcı bazı bilgilere bu bölümde yer verilmektedir.

### Gün 00

Bugün kullandığımız komut satırı ifadeleri şöyledir.

```bash
# Repoyu lokal bilgisayar klonlamak için
git clone https://github.com/buraksenyurt/oop-in-enterprise.git

# Makinedeki git komutlarını öğrenmek için
git --help

# Değişiklikleri görmek için
git status

# Değişiklikleri stage'e almak için
git add .

# Değişiklikleri local makinede commit etmek için
git commit -m "commit mesajı"

# Değişiklikleri repoya göndermek için
git push origin main

# Repodaki son değişiklikleri çekmek için
git pull origin main
```

![day_00_01](./images/day_00_01.png)

Dotnet tarafında kullandığımız komutlar ise şöyle.

```bash
# Makinede yüklü dotnet sürümünü öğrenmek için
dotnet --version

# Dotnet ile ne tür projeler geliştirebiliriz
dotnet new list

# Yeni bir console projesini dotnet komut satırından çalıştırdık
dotnet new console -o HelloWorld

# Programımızı çalıştırmak için
dotnet run
```

Bu ilk dersimize ait bazı önemli noktaları aşağıdaki gibi özetleyebiliriz.

- Github üzerinde Repository oluşturma
- Temel **markdown** komutları
- **Source Control** aracı olarak **git** CLI aracını kullandık.
- Kurumsal yazılım çözümlerinde standartlar önemlidir ve bunu sağlamanın birçok yolu var. Birisi de **Code Review** süreçlerini işletmektir. *(Anahtar kelimeler: Code Review, Pull Request, Merge Request)*
- Belli bir domain'e özgü veri yapısı tasarlarken ilk adımlar

### Gün 01

İkinci dersimizde yeni bir sınıf kütüphanesi oluşturup kısaca birbirleriyle ilişkili *business object*'lerin tasarımına değindik. Deneysel olarak insan kaynakları domain'ine ait *Candidate*, *Graduate*, *ContactInformation* gibi sınıflar tasarlamaya başladık. Anafikir olarak gerçek dünya iş problemlerini modelleyebileceğimiz tutarlı ve anlaşılır bir domain tasarımı üzerinde durmaya çalıştık. Üzerinde durduğumuz temel kullanımları şöyle özetleyebiliriz;

- Yeni bir dotnet solution oluşturulması
- Yeni bir proje oluşturulması (Örneğin bir class library) ve solution'a eklenmesi
- Basit entity sınıf tasarımı
- Basit enum türü kullanımı
- Primitive tipler; string, GUID, short, float, bool
- Kompleks tip kullanımı; `List<T>`

Bu derste kullandığımız komut satırı ifadeleri.

```bash
# Yeni bir dotnet solution oluşturulması
dotnet new sln -n HumanResources

# Yeni bir proje oluşturulması (Örneğin bir class library)
dotnet new classlib -o HumanResources.Domain

# Projeyi var olan bir Solution'a eklemek
dotnet sln add HumanResources.Domain/HumanResources.Domain.csproj

# Tüm Solution'ın derlenmesi
dotnet build
```

### Gün 02

Bu derste aşağıdaki konu başlıklarını ele aldığımız örnekler üzerinde çalıştık.

- Sadece alanlardan *(fields)* oluşan sınıf tasarımı.
- *Enum* veri türü kullanımı.
- Bir sınıftan nesne örneği oluşturmak için farklı yollar. *(Constructor kullanmadan)*
- Nesne örnek dizileri oluşturmak ve kullanmak.
- İleri yönlü iterasyonlar için *foreach* döngüsü kullanımı.
- Nesne metotları ve bu metotların nesne örnekleri üzerinden çağrılması.
- *Static* sınıflar ve metotların kullanımı.
- *Higher order functions* kavramına giriş niteliğinde Where, ForEach gibi metotların örnek kullanımı.
- Visual Studio ortamında temel *Debug* işlemleri *(Step into, Step over)* ve nesnelerin anlık durumlarının izlenmesi.

[Örnek kod dosyası](src/Fundamentals/ObjectInstances.cs)

### Gün 03

Önceki dersteki konuların tekrarını takiben geçmiş yıllara dönüp **CSV** formatında veri içeren müşteri verilerinin bir çağrı merkezi sistemine alınması üzerine aşağıdaki diagramdaki akışı değerlendirdik.

![Call Center Case](./images/CallCenterCase1999.png)

Ayrıca aşağıdaki konu başlıklarını ele aldık.

- Constructor overloading kavramı ve kullanımı
- Property'ler de private set kullanımı
- Temel seviyede *encapsulation* kavramı ve uygulaması
- *Exception* yapısı ve *try-catch* blokları ile hata yönetimi
- *Exception* yapısında *throw* ifadesi ile hata fırlatma
- *Rich Entity* kavramı ve basit örnek üzerinden incelenmesi
- *Debug* işlemleri ve *breakpoint* kullanımı

[Örnek kod dosyası](src/Fundamentals/ObjectInstances2.cs)

### Gün 04

Bu dersimizde aşağıdaki konu başlıklarını ele aldık.

- Github **commit**'lerin nasıl takip edileceğini ve değişikliklerin nasıl izlenebileceğini gördük.
- Kod tarafında kendi **Exception** sınıflarımızı nasıl yazabileceğimize baktık.
- En genel bakış açısı ile bazı yazılım mimarileri ve kavramları üzerinde durduk.
- [Quiz00](exams/quiz00.md) soruları üzerinden geçtik ve birkaçını birlikte çözdük.

#### Mimariler Hakkında Genel Kavramlar

Yazılım mimarilerini genellikle "Monolithic" ve "Distributed" olmak üzere iki ana kategoriye ayırmak mümkündür. Monolitik mimarilerde tüm uygulama tek bir birim olarak geliştirilir ve dağıtılır. Dağıtık mimarilerde ise uygulama birden fazla bağımsız birim olarak geliştirilir ve bu birimler birbirleriyle iletişim kurarak çalışır. Tabii birçok uygulama biçimi ve motifi vardır. Bunların birbirlerine göre avantaj ve dezavantajları **Richards & Ford'un, Fundamentals of Software Architecture** kitabında detaylı bir şekilde ele alınmıştır. Aşağıdaki tabloda bu mimarilerin bazı temel özellikleri kıyaslanmaktadır.

- **Monolithic mimariler:** Layered, Pipeline, Mikro Kernel
- **Distributed mimariler:** Service Based, Event Driven, Space Based, Service Oriented, Microservices

| | **Layered** | **Pipeline** | **Mikro Kernel** | **Service Based** | **Event Driven** | **Space Based** | **Service Oriented** | **Microservices** |
| --------- | --------- | ---------- | -------------- | --------------- | -------------- | ------------- | ------------------ | --------------- |
| **Partition Type** | Technical | Technical | Domain + Technical | Domain | Technical | Domain + Technical | Technical | Domain |
| **Number of Quanta** | 1 | 1 | 1 | 1..n | 1..n | 1..n | 1 | 1..n |
| **Deployability** | ★ | ★★ | | ★★★★ | ★★★ | ★★★ | ★ | ★★★★ |
| **Elasticity** | ★ | ★ | | ★★ | ★★★ | ★★★★ | ★★ | ★★★★★ |
| **Evolutianry** | ★ | ★★★ | | ★★★ | ★★★★★ | ★★★ | ★ | ★★★★★ |
| **Fault Tolerance** | ★ | ★ | | ★★★★ | ★★★★★ | ★★★ | ★★★ | ★★★★ |
| **Modularity** | ★ | ★★★ | | ★★★★ | ★★★★ | ★★★ | ★★★ | ★★★★★ |
| **Overall Cost** | ★★★★★ | ★★★★★ | ★★★★★ | ★★★★ | ★★★ | ★★ | ★ | ★ |
| **Performance** | ★★ | ★★ | ★★★ | ★★★ | ★★★★★ | ★★★★★ | ★★ | ★★ |
| **Reliability** | ★★★ | ★★★ | ★★★ | ★★★★ | ★★★ | ★★★★ | ★★ | ★★★★ |
| **Scalability** | ★ | ★ | ★ | ★★★ | ★★★★★ | ★★★★★ | ★★★★ | ★★★★★ |
| **Simplicity** | ★★★★★ | ★★★★★ | ★★★★ | ★★★ | ★ | ★ | ★ | ★ |
| **Testability** | ★★ | ★★★ | ★★★ | ★★★★ | ★★ | ★ | ★ | ★★★★ |

Bununla birlikte günümüzde yaygın kullanılan bazı mimarilerin temel birkaç özelliği aşağıdaki görselde yer alan taban tasarımlar üzerine inşa edilmiştir. Örneğin **hexagonal** ve **clean architecture**, **onion architecture** taban tasarımı üzerine inşa edilmiştir.

![Common Architectures](./images/day_04_01.png)

Layered, N-Tier ve Onion mimariler ile ilgili aşağıdaki tabloda yer alan bilgileri verebiliriz.

| **Özellik** | **Layered** | **N-Tier** | **Onion** |
| --------- | --------- | ---------- | -------------- |
| **Odak(Focus)** | Mantıksal ayrım *(kod bazlı katmanlar)* | Fiziksel ayrım *(Dağıtılabilir katmanlar)* | Domain bazında izolasyon ve test edilebilirlik |
| **Dağıtım(Deployment)** | Genelde tek sunucu *(single-tier)* | Her katman farklı bir makineye deploye edilebilir | Genellikle single-tier |
| **Bağımlılık Yönü(Dependency Direction)** | Aşağı yönlü - Downward *(UI->BLL->DAL)* | Layered ile aynı | İçe doğru *(Inward)* *(Infra -> App -> Domain)* |
| **Domain Katmanı Yeri** | Ortada | Ortada | Mimarinin çekirdeğinde *(merkezde)* |
| **Dependency Inversion** | Zorlanmaz | Zorlanmaz | Sözleşmeler *(Contract)* üzerinden zorunlu |
| **Infrastructure Dependency** | Core katmanı infra'ya bağımlıdır *(kötü tasarım)* | Genellikle core katmanı infra'ya bağımlıdır | Infra, Core'a bağımlıdır *(İyi tasarım)* |
| **Örnek Vakalar** | Yoğun CRUD(1) içeren uygulamalar, MVP'ler(2), basit uygulamalar | Günvelik *(security)*, yük dengesi *(load balancing)*, ölçek *(scalability)* kritik kurumsal çözümler | Infrastructure kaçaklarından sakınılan, sürekli gelişen iş kuralları içeren, domain bütünlüğü konusunda hassas, çekirdek *(core)* kuralların test edilmesi gereken karmaşık uygulamalar |

(1) CRUD: Create, Read, Update, Delete işlemlerini ifade eder.
(2) MVP: Most Viable Product, ürünün piyasaya sürülebilecek en temel özelliklerini içeren ve yatırımcıya gösterilebilecek ilk sürümüdür.

> Başlangıç seviyesinde bir mimari tasarım için [Hexagonal Architecture](https://github.com/buraksenyurt/HexagonalArchitecture_101) konusuna bakılabilir.

Değişen ihtiyaçlar ve standartlar doğrultusunda karışımıza farklı mimari tasarımlar da çıkabilir. Örneğin **Vertical Slice Architecture** ya da **Modular Monolith** gibi mimariler de günümüzde popüler olan mimari tasarımlar arasında yer almaktadır.

### Gün 05

Bu dersimizde farklı yazılımların birbirleriyle ağ ortamında nasıl haberleştiğine dair aşağıdaki çizelgede yer alan örnek senaryoyu inceledik.

![Services](./images/day_05_00.png)

Özellikle büyük çaplı legacy sistemler bir takım görev kritik süreçler içerebilirler. Bu tip süreçlerin sistemden kopartılması veya modernize edilmesi her zaman kolay değildir. İlerleyen zamanlarda daha modern/yeni uygulamaların, legacy sistemle çeşitli ortak süreçler üzerinden haberleşmesi gerekebilir. Bu tür durumlarda farklı uygulamalar arasında veri alışverişi yapmak ya da süreç işletmek için çeşitli yöntemler kullanılır. Anlık veya zamanlanmış veri alışverişi senaryolarında, genellikle **API**'ler, **Message Queues** veya **File Transfer** yöntemleri tercih edilir. Her birinin avantajları ve dezavantajları vardır ve seçim, uygulamanın ihtiyaçlarına, performans gereksinimlerine ya da mevcut altyapıya bağlı olarak değişir.

Anlık iletişimlerde **REST *(Representational State Transfer)*, **gRPC *(Google Remote Procedure Call)*** veya **SOAP *(Simple Object Access Protocol)*** tabanlı API'ler yaygın olarak kullanılır ancak bazı durumlarda anlık talep sayısı o kadar yüksek olur ki süreçlerin asenkron bir şekilde yönetilebilmesi gerekir. Bu gibi durumlarda Message Queue'lar devreye girebilir. RabbitMQ, Apache Kafka gibi araçlar bu tür senaryolarda popüler çözümler arasında yer alır. Diğer yandan bazı vakalarda dosya tabanlı veri alışverişi de tercih edilebilir *(FTP)*. Bu yöntem genellikle büyük veri setlerinin transferi veya belirli bir formatta veri paylaşımı gerektiğinde kullanılır.

Entegrasyon yöntemleri ile ilgili olarak aşağıdaki tabloda bazı yardımcı bilgiler yer almaktadır.

| **Kriter** | **API(REST/gRPC/SOAP)** | **Message Queue** | **File Transfer** |
| --------- | --------- | ---------- | -------------- |
| **İletişim Türü** | Genellikle senkron *(Request-Response)* | Asenkron *(Publish-Subscribe, Point-to-Point)* | Zamanlanmış *(Batch veya asenkron)* |
| **Gecikme *(Latency)*** | Düşük gecikme, anlık iletişim | Değişken. Kuyruk yoğunluğuna göre nanosaniyeden dakikalar mertebesine çıkabilir. | Yüksek gecikme, dosya transferi süresi ve dolayısıyla anlık iletişim için uygun değildir |
| **Veri Boyutu** | Küçük ve orta boyutlu JSON, XML, Protobuf içerikleri | Küçük boyutlu ancak sayıca çok fazla veri | Büyük ölçekli veri (Gigabyte mertebesi ve üzeri) |
| **Sistem Bağlılığı** | Sıkı bağlı *(Tight coupling)* Karşı sistemin o an ayakta olması gerekir. | Gevşek bağlı *(Loose coupling)* Üreten ve tüketen sistemlerden birbirinden bağımsızdır. | Gevşek bağlı *(Loose coupling)* Sadece ortak bir dosya sistemi veya sunucu gerekitir. |
| **Hata Yönetimi ve Güvenilirlik** | Uygulama seviyesinde. timeout, retry gibi mekanizmalarla yönetmek gerekir. | Yüksek. Mesajlar kaybolmaz, teslimat garantisi yüksektir. | Düşük/Orta. İletişim koparsa transferin baştan başlaması veya kaldığı yerden devam etmesi gerekir. Ek scriptler gerektirebilir. |

Gerçek hayat senaryoları açısından da durumu değerlendirebiliriz.

| **Senaryo** | **Yöntem** | **Neden?** |
| --------- | --------- | ---------- |
| Yeni bir web uygulamasının legacy **CRM *(Customer Relationship Management)*** sisteminden yararlanarak müşterilerin güncel bakiye veya kredibilite bilgilerine anlık olarak bakması gerekiyor. | API *(REST, gRPC)* | Anlık bilgi ihtiyacı var, düşük gecikme gereksinimi var, veri boyutu küçük. |
| Bir e-ticaret platformu, black-friday gibi yüksek trafik aldığı günlerde legacy sistemdeki faturalama süreçlerini sorunsuz şekilde kullanmak istiyor. | Message Queue | Anlık talep sayısı çok yüksek olabilir, süreçlerin asenkron yönetilmesi gerekir, mesaj kaybı olmamalıdır. |
| Muhasebe paketini kullanan legacy uygulamanın her gece sabaha karşı 04:00 sularında günsonu dökümlerini veri ambarı sistemine aktarması gerekiyor. | File Transfer *(FTP)* | Büyük veri setleri transfer edilecek, anlık iletişim gerekmiyor, zamanlanmış bir süreç var. |
| Legacy sistemde gerçekleşen bir bir tık, hata veya giriş işleminin, yeni nesil bir analiz platformuna *(Elasticsearch, Splunk gibi)* anlık olarak iletilmesi gerekiyor. | Message Queue *(RabbitMQ, Kafka vb.)* | Anlık talep sayısı çok yüksek olabilir, süreçlerin asenkron yönetilmesi gerekir, mesaj kaybı olmamalıdır. |

Dersimizin ikinci bölümünde nesne yönelimli dillerde bileşen bağımlılıklarının *(Dependency)* yönetimi ile ilgili ön hazırlıklara başladık ve çalışan ama sorunlu olan bir kod parçasını ele aldık. Bu kodun problemlerinin neler olduğunu keşfi öğrencilere bırakıldı. Sonraki derste bu kodun nasıl iyileştirilebileceği üzerine konuşacağız.

[Örnek kod dosyası](src/Fundamentals/DependencyManagement.cs)

## Gün 06
