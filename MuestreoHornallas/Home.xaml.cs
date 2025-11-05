using System.Timers;
using Timer = System.Timers.Timer;

namespace MuestreoHornallas;
public partial class Home
{
    private static int _contadorInsertadas;
    private readonly Timer _timer;
    private DateTime _startTime;
    private bool _timerStatus;
    public Home()
    {
        InitializeComponent();
        _timer = new Timer(1000);
        _timer.Elapsed += Timer_Elapsed;
    }
    private async void ImageButtonPressed(object sender, EventArgs e)
    {
        var imageButton = (ImageButton)sender;
        await imageButton.ScaleTo(1.2, 100);
    }
    private async void ImageButtonReleased(object sender, EventArgs e)
    {
        if (Configuracion.Guardado())
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
                TimerLabel.FontAttributes = FontAttributes.None;
                TimerLabel.Text = "Cronómetro";
            }
            else
            {
                _startTime = DateTime.Now;
                _timer.Start();
                TimerLabel.FontAttributes = FontAttributes.Bold;
                TimerLabel.Text = "0:00";
            }
            var imageButton = (ImageButton)sender;
            await imageButton.ScaleTo(0, 100);
            if (!_timerStatus)
            {
                CambiarEstilo("ImageButtonStop", imageButton);
            }
            else
            {
                CambiarEstilo("ImageButtonPlay", imageButton);
                _contadorInsertadas = 0;
                LimpiarHornallas();
                LimpiarBotonesEncendidoElectronico();
                LimpiarBotonesRetiene();
                LimpiarBotonesVoladura();
                EstadoLabel.Text = string.Empty;
            }
            await imageButton.ScaleTo(1.2, 100);
            await imageButton.ScaleTo(1, 100);
            _timerStatus = !_timerStatus;
        }
        else
        {
            await DisplayAlert("Error", "No se han guardado las configuraciones.\nNo se puede iniciar el cronómetro hasta que estén todas las configuraciones cargadas", "OK");
        }
    }
    private void Hornalla01ImageButton_Clicked(object sender, EventArgs e)
    {
        Hornalla.Primera = true;
        Hornalla.Segunda = false;
        Hornalla.Tercera = false;
        Hornalla.Cuarta = false;
        CambiarEstilo("Hornalla01Encendida", Hornalla01ImageButton);
        CambiarEstilo("Hornalla02Apagada", Hornalla02ImageButton);
        CambiarEstilo("Hornalla03Apagada", Hornalla03ImageButton);
        CambiarEstilo("Hornalla04Apagada", Hornalla04ImageButton);
    }
    private void Hornalla02ImageButton_Clicked(object sender, EventArgs e)
    {
        Hornalla.Primera = false;
        Hornalla.Segunda = true;
        Hornalla.Tercera = false;
        Hornalla.Cuarta = false;
        CambiarEstilo("Hornalla01Apagada", Hornalla01ImageButton);
        CambiarEstilo("Hornalla02Encendida", Hornalla02ImageButton);
        CambiarEstilo("Hornalla03Apagada", Hornalla03ImageButton);
        CambiarEstilo("Hornalla04Apagada", Hornalla04ImageButton);
    }
    private void Hornalla03ImageButton_Clicked(object sender, EventArgs e)
    {
        Hornalla.Primera = false;
        Hornalla.Segunda = false;
        Hornalla.Tercera = true;
        Hornalla.Cuarta = false;
        CambiarEstilo("Hornalla01Apagada", Hornalla01ImageButton);
        CambiarEstilo("Hornalla02Apagada", Hornalla02ImageButton);
        CambiarEstilo("Hornalla03Encendida", Hornalla03ImageButton);
        CambiarEstilo("Hornalla04Apagada", Hornalla04ImageButton);
    }
    private void Hornalla04ImageButton_Clicked(object sender, EventArgs e)
    {
        Hornalla.Primera = false;
        Hornalla.Segunda = false;
        Hornalla.Tercera = false;
        Hornalla.Cuarta = true;
        CambiarEstilo("Hornalla01Apagada", Hornalla01ImageButton);
        CambiarEstilo("Hornalla02Apagada", Hornalla02ImageButton);
        CambiarEstilo("Hornalla03Apagada", Hornalla03ImageButton);
        CambiarEstilo("Hornalla04Encendida", Hornalla04ImageButton);
    }
    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var elapsed = DateTime.Now - _startTime;
            TimerLabel.Text = $"{elapsed.Minutes}:{elapsed.Seconds:D2}";
        });
    }
    private void EEOKButton_Clicked(object sender, EventArgs e)
    {
        Boton.EncendidoElectronico.OK = true;
        Boton.EncendidoElectronico.NoOK = false;
        CambiarEstilo("ButtonSelected", EeokButton);
        CambiarEstilo("ButtonUnselected", EenookButton);
    }
    private void EENOOKButton_Clicked(object sender, EventArgs e)
    {
        Boton.EncendidoElectronico.OK = false;
        Boton.EncendidoElectronico.NoOK = true;
        CambiarEstilo("ButtonSelected", EenookButton);
        CambiarEstilo("ButtonUnselected", EeokButton);
    }
    private void CambiarEstilo(string nombreEstilo, NavigableElement control)
    {
        if (Resources.TryGetValue(nombreEstilo, out var value) && value is Style buttonStyle)
        {
            control.Style = buttonStyle;
        }
    }
    private void Voladura0Button_Clicked(object sender, EventArgs e)
    {
        Boton.Voladura.Cero = true;
        Boton.Voladura.Cuarto = false;
        Boton.Voladura.Medio = false;
        Boton.Voladura.Tercio = false;
        Boton.Voladura.Entero = false;
        CambiarEstilo("ButtonSelected", Voladura0Button);
        CambiarEstilo("ButtonUnselected", Voladura25Button);
        CambiarEstilo("ButtonUnselected", Voladura50Button);
        CambiarEstilo("ButtonUnselected", Voladura75Button);
        CambiarEstilo("ButtonUnselected", Voladura100Button);
    }
    private void Voladura25Button_Clicked(object sender, EventArgs e)
    {
        Boton.Voladura.Cero = false;
        Boton.Voladura.Cuarto = true;
        Boton.Voladura.Medio = false;
        Boton.Voladura.Tercio = false;
        Boton.Voladura.Entero = false;
        CambiarEstilo("ButtonUnselected", Voladura0Button);
        CambiarEstilo("ButtonSelected", Voladura25Button);
        CambiarEstilo("ButtonUnselected", Voladura50Button);
        CambiarEstilo("ButtonUnselected", Voladura75Button);
        CambiarEstilo("ButtonUnselected", Voladura100Button);
    }
    private void Voladura50Button_Clicked(object sender, EventArgs e)
    {
        Boton.Voladura.Cero = false;
        Boton.Voladura.Cuarto = false;
        Boton.Voladura.Medio = true;
        Boton.Voladura.Tercio = false;
        Boton.Voladura.Entero = false;
        CambiarEstilo("ButtonUnselected", Voladura0Button);
        CambiarEstilo("ButtonUnselected", Voladura25Button);
        CambiarEstilo("ButtonSelected", Voladura50Button);
        CambiarEstilo("ButtonUnselected", Voladura75Button);
        CambiarEstilo("ButtonUnselected", Voladura100Button);
    }
    private void Voladura75Button_Clicked(object sender, EventArgs e)
    {
        Boton.Voladura.Cero = false;
        Boton.Voladura.Cuarto = false;
        Boton.Voladura.Medio = false;
        Boton.Voladura.Tercio = true;
        Boton.Voladura.Entero = false;
        CambiarEstilo("ButtonUnselected", Voladura0Button);
        CambiarEstilo("ButtonUnselected", Voladura25Button);
        CambiarEstilo("ButtonUnselected", Voladura50Button);
        CambiarEstilo("ButtonSelected", Voladura75Button);
        CambiarEstilo("ButtonUnselected", Voladura100Button);
    }
    private void Voladura100Button_Clicked(object sender, EventArgs e)
    {
        Boton.Voladura.Cero = false;
        Boton.Voladura.Cuarto = false;
        Boton.Voladura.Medio = false;
        Boton.Voladura.Tercio = false;
        Boton.Voladura.Entero = true;
        CambiarEstilo("ButtonUnselected", Voladura0Button);
        CambiarEstilo("ButtonUnselected", Voladura25Button);
        CambiarEstilo("ButtonUnselected", Voladura50Button);
        CambiarEstilo("ButtonUnselected", Voladura75Button);
        CambiarEstilo("ButtonSelected", Voladura100Button);
    }
    private void RetieneButton_Clicked(object sender, EventArgs e)
    {
        Boton.Retiene.SiRetiene = true;
        Boton.Retiene.NoRetiene = false;
        CambiarEstilo("ButtonSelected", RetieneButton);
        CambiarEstilo("ButtonUnselected", NoRetieneButton);
    }
    private void NoRetieneButton_Clicked(object sender, EventArgs e)
    {
        Boton.Retiene.SiRetiene = false;
        Boton.Retiene.NoRetiene = true;
        CambiarEstilo("ButtonSelected", NoRetieneButton);
        CambiarEstilo("ButtonUnselected", RetieneButton);
    }
    private async void ConfirmarButton_Pressed(object sender, EventArgs e)
    {
        if (!_timerStatus)
        {
            await DisplayAlert("Error", "No se ha iniciado el timer", "OK");
            return;
        }
        if (!Hornalla.Primera && !Hornalla.Segunda && !Hornalla.Tercera && !Hornalla.Cuarta)
        {
            await DisplayAlert("Error", "Debe seleccionar una hornalla", "OK");
            return;
        }
        if (!Boton.EncendidoElectronico.OK && !Boton.EncendidoElectronico.NoOK)
        {
            await DisplayAlert("Error", "Debe seleccionar el encendido electrónico", "OK");
            return;
        }
        if (!Boton.Voladura.Cero &&
            !Boton.Voladura.Cuarto &&
            !Boton.Voladura.Medio &&
            !Boton.Voladura.Tercio &&
            !Boton.Voladura.Entero)
        {
            await DisplayAlert("Error", "Debe seleccionar un porcentaje de voladura", "OK");
            return;
        }
        if (!Boton.Retiene.SiRetiene && !Boton.Retiene.NoRetiene)
        {
            await DisplayAlert("Error", "Debe indicar si retiene", "OK");
            return;
        }
        var hornalla = (Hornalla.Primera) ? 1
            : (Hornalla.Segunda) ? 2
            : (Hornalla.Tercera) ? 3
            : (Hornalla.Cuarta) ? 4
            : 0;
        var voladura = (Boton.Voladura.Cero) ? 0
            : (Boton.Voladura.Cuarto) ? 25
            : (Boton.Voladura.Medio) ? 50
            : (Boton.Voladura.Tercio) ? 75
            : (Boton.Voladura.Entero) ? 100
            : 0;
        var encendidoElectronico = Boton.EncendidoElectronico.OK;
        var retiene = Boton.Retiene.SiRetiene;
        var partes = TimerLabel.Text.Split(':');
        var tiempo = (partes.Length == 2 && int.TryParse(partes[0], out var minutos) && int.TryParse(partes[1], out var segundos)) ? minutos * 60 + segundos : 0;
        Muestreo muestreo = new()
        {
            cocina_id = Configuracion.IdCocina,
            hornalla = hornalla,
            tiempo = tiempo,
            encendido_electronico = encendidoElectronico,
            voladura = voladura,
            retiene = retiene,
            dia = DateTime.Now,
            hora = DateTime.Now.TimeOfDay,
            presion_gas = double.Parse(Configuracion.PresionGas),
            presion_atmosferica = double.Parse(Configuracion.PresionAtmosferica),
            humedad_ambiente = double.Parse(Configuracion.HumedadAmbiente),
            temperatura_ambiente = double.Parse(Configuracion.TemperaturaAmbiente),
            fecha_alta = DateTime.Now
        };
        await Muestreo.InsertarMuestreo(muestreo, Configuracion.SharedClient);
        _contadorInsertadas += 1;
        EstadoLabel.Text = $"Registros insertados: {_contadorInsertadas}\nTiempo: {TimerLabel.Text}";
        LimpiarHornallas();
        CambiarEstilo("TextoOk", EstadoLabel);
        await EstadoLabel.ScaleTo(1.2, 100);
        await Task.Delay(1000);
        await EstadoLabel.ScaleTo(1, 100);
    }
    private void LimpiarHornallas()
    {
        Hornalla.Primera = false;
        Hornalla.Segunda = false;
        Hornalla.Tercera = false;
        Hornalla.Cuarta = false;
        CambiarEstilo("Hornalla01Apagada", Hornalla01ImageButton);
        CambiarEstilo("Hornalla02Apagada", Hornalla02ImageButton);
        CambiarEstilo("Hornalla03Apagada", Hornalla03ImageButton);
        CambiarEstilo("Hornalla04Apagada", Hornalla04ImageButton);
    }
    private void LimpiarBotonesEncendidoElectronico()
    {
        Boton.EncendidoElectronico.OK = false;
        Boton.EncendidoElectronico.NoOK = false;
        CambiarEstilo("ButtonUnselected", EeokButton);
        CambiarEstilo("ButtonUnselected", EenookButton);
    }
    private void LimpiarBotonesVoladura()
    {
        Boton.Voladura.Cero = false;
        Boton.Voladura.Cuarto = false;
        Boton.Voladura.Medio = false;
        Boton.Voladura.Tercio = false;
        Boton.Voladura.Entero = false;
        CambiarEstilo("ButtonUnselected", Voladura0Button);
        CambiarEstilo("ButtonUnselected", Voladura25Button);
        CambiarEstilo("ButtonUnselected", Voladura50Button);
        CambiarEstilo("ButtonUnselected", Voladura75Button);
        CambiarEstilo("ButtonUnselected", Voladura100Button);
    }
    private void LimpiarBotonesRetiene()
    {
        Boton.Retiene.SiRetiene = false;
        Boton.Retiene.NoRetiene = false;
        CambiarEstilo("ButtonUnselected", NoRetieneButton);
        CambiarEstilo("ButtonUnselected", RetieneButton);
    }
}