using Microsoft.AspNetCore.Mvc;
using Storage.Business;
using Storage.Domain;

namespace Storage.WebApi.Controllers;

[ApiController]
[Route("api/invoice")]
public class InvoiceCreateController : ControllerBase
{
    private readonly IInvoiceCreateService _invoiceCreateService;

    public InvoiceCreateController(IInvoiceCreateService invoiceCreateService)
    {
        _invoiceCreateService = invoiceCreateService;
    }

    // Save metodu HTTP protokolünün POST metoduna göre çağırılır.
    [HttpPost(Name = "SaveInvoice")]
    public async Task<Result<Guid>> Save([FromBody] InvoiceCreateSaveRequest invoiceCreateSaveRequest)
    {
        return await _invoiceCreateService
            .CreateAsync(
                invoiceCreateSaveRequest.TotalAmount ?? 0
                , Convert.FromBase64String(invoiceCreateSaveRequest.Base64Content));
    }
}


public record InvoiceCreateSaveRequest(decimal? TotalAmount, string Base64Content);