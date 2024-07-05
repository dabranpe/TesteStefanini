using MediatR;


namespace Questao5.Application;

public class ConsultarSaldoContaRequest : IRequest<ConsultarSaldoContaResponse>
{
    public string IdContaCorrente { get; set; }
}
