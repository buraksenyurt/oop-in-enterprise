namespace Fundamentals;

public class InterfaceApplication
{
    public static void Run()
    {
        // SendSmsToSomewhere("555-123-4567", "Hello, this is a test message!");

        // Eşitliğin sol tarafı bir Interface türü.
        // Sağ tarafı ise ondan türetilmiş(Inherited) olan asıl sınıfın bir örneği (Concrete Class)
        INotificationService notificationService = new SmsNotificationService();
        // ve bu atanabilme özelliği sebebiyle SendNotification metoduna gönderilebiliyor.
        SendNotification(notificationService, "Hello, this is a test message!");
    }

    /*
        SendNotification metodunun çalıştığı ortamını bir runtime olduğunu düşünelim.
        Metodun ilk parametresi INotificationService türünden bir nesne alıyor.
        Interface veya Abstract Class gibi türler, kendisinden türetilen nesne örneklerini taşıyabilirler.

        Sistem çalışma zamanında ne tür bir bildirim bileşeni kullanacağını bilmek zorunda değildir.
        Onun için, INotificationService arayüzünü implemente eden bir bileşen verilmesi yeterlidir.
    */
    public static void SendNotification(INotificationService notificationService, string message)
    {
        notificationService.Notify(message);
    }

    //public static void SendSmsToSomewhere(string phoneNumber, string message)
    //{
    //    var smsService = new SmsNotificationService();
    //    smsService.Notify(message);
    //}
}

/*
    Özellikle bağımlılıkların (Dependencies) yönetiminde Interface ve Abstract Class'lar sıklıkla kullanılır.
    Interface'ler davranışları tanımlayan birer sözleşeme (contract) olarak düşünülebilir.
    Bir interface herhangi bir tipe uygulandığında, bu tipin mutlaka yapması gereken davranışları (behaviors) tanımlar.

    Aşağıdaki arayüz Notify isimli bir metod tanımlar. Metodun belli bir parametre yapısı vardır.
    Geriye bir şey döndürmeyen (void) string türden parametre alan bir metotdur. 
     - message: Gönderilecek mesajın içeriği

    Bütün interface'ler isimlendirme standardı gereği "I" harfi ile başlar.
*/
public interface INotificationService
{
    void Notify(string message);
}

/*
    Sistemin gönderim yapabilme yeteneğine sahip olan ve Sms yoluyla bildirim yapma davranışını uygulayan bir bileşen yazdık.
*/
public class SmsNotificationService
    : INotificationService
{
    public void Notify(string message)
    {
        // SMS gönderme işlemleri burada yapılır
    }
}


/*
 Çalışma zamanı (Runtime) olan bir sistem, Notify davranışını kullanıyorsa, bu sisteme entegre olan başka parçalar
 kendi INotificationService türevlerini yazarak, sisteme yeni bildirim yöntemleri ekleyebilirler.

 Interface kullanımı bir sistemi, koduna müdahale etmeden yeni kabiliyetlerle genişletebilmenin bir yolunu da sunar.
*/