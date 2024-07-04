using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities;

public class Movimento
{
    [Key]
    [StringLength(37)]
    public string IdMovimento { get; set; } 

    [Required]
    [StringLength(37)]
    public string IdContaCorrente { get; set; }

    [Required]
    public DateTime DataMovimento { get; set; }

    [Required]
    [StringLength(1)]
    public char TipoMovimento { get; set; } 

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; }

    public Movimento() { }

    public Movimento(string idMovimento, string idContaCorrente, DateTime dataMovimento, char tipoMovimento, decimal valor)
    {        

        IdMovimento = idMovimento;
        IdContaCorrente = idContaCorrente;
        DataMovimento = dataMovimento;
        TipoMovimento = tipoMovimento;
        Valor = valor;
    }
}
