using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopTFTEC.API.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Sobrenome { get; set; }

        [StringLength(100)]
        public string Endereco1 { get; set; }

        [StringLength(100)]
        public string Endereco2 { get; set; }

        [StringLength(10, MinimumLength = 8)]
        public string Cep { get; set; }

        [StringLength(10)]
        public string Estado { get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public decimal PedidoTotal { get; set; }

        public int TotalItensPedido { get; set; }

        public DateTime PedidoEnviado { get; set; }

        public DateTime? PedidoEntregueEm { get; set; }

        public List<PedidoDetalhe> PedidoItens { get; set; }
    }
}