# OOP Test Soruları 02 [DRAFT]

Bu testin amacı matematiksel bazı kavramların programatik ortamda modellenmesi, veri yapıları ve algoritma performansı etkilerinin ölçülmesine yöneliktir.

*Not: Sorular .Net 10 sürümü üzerinden hazırlanmıştır.*

---

## Soru 1

Bir algoritmanın performansını değerlendirmek için kullanılan **BigO** notasyonu, algoritmanın çalışma zamanının veya bellek kullanımının girdi boyutuna göre nasıl değiştiğini ifade eden bir enstrümandır. Farklı türleri vardır. Örneğin, **O(1)**, **O(n)**, **O(n^2)** gibi. **O(1)**, algoritmanın çalışma zamanının girdi boyutundan bağımsız olduğunu ifade ederken, **O(n)** algoritmanın çalışma zamanının girdi boyutuna doğrusal olarak arttığını ifade eder. **O(n^2)** ise algoritmanın çalışma zamanının girdi boyutunun karesiyle arttığını ifade eder. Bu notasyon, algoritmaların verimliliğini karşılaştırmak ve büyük veri setleriyle çalışırken hangi algoritmanın daha uygun olduğunu belirlemek için kullanılır.

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
- D) Komplek sayıların gerçel ve sanal kökü için `Tuple<double, double>` kullanmak en uygun yaklaşımdır çünkü bu sayede iki değeri tek bir veri yapısında tutabiliriz.

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
- D) Algoritmanın çalışma sırasında oluşan değerleri bir **log** dosyasına yazdırırım. Böylece algoritmanın doğruluğunu manuel olarak da kontrol edebiliriz.
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

**Monte carlo** yönteminin doğası rastgele atılan dart oklarının bir çemberin içinde düşüp düşmemesine bağlıdır. Dolayısıyla bu sonuçların elde edilmesi sizi şaşırtmıyor.

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

- A) Bu teknik **Memoization** olarak adlandırılır. Recursive fonksiyonun her çağrısında aynı hesaplamaların tekrar yapılmasını önlemek için ara sonuçlar bir tabloya kaydedilir. Böylece büyük **n** ve **r** değerleri için bile hızlı sonuç alınır.
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

DEVAM EDECEK...
