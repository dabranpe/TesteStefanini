namespace Questao5.Application;

public interface IValidatorService<T>
{
    Task Validar(T entity);
}
