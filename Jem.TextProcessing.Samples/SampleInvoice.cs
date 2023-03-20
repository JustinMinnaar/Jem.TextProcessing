namespace Jem.TextProcessing.Samples;

public class SampleInvoice
{
    public string? To { get; set; }
    public string? Address { get; set; }
    public DateTime? Date { get; set; }
    public string? Phone { get; set; }
    public string? Salesman { get; set; }
    public string? Number { get; set; }
    public List<SampleInvoiceItem> Items { get; set; } = new();
}
