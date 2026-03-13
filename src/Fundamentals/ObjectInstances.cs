namespace Fundamentals;

public enum GameCategory
{
    Action,
    Adventure,
    RougeLike,
    Simulation,
    Strategy
}
public class Game
{
    // Bu Game sınıfı içerisinde yer alacak verileri aşağıdaki alanlar (Fields) ile tanımlayabiliriz.
    public int Id;
    public string Title;
    public short ReleaseYear;
    public GameCategory Category;
    public string Summary;
    public float Rating;
    // public char Flag; // 'A', 'X', 'B' gibi tek karakterlik bilgileri tutmak için char türünden bir alan tanımlayabiliriz.

    /*
        Bir veri yapısı kendi içerisinde iş yapan fonksiyonellikler (metotlar) barındırabilir.
        Örneğin aşağıdaki gibi bir oyub nesne örneğine ait bilgileri string olarak geriye belirli bir formatta döndüren bir metot kullanabiliriz.
    */
    public string GetInfo()
    {
        return $"[{Id}]-{Title} ({ReleaseYear}) - {Category} - Rating: {Rating}/10";
        /*
            Zihin yakan sorular bırakalım;

            Oyun bilgileri kimi ortamlarda JSON - JavaScript Object Notation formatında yazdırılacak 
            ya da kimi ortamlarda XML - eXtensible markup language formatında yazdırılacak.

            {
                "Id": 1,
                "Title": "Prince of Persia: The Sands of Time",
                "ReleaseYear": 2003,
                "Category": "Action",
                "Rating": 7.8
            }

            xml formatında yazdırılacak olsaydı;

            <Game>
                <id>1</id>
                <title>Prince of Persia: The Sands of Time</title>
                <releaseYear>2003</releaseYear>
                <category>Action</category>
                <rating>7.8</rating>
                <summary>The Prince of Persia: The Sands of Time is an action-adventure game developed by Ubisoft.</summary>
            </Game>

            hatta CSV - Comma Seperated Values formatında yazdırılacak olsaydı;

            1001|Prince of Persia: The Sands of Time|2003|Action|7.8|The Prince of Persia: The Sands of Time is an action-adventure game developed by Ubisoft.
         
            Peki, tüm bu formatları Game sınıfından bu metodun içerisinde mi ele alamlıyız (bir sürü if-else, switch-case bloklarıyla) 
            ya da bu sorumluluk başka bir sınıfta mı olmalı? Peki, yeni bir format eklemek istersek ne olacak?

            Bu sorularda genelikle sorumluluğun stratejiler üzerinden yönetilebileceği, kodu değiştirmeden bu stratejiler ile davranışın farklılaştırılabileceği
            tasarımlar ele alınmaya çalışır.
         */
    }
}
public static class ObjectInstances
{
    public static void Run()
    {
        /*
            Game sınıfından bir nesne (object instance) oluşturmak için kullanabileceğimiz yollardan sadece birisi.
            Nesneyi tanımlamak ve sonrasında alanlarına değerlerini atamak.
        */

        var princeOfPersia = new Game();
        princeOfPersia.Id = 1;
        princeOfPersia.Title = "Prince of Persia: The Sands of Time";
        princeOfPersia.ReleaseYear = 2003;
        princeOfPersia.Rating = 7.8F; // 7.8 varsayılan olarak double kabul edilir. Rating ise float türündendir. Dolayısıya açıkça float olduğunu ifade etmemiz gerekiyor.
        // Bu nedenle F harfi ile açıkça 7.8 değerinin float türünden(System.Single) ele alınması gerektiğini belirttik.
        princeOfPersia.Category = GameCategory.Action;
        princeOfPersia.Summary = "The Prince of Persia: The Sands of Time is an action-adventure game developed by Ubisoft.";


        // Bir başka nesne örneği oluşturma şeklide aşağıdaki gibidir.
        // Yani nesne tanımlanırken süslü parantezler içerisinde alanlara ilk değerler atanabilir.
        var redAlert = new Game
        {
            Id = 1002,
            Title = "Command & Conquer: Red Alert II",
            ReleaseYear = 1998,
            Rating = 9.2F,
            Category = GameCategory.Strategy,
            Summary = "Command & Conquer: Red Alert II is a real-time strategy game developed by Westwood Studios."
        };

        var superMario = new Game
        {
            Id = 2002,
            Title = "Super Mario Bros.",
            Category = GameCategory.Adventure,
        };
        superMario.ReleaseYear = 1985;

        /*
            inverntory isimli değişken Game nesne örneklerinden oluşan bir dizi (array).
            Dizi elemanlarını dışarıda tanımlanmış olan Game örneklerinden oluşturabileceğimiz gibi, sentaks içerisinde de yeni bir Game nesnesi örnekleyebiliriz.
         */
        var inventory = new Game[]
        {
            princeOfPersia,
            redAlert,
            superMario,
            new() { 
            // Dizinin kendisi sadece Game türünden nesnelerden oluştuğu için burada yeni bir Game bileşene oluşturulacağı zaten aşikar. O neden new() operatörü de kullanılabilir.
                Id = 3002,
                Title = "The Legend of Zelda: Breath of the Wild",
                ReleaseYear = 2017,
                Rating = 9.5F,
                Category = GameCategory.Adventure,
                Summary = "The Legend of Zelda: Breath of the Wild is an action-adventure game developed by Nintendo."
            },
            new Game {
                Id = 3003,
                Title = "Hollow Knight",
                ReleaseYear = 2017,
                Rating = 8.9F,
                Category = GameCategory.RougeLike,
                Summary = "Hollow Knight is a Metroidvania action-adventure game developed by Team Cherry."
            }
        };

        /*
         Fonksiyonel dil paradigmasına benzer şekilde kurgular da sözkonu.
         Örneğin Where metodu, parametre olarak isimsiz(anonymous) kod bloğu alır,
         sonuç bir listeye çıkar(ToList()) ve her bir elamanı için farklı bir kod bloğu çalıştıran ForEach metodu kullanılır.
         */
        inventory
            .Where(g => g.ReleaseYear > 2000)
            .ToList()
            .ForEach(g => Console.WriteLine(g.GetInfo()));

        //// inventory isimli dizinin elemalarını bir for döngüsü ile dolaşabiliriz. for döngüsü olmak zorunda değil.
        //// while, do while, foreach veya Higher Order Functions (LINQ) gibi diğer döngü türleri de kullanılabilir.
        //for (var i = 0; i < inventory.Length; i++)
        //{
        //    Console.WriteLine($"{inventory[i].Title.ToUpper()}");
        //}
        //// inventory gibi üzerinde ileri yönlü iterasyonlar ile işlemler yapacağımız durumlarda
        //// genelikle aşağıdaki gibi foreach döngüleri tercih edilebilir.
        //foreach (var game in inventory)
        //{
        //    Console.WriteLine($"{game.Title.ToUpper()} - ({game.ReleaseYear})");
        //}
        PrintGames(inventory);

        // invertory, Game türünden bir dizi. Peki buna yeni bir oyun bilgisi eklemek istersek...
        // Append metodu aslında yeni bir dizi oluşturup yeni oyunu o diziye ekler.
        var extendedGames = inventory.Append(new Game
        {
            Id = 4001,
            Title = "Horizon Zero Dawn",
            ReleaseYear = 2017,
            Category = GameCategory.Simulation,
            Rating = 8.7F,
            Summary = "Horizon Zero Dawn is an action role-playing game developed by Guerrilla Games."
        });

        /* 
            Extend ettiğimiz inventory içeriğini yeniden yazdırma ihtiyacımız olduğunu düşünelim.
            Yukarıdaki for döngüsünün aynısını tekrar yazmak yerine bunu ele alan ayrı bir metod ile kod tekrarını engelleyebiliriz.
        */
        //foreach (var game in inventory)
        //{
        //    Console.WriteLine($"{game.Title.ToUpper()} - ({game.ReleaseYear})");
        //}
        // extendedGames aslında IEnumerable<Game> türünden bir koleksiyondur.
        // PrintGames metodu ise, Game türünden bir dizi ile çalışır. O yünzden ToArray metodu ile IEnumerable<Game> türünden Game[] türüne bir dönüşüm yapılır.
        // PrintGames(extendedGames.ToArray());
        // Aynı ifadeyi aşağıdaki gibi de yazabiliriz.
        PrintGames([.. extendedGames]);
    }

    /*
        PrintGames herhangibir erişim belirleyicisi(access modifier) içermiyor, dolayısıyla sadece tanımlandığını sınıf içerisinde kullanılabilir.
        Tek bir parametre alıyor ki o da Game türünden elemanlar içeren bir dizi.
        Geriye herhangibir değer döndürmüyor, bu nedenle void olarak tanımlandı.
     */
    static void PrintGames(Game[] games)
    {
        foreach (var game in games)
        {
            /*
                Şu an için oyunların sadece Title ve ReleaseYear bilgisi ekrana yazdırılıyor.
                Aslında bir oyunla alakalı bilgileri yazdırmak için kullanılan ortam değişebilir. 
                Konsola yazdırabileceğimiz gibi, bir text dosya içerisine de yazdırılabilir.
                Şu durumda hangi bilgilerin nereye yazılacağına PrintGames metodundaki for döngüsünde karar veriyoruz.
                Bu çok tercih ettiğimiz bir yöntem değil. Belki de oyun hakkındaki bilgileri geriye dönen bir başka metodu
                game türü içerisinde oluşturmalıyız.
            */
            // Console.WriteLine($"{game.Title.ToUpper()} - ({game.ReleaseYear})");
            Console.WriteLine(game.GetInfo());
        }
    }
}