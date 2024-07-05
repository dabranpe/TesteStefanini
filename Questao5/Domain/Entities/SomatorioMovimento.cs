using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities;

public class SomatorioMovimento
{    
    public string IdContaCorrente { get; set; }     
    
    public char TipoMovimento { get; set; }
        
    public decimal Valor { get; set; }
}
