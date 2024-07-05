using MediatR;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database;

namespace Questao5.Application;

public class ConsultarSaldoContaHandler : IRequestHandler<ConsultarSaldoContaRequest, ConsultarSaldoContaResponse>
{
    IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IValidatorService<ConsultarSaldoContaRequest> _validator;

    public ConsultarSaldoContaHandler(IContaCorrenteRepository contaCorrenteRepository,
                                  IValidatorService<ConsultarSaldoContaRequest> validator)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _validator = validator;
    }

    public async Task<ConsultarSaldoContaResponse> Handle(ConsultarSaldoContaRequest request, CancellationToken cancellationToken)
    {
        decimal valorCredito = 0, valorDebito = 0;

        await _validator.Validar(request);
                
        var somatorio = await _contaCorrenteRepository.GetSomatorioMovimentoByIdContaAsync(request.IdContaCorrente);

        if (somatorio != null) 
        {
            valorCredito = (somatorio.Any(x => (int)x.TipoMovimento == (int)TipoMovimento.Credito) ?
                                    somatorio.First(x => (int)x.TipoMovimento == (int)TipoMovimento.Credito).Valor : 0);

            valorDebito = (somatorio.Any(x => (int)x.TipoMovimento == (int)TipoMovimento.Debito) ?
                                    somatorio.First(x => (int)x.TipoMovimento == (int)TipoMovimento.Debito).Valor : 0);
        }

        return new ConsultarSaldoContaResponse { Saldo = valorCredito - valorDebito };
    }
}
