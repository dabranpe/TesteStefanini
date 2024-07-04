using MediatR;
using Questao5.Infrastructure.Database;

namespace Questao5.Application.Handlers
{
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
            throw new NotImplementedException();
        }
    }
}
