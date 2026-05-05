using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using EtiquetadoAuto.Models;

namespace EtiquetadoAuto.Services
{
    public class PdfService
    {
        public PdfService()
        {
            // QuestPDF requiere aceptar su licencia gratuita para proyectos pequeños/educativos
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public string GenerarEtiquetas(List<Producto> productos)
        {
            // Guardamos el PDF en la memoria temporal del móvil
            var rutaPdf = Path.Combine(FileSystem.CacheDirectory, "Etiquetas_Inventario.pdf");

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Etiquetas de Inventario")
                        .SemiBold().FontSize(20).FontColor(Colors.BlueDarken2);

                    page.Content().PaddingVertical(1, Unit.Centimetre).Grid(grid =>
                    {
                        grid.VerticalSpacing(15);
                        grid.HorizontalSpacing(15);
                        grid.Columns(2); // Ponemos 2 etiquetas por fila

                        foreach (var prod in productos)
                        {
                            // Dibujamos el recuadro de la etiqueta
                            grid.Item().Border(1).BorderColor(Colors.GreyLighten1).Padding(15).Column(col =>
                            {
                                col.Item().Text(prod.Nombre).Bold().FontSize(16);
                                col.Item().Text($"CÓDIGO: {prod.Codigo}").FontColor(Colors.GreyDarken2);
                                col.Item().PaddingTop(5).Text($"CANTIDAD: {prod.Cantidad}").FontSize(14).SemiBold();
                            });
                        }
                    });
                });
            })
            .GeneratePdf(rutaPdf);

            return rutaPdf;
        }
    }
}