using PRG_MAUI_Car_Register.ViewModel;
namespace PRG_MAUI_Car_Register
{
    public partial class App : Application
    {
        public static MainPageViewModel MainVM { get; private set; } = new MainPageViewModel();

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Task.Run(async () =>
            {
                await MainVM.LoadAll();
            });
        }
    }
}
