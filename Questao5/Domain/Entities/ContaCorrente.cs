using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    [Key]
    [StringLength(37)]
    public string IdContaCorrente { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Numero { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    public int Ativo { get; set; }
}
