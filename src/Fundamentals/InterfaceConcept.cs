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

        /*
            Sisteme yeni bir bildirim yöntemi eklediğimizi düşünelim. (Örneğin, EmailNotificationService gibi)
            Bunu kullanarak mesaj göndermek de oldukça kolaydır.
            Aşağıdaki çağrımda ilk argüman olarak yeni bir EmailNotificationService nesne örneği oluşturup gönderdik.
            Bunu yaparken, SendNotification metoduna müdahele etmediğimize, içerisinde bir if veya switch-case
            bloğu kullanmadığımıza dikkat edelim.
        */
        SendNotification(new EmailNotificationService(), "Hello, this is a test message!");

        /*
         Kendi sistemimizdeki QueueNotificationService bileşeni, RabbitMQ isimli bir asenkron
         kuyruk sistemi ile çalışır. Bu bileşenin oluşturulması sırasında ilgili bağımlılık
         QueueNotificationService constructor'ına enjekte edilir. Bu sayede, SendNotification metoduna
         herhangi bir müdahele yapmadan, bu yeni bildirim bileşenini de kullanabiliriz.         
        */
        var queueNotifyService = new QueueNotificationService(
            new RabbitMQ.Client.ConnectionFactory().CreateConnectionAsync().GetAwaiter().GetResult());
        SendNotification(queueNotifyService, "Hello, this is a test message!");
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

    Interface türleri polimorfik enstrümanlardır. Yani, kendisini implemente eden asıl nesnelere bürünüp çalışırlar.
    Bknz: Polymorphism (Polimorfizm) kavramına bakmakta yarar var.
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
        Console.WriteLine($"SMS Sending: `{message}`");
    }
}

public class EmailNotificationService
    : INotificationService
{
    public void Notify(string message)
    {
        // Email gönderme işlemleri burada yapılır
        Console.WriteLine($"Email Sending: `{message}`");
    }
}

public class QueueNotificationService
    : INotificationService
{
    // RabbitMQ ile çalışacak bir bileşenemiz var.
    // RabbitMQ nesnesi de Constructor üzerinden bu bileşene enjekte ediliyor.
    protected readonly RabbitMQ.Client.IConnection connection;

    public QueueNotificationService(RabbitMQ.Client.IConnection connection) // Constructor Injection (DI) tekniği ile RabbitMQ bağlantısı enjekte ediliyor.
    {
        this.connection = connection;
    }

    // Gerçek hayat senaryosunda kuyruk sistemini kullanan bileşen de buraya
    // dışarıdan bir bağımlılık olarak yine Interface'ler üzerinden enjekte edilebilir.
    public void Notify(string message)
    {
        // Burada RabbitMQ.Client kütüphanesini kullanarak mesaj kuyruğuna bağlanıp,
        // mesajı kuyruğa ekleme işlemi yapılır.

        // Mesaj kuyruğuna ekleme işlemleri burada yapılır
        Console.WriteLine($"Queueing Message: `{message}`");
    }
}


/*
 Çalışma zamanı (Runtime) olan bir sistem, Notify davranışını kullanıyorsa, bu sisteme entegre olan başka parçalar
 kendi INotificationService türevlerini yazarak, sisteme yeni bildirim yöntemleri ekleyebilirler.

 Interface kullanımı bir sistemi, koduna müdahale etmeden yeni kabiliyetlerle genişletebilmenin bir yolunu da sunar.
*/