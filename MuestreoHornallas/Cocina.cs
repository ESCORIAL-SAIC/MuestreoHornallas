using System.Net.Http.Json;

namespace MuestreoHornallas
{
    public class Cocina
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_alta { get; set; }
        public static async Task<List<Cocina>> ObtenerCocinas(HttpClient httpClient)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Cocina>>("api/hornallas/cocinas");
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
