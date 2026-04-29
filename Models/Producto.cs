namespace EtiquetadoAuto.Models
{
    public class Producto
    {
        public string Nombre { get; set; } = "";
        public int Cantidad { get; set; }
        public string Codigo { get; set; } = "";     // Requerido por Inventory.razor
        public int Quantity { get; set; }           // Requerido por Inventory.razor
        public DateTime LastUpdate { get; set; } = DateTime.Now; // Requerido por Inventory.razor
    }
}