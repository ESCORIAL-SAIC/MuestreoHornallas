using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace MuestreoHornallas
{
    public class Muestreo
    {
        public int id { get; set; }
        public int cocina_id { get; set; }
        public int hornalla { get; set; }
        public int tiempo { get; set; }
        public bool encendido_electronico { get; set; }
        public int voladura { get; set; }
        public bool retiene { get; set; }
        public DateTime dia { get; set; }
        public TimeSpan hora { get; set; }
        public double presion_gas { get; set; }
        public double presion_atmosferica { get; set; }
        public double humedad_ambiente { get; set; }
        public double temperatura_ambiente { get; set; }
        public DateTime fecha_alta { get; set; }
        public static async Task<Muestreo> InsertarMuestreo(Muestreo datos, HttpClient httpClient)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/hornallas/muestreo", datos);
                if (response.IsSuccessStatusCode)
                {
                    string contenidoRespuesta = await response.Content.ReadAsStringAsync();
                    JObject jsonObj = JObject.Parse(contenidoRespuesta);
                    Muestreo muestreo = jsonObj["Data"].ToObject<Muestreo>();
                    return muestreo;
                }
                else
                {
                    throw new Exception("Error serializando datos.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
