using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Business;

public class InvoiceReadService : IInvoiceReadService
{
    private readonly IStorageService _storageService;
    public InvoiceReadService(IStorageService storageService)
    {
        _storageService = storageService;
    }
    public async Task<Result<byte[]>> GetInvoiceContentAsync(Guid invoiceId)
    {
        try
        {
            var content = await _storageService.LoadAsync(invoiceId.ToString());
            return new Result<byte[]>
            {
                IsSuccess = true,
                Value = content
            };
        }
        catch (Exception ex)
        {
            return new Result<byte[]>
            {
                IsSuccess = false,
                Value = null,
                ErrorMessage = ex.Message
            };
        }
    }
}
