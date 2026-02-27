// Aynı konu başlığı altında yer alacak enstrümanları topladığımız isim alanı gibi düşünebiliriz.
namespace HelloWorld;

public class Application
{
    // Programın giriş noktası(Entry Point)
    // Heryerden erişilebilen - public
    // Çalışmak için nesne örneğine ihtiyaç duymayan - static
    // Geriye bir sonuç döndürmeyen - void
    // Parametre almayan bir metot
    public static void Main()
    {
        // Console sınıfının static WriteLine metoduna bir parametre göndererek konsola çıktı basıyoruz
        Console.WriteLine("Hello World!");
    }
}

// Versiyon 0
// Amaç; İş başvurusunda bulunan adayları programatik ortamda temsil edecek bir veri modeli tasarlamak
public class Candidate
{
    // Bu nesne örneğinini oluşturma yöntemleri nelerdir?
    // Validasyonlar nerede nasıl yapılabilir?
    // Farklı veri yapıları ile ilişkilendirilebilir mi?
    public string FirstName { get; set; }
    public string MidName { get; set; }
    public string LastName { get; set; }
    // public string FullName { get; set; }
    public DateTime BirthDate { get; set; } // Geçerli bir doğum tarihi bilgisi girilip girilmediğini nasıl anlarız?
    public bool IsActive { get; set; } = true; // Aktif olup olmama bilgisi burada mı durmalı yoksa başka bir yerde mi?
    public string Graduate { get; set; } // Lisans, Yüksek Lisans, Doktora, Prof vb bazı bilinen bilgiler
                                         // string olarak mı tutulmalı yoksa daha tutarlı bir veri yapısı mı olmalı?

    // Hangi bölümden mezun?
    // Hangi okuldan mezun?

    // E-posta adresiniz nedir? (Burada yazıp da aklımıza gelmeyen iletişim bilgilerini nasıl tarifleriz?
    // Birden fazla e-posta adresi varsa ya da telefonu? Hangisi ilk iletişim tercihi?)
    // Telefon numaranız nedir?
    // Posta adresiniz nedir?

    // Kaç yıldır bu işle uğraşıyorsunuz? 
    // (Adaydan mı alınmalı, girilen tecrübe bilgilerine göre otomatik olarak mı hesaplanmlı?)

    // İlgi alanlarınız neler?
    // Önceki deneyimleriniz? (Kaç deneyim girilebilir?)
    // Bildiğiniz yabancı diller? (Skorlaması nasıl girilir?)
    // Hobileriniz neler?
    // Yapmış olduğunu projeler?
    // Referanslarınız?
    // Vesikalık fotoğrafınız 
    // (Bir fotoğrafı nesnel olarak nasıl saklarım? Sistemde nasıl dolaştırırım. Nasıl depolarım? Nerede depolarım?)
    // Gelecek vizyonunuz
}
