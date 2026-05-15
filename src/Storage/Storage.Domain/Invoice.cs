namespace Storage.Domain;

public class Invoice
{
    public Guid ID { get; set; }
    public DateTime AcceptDate { get; set; }
    public decimal TotalAmount { get; set; }
    public Asset EInvoice { get; set; }
}
