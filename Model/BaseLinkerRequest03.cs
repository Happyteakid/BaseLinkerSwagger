using System.ComponentModel.DataAnnotations;

namespace BaselinkerREST.Model;

public class BaseLinkerRequest03
{
    public int? invoice_id { get; set; }
    public string? file { get; set; }
    [MaxLength (30)]
    public string? external_invoice_number { get; set; }

    public BaseLinkerRequest03(int invoice_id, string? file, string externalInvoiceNumber)
    {
        this.invoice_id = invoice_id;
        this.file = file;
        this.external_invoice_number = externalInvoiceNumber;
    }

    public BaseLinkerRequest03()
    {

    }

}