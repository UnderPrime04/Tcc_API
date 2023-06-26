using TccApi.Models;
using TccApi.Services.Pagamentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace TccApi.ViewModels.Pagamentos
{
    public class ListagemPagamentoViewModel : BaseViewModel
    {
        private PagamentoService pService;

        public ObservableCollection<Pagamento> Pagamentos { get; set; }

        public ListagemPagamentoViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PagamentoService(token);
            Pagamentos = new ObservableCollection<Pagamento>();
            _ = ObterPagamentos();

            NovoPagamento = new Command(async () => { await ExibirCadastroPagamento(); });
            RemoverPagamentoCommand = new Command<Pagamento>(async (Pagamento p) => { await RemoverPagamento(p); });
        }
        public ICommand NovoPagamento { get; }
        public ICommand RemoverPagamentoCommand { get; }



        public async Task ExibirCadastroPagamento()
        {
            try
            {
                await Shell.Current.GoToAsync("cadPagamentoView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private Pagamento pagamentoSelecionado;

        public Pagamento PagamentoSelecionado
        {
            get { return pagamentoSelecionado; }
            set
            {
                if (value != null)
                {
                    pagamentoSelecionado = value;
                    Shell.Current.GoToAsync($"cadPersonagemView?pId={pagamentoSelecionado.Id_Contrato}");
                }
            }
        }

        public async Task RemoverPagamento(Pagamento p)
        {
            try
            {
                if (await Application.Current.MainPage
                    .DisplayAlert("Confirmação", $"Confirma a remoção de {p.Id_Contrato}?", "Sim", "Não"))
                {
                    await pService.DeletePagamentoAsync(p.Id_Pagamento);

                    await Application.Current.MainPage.DisplayAlert("Mensagem",
                        "Pagamento removido com sucesso!", "Ok");

                    _ = ObterPagamentos();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public async Task ObterPagamentos()
        {
            try //Junto com o Cacth evitará que erros fechem o aplicativo
            {
                Pagamentos = await pService.GetPagamentoAsync();
                OnPropertyChanged(nameof(Pagamentos)); //Informará a View que houve carregamento                       
            }
            catch (Exception ex)
            {
                //Captará o erro para exibir em tela
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
