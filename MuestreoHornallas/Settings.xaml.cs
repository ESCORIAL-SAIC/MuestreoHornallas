using System.Globalization;
using System.Text.Json;

namespace MuestreoHornallas
{
    public partial class Settings
    {
        private List<Cocina> _cocinas = new();
        public Settings()
        {
            InitializeComponent();
            ApiKeyEntry.Text = Preferences.Get("ApiToken", string.Empty);
            ApiUrlEntry.Text = Preferences.Get("ApiUrl", string.Empty);
            if (!string.IsNullOrEmpty(ApiUrlEntry.Text) && !string.IsNullOrEmpty(ApiKeyEntry.Text))
            {
                ObtenerDatosClima();
                CargarCocinas();
            }
        }
        private async void CargarCocinas()
        {
            _cocinas = await Cocina.ObtenerCocinas(Configuracion.SharedClient);
            CocinaPicker.ItemsSource = _cocinas;
        }
        private async void ObtenerDatosClima()
        {
            LoadingActivityIndicator.IsRunning = true;
            LoadingActivityIndicator.IsVisible = true;
            HumedadAmbienteEntry.IsVisible = false;
            PresionAtmosfericaEntry.IsVisible = false;
            TemperaturaAmbienteEntry.IsVisible = false;

            try
            {
                Location location = await GetLocation();
                var respuesta = await CallApi(location);
                HumedadAmbienteEntry.Text = respuesta.main.humidity.ToString();
                PresionAtmosfericaEntry.Text = respuesta.main.pressure.ToString();
                TemperaturaAmbienteEntry.Text = respuesta.main.temp.ToString(CultureInfo.CurrentCulture);
                HumedadAmbienteEntry.IsVisible = true;
                PresionAtmosfericaEntry.IsVisible = true;
                TemperaturaAmbienteEntry.IsVisible = true;
            }
            catch (FeatureNotSupportedException featureNotSupportedException)
            {
                await DisplayAlert("Error", featureNotSupportedException.Message, "OK");
            }
            catch (PermissionException permissionException)
            {
                await DisplayAlert("Error", permissionException.Message, "OK");
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", "Error de conexión: No se pudo conectar a la API", "OK");
            }
            catch (JsonException)
            {
                await DisplayAlert("Error", "Error en la respuesta JSON de la API", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ocurrió un error inesperado: " + ex.Message, "OK");
            }
            finally
            {
                LoadingActivityIndicator.IsRunning = false;
                LoadingActivityIndicator.IsVisible = false;
            }
        }
        private async void GuardarButton_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("ApiToken", ApiKeyEntry.Text);
            Preferences.Set("ApiUrl", ApiUrlEntry.Text);

            if (!string.IsNullOrEmpty(Preferences.Get("ApiToken", string.Empty)) && !string.IsNullOrEmpty(Preferences.Get("ApiUrl", string.Empty)))
            {
                await DisplayAlert("Guardado correcto", "Configuración de API guardada correctamente", "OK");
            }

            if (
                !string.IsNullOrWhiteSpace(PresionGasEntry.Text) &&
                !string.IsNullOrWhiteSpace(PresionAtmosfericaEntry.Text) &&
                !string.IsNullOrWhiteSpace(HumedadAmbienteEntry.Text) &&
                !string.IsNullOrWhiteSpace(TemperaturaAmbienteEntry.Text) &&
                CocinaPicker.SelectedIndex != -1
            )
            {
                Configuracion.PresionGas = PresionGasEntry.Text;
                Configuracion.PresionAtmosferica = PresionAtmosfericaEntry.Text;
                Configuracion.HumedadAmbiente = HumedadAmbienteEntry.Text;
                Configuracion.TemperaturaAmbiente = TemperaturaAmbienteEntry.Text;
                Configuracion.IdCocina = _cocinas[CocinaPicker.SelectedIndex].id;
                
                await DisplayAlert("Guardado correcto", "Configuracion guardada correctamente", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Debe completar todos los datos para continuar", "OK");
            }
        }
        private void OnRefresh(object sender, EventArgs e)
        {
            ObtenerDatosClima();
            CargarCocinas();
            RefreshView.IsRefreshing = false;
        }
        private async Task<Location> GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High));
                if (location != null)
                {
                    return location;
                }
                else
                {
                    throw new Exception("Error al acceder a datos de la API");
                }
            }
            catch (FeatureNotSupportedException featureNotSupportedException)
            {
                await DisplayAlert("Error", featureNotSupportedException.Message, "OK");
                throw;
            }
            catch (PermissionException permissionException)
            {
                await DisplayAlert("Error", permissionException.Message, "OK");
                throw;
            }
            catch (Exception exception)
            {
                await DisplayAlert("Error", exception.Message, "OK");
                throw;
            }
        }
        private static async Task<Clima> CallApi(Location location)
        {
            var apiKey = Preferences.Get("ApiToken", string.Empty);
            var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&units=metric&appid={apiKey}";
            try
            {
                HttpClient httpClient = new();
                var response = await httpClient.GetStringAsync(apiUrl);
                var responseObj = JsonSerializer.Deserialize<Clima>(response);
                return responseObj;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión: No se pudo conectar a la API");
            }
            catch (JsonException)
            {
                throw new JsonException("Error en la respuesta JSON de la API");
            }
        }
    }
}