using EtiquetadoAuto.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EtiquetadoAuto.Services
{
    public class StockService
    {
        public async Task<List<Producto>> GetStockAsync()
        {
            try
            {
                // DATOS DE PRUEBA (Sustituye la lista vacía por esta)
                var listaPrueba = new List<Producto>
                {
                    new Producto { Id = "1", Codigo = "75010203", Nombre = "Caja de Leche", Quantity = 50, LastUpdate = DateTime.Now },
                    new Producto { Id = "2", Codigo = "84123456", Nombre = "Detergente 1L", Quantity = 12, LastUpdate = DateTime.Now },
                    new Producto { Id = "3", Codigo = "12345678", Nombre = "Paquete Arroz", Quantity = 100, LastUpdate = DateTime.Now }
                };

                await Task.Delay(500); // Simula latencia de red
                return listaPrueba;
            }
            catch (Exception)
            {
                return new List<Producto>();
            }
        }
    }
}