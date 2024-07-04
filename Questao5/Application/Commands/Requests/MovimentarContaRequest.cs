using MediatR;


namespace Questao5.Application;

public class MovimentarContaRequest : IRequest<MovimentarContaResponse>
{
    public string IdContaCorrente { get; set; }
    public char TipoMovimento { get; set; }
    public decimal Valor { get; set; }
}
