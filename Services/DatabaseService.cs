using SQLite;
using EtiquetadoAuto.Models;

namespace EtiquetadoAuto.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _conexion;

        public DatabaseService()
        {
            // Busca la ruta segura del móvil para guardar el archivo .db3
            string rutaDb = Path.Combine(FileSystem.AppDataDirectory, "InventarioApp.db3");
            _conexion = new SQLiteAsyncConnection(rutaDb);
            
            // Crea la tabla si no existe
            _conexion.CreateTableAsync<Producto>().Wait();
        }

        // Método para guardar un producto
       public async Task GuardarProducto(Producto nuevoProducto)
        {
             // Buscamos si ya existe un producto con el mismo nombre (sin importar mayúsculas/minúsculas)
            var productoExistente = await _conexion.Table<Producto>()
                .Where(p => p.Nombre.ToLower() == nuevoProducto.Nombre.ToLower())
                .FirstOrDefaultAsync();

            if (productoExistente != null)
            {
                // Si existe, sumamos la cantidad y actualizamos
                productoExistente.Cantidad += nuevoProducto.Cantidad;
                // Opcional: Actualizar el código si el nuevo trae uno diferente
                productoExistente.Codigo = nuevoProducto.Codigo; 
                await _conexion.UpdateAsync(productoExistente);
            }
            else
            {
                // Si no existe, lo insertamos como nuevo
                await _conexion.InsertAsync(nuevoProducto);
            }
        } 

        // Método para ver todos los productos (lo usaremos luego en la pantalla de inventario)
        public async Task<List<Producto>> ObtenerTodos()
        {
            return await _conexion.Table<Producto>().ToListAsync();
        }
    }
}