using FluentValidation;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database;

namespace Questao5.Application;

public class ConsultarSaldoContaRequestValidator : AbstractValidator<ConsultarSaldoContaRequest>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public ConsultarSaldoContaRequestValidator(IContaCorrenteRepository contaCorrenteRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        string complementoMensagem = "consultar o saldo";

        RuleFor(x => x)
            .CustomAsync(async (request, context, cancellationToken) => {

                var conta = await _contaCorrenteRepository.GetByIdAsync(request.IdContaCorrente);

                if (conta is null)
                    context.AddFailure(string.Format(Validacoes.INVALID_ACCOUNT.GetDescription(), complementoMensagem));
                else
                {
                    if (conta.Ativo != (int)Ativo.Ativo)
                        context.AddFailure(string.Format(Validacoes.INACTIVE_ACCOUNT.GetDescription(), complementoMensagem));
                }                

            });


    }
}
