namespace DiarioPro;

public partial class ListStatsPage : ContentPage
{
    private List<RegistroDiario> registros;

    public ListStatsPage()
    {
        InitializeComponent();
        CargarRegistros();
    }

    // M�todo que se ejecuta cuando la p�gina aparece
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Vuelve a cargar los registros guardados previamente
        CargarRegistros();
    }

    // Evento que se dispara cuando el bot�n de eliminar un registro es clicado
    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Obtiene el bot�n que fue clicado
        Button button = (Button)sender;

        // Obtiene el registro asociado al bot�n (vinculado por CommandParameter)
        RegistroDiario registro = (RegistroDiario)button.CommandParameter;

        // Muestra un mensaje de confirmaci�n al usuario antes de eliminar
        bool confirmacion = await DisplayAlert("Confirmar Eliminacion", "Estas seguro de que deseas eliminar este registro",
            "Si", "No");

        // Si el usuario confirma la eliminaci�n, se procede a eliminar el registro
        if (confirmacion)
        {
            // Elimina el registro de la lista
            registros.Remove(registro);

            // Actualiza la fuente de datos del ListView (primero limpia y luego reasigna la lista actualizada)
            listViewRegistros.ItemsSource = null;
            listViewRegistros.ItemsSource = registros;

            // Guarda los registros actualizados (sin el registro eliminado)
            GuardarRegistros();
        }
    }

    // M�todo que carga los registros guardados en las preferencias
    private void CargarRegistros()
    {
        // Verifica si existen registros guardados en las preferencias
        if (Preferences.ContainsKey("registrosDiarios"))
        {
            // Recupera la lista de registros en formato JSON
            var json = Preferences.Get("registrosDiarios", string.Empty);

            // Deserializa el JSON a una lista de objetos de tipo RegistroDiario
            registros = System.Text.Json.JsonSerializer.Deserialize<List<RegistroDiario>>(json) ?? new List<RegistroDiario>();

            // Ordena los registros por fecha (de m�s antiguo a m�s reciente)
            registros = registros.OrderBy(r => r.Fecha).ToList();

            // Asigna la lista ordenada al ListView para mostrar los registros
            listViewRegistros.ItemsSource = registros;
        }
        else
        {
            // Si no existen registros, inicializa una lista vac�a
            registros = new List<RegistroDiario>();
        }
    }

    // M�todo que guarda los registros en las preferencias
    private void GuardarRegistros()
    {
        // Convierte la lista de registros a formato JSON
        var json = System.Text.Json.JsonSerializer.Serialize(registros);

        // Guarda el JSON en las preferencias del dispositivo
        Preferences.Set("registrosDiarios", json);
    }

    // Evento que se dispara cuando el bot�n "Limpiar Todos" es clicado
    private async void LimpiarClicked(object sender, EventArgs e)
    {
        // Solo realiza la acci�n si hay registros en la lista
        if (registros.Count > 0)
        {
            // Muestra un mensaje de confirmaci�n al usuario antes de eliminar todos los registros
            bool confirmacion = await DisplayAlert("Confirmar Eliminacion", "Estas seguro de que deseas eliminar TODOS los registros??",
                "Si", "No");

            // Si el usuario confirma, se eliminan todos los registros
            if (confirmacion)
            {
                // Limpia la lista de registros
                registros.Clear();

                // Actualiza la interfaz de usuario (el ListView se actualiza con la lista vac�a)
                listViewRegistros.ItemsSource = null;
                listViewRegistros.ItemsSource = registros;

                // Guarda la lista vac�a en las preferencias
                GuardarRegistros();
            }
        }
    }
}
