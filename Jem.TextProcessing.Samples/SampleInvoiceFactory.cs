namespace Jem.TextProcessing.Samples;

public static class SampleInvoiceFactory
{
    public static SampleInvoice CreateSampleInvoice()
    {
        var invoice = new SampleInvoice
        {
            To = "John Doe",
            Address = "123 Main St.\nAnytown, USA",
            Date = DateTime.Today,
            Phone = "(555) 555-1234",
            Salesman = "Jane Smith",
            Number = "INV-2022001",
            Items = new List<SampleInvoiceItem>
            {
                new SampleInvoiceItem
                {
                    Code = "ITEM001",
                    Description = "Widget",
                    Qty = 2,
                    Amount = 100,
                    VatCode = "VAT001",
                    VatAmount = 20,
                    Total = 240
                },
                new SampleInvoiceItem
                {
                    Code = "ITEM002",
                    Description = "Gadget",
                    Qty = 1,
                    Amount = 50,
                    VatCode = "VAT001",
                    VatAmount = 10,
                    Total = 60
                }
            }
        };

        return invoice;
    }
}
