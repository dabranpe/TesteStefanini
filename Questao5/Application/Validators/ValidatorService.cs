using FluentValidation;
using Questao5.Infrastructure;
using System.Text;

namespace Questao5.Application;

public class ValidatorService<T> : IValidatorService<T>
{
    private readonly IValidator<T> _validator;

    public ValidatorService(IValidator<T> validator) => _validator = validator;

    public async Task Validar(T entity)
    {
        var validacao = await _validator.ValidateAsync(entity);
        if (validacao.IsValid is false)
        {
            List<string> validationErros = new();
            foreach (var erro in validacao.Errors)
                validationErros.Add(erro.ErrorMessage.ToString());

            throw new CustomErrorException(validationErros);
        }
    }
}
