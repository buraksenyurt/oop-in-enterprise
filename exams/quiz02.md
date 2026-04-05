# OOP Test Soruları 02

Bu testin amacı matematiksel bazı kavramların programatik ortamda modellenmesi, veri yapıları ve algoritma performansı etkilerinin ölçülmesine yöneliktir.

*Not: Sorular .Net 10 sürümü üzerinden hazırlanmıştır.*

---

## Soru 1

Bir algoritmanın performansını değerlendirmek için kullanılan **[BigO](https://www.bigocheatsheet.com/)** notasyonu, algoritmanın çalışma zamanının veya bellek kullanımının girdi boyutuna göre nasıl değiştiğini ifade eden bir enstrümandır. Farklı türleri vardır. Örneğin, **O(1)**, **O(n)**, **O(n^2)** gibi. **O(1)**, algoritmanın çalışma zamanının girdi boyutundan bağımsız olduğunu ifade ederken, **O(n)** algoritmanın çalışma zamanının girdi boyutuna doğrusal olarak arttığını ifade eder. **O(n^2)** ise algoritmanın çalışma zamanının girdi boyutunun karesiyle arttığını ifade eder. Bu notasyon, algoritmaların verimliliğini karşılaştırmak ve büyük veri setleriyle çalışırken hangi algoritmanın daha uygun olduğunu belirlemek için kullanılır.

Elimizde bir milyon elemanlı iki devasa asal sayı kümesi olduğunu varsayalım. **A** ve **B** isimli bu asal sayı kümelerinin kesişimlerini bulmamız gerekiyor. Bununla ilgili ilk protopipi aşağıdaki kod parçasında görüldüğü gibi yazdığımızı düşünelim.

```csharp
public List<int> FindIntersection(List<int> setA, List<int> setB)
{
    var intersection = new List<int>();
    foreach (var numberA in setA)
    {
        if (setB.Contains(numberA))
        {
            intersection.Add(numberA);
        }
    }
    return intersection;
}
```

Sizce bu kod parçası ile ilgili olarak aşağıdaki ifadelerden hangisi doğrudur?

- A) **Contains** çok hızlı çalışan bir metottur ve bu nedenle kodun **BigO** değeri **O(n)** olarak ifade edilir.
- B) Kodun **BigO** değeri **O(n^2)** olarak ifade edilir. Performansı artırmak için setB'yi bir **HashSet**'e dönüştürmek ve **Contains** yerine **HashSet**'in **O(1)** zaman karmaşıklığına sahip **Contains** metodunu kullanmak gerekir.
- C) Kod çalışma zamanında **segmentation fault** hatasının oluşmasına neden olur çünkü intersection listesi sürekli büyümekte ve bellek sınırlarının aşılmasına neden olmaktadır. Bu nedenle, kodun **BigO** değeri **O(n^3)** olarak ifade edilir.
- D) `List<int>` veri yapısını kullanmak yerine int[] dizisi kullanmak algoritmik karmaşıklığı azaltır ve kodun **BigO** değeri **O(log n)** olarak ifade edilir.

## Soru 2

**C#** sunduğu enstrümanlar açısından oldukça zengin bir dildir. Bir modeli tasarlarken **class**, **struct**, **record** gibi farklı türlerde veri yapıları kullanabiliriz. Her birinin kendine özgü kullanım senaryoları ve avantajları vardır. Örneğin, **class** referans tipi olarak davranır ve genellikle karmaşık nesneleri modellemek için kullanılırken, **struct** değer tipi olarak davranır ve genellikle küçük, basit veri yapıları için tercih edilir. **record** ise hem referans hem de değer tipi özelliklerini taşıyan, özellikle immutable *(değiştirilemez)* veri modelleri için tercih edilen bir türdür.

Bu bilgiler ışığında milyonlarca kompleks sayının *(complex numbers)* kullanıldığı bir simülasyon kodu üzerinde çalıştığımızı düşünelim. Bu programda kompleks sayıları temsil edecek bir veri yapısına ihtiyacımız var. Sizce aşağıdaki şıklardan hangisi en güvenli ve performanslı yoldur?

- A) Kompleks sayıları temsil etmek için bir **class** tanımlamak en uygun yaklaşımdır çünkü karmaşık nesneleri modellemek için referans tipi kullanmak daha esneklik sağlar.
- B) Kompleks sayıları temsil etmek için bir **struct** tanımlamak en uygun yaklaşımdır çünkü küçük, basit veri yapıları için değer tipi kullanmak daha performanslıdır.
- C) Kompleks sayıları temsil etmek için **record struct *(readonly struct)*** tanımlamak en uygun yaklaşımdır çünkü bu sayede Heap'te gereksiz bellek tahsisi yapılmaz, **Garbage Collector**'un yükü hafifler ve nesneler immutable olur, bu da thread-safe bir yapı sağlar.
- D) Kompleks sayıların gerçel ve sanal kökü için `Tuple<double, double>` kullanmak en uygun yaklaşımdır çünkü bu sayede iki değeri tek bir veri yapısında tutabiliriz.

## Soru 3

Matematikteki sonsuz dizileri *(Fibonacci, Taylor, vb.)* programatik olarak modellemek bazen zordur çünkü bu diziler teorik olarak sonsuz sayıda eleman içerir ve bellekte taşma hatalarına *(out of memory)* neden olabilirler. **C#** dilinde bu tip serileri modellerken *yield return* ifadesi sıklıkla kullanılır. Aşağıdaki kod parçasını göz önüne alalım.

```csharp
public IEnumerable<long> GenerateFibonacci()
{
    long a =0, b=1;
    yield return a;
    yield return b;

    while (true)
    {
        long next = a + b;
        yield return next;
        a = b;
        b = next;        
    }
}
```

Bu kodun çalışma mantığı ile ilgili olarak aşağıdaki ifadelerden hangisi **yanlıştır?**

A-) Bu kod bellekte oluşturulan tüm fibonacci sayılarını tutmaya çalışır ve en nihayetinde program **OutOfMemoryException** hatası vererek çöker.
B-) Bu kod, *yield return* ifadesi sayesinde her seferinde sadece bir sonraki fibonacci sayısını üretir ve bellekte sadece o sayı tutulur. Bu sayede sonsuz bir dizi oluşturulabilir ve bellek taşması olmaz.
C-) Metot, `IEnumerable<long>` döndürdüğü için çağıran taraf **GenerateFibonacci().Take(10)** veya **GenerateFibonacci().Skip(100).Take(10)** gibi ifadelerle istediği kadar fibonacci sayısını alabilir.
D-) Metot çalıştırıldığında sonsuz bir dönyü oluşmaz, değerler yalnızca ihtiyaç duyulduğunda üretilir *(Bir foreach döngüsü veya LINQ sorgusu tarafından tüketildiğinde)*. Bu *Lazy Evaluation* olarak bilinir ve performans açısından avantaj sağlar.

## Soru 4

Bazı problemlerde çok hassas küsürat değerleri ile çalışmamız gerekebilir. **C#** programlama dilinde bu tür durumlarda için **double**, **decimal**, **float** gibi farklı veri türleri yer alır. Bir limit problemi üzerinde çalıştığımızı ve testler sırasında aşağıdaki kod parçasını ele aldığımızı düşünelim.

```csharp
double x = 0.1;
double y = 0.2;
double sum = x + y;
Console.WriteLine(sum == 0.3); 
```

Sizce bu kod çalıştırıldığında terminal ekranına ne yazar ve bunu sebebi nedir?

- A) Terminal ekranına **True** yazar çünkü matematiksel olarak 0.1 + 0.2 = 0.3'tür ve C#'ın **double** veri türü bu işlemi doğru şekilde temsil eder.
- B) Terminal ekranına **False** yazar çünkü **double** veri türü, ikili tabanda ondalık sayıları tam olarak temsil edemez *(0.3 yerine 0.30000000000000004 gibi bir değer alır)*. Bunun sebebi double veri türünün **IEEE 754** standardını kullanmasıdır.
- C) Terminal ekranına **True** yazar çünkü .Net derleyicisi bunu derleme zamanında *(Compile Time)* optimize eder ve matematiksel olarak doğru sonucu verir.
- D) Aslında bu kod derlenmez zira **==** operatörü ile iki double değer karşılaştırılamaz.

## Soru 5

Rota optimizasyonu yapan bir programda hedef lokasyona gidilecek en kısa yolu bulmak için **Dijkstra** algoritmasını kullanmaya karar verdiniz. Şehir detayları ise bir veritabanında yer alıyor. Veritabanından şehir bilgilerini çektikten sonra algoritmayı uygulamak için aşağıdaki kod parçasını yazdığınızı düşünelim. *(Path ve SqlDatabase sınıflarının tanımlarını göz ardı edelim)*

```csharp
public class RouteOptimizer{
    public Path FindShortestPath(City start, City end)
    {
        var db = new SqlDatabase("Server=.;Database=CityData;Trusted_Connection=True;");
        var graph = db.GetCityGraph(); // Şehirler arası mesafeleri içeren bir grafik yapısı döndürür
        // Dijkstra algoritması burada uygulanır
        return path; // Burada bulunan en kısa yol döndürülür
    }
}
```

Algoritmamız harika çalışıyor olmasına rağmen test mühendisi arkadaşımızın kodla ilgili endişeleri var. Kodun test edilemz olduğunu ve gerçekten bir veritabanı bağlantısı olmadan bir birim test *(unit test)* yazmanın mümkün olmadığını söylüyor. Dolayısıyla algoritma mantığını veritabanı altyapısından ayırmanız gerekiyor. Aşağıdaki seçeneklerden hangisini kullanırsınız.

- A) Test mühendisinin görüşüne itiraz ederim zira veritabanı ile algoritmayı aynı metot altında toplamak performans açısından avantajlıdır.
- B) Algoritmanın çalışma sırasında oluşan değerleri bir **log** dosyasına yazdırırım. Böylece algoritmanın doğruluğunu manuel olarak da kontrol edebiliriz.
- C) Veritabanı bağlantısını ve algoritma mantığını birbirinden ayırmak için **Dependency Injection** tekniğini kullanırım. Veritabanı erişimi için bir **ICityGraphProvider** arayüzü tanımlar ve bu arayüzü uygulayan somut bir sınıf kullanırım. **RouteOptimizer** sınıfının yapıcı metoduna *(constructor)* bu arayüzü enjekte ederim. Böylece algoritmayı test ederken gerçek veritabanı bağlantısı yerine sahte *(mock)* bir **ICityGraphProvider** implementasyonu kullanarak sadece algoritmanın doğruluğunu test eden birim testler yazabilirim.
- D) FindShortestPath metodunu SqlDatabase sınıfının bir metodu haline getiririm.

## Soru 6

**Pi *(π)*** sayısının değeri sonsuz ondalık basamağına sahip bir irrasyonel sayıdır. Matematikteki birçok problemde **π** sayısının yüksek hassasiyetle hesaplanması gerekebilir. Bu konuda çalışan bir arkadaşınız bu değeri hesaplamak için **Monte Carlo** yöntemini tercih ettiği bir kod parçası geliştirmiş. Rastgele x, y koordinatları üretip bu noktaların birim çemberin içine düşüp düşmediğini kontrol ederek **π** sayısının yaklaşık değerini hesaplattığını belirtmekte. Bu amaçla aşağıdaki gibi bir kod parçası yazmış.

```csharp
int inCircle = 0;
long totalIterations = 10_000_000;

using (ThreadLocal<Random> threadLocalRandom = new(() => new Random(Guid.NewGuid().GetHashCode())))
{
    Parallel.For(0, totalIterations, i =>
    {
        Random localRandom = threadLocalRandom.Value!;

        double x = localRandom.NextDouble();
        double y = localRandom.NextDouble();

        if (x * x + y * y <= 1.0)
        {
            Interlocked.Increment(ref inCircle);
        }
    });
}
Console.WriteLine($"Estimated value of π: {4.0 * inCircle / totalIterations}");
```

Kodu denediğinizdeyse aşağıdaki çalışma zamanı sonuçlarını elde ettiğinizi gözlemliyorsunuz.

```text
Estimated value of π: 3.1415036
Estimated value of π: 3.1419916
Estimated value of π: 3.140818
Estimated value of π: 3.1401364
```

**Monte Carlo** yönteminin doğası rastgele atılan dart oklarının bir çemberin içinde düşüp düşmemesine bağlıdır. Dolayısıyla bu sonuçların elde edilmesi sizi şaşırtmıyor.

- I. **if** bloğunda kullanılan Interlocked.Increment metodu, çoklu iş parçacığı ortamında **inCircle** değişkenine yapılan eşzamanlı erişimlerin neden olabileceği yarış durumlarını *(race condition)* önlemek için kullanılır. Bu sayede doğru sayım yapılması sağlanır.
- II. Kodun performansını artırmak için `ThreadLocal<Random>` kullanılmıştır. Bu sayede her iş parçacığı kendi **Random** örneğine sahip olur ve bu da rastgele sayı üretiminde çakışmaları önler.
- III. İterasyonun paralel çalıştırılabilmesi için **Parallel.For** kullanılmıştır.
- IV. Toplam iterasyon sayısını **long** türünün kapasitesine bağlı olarak artırmak mümkündür.

Bu bilgiler ışığında yukarıda belirtilen ifadelerden hangisi veya hangileri doğrudur?

- A) Yalnızca I ve II ifadeleri doğrudur.
- B) Yalnızca II ve III ifadeleri doğrudur.
- C) Yalnızca I, II ve III ifadeleri doğrudur.
- D) Hepsi doğrudur.

## Soru 7

Kombinasyon hesaplamalarında ve Pascal üçgenlerinde sıkça karşılaşılan hesaplamalar ciddi anlamda recursive *(özyinelemeli)* fonksiyon çağrıları gerektirebilir. Aşağıdaki kod parçasını göz önüne alalım.

```csharp
public int CalcCombination(int n, int r)
{
    if (r == 0 || n == r)
        return 1;
    return CalcCombination(n - 1, r - 1) + CalcCombination(n - 1, r);
}
```

Kod matematiksel olarak doğru olsa da, büyük **n** ve **r** değerleri için çalıştırıldığında ciddi performans sorunlarına yol açar. Ancak aşağıdaki gibi bir kod parçası ile aynı sonucu çok daha hızlı bir şekilde elde edebiliriz.

```csharp
public class Fermat
{
    public static int CalcCombination(int n, int r)
    {
        if (r == 0 || n == r)
            return 1;

        int[,] memo = new int[n + 1, r + 1];
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= Math.Min(i, r); j++)
            {
                if (j == 0 || i == j)
                    memo[i, j] = 1;
                else
                    memo[i, j] = memo[i - 1, j - 1] + memo[i - 1, j];
            }
        }
        return memo[n, r];
    }
}
```

Sizce burada kullanılan teknik nedir ve neden performansı artırır?

- A) Bu teknik Tabulation *(Aşağıdan Yukarıya Dinamik Programlama)* olarak adlandırılır. **Recursive** fonksiyonun getirdiği çağrı yığını *(call stack)* yükünü ortadan kaldırmak için sonuçlar en küçük problemden başlanarak iteratif bir şekilde hesaplanır ve bir tabloya *(memo dizisi)* kaydedilir.
- B) Bu teknik **Static Programming** olarak adlandırılır ve metodun statik olarak tanımlanması sayesinde derleyici tarafından optimize edilerek performans artışı sağlanır.
- C) Bitmasking yapılmıştır. Bu teknik ile kombinasyon hesaplamaları bit düzeyinde optimize edilir ve performans artışı sağlanır.
- D) Bu teknik **Tail Recursion** olarak adlandırılır. Recursive çağrıların son işlem olarak yapılması sağlanır ve derleyici tarafından optimize edilerek performans artışı sağlanır.

## Soru 8

Otostopçunun Galaksi Rehberi *(The Hitchhiker's Guide to the Galaxy)* romanında "Hayat, Evren ve Her Şeye Dair Nihai Sorunun Cevabı", **Deep Thought** adlı süper bilgisayar tarafından tam 7.5 milyon yıllık bir hesaplama ile bulunmuştur.

```csharp
public class DeepThought
{
    public int CalculateUltimateAnswer()
    {
        int answer = 0;
        // 7.5 milyon yıllık simülasyon...
        answer = (int)Math.Pow(6, 2) + 6; 
        return answer;
    }
}
```

Galaksiyi gezmediğinizi düşünürsek bu metodun genel kültürde temsil ettiği nihai sonuç kaçtır?

- A) 0
- B) 3.14159
- C) Infinity
- D) 42

## Soru 9

Uygulamalar sıklıkla log biriktirirler. Bu loglar üretim ortamlarında bu işe özel sistemler tarafından toplanır, analiz edilir ve görselleştirilir. Bu konuda geliştirilmiş birçok standart vardır. Ancak bazen geliştirme ortamlarında ve uygulamaların testleri sırasında bu tip loglar en basit çözüm olarak terminale veya text dosyalara yazdırılır. Örnek dört satırı verilen aşağıdaki içeriğe sahip bir log dosyası oluştuğunu düşünebiliriz.

```text
2024-01-01 12:00:00,000 [INFO] Application started.
2024-01-01 12:00:01,000 [ERROR] An unexpected error occurred.
2024-01-01 12:00:02,000 [WARN] Low disk space.
2024-01-01 12:00:03,000 [INFO] Application stopped.
```

Bir süre sonra **log** dosyasının **100 MB**'ın üzerine çıktığını ve belirli kategorideki logları analiz etmek istediğimizi düşünelim. Aklınıza gelen ilk yöntem dosyayı satır bazında okumak, her satırı parçalamak ve kategori ile ilgili kelimeleri aramak olabilir. Bunun için de **String** sınıfı ile gelen **Substring** ve **Split** metotlarını kullandığınız bir kod parçası geliştirdiğinizi varsayalım. Ne var ki bir süre sonra analiz uygulamasının çok fazla **RAM** tükettiğini ve **Garbage Collector**'ın sürekli devreye girerek uygulamayı yavaşlattığını gözlemliyorsunuz. Burada açıkça görünen bellek tahsisi *(memory allocation)* problemini çözmek ve performansı artırmak için aşağıdaki şıklarda belirtilen yollardan hangisini tercih edersiniz?

- A) Log dosyasını parçalamak yerine **Regular Expressions** kullanarak doğrudan kategori bilgisine erişmeye çalışırım. Böylece gereksiz string parçalama işlemlerinden kaçınarak bellek tahsisi yapmamış olurum.
- B) Dosyayı asenkron okuyarak problemi çözmeye çalışırım. Burada **FileStream** sınıfının **ReadAsync** metodunu kullanarak dosyayı parça parça okuyabilir ve her parçayı analiz edebilirim. Bu sayede bellek kullanımını azaltabilirim.
- C) Metin *(text)* işlemlerini hızlandırmak için **StringBuilder** sınıfını kullanırım. **StringBuilder**, string birleştirme işlemlerinde daha az bellek tahsisi yapar ve performansı artırır.
- D) **string** yerine `Span<char>` ve `ReadOnlySpan<char>` yapılarını kullanırım. Bu yapılar mevcut bellek üzerinde dilimleme *(slicing)* işlemi yapar ve **Heap** üzerinde yeni string nesneleri oluşmasına gerek kalmadan sıfır bellek tahsisiyle *(zero-allocation)* çalışır. Böylece büyük log dosyalarını analiz ederken bellek kullanımını önemli ölçüde azaltabilirim.

## Soru 10

Veritabanında kayıtlı kullanıcıları çeken ve bunun için **Entity Framework** kullanan aşağıdaki kod parçasını göz önüne alalım.

```csharp
IEnumerable<User> users = dbContext.Users.Where(u => u.IsActive);

if(users.Count() > 0){
    foreach(var user in users){
        Console.WriteLine(user.Name);
    }
}
```

Kod çalışıyor olsa da teknik açıdan bir darboğaz *(bottleneck)* oluşturur. Çünkü **users.Count()** ifadesi tüm aktif kullanıcıları veritabanından çekerek sayısını hesaplar ve ardından **foreach** döngüsü tekrar aynı kullanıcıları çekmeye çalışır. Bu **Multiple Enumeration** olarak adlandırılır ve performans sorunlarına yol açabilir. Bu problemi çözmek için aşağıdaki şıklardan hangisi en uygun yaklaşımdır?

- A) **users.Count()** ifadesini **users.Any()** ile değiştirip sadece aktif kullanıcıların var olup olmadığını kontrol eder ve gereksiz yere tüm kullanıcıları çekmemiş olurum.
- B) **ToList()** metodu kullanarak **users** koleksiyonunu bir listeye dönüştürürüm. Böylece veritabanından sadece bir kez çekme işlemi yapılır ve ardından liste üzerinde istediğimiz kadar işlem yapabiliriz.
- C) **foreach** döngüsü yerine **LINQ**'in **.ForEach()** veya **Parallel.ForEach()** metodunu kullanırım, böylece okuma ve yazdırma işlemleri asenkron yapılarak darboğaz oluşması engellenir.
- D) Koleksiyonu belleğe almamak için `IEnumerable<User>` yerine `IQueryable<User>` arayüzünü *(interface)* kullanırım. Böylece veritabanı sorgusunu optimize etmiş oluruz.

## Soru 11

Bilgisayar programcısı **Sir Tony Hoare**, "Milyar Dolarlık Hata(The Billion Dollar Mistake)" olarak adlandırdığı **null** referans kavramını 1965 yılında ortaya atmıştır *(QuickSort algoritması ile de bilinen Hoare, sayısız önemli çalışmasının yanında 1980'de Turing ödülü almış ve ne yazık ki 5 Mart 2026'da 92 yaşında aramızdan ayrılmıştır)* O zamanlar bu kavramın programlama dillerine esneklik ve kolaylık sağlayacağını düşünmüştür. Ancak zamanla **null** referansların neden olduğu hataların *(NullReferenceException)* yazılım geliştirme sürecinde ciddi sorunlara yol açtığı görülmüştür.

Modern C# sürümlerinde *(Özellikle .Net 8 ve sonrasında)*, **Nullable Reference Types** özelliği varsayılan olarak açık gelmektedir. Buna göre aşağıdaki kod parçasını göz önüne alalım.

```csharp
public class User
{
    public string Name { get; set; }
    public string? Email { get; set; }
}

public void PrintUserInfo(User user)
{
    Console.WriteLine($"Name: {user.Name}");
    Console.WriteLine($"Email: {user.Email ?? "Email not provided"}");
}
```

Bu kod parçası ile ilgili olarak aşağıdaki şıklardan hangisi doğru bir teknik tespittir?

- A) **Email** özelliği **string?** ile **nullable** olarak işaretlenmiş ve **?? *(null-coalescing operator)*** kullanılarak olası tüm çalışma zamanı hatalarının önüne geçilmiştir.
- B) Eğer **user.Name** veritabanından **null** olarak gelirse, **Console.WriteLine** metodu *string interpolation* işlemi esnasında bir **NullReferenceException** hatası verecek ve uygulama çökecektir.
- C) Geliştirici property seviyesinde önlem alsa da, **PrintUserInfo** metoduna gönderilen **user** parametresinin kendisinin **null** olup olmadığını kontrol etmemektedir. Metoda **null** bir argüman geçildiğinde **user.Name** çağrısı anında **NullReferenceException** hatası fırlatır. Ayrıca derleyici, **Name** özelliğine ilk değeri atanmadığı için **CS8618** *(Non-nullable property 'Name' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable.)* hatası verecektir.
- D) **Email** özelliği **string?** türünde olduğu için değer tipi *(value type)* gibi davranır. Bu nedenle **??** operatörü yerine **user.Email.HasValue** kullanılarak kontrol edilmelidir, aksi halde kod zaten derlenmez.

## Soru 12

Geliştirmekte olduğunuz sistemde, her gece saat 04:00'te oluşan Gigabyte'lar boyutundaki günsonu döküm verilerini bir veri ambarı *(Data Warehouse)* sistemine aktarmanız gerekmektedir. Bu entegrasyon için aşağıdaki şıklarda yöntem ve hata yönetimi stratejileri verilmiştir. Sizce hangi yöntem ve strateji en uygun olanıdır?

- A) **File Transfer** tercih edilmelidir; ancak iletişim koparsa transferin baştan başlaması veya kaldığı yerden devam etmesi için ek scriptler gerektirebilir *(Düşük/Orta güvenilirlik)*.
- B) **Message Queue** tercih edilmelidir; çünkü büyük veri setleri *(Gigabyte ve üzeri)* için en düşük gecikmeyi sağlar *(Yüksek güvenilirlik)*.
- C) **File Transfer** tercih edilmelidir; teslimat garantisi yüksektir, iletişim kopsa bile mesajlar asla kaybolmaz *(Yüksek güvenilirlik)*.
- D) **API** tercih edilmelidir; timeout mekanizmaları ile büyük dosyalar en hızlı şekilde iletilir *(Uygulama seviyesinde güvenilirlik)*.

## Soru 13

Farklı sistemlerin bir arada çalıştığı senaryolar kurumsal çözümlerde oldukça yaygındır. Ağ problemleri, veri tutarsızlıkları, performans sorunları gibi birçok zorluk ortaya çıkabilir. Ayrıca verinin güvenilir bir şekilde iletilmesi, işlenmesi ve saklanması da önemli bir konudur. Bu bilgileri göz önüne alırsak sizce aşağıdaki şıklardan hangisi **yanlıştır?**

- A) **Message Queue** sistemlerinde mesajların kaybolma riski yoktur veya çok azdır, teslimat garantisi yüksektir.
- B) **Message Queue** yönteminin gecikme *(latency)* süresi sabittir ve hiçbir zaman dakikalar mertebesine çıkmaz.
- C) **API** *(REST/gRPC)* iletişiminde hata yönetimi uygulama seviyesindedir ve timeout, retry gibi mekanizmalarla yönetilir.
- D) **Dosya transferi** *(File Transfer)* güvenilirlik düşük veya orta seviyededir.

## Soru 14

Bir elektronik ticaret platformunuzda **"Black Friday"** gibi çok yoğun trafik alınan kampanya dönemlerinde, faturalama süreçleri için **API haberleşmesi** yerine **Message Queue *(RabbitMQ, Kafka vb.)*** araçlarının tercih edilmesinin temel nedeni aşağıdakilerden hangisidir?

- A) **Message Queue** araçlarının her durumda API'lerden daha düşük *(nanosaniyelik)* ve sabit bir gecikme süresi *(latency)* sunması.
- B) API'lerin **JSON *(JavaScript Object Notation)***, **XML *(Extensible Markup Language)*** veya **Protobuf *(Protocol Buffers)*** gibi veri formatlarını yüksek trafikte işleyeMEmesi.
- C) **Message Queue** sistemlerinin karşı sistemin anlık olarak ayakta olmasını zorunlu kılması *(sıkı bağlılık sağlaması)*.
- D) Anlık talep sayısının çok yüksek olabileceği durumlarda süreçlerin asenkron olarak yönetilebilmesi ve mesaj teslimat garantisi sayesinde veri kaybının önlenmesi.

## Soru 15

Bir bayi otomasyon sisteminde saniyede milyonlarca event'in anlık olarak işlenmesi ve yüksek yanıt sürelerinin sağlanması gerekmektedir. Ölçeklenebilir olması beklenen sisteme dahil olan servisler arasında yoğun ağ gecikmeleri *(Network Latency)* oluşması da **istenememektedir**. Bu senaryoda olduğu gibi aşırı yük ve anlık veri işleme ihtiyacı için yüksek ağ trafiği yaratan mikroservisler yerine aşağıdaki mimarilerden hangisi performans ve ölçeklenebilirlik açısından daha uygun bir çözüm sunar?

- A) Layered *(Katmanlı) Mimari
- B) Mikro Kernel Mimarisi
- C) Service-Oriented Architecture *(SOA)*
- D) Event-Driven Architecture *(EDA)*

## Soru 16

Yaşı on yılın üzerinden olan devasa bir monolit sistemde çalıştığınızı düşünelim. Değişen iş gereksinimleri nedeniyle sistem ihtiyaçlara hızlı yanıt veremez hale gelmiştir. Sistemde sadece belirli iş süreçlerinde *(Kredi onayı, müşteri limit sorglulaması vb)* bağımsız ve hızlı güncellemeler yapabilmeye ihtiyaç vardır. Ancak sistemin geri kalanında büyük çaplı bir refactoring yapmak mümkün değildir. Diğer yandan yeni iş süreçlerini karşılayacak modüllerin kendi veritabanları ile çalışması gerekmektedir. Bu durumda hem iş alanı odaklı bağımsız dağıtılabilir *(independent deployable)* birimler oluşturmak hem de mevcut monolit yapıyı korumak için aşağıdaki mimarilerden hangisini kullanmayı tercih edersiniz?

- A) Layered *(Katmanlı)* Mimari
- B) Microservice *(Mikro servis)* Architecture
- C) Pipeline Architecture
- D) Space Based Architecture

## Soru 17

Çekirdek iş kurallarının dış dünyadan, veri tabanlarından ve kullanıcı arayüzü kütüphanelerinden *(UI Framework)* tamamen izole edilmesi gereken bir yazılım projesinde görev aldığımızı düşünelim. Bu yaklaşım, sistemin çekirdek mantığının altyapı bileşenleri ayağa kaldırılmadan da çok rahat ve hızlı bir şekilde test edilebilir olmasını sağlar. Bu amacı gerçekleştirmek için kullanılması gereken temel mimari tasarım felsefesi aşağıdakilerden hangisidir?

- A) Onion Architecture
- B) Layered *(Katmanlı) Mimari*
- C) Microservice Architecture
- D) Event-Driven Architecture *(EDA)*

## Soru 18

Kullanıcı trafiğinde ani ve çok büyük dalgalanmalar yaşanan *(örneğin kampanya veya toplu indirim uygulanan, reklam çıkılan günlerde vb)* bir e-ticaret platformunda, veri tabanında yaşanan darboğazları *(bottleneck)* aşmak ve sisteme anında yeni kaynaklar ekleyip çıkarabilmek *(elasticity)* birincil stratejik hedef olarak belirlenmiştir. Merkezi bir veri tabanı kullanmak yerine bellek içi veri kümeleri *(in-memory data grids)* kullanarak söz konusu darboğazların aşılabileceği öngörülmektedir. Sizce aşağıdaki mimarilerden hangisi bu hedeflere ulaşmak için en uygun çözümü sunar?

- A) Layered *(Katmanlı) Mimari*
- B) Microservice *(Mikro servis)* Architecture
- C) Pipeline Architecture
- D) Space Based Architecture

## Soru 19

Bir yazılım ekibi yeni bir fikri hızlıca pazara sunmak *(Time to Market)* için basit, başlangıç maliyeti düşük ve geliştirilmesi görece kolay bir mimari çözüm arayışındadır. Ölçeklenebilirlik *(Scalability)* ve yüksek performans şu an için ikincil plandadır. Yazılım mimarilerinin doğası gereği yapısal karmaşıklığı en az ve genel maliyeti *(Overall Cost)* en düşük olan mimari yaklaşımı aşağıdakilerden hangisidir?

- A) Layered *(Katmanlı) Mimari*
- B) Microservice *(Mikro servis)* Architecture
- C) Pipeline Architecture
- D) Event-Driven Architecture *(EDA)*

## Soru 20

Saniyede on binin üzerinde okuma *(Read)* ve sadece birkaç yüz yazma *(Write)* işleminin gerçekleştiği bir uygulama çözümü düşünelim. Aynı veri modeli kullanıldığı için bu model üzerinden hem okuma hem de yazma işlemini yapmak veritabanı tarafında kilitlenmelere veya kullanılan stratejiye göre uzun süre beklemelere neden oluyor. Yazılım mimari ekibi bu sorunu çözmek için veritabanı komutlarının ayrıştırılmasını öneriyor. Hatta bu işlemleri veritabanını ayırarak yapılabileceğini belirtiyor. Buna göre okuma ve yazma işlemlerini birbirinden ayrı veritabanlarında yaparak bu darboğazın aşılabileceği öngörülüyor. Tabii ortada bir başka problem daha var; yazılan verinin sadece okuma amaçlı kullanılan veritabnaına aktarılarak senkronizasyonun sağlanması ve eksik kalan parçaların da bir şekilde tamamlanması gerekiyor. Sizce verinin okuma ve yazma işlemlerinin ayrıştırılması ve senkronizasyonun sağlanması için aşağıdaki şıklardan hangisini tercih edersiniz?

- A) **Strangler Fig *(Strangler Pattern)*** prensibi ile mevcut veritabanını yavaş yavaş okuma ve yazma işlemlerini ayrı veritabanlarına yönlendirecek şekilde refactor ederiz. Senkronizasyon için de bir ETL *(Extract, Transform, Load)* süreci kurarak yazma işlemi gerçekleştiğinde okuma veritabanına gerekli güncellemelerin yapılmasını sağlayabiliriz.
- B) **Active-Record Pattern** prensibi ile her iki veritabanına da aynı anda yazma işlemi yaparız. Okuma işlemi için ise sadece okuma veritabanını kullanırız. Senkronizasyon için de her iki veritabanında aynı anda güncelleme yapılmasını sağlayarak veri tutarlılığını koruruz.
- C) **CQRS (Command Query Responsibility Segregation)** prensibi ile okuma ve yazma işlemlerini birbirinden ayırırız. Senkronizasyon için **Event Bus** veya **Message Broker** sistemlerinden yararlanarak yazma işlemi gerçekleştiğinde okuma veritabanına gerekli güncellemelerin yapılmasını sağlayabiliriz.
- D) Bir **API Gateway** kullanarak okuma ve yazma işlemlerini yönlendiririz. **API Gateway**, gelen istekleri analiz ederek okuma işlemleri için bir veritabanına, yazma işlemleri için ise başka bir veritabanına yönlendirebilir. Senkronizasyon için de **API Gateway** üzerinde bir mekanizma kurarak yazma işlemi gerçekleştiğinde okuma veritabanına gerekli güncellemelerin yapılmasını sağlayabiliriz.

## Cevap Anahtarı

| Soru | Doğru Cevap |
|------|-------------|
| 1    | B           |
| 2    | C           |
| 3    | A           |
| 4    | B           |
| 5    | C           |
| 6    | D           |
| 7    | A           |
| 8    | D           |
| 9    | D           |
| 10   | B           |
| 11   | C           |
| 12   | A           |
| 13   | B           |
| 14   | D           |
| 15   | D           |
| 16   | B           |
| 17   | A           |
| 18   | D           |
| 19   | A           |
| 20   | C           |
