using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace TheFullStackTeam.Application.Services
{
    public class HeaderEventHandler : IEventHandler
    {
        private readonly Image _logo;
        private readonly string _headerText;

        public HeaderEventHandler( Image image, string id )
        {
            _logo = image;
            _headerText = id;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas1 = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rectangle = new Rectangle(35, page.GetPageSize().GetTop() - 75, page.GetPageSize().GetRight() - 70, 55);
            new Canvas(canvas1, rectangle).Add(getTable(docEvent));
        }

        private Table getTable(PdfDocumentEvent docEvent)
        {
          float[] cellWidth = { 20f, 80f };
                  Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

                  Style styleCell = new Style().SetBorder(Border.NO_BORDER);
                  PdfFont pdfFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

                  Style styleText = new Style().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

                  Cell cell = new Cell().Add(_logo.SetAutoScale(true)).SetBorder(Border.NO_BORDER);
                 tableEvent.AddCell(cell).AddStyle(styleCell).SetTextAlignment(TextAlignment.LEFT);

                 cell = new Cell()
                     .Add(new Paragraph(_headerText).SetFont(pdfFont).SetFontColor(ColorConstants.GRAY))
                     .Add(new Paragraph("The Full Stack Team").SetFont(pdfFont).SetFontColor(ColorConstants.GRAY))
                     .AddStyle(styleText).AddStyle(styleCell)
                     .SetBorder(Border.NO_BORDER);
                    tableEvent.AddCell(cell);

                  return tableEvent;

        }
    }
}
