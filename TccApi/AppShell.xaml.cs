using TccApi.Views.Pagamentos;

namespace TccApi;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();


        Routing.RegisterRoute("cadPagamentoView", typeof(CadastroPagamentoView));
    }
}
