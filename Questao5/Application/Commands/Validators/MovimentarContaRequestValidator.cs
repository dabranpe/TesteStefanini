﻿using FluentValidation;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database;

namespace Questao5.Application;

public class MovimentarContaRequestValidator : AbstractValidator<MovimentarContaRequest>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public MovimentarContaRequestValidator(IContaCorrenteRepository contaCorrenteRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;

        RuleFor(x => x)
            .CustomAsync(async (request, context, cancellationToken) => {

                var conta = await _contaCorrenteRepository.GetByIdAsync(request.IdContaCorrente);

                if (conta is null)
                    context.AddFailure(Validacoes.INVALID_ACCOUNT.GetDescription());
                else
                {
                    if (conta.Ativo != (int)Ativo.Ativo)
                        context.AddFailure(Validacoes.INACTIVE_ACCOUNT.GetDescription());
                }

                if (request.Valor <=0)
                    context.AddFailure(Validacoes.INVALID_VALUE.GetDescription());

                if (!Enum.IsDefined(typeof(TipoMovimento), (int)request.TipoMovimento))
                    context.AddFailure(Validacoes.INVALID_TYPE.GetDescription());

            });

        
    }
}
