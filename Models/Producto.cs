namespace EtiquetadoAuto.Models
{
    public class Producto
    {
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string Codigo { get; set; } = string.Empty; // Nuevo
        public int Quantity { get; set; }           // Nuevo (para Inventory.razor)
        public DateTime LastUpdate { get; set; } = DateTime.Now; // Nuevo
    }
}