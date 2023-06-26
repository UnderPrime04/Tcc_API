using TccApi.Views.Pagamentos;

namespace TccApi;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
