using Microsoft.AspNetCore.Mvc;
using Storage.Business;
using Storage.Domain;

namespace Storage.WebApi.Controllers;

[ApiController]
[Route("api/invoice")]
public class InvoiceReadController : ControllerBase
{
    private readonly IInvoiceReadService _invoiceReadService;

    public InvoiceReadController(IInvoiceReadService invoiceReadService)
    {
        _invoiceReadService = invoiceReadService;
    }

    [HttpGet(Name = "GetInvoiceContent")]
    public async Task<Result<byte[]>> Get(Guid invoiceId)
    {
        return await _invoiceReadService.GetInvoiceContentAsync(invoiceId);
    }
}