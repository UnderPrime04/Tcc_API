using TccApi.ViewModels.Pagamentos;

namespace TccApi.Views.Pagamentos;

public partial class CadastroPagamentoView : ContentPage
{

    private CadastroPagamentoViewModel cadViewModel;

    public CadastroPagamentoView()
    {
        InitializeComponent();

        cadViewModel = new CadastroPagamentoViewModel();
        BindingContext = cadViewModel;
        Title = "Novo Pagamento";
    }
}
