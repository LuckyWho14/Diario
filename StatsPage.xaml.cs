namespace DiarioPro;

public partial class StatsPage : ContentPage
{
    // Lista de registros del diario
    private List<RegistroDiario> registros; 

    public StatsPage()
    {
        InitializeComponent();
    }

    // Se llama cuando la p�gina aparece
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Establece el contexto de enlace de datos a la instancia actual
        BindingContext = this; 
        CargarRegistros();
        CalcularEstadisticas();
    }

    // M�todo para cargar los registros desde las preferencias almacenadas
    private void CargarRegistros()
    {
        // Verifica si existen registros guardados en las preferencias
        if (Preferences.ContainsKey("registrosDiarios"))
        {
            // Recupera los registros desde las preferencias
            var json = Preferences.Get("registrosDiarios", string.Empty);
            // Deserializa el JSON a una lista de registros
            registros = System.Text.Json.JsonSerializer.Deserialize<List<RegistroDiario>>(json) ?? new List<RegistroDiario>();
        }
        else
        {
            // Si no existen registros, inicializa una lista vac�a
            registros = new List<RegistroDiario>();
        }
    }

    // M�todo para calcular las estad�sticas de los registros
    private void CalcularEstadisticas()
    {
        // Si no hay registros, muestra un mensaje en las etiquetas
        if (registros.Count == 0)
        {
            promedioActividadLabel.Text = "No hay datos";
            promedioEnergiaLabel.Text = "No hay datos";
            progresoSemanal.Progress = 0;
            return; 
        }

        // Filtra los registros que corresponden a los �ltimos 7 d�as
        var registrosUltimaSemana = registros.Where(r => (DateTime.Now - r.Fecha).Days <= 7).ToList();

        // Si existen registros en la �ltima semana
        if (registrosUltimaSemana.Count > 0)
        {
            // Calcula el promedio de la actividad f�sica y el nivel de energ�a
            var promedioActividad = registrosUltimaSemana.Average(r => r.lvlActiv);
            var promedioEnergia = registrosUltimaSemana.Average(r => r.lvlEnerg);

            // Actualiza las etiquetas con los promedios calculados
            promedioActividadLabel.Text = $"Promedio actividad f�sica: {promedioActividad:F1}";

            // Actualiza la barra de progreso con el promedio de actividad f�sica
            progresoSemanal.Progress = promedioActividad / 10;

            // Actualiza la etiqueta del promedio de energ�a
            promedioEnergiaLabel.Text = $"Promedio energ�a: {promedioEnergia:F1}";
        }
        else
        {
            // Si no hay registros en la �ltima semana, muestra mensajes indicando que no hay datos
            promedioActividadLabel.Text = "No hay datos";
            promedioEnergiaLabel.Text = "No hay datos";
            progresoSemanal.Progress = 0;
        }
    }
}
