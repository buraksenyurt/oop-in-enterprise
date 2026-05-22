using Microsoft.Extensions.Logging;
using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Business;

public class InvoiceReadService : IInvoiceReadService
{
    private readonly IStorageService _storageService;
    private readonly ILogger<InvoiceReadService> _logger;

    public InvoiceReadService(IStorageService storageService, ILogger<InvoiceReadService> logger)
    {
        _storageService = storageService;
        _logger = logger;
    }
    public async Task<Result<byte[]>> GetInvoiceContentAsync(Guid invoiceId)
    {
        try
        {
            var content = await _storageService.LoadAsync(invoiceId.ToString());
            _logger.LogInformation("Invoice {InvoiceId} retrieved successfully.", invoiceId);
            return new Result<byte[]>
            {
                IsSuccess = true,
                Value = content
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving invoice {InvoiceId}.", invoiceId);
            return new Result<byte[]>
            {
                IsSuccess = false,
                Value = null,
                ErrorMessage = ex.Message
            };
        }
    }
}
