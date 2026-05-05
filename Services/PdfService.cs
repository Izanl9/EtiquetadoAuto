using QuestPDF.Fluent;
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
            var rutaPdf = Path.Combine(FileSystem.CacheDirectory, "Etiquetas_Inventario.pdf");

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    // 1. Ruta completa para que C# sepa qué es un A4
                    page.Size(QuestPDF.Helpers.PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    
                    // 2. Colores Hexadecimales puros (¡cero conflictos!)
                    page.PageColor("#FFFFFF"); // Fondo Blanco
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Etiquetas de Inventario")
                        .SemiBold().FontSize(20).FontColor("#1976D2"); // Azul oscuro

                    page.Content().PaddingVertical(1, Unit.Centimetre).Grid(grid =>
                    {
                        grid.VerticalSpacing(15);
                        grid.HorizontalSpacing(15);
                        grid.Columns(2); // Ponemos 2 etiquetas por fila

                        foreach (var prod in productos)
                        {
                            // Borde Gris claro (#BDBDBD)
                            grid.Item().Border(1).BorderColor("#BDBDBD").Padding(15).Column(col =>
                            {
                                col.Item().Text(prod.Nombre).Bold().FontSize(16);
                                
                                // Texto Gris oscuro (#616161)
                                col.Item().Text($"CÓDIGO: {prod.Codigo}").FontColor("#616161"); 
                                
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