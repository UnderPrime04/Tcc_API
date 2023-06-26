using TccApi.ViewModels.Pagamentos;

namespace TccApi.Views.Pagamentos;

public partial class ListagemView : ContentPage
{
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = viewModel.ObterPagamentos();
    }

    ListagemPagamentoViewModel viewModel;

	public ListagemView()
	{
		InitializeComponent();

		viewModel = new ListagemPagamentoViewModel();
		BindingContext = viewModel;
		Title = "Pagamentos - App Tcc Api";
	}
}