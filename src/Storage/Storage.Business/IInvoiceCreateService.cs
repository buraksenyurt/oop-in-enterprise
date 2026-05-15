using Storage.Domain;

namespace Storage.Business;

public interface IInvoiceCreateService
{
    Task<Result<Guid>> CreateAsync(decimal totalAmount, byte[] eInvoiceContent);
}
