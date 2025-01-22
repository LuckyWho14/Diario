namespace DiarioPro
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            // Llama al método que muestra el saludo al usuario
            MostrarSaludo();
        }

        private void MostrarSaludo()
        {
            // Obtiene el nombre del usuario desde el sistema operativo
            string nombreUsuario = Environment.UserName;

            // Asigna el texto de la etiqueta 'welcomeLbl' para mostrar el saludo
            welcomeLbl.Text = $"¡Bienvenido, {nombreUsuario}!";
        }

    }

}
