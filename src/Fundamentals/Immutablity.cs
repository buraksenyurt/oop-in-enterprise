namespace Fundamentals;

public class Immutablity
{
    public static void Run()
    {
        var hugo = new Character("Hugo", BasicColor.Green);
        //hugo.Name = "Super Mario"; // readonly olduğundan değiştirilemez, hata verir.
        //hugo.FavoriteColor = BasicColor.Red; // readonly olduğundan değiştirilemez, hata verir.
        Console.WriteLine($"{hugo.Name}'s favorite color is {hugo.FavoriteColor}");

        var maria = new CharacterV2("Maria", BasicColor.Red); // record türüde immutable olma halini bu senaryo karşılıyor.
        //maria.Name = "Super mario";
        //maria.FavoriteColor = BasicColor.Green;
        Console.WriteLine($"{maria.Name}'s favorite color is {maria.FavoriteColor}");

        var aleksa = new CharacterV3("Aleksa", BasicColor.Blue);
        //aleksa.Name = "Super Mario"; // readonly struct olduğu için hata verir, değiştirilemez.
        //aleksa.FavoriteColor = BasicColor.Green; // readonly struct olduğu için hata verir, değiştirilemez.
        Console.WriteLine($"{aleksa.Name}'s favorite color is {aleksa.FavoriteColor}");

        var id = Guid.NewGuid();
        Console.WriteLine($"Generated ID: {id}");

        //// Score özelliği Object türünden olduğu için aşağıdaki gibi kullanılabilir ama bunu asla tercih etmeyiz.
        //// Çünkü veri türü tutarlılığı ortadan kalkar. Yani bool gibi değer de atayabilirsiniz.
        //var burak = new GraduateV1 { Score = 85 }; 
        //var ayse = new GraduateV1 { Score = 3.4 };
        //var veli = new GraduateV1 { Score = "A+" };
        //var mehmet = new GraduateV1 { Score = true };

        //// Score özelliği string türünden olduğu için aşağıdaki gibi kullanılabilir ama bu da pek tercih edilmez. 
        //// Çünkü veri tutarlılığı yine sağlanamaz. Sayısal olması gereken ifadeler bile metinsel olarak dolaşır
        //// ve yine bool, char gibi alakasız türler de kullanılabilir.
        //var grad1= new GraduateV2 { Score = "85" };
        //var grad2= new GraduateV2 { Score = "3.4" };
        //var grad3= new GraduateV2 { Score = "A+" };

        // Yine de mükemmel değil ama diğer seçenekler göre en iyisi. 
        // Score özelliği Score türünden olduğu için aşağıdaki gibi kullanılabilir ve veri tutarlılığı sağlanır.
        var grad4 = new Graduate { Score = new Score(100, Notation.Numeric100) };
        var grad5 = new Graduate { Score = new Score("A+", Notation.Letter) };
        var grad6 = new Graduate { Score = new Score(3.4, Notation.Numeric4) };

        //todo@buraksenyurt En ideal yaklaşımı da uygulayalım. Belki bir arayüz türetmesi ile ilerlenebilir.
    }
}

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

public class GraduateV1
{
    // object keyword ile belirtilen tür Object tipidir. .Net'te herkes bir Object'tir. En genel türdür. Yani herkesi taşıyabilir.
    public object Score { get; set; }
}

public class GraduateV2
{
    public string Score { get; set; }
}

// Bu kullanım şekillerinin hepsi immutable nesne oluşturmamıza olanak sağlıyor. Peki,
// neden class, neden readonly struct, neden record veya ne zaman hangisi?

public readonly struct CharacterV3
{
    public string Name { get; }
    public BasicColor FavoriteColor { get; }

    public CharacterV3(string name, BasicColor favoriteColor)
    {
        Name = name;
        FavoriteColor = favoriteColor;
    }
}

public record CharacterV2(string Name, BasicColor FavoriteColor); // record türünde, belirtilen tüm alanlara ilk değerleri atamak gerekir(nesne örneğini hazırlarken)

public class Character
{
    // Character sınıfının Name ve FavoriteColor property'leri sadece okunabilir (get) olarak tanımlanmıştır.
    // Bu sebepten bir Character nesne örneği oluşturulduktan sonra, bu özellikler ilk değerlerini alır ama sonradan değiştirilemez.
    // Böylece nesnenin immutable(değiştirilemez) olmasını sağlarız.
    public string Name { get; }
    public BasicColor FavoriteColor { get; }

    public Character(string name, BasicColor favoriteColor)
    {
        Name = name;
        FavoriteColor = favoriteColor;
    }
}
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
