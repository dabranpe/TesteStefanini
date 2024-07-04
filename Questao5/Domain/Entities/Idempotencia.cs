using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities
{
    public class Idempotencia
    {
        [Key]
        [StringLength(37)]
        public string ChaveIdempotencia { get; set; }

        [StringLength(1000)]
        public string Requisicao { get; set; } 

        [StringLength(1000)]
        public string Resultado { get; set; }

        public Idempotencia() { }

        public Idempotencia(string chaveIdempotencia, string requisicao, string resultado)
        {
            ChaveIdempotencia = chaveIdempotencia;
            Requisicao = requisicao;
            Resultado = resultado;
        }
    }
}
