namespace MuestreoHornallas
{
    public static class Configuracion
    {
        public static string PresionGas { get; set; } = null;
        public static string PresionAtmosferica { get; set; } = null;
        public static string HumedadAmbiente { get; set; } = null;
        public static string TemperaturaAmbiente { get; set; } = null;
        public static int IdCocina { get; set; } = -1;
        public static readonly HttpClient SharedClient = new()
        {
            BaseAddress = new Uri(Preferences.Get("ApiUrl", string.Empty)),
        };
        public static bool Guardado()
        {
            return !string.IsNullOrEmpty(PresionGas) &&
                   !string.IsNullOrEmpty(PresionAtmosferica) &&
                   !string.IsNullOrEmpty(HumedadAmbiente) &&
                   !string.IsNullOrEmpty(TemperaturaAmbiente) &&
                   IdCocina != -1;
        }
    }
}
