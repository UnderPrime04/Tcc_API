using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccApi.Models;

namespace TccApi.Services.Pagamentos
{
    public class PagamentoService : Request
    {
        private readonly Request _request;
        private const String apiUrlBase = "http://TCCApi.somee.com/TccApi/Pagamento";

        private string _token;

        public PagamentoService(string token)
        {
            _request = new Request();
            _token = token;
        }
        public async Task<ObservableCollection<Pagamento>> GetPagamentoAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Pagamento> listaPagamentos = await
            _request.GetAsync<ObservableCollection<Models.Pagamento>>(apiUrlBase + urlComplementar, _token);
            return listaPagamentos;
        }
        public async Task<Pagamento> GetPagamentosAsync(int pagamentoId)
        {
            string urlComplementar = string.Format("/{0}", pagamentoId);
            var pagamento = await _request.GetAsync<Models.Pagamento>(apiUrlBase +
            urlComplementar, _token);
            return pagamento;
        }
        public async Task<int> PostPagamentoAsync(Pagamento p)
        {
            return await _request.PostReturnIntTokenAsync(apiUrlBase, p, _token);
        }
        public async Task<int> PutPagametoAsync(Pagamento p)
        {
            var result = await _request.PutAsync(apiUrlBase, p, _token);
            return result;
        }
        public async Task<int> DeletePagamentoAsync(int pagamentoId)
        {
            string urlComplementar = string.Format("/{0}", pagamentoId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }
}
