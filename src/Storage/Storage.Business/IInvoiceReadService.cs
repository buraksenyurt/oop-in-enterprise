using Storage.Domain;

namespace Storage.Business;

public interface IInvoiceReadService
{
    Task<Result<byte[]>> GetInvoiceContentAsync(Guid invoiceId);
}
