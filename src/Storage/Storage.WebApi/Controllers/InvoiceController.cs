using Microsoft.AspNetCore.Mvc;
using Storage.Business;
using Storage.Domain;

namespace Storage.WebApi.Controllers;

/*
    InvoiceController sınıfının bir web api olarak hareket edebilmesi için,
    çalışma zamanına ApiController ve Route gibi attribute'lar yardımıyla bazı metadata
    bilgileri eklenir.
*/
[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceCreateService _invoiceCreateService;
    private readonly IInvoiceReadService _invoiceReadService;

    public InvoiceController(IInvoiceCreateService invoiceCreateService, IInvoiceReadService invoiceReadService)
    {
        _invoiceCreateService = invoiceCreateService;
        _invoiceReadService = invoiceReadService;
    }

    // Save metodu HTTP protokolünün POST metoduna göre çağırılır.
    [HttpPost(Name = "SaveInvoice")]
    public async Task<Result<Guid>> Save([FromBody] InvoiceSaveRequest invoiceSaveRequests)
    {
        return await _invoiceCreateService
            .CreateAsync(
                invoiceSaveRequests.TotalAmount
                , Convert.FromBase64String(invoiceSaveRequests.Base64Content));
    }

    [HttpGet(Name ="GetInvoiceContent")]
    public async Task<Result<byte[]>> Get(Guid invoiceId)
    {
        return await _invoiceReadService.GetInvoiceContentAsync(invoiceId);
    }
}

//// API tarafındaki Save metoduna gelen isteği tarif eden sınıf
//public class InvoiceSaveRequest
//{
//    public decimal TotalAmount { get; set; }
//    public string Base64Content { get; set; }
//}

public record InvoiceSaveRequest(decimal TotalAmount, string Base64Content);    