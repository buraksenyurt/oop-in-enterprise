namespace HumanResources.Domain;

/*
 public : heryerden erişilebilir. Bu library içerisindeki her yerden veya bu library'yi referans eden
 diğer projelerden erişilebilir.

 internal: Sadece bu library içerisindeki üyelerden erişilebilir, dışarıdan erişilemez.
 protected, protected internal, private protected gibi erişim belirleyicileri de (access modifier) vardır.
*/

/*
    Mail Informations:

    burakselimsenyurt@gmail.com personal
    burakselim.senyurt@d-teknoloji.com.tr company

    Phone Informations:

    0555 555 55 55 mobile, (Primary)
    0212 555 55 55 job

    Physical Addresses:

    İstanbul, Türkiye, Çıkmaz Sokak ...
    Maslak, Büyükdere Caddesi, No: 1010101010, İstanbul, Türkiye
*/
public class EMail
{
    //todo: Tamamlanacak
}
public class Phone
{
    //todo: Tamamlanacak
}
public class PhysicalAddress
{
    //todo: Tamamlanacak
}
public class ContactInformation
{
    public List<EMail> Emails { get; set; } = [];
    public List<Phone> Phones { get; set; } = [];
    public List<PhysicalAddress> Addresses { get; set; }=  [];
}
