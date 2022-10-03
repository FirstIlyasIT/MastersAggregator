using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MasterAggregator.Desktop.ViewModels;
using MasterAggregator.Desktop.Views;
using MasterAggregator.Desktop.Services;  



namespace MasterAggregator.Desktop
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    //передаем тестовые данные через UserSevic
                    DataContext = new MainWindowViewModel(new UserSevic()),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}