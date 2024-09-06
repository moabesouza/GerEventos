using System.ComponentModel.DataAnnotations;

namespace GerEventos.Entities
{
    public enum MeioPagamentoEnum
    {
        [Display(Name = "Dinheiro")]
        Dinheiro = 1,

        [Display(Name = "Cartão de Crédito")]
        CartaoCredito = 2,

        [Display(Name = "Cartão de Débito")]
        CartaoDebito = 3,

        [Display(Name = "Transferência Bancária")]
        TransferenciaBancaria = 4,

        [Display(Name = "Pix")]
        Pix = 5,

        [Display(Name = "Boleto")]
        Boleto = 6
    }
}
