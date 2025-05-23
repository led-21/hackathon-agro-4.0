using AgroSynapse.Resources.Splash;

namespace AgroSynapse
{
    public partial class AppShell : Shell
    {
        static bool _splash = false;
        public AppShell()
        { 
            InitializeComponent();
            if (!_splash)
            {
                _splash = true;
                Navigation.PushAsync(new Splash()); // Navega para a tela principal
            }
        }
    }
}
