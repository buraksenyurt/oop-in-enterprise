using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Business;

public class InvoiceCreateService : IInvoiceCreateService
{
    private readonly IStorageService _storageService;
    public InvoiceCreateService(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<Result<Guid>> CreateAsync(decimal totalAmount, byte[] eInvoiceContent)
    {
        var invoice = new Invoice
        {
            ID = Guid.NewGuid(),
            AcceptDate = DateTime.Now,
            TotalAmount = totalAmount,
            EInvoice = new Asset(
                        $"{Guid.NewGuid()}.pdf",
                        eInvoiceContent
                    )
        };
        try
        {
            // Burada belki faturanun veritabanına kayıt edilmesi vs gibi işlemler vars
            await _storageService.SaveAsync(invoice.EInvoice);

            return new Result<Guid>
            {
                IsSuccess = true,
                Value = invoice.ID
            };
        }
        catch (Exception ex)
        {
            return new Result<Guid>
            {
                IsSuccess = false,
                Value = invoice.ID,
                ErrorMessage = ex.Message
            };
        }
    }
}
