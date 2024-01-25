using iText.Html2pdf;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using TheFullStackTeam.Application.Services.Abstract;
using Image = iText.Layout.Element.Image;

namespace TheFullStackTeam.Application.Services
{

    public class HtmlToPdf : IHtmlToPdf
    {

        public async Task<string> WritePdf(string htmlTemplate, string moniker, string ident)
        {
          ConverterProperties properties = new ConverterProperties();
          MemoryStream stream = new MemoryStream();
          PdfWriter writer= new PdfWriter(stream);
          PdfDocument pdfDocument = new PdfDocument(writer);
          PageSize pageSize = PageSize.A4;
          pdfDocument.SetDefaultPageSize(pageSize);

          var img = new Image(ImageDataFactory.Create("https://devtfststorage.blob.core.windows.net/common/toHeader.png"));
          pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new HeaderEventHandler(img, ident));
     
          createPdf(htmlTemplate, pdfDocument, properties);
          
          var encodePdf = Convert.ToBase64String(stream.ToArray());
          pdfDocument.Close();

           
            return encodePdf;
        }


        public void createPdf(string html, PdfDocument pdf, ConverterProperties converterProperties)
        {  
            HtmlConverter.ConvertToPdf(html, pdf, converterProperties);
            
           
        }

     
    }
}
