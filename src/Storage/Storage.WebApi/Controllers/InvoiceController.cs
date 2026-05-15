using Microsoft.AspNetCore.Mvc;
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
    // Save metodu HTTP protokolünün POST metoduna göre çağırılır.
    [HttpPost(Name = "SaveInvoice")]
    public Result<Guid> Save([FromBody] InvoiceSaveRequest invoiceSaveRequests)
    {
        var invoice = new Invoice
        {
            ID = Guid.NewGuid(),
            AcceptDate = DateTime.Now,
            TotalAmount = invoiceSaveRequests.TotalAmount,
            EInvoice = new Asset(
                $"{Guid.NewGuid()}.pdf",
                Convert.FromBase64String(invoiceSaveRequests.Base64Content)
            )   
        };

        // Burada business servis çağrısı yapılacak

        return new Result<Guid>
        {
            IsSuccess = true,
            Value = invoice.ID,
        };
    }
}

// API tarafındaki Save metoduna gelen isteği tarif eden sınıf
public class InvoiceSaveRequest
{
    public decimal TotalAmount { get; set; }
    public string Base64Content { get; set; }
}