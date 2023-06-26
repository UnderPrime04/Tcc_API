using TccApi.Models;
using TccApi.Services.Pagamentos;
using TccApi.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace TccApi.ViewModels.Pagamentos
{
    [QueryProperty("PagamentoSelecionadoId", "pId")]
    public class CadastroPagamentoViewModel : BaseViewModel
    {
        private PagamentoService pService;
        private int id_Pagamento;
        private int id_Contrato;
        private decimal valor;

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }

        public CadastroPagamentoViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PagamentoService(token);
            _ = ObterMetodos();

            SalvarCommand = new Command(async () => { await SalvarPagamento(); });
        }

        private string pagamentoSelecionadoId;

        public string PagamentoSelecionadoId
        {
            set
            {
                if (value != null)
                {
                    pagamentoSelecionadoId = Uri.UnescapeDataString(value);
                    CarregarPagamento();
                }
            }
        }

        public int Id_Pagamento
        {
            get => id_Pagamento;
            set
            {
                id_Pagamento = value;
                OnPropertyChanged();
            }
        }

        public int Id_Contrato
        {
            get => id_Contrato;
            set
            {
                id_Contrato = value;
                OnPropertyChanged();
            }
        }

        public decimal Valor
        {
            get => valor;
            set
            {
                valor = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TipoMetodo> listaTiposMetodo;

        public ObservableCollection<TipoMetodo> ListaTiposMetodo
        {
            get { return listaTiposMetodo; }
            set
            {
                if (value != null)
                {
                    listaTiposMetodo = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task ObterMetodos()
        {
            try
            {
                ListaTiposMetodo = new ObservableCollection<TipoMetodo>();
                ListaTiposMetodo.Add(new TipoMetodo() { Id = 1, Descricao = "Pix" });
                ListaTiposMetodo.Add(new TipoMetodo() { Id = 2, Descricao = "Cartao_cred" });
                ListaTiposMetodo.Add(new TipoMetodo() { Id = 2, Descricao = "Cartao_deb" });
                ListaTiposMetodo.Add(new TipoMetodo() { Id = 3, Descricao = "Boleto" });
                OnPropertyChanged(nameof(ListaTiposMetodo));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        private TipoMetodo tipoMetodoSelecionado;

        public TipoMetodo TipoMetodoSelecionado
        {
            get { return tipoMetodoSelecionado; }
            set
            {
                if (value != null)
                {
                    tipoMetodoSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }
        public async Task SalvarPagamento()
        {
            try
            {
                Pagamento model = new Pagamento()
                {
                    Id_Contrato = this.id_Contrato,
                    Valor = this.valor,                  
                    Id_Pagamento = this.id_Pagamento,
                    Metodo = (MetodoEnum)tipoMetodoSelecionado.Id
                };
                if (model.Id_Pagamento == 0)
                    await pService.PostPagamentoAsync(model);
                else
                    await pService.PutPagametoAsync(model);

                await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");
                await Shell.Current.GoToAsync("..."); //Remove a página atual da pilha de páginas
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        private async void CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }
        public async void CarregarPagamento()
        {
            try
            {
                Pagamento p = await pService.GetPagamentoAsync(int.Parse(pagamentoSelecionadoId));

                this.Id_Contrato = p.Id_Contrato;              
                this.Valor = p.Valor;            
                this.Id_Pagamento = p.Id_Pagamento;
                
                tipoMetodoSelecionado = this.ListaTiposMetodo
                    .FirstOrDefault(tMetodo => tMetodo.Id == (int)p.Metodo);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
