using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using EtiquetadoAuto.Models;

namespace EtiquetadoAuto.Services
{
    public class PdfService
    {
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public string GenerarEtiquetas(List<Producto> productos)
        {
            // AGRUPACIÓN: Sumamos elementos con el mismo nombre antes de imprimir
            var productosAgrupados = productos
                .GroupBy(p => p.Nombre.Trim().ToLower())
                .Select(g => new Producto
                {
                    Nombre = g.First().Nombre, // Mantenemos el nombre original del primero
                    Codigo = g.First().Codigo,
                    Cantidad = g.Sum(p => p.Cantidad) // Sumamos todas las cantidades
                })
                .ToList();

            var rutaPdf = Path.Combine(FileSystem.CacheDirectory, "Etiquetas_Consolidadas.pdf");

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    // . . . (resto de la configuración del PDF)

                    page.Content().PaddingVertical(1, Unit.Centimetre).Grid(grid =>
                    {
                        grid.Columns(2);
                        grid.VerticalSpacing(15);
                        grid.HorizontalSpacing(15);

                        foreach (var prod in productosAgrupados) // Usamos la lista agrupada
                        {
                            for (int i = 0; i < prod.Cantidad; i++)
                            {
                                grid.Item().Border(1).BorderColor("#BDBDBD").Padding(10).Column(col =>
                                {
                                    col.Item().Text(prod.Nombre).Bold().FontSize(14);
                                    col.Item().Text($"CÓDIGO: {prod.Codigo}").FontSize(10).FontColor("#616161");
                                    col.Item().AlignRight().Text($"Unidad {i + 1} de {prod.Cantidad}").FontSize(8).Italic();
                                });
                            }
                        }
                    });
                });
            })
            .GeneratePdf(rutaPdf);

            return rutaPdf;
        }
    }
}