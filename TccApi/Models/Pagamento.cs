using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccApi.Models.Enuns;

namespace TccApi.Models
{
    public class Pagamento
    {
        public int Id_Pagamento { get; set; }
        public int Id_Contrato { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public MetodoEnum Metodo { get; set; }
    }
}
