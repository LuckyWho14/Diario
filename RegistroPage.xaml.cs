namespace DiarioPro;

public partial class RegistroPage : ContentPage
{
    private List<RegistroDiario> registros;  // Lista para almacenar los registros de los d�as

    public RegistroPage()
    {
        InitializeComponent();  

        // Inicializa la lista de registros
        registros = new List<RegistroDiario>();

        // Event handler para el control Slider
        activitySlider.ValueChanged += activitySlider_ValueChanged;

        // Inicializa la barra de progreso seg�n el valor del slider
        progresoSemanal.Progress = activitySlider.Value / 10;  
    }

    // M�todo que se ejecuta cuando la p�gina aparece
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Carga los registros guardados previamente
        CargarRegistros(); 
    }

    // M�todo que carga los registros desde las preferencias (almacenados localmente)
    private void CargarRegistros()
    {
        // Verifica si existen registros guardados
        if (Preferences.ContainsKey("registrosDiarios"))
        {
            // Obtiene los registros guardados en formato JSON y deserializa el JSON en una lista de registros
            var json = Preferences.Get("registrosDiarios", string.Empty); 
            registros = System.Text.Json.JsonSerializer.Deserialize<List<RegistroDiario>>(json) ?? new List<RegistroDiario>(); 
        }
    }

    // M�todo que actualiza la barra de progreso al cambiar el valor del slider
    private void activitySlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        progresoSemanal.Progress = e.NewValue / 10; 
    }

    // M�todo que actualiza el nivel de energ�a cuando se cambia el valor del Stepper
    private void energyStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        energyValueLabel.Text = $"{e.NewValue}"; 
    }

    // M�todo que se ejecuta cuando el bot�n "Guardar Registro" es presionado
    private void Button_Clicked(object sender, EventArgs e)
    {
        // Crea un nuevo registro con los datos ingresados
        var nuevoRegistro = new RegistroDiario
        {
            Fecha = datePicker.Date,  
            Nota = editorNota.Text,  
            lvlActiv = (int)activitySlider.Value,  
            lvlEnerg = (int)energyStepper.Value
        };

        registros.Add(nuevoRegistro); 
        GuardarRegistros();  
        DisplayAlert("Exito", "Registro guardado con �xito", "OK");  
        Limpiar(); 
    }

    // M�todo que guarda los registros en las preferencias locales (en formato JSON)
    private void GuardarRegistros()
    {
        // Conveirte la lista de registros a formato JSON y guarda JSON en preferencias
        var json = System.Text.Json.JsonSerializer.Serialize(registros);
        Preferences.Set("registrosDiarios", json);
    }

    // M�todo que limpia los campos de entrada
    private void Limpiar()
    {
        editorNota.Text = String.Empty;  
        activitySlider.Value = 0;  
        energyStepper.Value = 0;  
    }
}
