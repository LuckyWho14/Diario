using Microsoft.Maui.Graphics.Text;

namespace DiarioPro
{
    public partial class AppShell : Shell
    {
        
        public AppShell()
        {
            InitializeComponent();

            /*
             * Se utiliza para cambiar los colores de la cabecera
             * Recibe 2 parametros de color segun el tema del sistema
             *      El primero para el tema claro, y el segundo para el tema oscuro
             *      En este caso he utilizado el mismo color para los 2, ya que solo he trabajado con un esquema de colores
             */
            this.SetAppThemeColor(Shell.BackgroundColorProperty, 
                Color.FromHex("#E6FFE6"), Color.FromHex("#E6FFE6")); // Fondo
            this.SetAppThemeColor(Shell.TitleColorProperty, 
                Color.FromHex("#B24C93"), Color.FromHex("#B24C93")); // Título
        }
    }
}
