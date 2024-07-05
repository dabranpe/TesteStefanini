using Questao5.Application;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Swagger;

public class MovimentarContaRequestExample : IExamplesProvider<MovimentarContaRequest>
{
    public MovimentarContaRequest GetExamples()
    {
        return new MovimentarContaRequest
        {
            IdContaCorrente = "5a3cc8a9-27db-4416-860c-1325bf345f7f",
            TipoMovimento = 'D', 
            Valor = 50
        };
    }
}
