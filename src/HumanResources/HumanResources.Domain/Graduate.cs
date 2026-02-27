namespace HumanResources.Domain;

/*
    Örnek veriler;

    Lisans
    Y.T.Ü.
    Matematik Mühendisliği
    1999
    3.5 / C+

    Yüksek Lisans
    Doğuş Üniversitesi
    İşletme
    2001
    3.2 / C / 70

    Lise...
*/

public enum GraduateKind
{
    HighSchool,
    University,
    PostGraduate
}

public class Graduate
{
    public GraduateKind Kind { get; set; }
    public required string Title { get; set; } // required eklediğimiz için Title ve Department bilgileri mutlaka girilmek zorunda
    public required string Department { get; set; }
    public bool IsActive { get; set; }
    public short Year { get; set; } // Mezuniyet yılının anlamlı olması için makul bir değere sahip olması gerekir. M.Ö. olmaz mesela.
    public float Score { get; set; } // Kimi okulun mezuniyet puanı sayısal kimisinin alfabetik olabilir. Nasıl yönetebiliriz?
    // 100 üstünden 70, C+, 3.4 gibi farklı şekillerde tutulabilir.
    // Şöyle bir fonksiyonellik ihtiyacımız olsun; 100 üstünden 70 giriliyor ama istersek C olarak gösterebiliriz.
    public List<Graduate> Graduates { get; set; } = [];
    public ContactInformation ContactInformation { get; set; }
}
