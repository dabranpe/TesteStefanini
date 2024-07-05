using MediatR;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Application;

public class MovimentarContaHandler : IRequestHandler<MovimentarContaRequest, MovimentarContaResponse>
{
    IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IValidatorService<MovimentarContaRequest> _validator;

    public MovimentarContaHandler(IContaCorrenteRepository contaCorrenteRepository,
                                  IValidatorService<MovimentarContaRequest> validator)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _validator = validator;
    }

    public async Task<MovimentarContaResponse> Handle(MovimentarContaRequest request, CancellationToken cancellationToken)
    {
        await _validator.Validar(request);

        var movimento = new Movimento(Guid.NewGuid().ToString(), request.IdContaCorrente, DateTime.Now.ToString("dd/MM/yyyy"), request.TipoMovimento, request.Valor);

        await _contaCorrenteRepository.AddMovimentoAsync(movimento);

        return new MovimentarContaResponse { IdMovimento = movimento.IdMovimento };

    }
}
