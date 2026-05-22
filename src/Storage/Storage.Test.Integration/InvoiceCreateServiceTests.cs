using Microsoft.Extensions.Logging;
using Moq;
using Storage.Business;
using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Test.Integration;

public class InvoiceCreateServiceTests
{
    // Aşağıdaki birim test metodu sadece Success olma durumunu test ediyor. Olası tüm senaryolar için ayrı ayrı birim testler yazmak gerekir.
    [Fact]
    public async Task SaveInvoice_ValidInvoice_ReturnsSuccessResult()
    {
        // Arrange
        decimal totalAmount = 100.00m;
        byte[] eInvoiceContent = [0x25, 0x50, 0x44, 0x46];

        /*
            InvoiceCreateService sınıfının CreateAsync metodunu test ediyoruz.
            Bu sınıfın ihtiyaç duyduğu iki bağımlılık (Dependency) var: IStorageService ve ILogger<InvoiceCreateService>.
            Buradaki testin amacı CreateAsync'in doğru bir şekilde çalıştığını doğrulamak. Bu nedenle gerçekten bir AWS S3 ortamına
            veya Serilog üzerinden Graylog gibi bir loglama sistemine gitmesine gerek yok.
            Zira, proje testleri çoğunlukla CI/CD hatlarında çalışır ve bu hatlarda dış entegrasyon noktaları kapalıdır.

            Bu yüzden IStorageService veya ILogger ın yerine geçecek, onların davranışını taklit edecek mock nesneler kullanarak hareket ederiz.
            Neyse ki InvoiceCreateService'in tüm bağımlılıkları bir Interface ile enjekte edilmekte. (Interface kullanmanın bir faydası daha)

            NOT: Aşağıdaki gibi interface türevlerini yazmak yerine sadece mock amaçlı kullanımlarda özelleşmiş kütüphaneler de kullanılabilir.
            Bknz: Moq, NSubstitute, FakeItEasy
        */

        //InvoiceCreateService service = new InvoiceCreateService(
        //    new AwsS3StorageService()
        //    , new Logger<InvoiceCreateService>(new LoggerFactory()));
        var service = new InvoiceCreateService(
            new MockStorageService() // mock nesneleri verdik
            , new MockLogger());

        // Act
        var actual = await service.CreateAsync(totalAmount, eInvoiceContent);
        var expected = new Result<Guid>
        {
            IsSuccess = true,
            Value = actual.Value
        };

        // Assert
        Assert.True(actual.IsSuccess);
        Assert.Equal(expected.Value, actual.Value);
    }

    /*
        Yukarıdaki testin aynısını Moq kütüphanesini kullanarak yazalım.
    */
    [Fact]
    public async Task SaveInvoice_ValidInvoice_ReturnsSuccessResult_WithMoq()
    {
        // Arrange
        decimal totalAmount = 100.00m;
        byte[] eInvoiceContent = [0x25, 0x50, 0x44, 0x46];
        // IStorageService implementasyonu için bir mock nesne oluşturuluyor.
        var mockStorageService = new Mock<IStorageService>();
        // IStorageService'in SaveAsync metodunun bu test metodu özelinde CompletedTask döndürülmesi sağlanıyor
        mockStorageService.Setup(s => s.SaveAsync(It.IsAny<Asset>())).Returns(Task.CompletedTask);
        // Loglama işini de mocklamak gerekiyor, çünkü gerçek bir logger kullanmak istemiyoruz.
        // ILogger<InvoiceCreateService> için bir mock nesne oluşturuluyor.
        var mockLogger = new Mock<ILogger<InvoiceCreateService>>();
        var service = new InvoiceCreateService(
            mockStorageService.Object
            , mockLogger.Object);

        // Act
        var actual = await service.CreateAsync(totalAmount, eInvoiceContent);
        var expected = new Result<Guid>
        {
            IsSuccess = true,
            Value = actual.Value
        };

        // Assert
        Assert.True(actual.IsSuccess);
        Assert.Equal(expected.Value, actual.Value);
    }

    /*
        InvoiceCreateService'in CreateAsync metodunun, IStorageService'in SaveAsync metodunun 
        bir hata fırlatması durumunda nasıl davrandığını test eden birim test metodu.
    */
    [Fact]
    public async Task SaveInvoice_StorageServiceThrowsException_ReturnsFailureResult()
    {
        // Arrange
        decimal totalAmount = 100.00m;
        byte[] eInvoiceContent = [0x25, 0x50, 0x44, 0x46];
        var mockStorageService = new Mock<IStorageService>();

        // Bu sefer SaveAsync metodunun bir hata fırlatması sağlanıyor.
        mockStorageService.Setup(s => s.SaveAsync(It.IsAny<Asset>())).ThrowsAsync(new Exception("Storage error"));
        var mockLogger = new Mock<ILogger<InvoiceCreateService>>();
        var service = new InvoiceCreateService(
            mockStorageService.Object
            , mockLogger.Object);

        // Act
        var actual = await service.CreateAsync(totalAmount, eInvoiceContent);

        // Assert
        Assert.False(actual.IsSuccess);
        Assert.Equal("Storage error", actual.ErrorMessage);
    }
}

// Bu sınıf, IStorageService'i uygular, gerçekten bir depolama hizmetine erişmez, sadece geriye işlemin başarılı yapıldığına dair sonuçlar döner.
// Unit Test'lerde Mock servisleri yazarken tasarımın çarpık tarafları da ortaya çıkar ve bazı düzenlemeler yapılabilir(refactor)
public class MockStorageService : IStorageService
{
    public Task<byte[]> LoadAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Asset asset)
    {
        return Task.CompletedTask;
    }
}

public class MockLogger : ILogger<InvoiceCreateService>
{
    public IDisposable BeginScope<TState>(TState state) => null;
    public bool IsEnabled(LogLevel logLevel) => true;
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        // Loglama işlemi burada yapılmaz, sadece test amaçlı bir mock sınıfı
    }
}
