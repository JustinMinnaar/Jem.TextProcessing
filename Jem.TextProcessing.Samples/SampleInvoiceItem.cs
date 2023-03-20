namespace Jem.TextProcessing.Samples;

public class SampleInvoiceItem
{
    public string? Code { get; set; }
    public string? Description { get; set; }
    public int? Qty { get; set; }
    public decimal? Amount { get; set; }
    public string? VatCode { get; set; }
    public decimal? VatAmount { get; set; }
    public decimal? Total { get; set; }
}
