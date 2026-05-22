using Microsoft.Extensions.Logging;
using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Business;

public class InvoiceCreateService : IInvoiceCreateService
{
    private readonly IStorageService _storageService;
    private readonly ILogger<InvoiceCreateService> _logger;

    public InvoiceCreateService(IStorageService storageService, ILogger<InvoiceCreateService> logger)
    {
        _storageService = storageService;
        _logger = logger;
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
            _logger.LogInformation("Invoice {InvoiceId} created successfully.", invoice.ID);

            return new Result<Guid>
            {
                IsSuccess = true,
                Value = invoice.ID
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating invoice {InvoiceId}.", invoice.ID);
            return new Result<Guid>
            {
                IsSuccess = false,
                Value = invoice.ID,
                ErrorMessage = ex.Message
            };
        }
    }
}
