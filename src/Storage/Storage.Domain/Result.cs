namespace Storage.Domain;

/*
    Sınıfımız amacı business metotlardan dönüşlerde dönen veriyi, 
    işlemin başarılı olup olmadığını, hata varsa hata mesajını tek bir yapıda döndürmek.

    Result sınıfı generic bir sınıftır. (<T> yazımına dikkat edelim)
    Buna göre Result sınıfı kullanırken T gördüğümüz yere herhangibir tip(type) verebiliriz.
    int, string, bool ya da kendi yazmış olduğumuz bir tür olabilir.

    Aşağıdaki sınıfta T türü Value özelliğinin tipini temsil eder.
*/
public class Result<T>
    // where ile T türü için bazı kıstaslar belirleyebiliriz (generic constraints)
    // Aşağıdaki kıstılara göre T tipi bir referans türü olmalı ve varsayılan yapıcı metodu (new()) içermelidir.
    //// where T : class, new()
{
    public bool IsSuccess { get; set; }
    public T Value { get; set; } = default!;
    public string ErrorMessage { get; set; } = string.Empty;
}
