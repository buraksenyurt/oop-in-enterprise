namespace Fundamentals;

/*
    Aşağıdaki örnek kod çalışıyor ancak yazılım mühendisliği prensipleri açısından baktığımızda,
    bazı kusurları var ve daha iyi bir tasarıma gidilebilir.

    Problemler:

    1. AlertManager sınıfı NotificationService'e doğrudan bağımlıdır. 
    Bir başka deyişle, AlertManager sınıfının NotificationService'in nasıl çalıştığını bilmesi gerekir. 
    Eğer NotificationService'in iç yapısı değişirse AlertManager nesnesi de bundan etkilenebilir.

    2. AlertManager sınıfı oyuncuların listesini doğrudan yönetmektedir. 
    Bu, AlertManager'ın oyuncuların nasıl yönetileceği konusunda sorumluluk sahibi olduğu anlamına gelir, 
    bu da tek bir sınıfın birden fazla sorumluluğu olduğu anlamına gelir (Single Responsibility prensibi ihlal edilir).

    3. AlertManager sınıfı mesaj gönderme yöntemini (SendAlert) doğrudan kullanmaktadır. 
    Eğer AlertManager'ın mesaj gönderme yöntemini değiştirmesi gerekirse, 
    SendAlert yöntemini de değiştirmesi gerekir ki bu da Open/Closed prensibinin ihlali anlamına gelir.

    Özellikle 1nci ve 2nci problemler, AlertManager'ın NotificationService'e sıkı (tightly-coupled) bir şekilde bağlı olduğunu gösterir.
    Bu, kodun bakımını zorlaştırır ve değişiklik yaparken daha fazla hata yapma riskini artırır.
    Dolayısıyla ilk iki problemi çözmek için Dependency Injection prensibini uygulamak iyi bir çözüm olabilir.

    !!! Çözümü sonraki derste ele alacağız !!!
*/
public class DependencyManagements
{
    public static void Run()
    {
        var mario = new Player
        {
            Id = Guid.NewGuid(),
            Nickname = "Super Maryoo",
            Email = "mario@example.com",
            PhoneNumber = "123-456-7890"
        };
        var alertManager = new AlertManager();
        alertManager.players.Add(mario);
        alertManager.players.Add(new Player
        {
            Id = Guid.NewGuid(),
            Nickname = "Luigi",
            Email = "luigi@example.com",
            PhoneNumber = "098-765-4321"
        });
        alertManager.SendAlert("Server maintenance at midnight.", "Text");
    }
}

// Belirli kriterlere uyan oyunculara toplu mesaj gönderimi yapmak için kullandığımız sınıf
public class AlertManager
{
    private readonly NotificationService notificationService = new();

    public List<Player> players = [];

    public void SendAlert(string message, string target)
    {
        foreach (var player in players)
        {
            notificationService.Send(player, message, target);
        }
    }
}
public class NotificationService
{
    public void Send(Player player, string message, string target)
    {
        if (target == "Console")
            Console.WriteLine($"Notification sent to {player.Nickname}: {message}");
        else if (target == "Text")
            Console.WriteLine($"Text message sent to {player.PhoneNumber}: {message}");
        else if (target == "Email")
            Console.WriteLine($"Email sent to {player.Email}: {message}");
    }
}
public class Player
{
    public Guid Id { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
