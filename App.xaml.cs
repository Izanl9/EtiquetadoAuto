namespace EtiquetadoAuto;

public partial class App : Application
{
    public App()
    {
        // Esta línea es la que une el C# con el diseño del App.xaml
        InitializeComponent();

        // Esta línea define que la navegación principal será el AppShell
        MainPage = new AppShell();
    }
}