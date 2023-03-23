using Jem.TextProcessing.Data;

namespace Jem.TextProcessing.Samples;

public class SampleTextPageGenerator
{
    public TextPage GenerateInvoicePage(SampleInvoice invoice, TextFont font, double leftMargin, double topMargin)
    {
        var page = new TextPage();

        // Generate header block
        var headerBlock = page.AddBlock
            (font, leftMargin, topMargin, new List<string>(), 1.5);
        headerBlock.AddLine(font, "INVOICE", 2);
        headerBlock.AddLine(font, $"Number: {invoice.Number}", 1.2);
        headerBlock.AddLine(font, $"Date: {invoice.Date:d}", 1.2);
        headerBlock.AddLine(font, $"To: {invoice.To}", 1.2);
        headerBlock.AddLine(font, $"Address: {invoice.Address}", 1.2);
        headerBlock.AddLine(font, $"Phone: {invoice.Phone}", 1.2);
        headerBlock.AddLine(font, $"Salesman: {invoice.Salesman}", 1.2);

        // Generate item table block
        var itemTableBlock = page.AddBlock
            (font, leftMargin, headerBlock.Rect.Bottom + 50, new List<string>(), 1.5);
        var columnHeaderLine = itemTableBlock.AddLine(font, "Code  Description  Qty  Amount  Vat Code  Vat Amount  Total", 1.2);
        columnHeaderLine.Rect.Width = 600; // Expand the width of the header line to match the table columns
        double itemTop = columnHeaderLine.Rect.Bottom + 20;
        foreach (var item in invoice.Items)
        {
            var itemLine = itemTableBlock.AddLine(font, $"{item.Code}  {item.Description}  {item.Qty}  {item.Amount:C}  {item.VatCode}  {item.VatAmount:C}  {item.Total:C}", 1.2);
            itemLine.Rect.Top = itemTop;
            itemTop = itemLine.Rect.Bottom + 10;
        }

        return page;
    }
}
